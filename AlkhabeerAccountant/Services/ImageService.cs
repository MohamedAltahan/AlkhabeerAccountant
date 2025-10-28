using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AlkhabeerAccountant.Services
{
    public class ImageService
    {
        private readonly string _rootFolder;

        public ImageService(string appName = "AlkhabeerAccountant")
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _rootFolder = Path.Combine(appData, appName, "Images");

            // تأكد من وجود المجلد الرئيسي
            Directory.CreateDirectory(_rootFolder);
        }

        // ========================= رفع الصورة =========================
        public async Task<string> ReplaceAsync(string oldImagePath, string category = "General")
        {
            try
            {
                // 🟩 2️⃣ ارفع الصورة الجديدة
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                    Title = "اختيار صورة جديدة"

                };

                if (dialog.ShowDialog() == true)
                {
                    var fileInfo = new FileInfo(dialog.FileName);
                    //  تحقق من الحجم
                    if (fileInfo.Length > (1 * 1024 * 1024))
                    {
                        MessageBox.Show("⚠️ حجم الصورة أكبر من 1 ميجا! الرجاء اختيار صورة أصغر.",
                                        "تحذير", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return null;
                    }

                    var folder = Path.Combine(_rootFolder, category);
                    Directory.CreateDirectory(folder);

                    var fileName = Path.GetFileName(dialog.FileName);
                    var destination = Path.Combine(folder, fileName);

                    await Task.Run(() => File.Copy(dialog.FileName, destination, true));

                    // 🟦 1️⃣ احذف الصورة القديمة إن وجدت
                    if (!string.IsNullOrWhiteSpace(oldImagePath) && File.Exists(oldImagePath))
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(oldImagePath) || !File.Exists(oldImagePath))
                            {
                                MessageBox.Show("⚠️ الملف غير موجود أو المسار فارغ.",
                                                "تحذير",
                                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            }

                            // ✅ 3. احذف الصورة بأمان
                            File.Delete(oldImagePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"⚠️ فشل حذف الصورة القديمة:\n{ex.Message}",
                                            "Old Image Delete Error",
                                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                    return destination; // ✅ المسار الجديد
                }
                return oldImagePath; // 🟡 لو المستخدم لغى، نحافظ على القديمة
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطأ أثناء استبدال الصورة:\n{ex.Message}",
                                "Image Replace Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return oldImagePath;
            }
        }


        // ========================= حذف الصورة =========================
        public bool Remove(string imagePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                {
                    MessageBox.Show("⚠️ الملف غير موجود أو المسار فارغ.",
                                    "تحذير",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                // ✅ 3. احذف الصورة بأمان
                File.Delete(imagePath);

                MessageBox.Show("🗑️ تم حذف الصورة بنجاح.",
                                "تم",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"⚠️ الملف مستخدم من عملية أخرى:\n{ioEx.Message}",
                                "File Access Error",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطأ غير متوقع أثناء حذف الصورة:\n{ex.Message}",
                                "Image Delete Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }

        // ========================= أداة مساعدة للبحث في الواجهة =========================
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
                yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T typedChild)
                    yield return typedChild;

                foreach (var childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        public BitmapImage LoadImage(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                return null;

            try
            {
                var bitmap = new BitmapImage();
                using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; // ✅ يحمّل الصورة في الذاكرة بالكامل
                    bitmap.UriSource = null;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                bitmap.Freeze(); // ✅ لتفادي مشاكل الـ Thread
                return bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ فشل تحميل الصورة:\n{ex.Message}", "Image Load Error");
                return null;
            }
        }

    }
}
