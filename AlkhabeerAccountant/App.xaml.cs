using Alkhabeer.Data;
using Alkhabeer.Data.Repositories;
using AlkhabeerAccountant.Services;
using AlkhabeerAccountant.ViewModels.Setting;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace AlkhabeerAccountant
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // (dependencies)بنعمل قائمة الخدمات اللي هنضيف فيها كل الكائنات
            // اللي المشروع ممكن يحتاجها.
            //"Container"تقدر تتخيلها كأنك بتجهز 
            //فاضي هتحط فيه كل الاوبجكتس اللي هتستخدمها.
            var services = new ServiceCollection();

            services.AddDbContext<DBContext>(options =>
                options.UseMySql(
                    "server=localhost;database=desktop_pos;user=root;password=;port=3307",
                    new MySqlServerVersion(new Version(8, 0, 30))));
            //Repositoryهنا بنسجّل الـ 
            //الخاص بالإعدادات في Container 
            services.AddTransient<SettingRepository>();
            services.AddTransient<SettingMainViewModel>();
            services.AddTransient<CompanySettingViewModel>();
            services.AddSingleton<ImageService>();
            //بعد ما سجّلنا كل الخدمات، بنبني
            //"Service Provider" النهائي.
            ServiceProvider = services.BuildServiceProvider();
            // ✅ Connect Toolkit to your DI container
            Ioc.Default.ConfigureServices(ServiceProvider);

            //recall the base class's OnStartup method because we are overriding it.
            base.OnStartup(e);
        }
    }
}
