using System.Windows.Documents;

namespace SummaSQLGame.Helpers
{
    class ExternalHyperlink : Hyperlink
    {
        public ExternalHyperlink() 
        {
            Click += ClickHandler;
        }

        public void ClickHandler(object sender, EventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            string destinationurl = link.NavigateUri.ToString();
            var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
        }
    }
}
