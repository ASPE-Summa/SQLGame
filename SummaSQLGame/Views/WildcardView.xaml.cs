using System.Windows;
using System.Windows.Controls;

namespace SummaSQLGame.Views
{
    /// <summary>
    /// Interaction logic for WildcardView.xaml
    /// </summary>
    public partial class WildcardView : UserControl
    {
        public WildcardView()
        {
            InitializeComponent();
        }

        private void tbSql_Loaded(object sender, RoutedEventArgs e)
        {
            using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SummaSQLGame.Assets.Resources.sql.xshd"))
            {
                using (var reader = new System.Xml.XmlTextReader(stream))
                {
                    tbSql.SyntaxHighlighting =
                        ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(reader,
                        ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
                }
            }
        }
    }
}
