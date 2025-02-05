using SummaSQLGame.Helpers;
using SummaSQLGame.Models;

namespace SummaSQLGame.ViewModels
{
    public class SelectViewModel : BaseExplanationViewModel
    {
        public event EventHandler<EventArgs> UpdateProgressEvent;

        #region constructor
        public SelectViewModel()
        {
            _subject = Subjects.SELECT;
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Default,
                    Dialog = "Welkom bij je eerste les over SQL. De Structured Query Language. \n SQL is de taal die we gebruiken om een database aan te spreken. \n\n In dit onderdeel gaan we middels de taal SQL leren gegevens op te halen uit de database.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Rechts naast deze dialoog staan hyperlinks waar je meer informatie kunt vinden over huidige onderwerpen. In dit geval het SELECT statement. Op de website vindt je zowel extra uitleg als voorbeelden.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Boven de linkjes zie je een afbeelding van wat we noemen een tabel. Een tabel is een soort container waar we aan elkaar verwante data opslaan. In dit geval staat er alle informatie in over honden.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Om de data op te halen moeten we een Query (vraag) aan de database stellen. In die query vertellen we welke informatie we willen hebben en waar die informatie vandaan komt. In dit geval willen we alles ophalen uit de tabel honden, dat doen we met de query : SELECT * FROM honden; \n\n Probeer het eens uit in de TextBox boven mij en klik op Uitvoeren.",
                    CanPass = false,
                    AnswerQuery = "select * from honden;" 
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Goed gedaan! Laten we de query analyseren. \nMet het woord SELECT geven we het type query aan, in dit geval willen we data ophalen. \n* geeft aan dat we alle informatie willen. \n Met FROM geven we aan uit welke tabel de informatie komt, in dit geval FROM honden. \n De ; duidt het einde van de query aan.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Nu hebben we alle informatie uit honden opgehaald. Soms wil je echter maar een beperkte hoeveelheid informatie weten. \nDe tabel honden bestaat uit drie kolommen; id, naam en ras. In plaats van een * kunnen we ook een specifieke kolom opgeven. \n\n Probeer nu eens de namen op te halen uit de tabel honden.",
                    CanPass = false,
                    AnswerQuery = "select naam from honden;" 
                },
                new Explanation()
                {
                    Image = Avatars.Content,
                    Dialog = "Top! \nTot slot is het ook mogelijk meerdere kolommen tegelijk op te halen. Deze moeten dan worden gescheiden met een komma. \n\n Probeer nu eens zowel de naam als het ras van de honden op te halen.",
                    CanPass = false,
                    AnswerQuery = "select naam, ras from honden;"
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Gefeliciteerd! Je hebt het onderdeel SELECT afgerond. Je kunt nog even vrij oefenen met queriën op de tabel honden. Of verder gaan naar het volgende onderdeel."
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
