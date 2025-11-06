using Alkhabeer.Data;
using Alkhabeer.Data.Repositories;
using Alkhabeer.Data.Seeders;
using Alkhabeer.Service;
using AlkhabeerAccountant.Services;
using AlkhabeerAccountant.ViewModels.Setting;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace AlkhabeerAccountant
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // new instance of ServiceCollection to hold all of our services
            var services = new ServiceCollection();

            // 🔹 Call the method that automatically registers all dependencies
            ConfigureServices(services);

            // Build the final ServiceProvider after all registrations
            ServiceProvider = services.BuildServiceProvider();

            // Connect Toolkit to your DI container
            Ioc.Default.ConfigureServices(ServiceProvider);

            // 🔹 Seed the database each time the app starts
            using (var scope = App.ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DBContext>();
                context.Database.EnsureCreated(); // Create DB if it doesn't exist
                DatabaseSeeder.Seed(context);     // Run manual seeders
            }

            //recall the base class's OnStartup method because we are overriding it.
            base.OnStartup(e);
        }

        /// <summary>
        /// Automatically register all Services, Repositories, and ViewModels in the DI container automatically.
        /// </summary>
        /// <param name="services">IServiceCollection container</param>
        private void ConfigureServices(IServiceCollection services)
        {
            // 🔸 Register DB Context manually
            services.AddDbContext<DBContext>(options =>
                options.UseMySql(
                    "server=localhost;database=desktop_pos;user=root;password=;port=3307",
                    new MySqlServerVersion(new Version(8, 0, 30))), ServiceLifetime.Scoped);

            // ✅ Scan current assembly + Data assembly
            var currentAssembly = Assembly.GetExecutingAssembly();
            var dataAssembly = typeof(SettingRepository).Assembly;
            var serviceAssembly = typeof(BankService).Assembly;

            var allAssemblies = new[] { currentAssembly, dataAssembly, serviceAssembly };

            //scan all assemlies in the solution
            foreach (var assembly in allAssemblies)
            {
                foreach (var type in assembly.GetTypes()
                         .Where(t => t.IsClass && !t.IsAbstract &&
                                    (t.Name.EndsWith("Service") ||
                                     t.Name.EndsWith("Repository") ||
                                     t.Name.EndsWith("ViewModel"))))
                {
                    services.AddTransient(type);
                }
            }

            // 🔹 Specific singletons (global utilities) you must register manually
            services.AddSingleton<ImageService>();
        }

    }
}

