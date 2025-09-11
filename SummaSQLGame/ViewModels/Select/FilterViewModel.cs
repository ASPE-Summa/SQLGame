using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using SummaSQLGame.Services;

namespace SummaSQLGame.ViewModels.Select
{
    public class FilterViewModel : BaseExplanationViewModel
    {
        public event EventHandler<EventArgs> UpdateProgressEvent;

        #region constructor
        public FilterViewModel(IQueryService queryService) : base(queryService)
        {
            _subject = Subjects.WHERE;
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Default,
                    Dialog = "Je weet nu hoe je alle informatie kunt ophalen uit een tabel en hoe je specifieke kolommen uit een tabel kunt opvragen. Tot nu toe krijgen we telkens alle rijen terug. Dat is niet handig als je een hele waslijst aan data door moet zoeken voor antwoord op een specifieke vraag.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Wellicht is het handig om te zien wat ik bedoel met een waslijst aan data. Daarmee kunnen we gelijk het selecteren nog eens oefenen. \n\n Schrijf een query om alle informatie op te halen uit de tabel genaamd `bieren`",
                    CanPass = false,
                    AnswerQuery =  "select * from bieren;" 
                },
                new Explanation()
                {
                    Image = Avatars.Anxious,
                    Dialog = "Jemig dat is veel bier. Maar nu wil ik alleen de bieren zien van het type LAGER, AMBER. \n\n Om dat te doen moet ik in mijn query gebruik maken van een WHERE statement. Bijvoorbeeld als volgt: \n\n SELECT * FROM bieren WHERE soort = \"LAGER, AMBER\" \n\n Probeer het eens. (Let op: LAGER,AMBER is hoofdlettergevoelig)",
                    CanPass = false,
                    AnswerQuery = "select * from bieren where soort = \"LAGER, AMBER\";"
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Kijk, dat is een stuk overzichtelijker. Als we de query analyseren zie je dat na WHERE moet worden aangegeven op welke kolom we willen filteren. En dat de waarde waarop we filteren tussen aanhalingstekens staat. Dit is om te voorkomen dat speciale tekens (zoals de komma) als deel van de query zelf gezien worden.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Probeer het nu eens zelf, selecteer alle bieren uit Duitsland (GER).",
                    CanPass = false,
                    AnswerQuery = "select * from bieren where land = \"GER\";" 
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Goed gedaan, maar nu hebben we wel weer alle biersoorten. Als bieren willen die zowel van het type LAGER, AMBER zijn als uit het land GER kunnen we gebruik maken van het woordje AND Bijvoorbeeld. \n\n SELECT * FROM bieren WHERE soort = \"LAGER, AMBER\" AND land = \"GER\"",
                    CanPass = false,
                    AnswerQuery = "select * from bieren where soort = \"LAGER, AMBER\" and land = \"GER\";"
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Top, nu heb ik een mooi lijstje duitse bieren om te proeven! \n\n We hebben nu bieren waarbij beide filters waar zijn. Stel we willen bieren die ofwel uit duitsland komen of van het soort lager,amber zijn. \n\n Dat is eigenlijk heel gemakkelijk, we hoeven dan alleen het woordje AND in onze query te vervangen door OR. Probeer het eens.",
                    CanPass = false,
                    AnswerQuery = "select * from bieren where soort = \"LAGER, AMBER\" or land = \"GER\";"
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Zoals je misschien al had verwacht, is de lijst nu weer groter geworden. We hebben nu alle bieren uit duitsland zowel als alle amber lagers te pakken.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Maar nu even iets anders. We hebben tot nu toe gefilterd op woorden die gelijk zijn aan (=) wat we opgeven. Maar je kunt ook filteren op groter dan/kleiner dan met de tekens < en >. \n\n Probeer eens alle bieren te selecteren met een alcoholpercentage kleiner dan 5%.",
                    CanPass = false,
                    AnswerQuery = "select * from bieren where alcoholpercentage < 5;" 
                },
                new Explanation()
                {
                    Image = Avatars.Content,
                    Dialog = "Kijk eens aan, je bent een expert in filteren aan het worden. Nog een laatste query om de onderwerpen in dit onderdeel te controleren. \n\n Selecteer alle bieren van de soort WHEAT (GRAIN) met een alcoholpercentage groter dan 3% die afkomstig zijn uit Frankrijk (FRA)",
                    CanPass = false,
                    AnswerQuery =  "select * from bieren where soort = \"WHEAT (GRAIN)\" and alcoholpercentage > 3 and land = \"FRA\";"
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Gefeliciteerd, je hebt het onderdeel Filteren afgerond!"
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
