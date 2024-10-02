using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
