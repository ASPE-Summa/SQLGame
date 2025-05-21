using Microsoft.Extensions.DependencyInjection;
using SummaSQLGame.Services;
using SummaSQLGame.ViewModels;
using SummaSQLGame.Views;
using System.Windows;

namespace SummaSQLGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            services.AddSingleton<ISaveStateService, SaveStateService>();
            services.AddSingleton<IMainViewModelContext, MainViewModel>();
            services.AddSingleton<IQueryService, QueryService>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<ChallengeViewModel>();
            services.AddTransient<SelectViewModel>();
            // Register other ViewModels as needed
            _serviceProvider = services.BuildServiceProvider();
            base.OnStartup(e);
            MainWindow = new MainView()
            {
                DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
            };
            MainWindow.Show();
        }
    }

}
