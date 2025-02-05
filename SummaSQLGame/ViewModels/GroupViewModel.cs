using SummaSQLGame.Helpers;
using SummaSQLGame.Models;

namespace SummaSQLGame.ViewModels
{
    public class GroupViewModel : BaseExplanationViewModel
    {
        public event EventHandler<EventArgs> UpdateProgressEvent;


        #region constructor
        public GroupViewModel()
        {
            _subject = Subjects.JOIN;
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Explaining,
                    Dialog = "In het vorige onderdeel heb je leren rekenen middels aggregaatfuncties. Er werdt echter elke keer maar 1 resultaat opgehaald. Stel we willen niet de hoogst scorende anime in het geheel maar de hoogst scorende anime per studio? In dat geval maken we gebruik van de GROUP BY.\n\nLaten we het gelijk proberen:\n\n SELECT MAX(score), studios, engelsenaam FROM anime GROUP BY studios, engelsenaam ORDER BY score DESC;",
                    CanPass = false,
                    AnswerQuery =  "select max(score), studios, engelsenaam from anime group by studios, engelsenaam order by score desc;" 
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Zonder de group by kreeg je dankzij MAX enkel de hoogs scorende anime en de daarbij horende studios. Nu wordt van elke unieke studio (of combinatie van studios) de hoogst scorende anime weergeven.\n\n Belangrijk om te onthouden is dat alle non-aggregaat kolommen die je selecteerd in de GROUP BY terechtkomen. Anders krijg je een onbetrouwbaar resultaat."
                },
                new Explanation()
                {
                    Image = Avatars.Anxious,
                    Dialog = "Stel een studio (of combinatie van studios) heeft 2 anime die exact dezelfde score hebben als MAX. Als engelsenaam niet in de GROUP BY zat, zou de query enkel één van deze anime teruggeven en heb je geen controle over welke."

                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Probeer het nu eens zelf. Dit is een best pittige vraag maar als je al zo ver bent gekomen heb ik er vertrouwen in dat het je gaat lukken. \n\nSelecteer de genres en gemiddelde score van anime waarbij het type OVA is, gegroupeerd per genres, gesorteerd op genres oplopend.",
                    CanPass = false,
                    AnswerQuery = "select genres, avg(score) from anime where type = \"OVA\" group by genres order by genres;"
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Het is je gelukt, topper! Je hebt dit onderdeel afgerond, nog maar 1 onderdeel te gaan en je bent een meester van SELECT queries."
                }
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
