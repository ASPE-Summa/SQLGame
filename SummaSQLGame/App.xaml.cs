using SummaSQLGame.Databases;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainView()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();
        }
    }

}
