using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;

namespace SummaSQLGame.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void DragWindow(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && WindowState != WindowState.Maximized)
            {
                DragMove();
            }
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_Maximize_Click(object sender, RoutedEventArgs e)
        {
            PackIcon content = (PackIcon)btn_Maximize.Content;
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                content.Kind = PackIconKind.Maximize;
                return;
            }
            WindowState = WindowState.Maximized;
            content.Kind = PackIconKind.WindowRestore;
            
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
