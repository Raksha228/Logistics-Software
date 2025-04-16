using Backend.DataAccess;
using Logistics_Software.Services;
using Logistics_Software.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Logistics_Software
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static IServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var loginView = new Views.LoginView
            {
                DataContext = ServiceProvider.GetRequiredService<LoginViewModel>()
            };

            loginView.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=localhost;Database=LogisticsDb;Trusted_Connection=True;TrustServerCertificate=True;"));

            // Services
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<UserContextService>();
            services.AddTransient<AuthenticationService>();
            services.AddTransient<OrderService>();
            services.AddTransient<MessageService>();
            services.AddTransient<DialogService>();
            services.AddTransient<ReportService>();

            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<ClientDashboardViewModel>();
            services.AddTransient<ManagerDashboardViewModel>();
            services.AddTransient<AdminDashboardViewModel>();
            services.AddTransient<OrderViewModel>();
            services.AddTransient<ChatViewModel>();
            services.AddTransient<ProfileViewModel>();
        }

    }

}
