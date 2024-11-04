using SummaSQLGame.Helpers;
using SummaSQLGame.Models;

namespace SummaSQLGame.ViewModels
{
    public class SortViewModel : BaseExplanationViewModel
    {
        public event EventHandler<EventArgs> UpdateProgressEvent;

        #region constructor
        public SortViewModel()
        {
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Default,
                    Dialog = "Een nieuw onderwerp, een nieuwe tabel. Dit keer gaan we gebruik maken van de tabel spotify, waarin informatie staat over hoe vaak nummers in spotify gestreamed zijn. \n\n Bestudeer de afbeelding van de tabel eens en selecteer alle liedjes met het verschijningsjaar 2022 of hoger is om verder te gaan.",
                    CanPass = false,
                    AcceptedQueries = {"select * from spotify where verschijningsjaar >= 2022;", "select * from spotify where verschijningsjaar > 2021;"}
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Top, de onderdelen select en where beheers je al aardig. Nu gaan we er nog een schepje bovenop doen. \n\n Als we zo veel data hebben is het handig deze te sorteren, bijvoorbeeld op naam, dit doen we met de volgende query: \n\n SELECT * FROM spotify ORDER BY titel;",
                    CanPass = false,
                    AcceptedQueries = {"select * from spotify order by titel;"}
                },
                new Explanation()
                {
                    Image = Avatars.Content,
                    Dialog = "De nummers zijn nu gesorteerd op titel in oplopende volgorde (van a tot z), dit noemen we ASC (ascending). Stel we willen de nummers gesorteerd hebben op aflopende volgorde (z tot a) dan noemen we dat DESC (descending). \n\n Voeg nu DESC toe achteraan je query.",
                    CanPass = false,
                    AcceptedQueries = {"select * from spotify order by titel desc;"}
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Je kunt ook meerdere kolommen opgeven om bij te sorteren, je moet ze dan scheiden met een komma. De data wordt eerst gesorteerd op de eerste opgegeven kolom. Als daar meerdere dezelfde waardes staan, wordt gesorteerd op de tweede opgegeven kolom enzovoort.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Probeer nu eens alle liederen te selecteren \n\n éérst gesorteerd op streams aflopend \n\n dan op titel oplopend.",
                    CanPass = false,
                    AcceptedQueries = { "select * from spotify order by streams desc, titel;", "select * from spotify order by streams desc, titel asc;" }
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Hartstikke goed, maar ik heb nog één laatste opdracht voor dit onderwerp. \n\n Je kunt de order by natuurlijk ook combineren met andere SQL statements, zoals de where. \n\n Probeer nu eens alle liedjes op te halen van de artiest Taylor Swift, gesorteerd op aantal streams (aflopend). Let op dat de order by altijd na de where komt",
                    CanPass = false,
                    AcceptedQueries = { "select * from spotify where artiesten = \"taylor swift\" order by streams desc;", "select * from spotify where artiesten LIKE \"%taylor swift%\" order by streams desc;" }
                },
                new Explanation()
                {
                    Image = Avatars.Content,
                    Dialog = "Top! Je hebt het onderdeel sorteren afgerond."
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
