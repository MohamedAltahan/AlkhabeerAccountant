using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace AlkhabeerAccountant
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            //add border when maximize and remove it when restore
            this.StateChanged += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    MainBorder.Margin = new Thickness(6);
                }
                else
                {
                    MainBorder.Margin = new Thickness(0);
                }
            };

            //Assume you’ll get these later from DB or login session
            //string userName = "كريم الشركاوي";
            //string roleName = "مدير النظام"; // or "كاشير", "محاسب", etc.
            DateTime loginTime = DateTime.Now;

            // Find the StatusBar and update info
            //StatusBarControl.SetLoginInfo(userName, roleName, loginTime);

            //this.SourceInitialized += YourWindow_SourceInitialized;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //    private void YourWindow_SourceInitialized(object sender, EventArgs e)
        //    {
        //        // This ensures the window respects the taskbar when maximized
        //        IntPtr handle = new WindowInteropHelper(this).Handle;
        //        HwndSource.FromHwnd(handle)?.AddHook(WindowProc);
        //    }

        //    private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //    {
        //        switch (msg)
        //        {
        //            case 0x0024: // WM_GETMINMAXINFO
        //                WmGetMinMaxInfo(hwnd, lParam);
        //                handled = true;
        //                break;
        //        }
        //        return IntPtr.Zero;
        //    }

        //    private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        //    {
        //        var mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

        //        // Get the monitor the window is on
        //        IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

        //        if (monitor != IntPtr.Zero)
        //        {
        //            var monitorInfo = new MONITORINFO();
        //            monitorInfo.cbSize = Marshal.SizeOf(typeof(MONITORINFO));
        //            GetMonitorInfo(monitor, ref monitorInfo);

        //            var rcWorkArea = monitorInfo.rcWork;
        //            var rcMonitorArea = monitorInfo.rcMonitor;

        //            mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
        //            mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
        //            mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
        //            mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);

        //            Marshal.StructureToPtr(mmi, lParam, true);
        //        }
        //    }

        //    // Required Win32 API declarations
        //    [StructLayout(LayoutKind.Sequential)]
        //    public struct MINMAXINFO
        //    {
        //        public POINT ptReserved;
        //        public POINT ptMaxSize;
        //        public POINT ptMaxPosition;
        //        public POINT ptMinTrackSize;
        //        public POINT ptMaxTrackSize;
        //    }

        //    [StructLayout(LayoutKind.Sequential)]
        //    public struct POINT
        //    {
        //        public int x;
        //        public int y;
        //    }

        //    [StructLayout(LayoutKind.Sequential)]
        //    public struct MONITORINFO
        //    {
        //        public int cbSize;
        //        public RECT rcMonitor;
        //        public RECT rcWork;
        //        public uint dwFlags;
        //    }

        //    [StructLayout(LayoutKind.Sequential)]
        //    public struct RECT
        //    {
        //        public int left;
        //        public int top;
        //        public int right;
        //        public int bottom;
        //    }

        //    [DllImport("user32.dll")]
        //    private static extern IntPtr MonitorFromWindow(IntPtr handle, uint flags);

        //    [DllImport("user32.dll")]
        //    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        //    private const uint MONITOR_DEFAULTTONEAREST = 0x00000002;
    }
}