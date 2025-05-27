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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SummaSQLGame.Views.Puzzles
{
    /// <summary>
    /// Interaction logic for SqlPracticeView.xaml
    /// </summary>
    public partial class SqlPracticeView : UserControl
    {
        public SqlPracticeView()
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
