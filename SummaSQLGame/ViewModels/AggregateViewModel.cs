using SummaSQLGame.Helpers;
using SummaSQLGame.Models;

namespace SummaSQLGame.ViewModels
{
    public class AggregateViewModel : BaseExplanationViewModel
    {
        public event EventHandler<EventArgs> UpdateProgressEvent;

        #region constructor
        public AggregateViewModel()
        {
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Explaining,
                    Dialog = "Rekenen in databases doen we aan de hand van zogenaamde Aggregaatfuncties. Dit zijn min, max, count, sum en avg. Deze functies kunnen we op enkele kolommen in de database uitvoeren. Denk bijvoorbeeld aan de laagste score of een gemiddelde uitrekenen. \n MIN - Geeft de laagste waarde in een kolom terug \n MAX - Geeft de hoogste waarde in een kolom terug \n COUNT - Telt de hoeveelheid rijen in je selectie \n SUM - Telt numerieke waardes bij elkaar op en geeft het totaal \n AVG - Berekend een gemiddelde van een numerieke kolom.",
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
