using SummaSQLGame.Helpers;
using SummaSQLGame.Models;

namespace SummaSQLGame.ViewModels
{
    public class JoinViewModel : BaseExplanationViewModel
    {
        public event EventHandler<EventArgs> UpdateProgressEvent;

        #region constructor
        public JoinViewModel()
        {
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Explaining,
                    Dialog = "In het vorige onderdeel heb je leren rekenen middels aggregaatfuncties. Er werdt echter elke keer maar 1 resultaat opgehaald. Stel we willen niet de hoogst scorende anime in het geheel maar de hoogst scorende anime per studio? In dat geval maken we gebruik van de GROUP BY.\n\nLaten we het gelijk proberen:\n\n SELECT MAX(score), studios, engelsenaam FROM anime GROUP BY studios ORDER BY score DESC;",
                    CanPass = false,
                    AcceptedQueries = { "select max(score), studios, engelsenaam from anime group by studios order by score desc;" }
                },
            };
            CurrentExplanation = _explanations.First();
        }
        #endregion

        protected override void UpdateProgress()
        {
            EventArgs args = new EventArgs();
            UpdateProgressEvent?.Invoke(this, new EventArgs());
        }
    }
}
