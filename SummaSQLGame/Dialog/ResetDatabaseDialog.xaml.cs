using System.Windows;

namespace SummaSQLGame.Dialog
{
    public partial class ResetDatabaseDialog : Window
    {
        public ResetDatabaseDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
