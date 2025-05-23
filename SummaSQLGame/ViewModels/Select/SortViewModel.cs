﻿using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using SummaSQLGame.Services;

namespace SummaSQLGame.ViewModels.Select
{
    public class SortViewModel : BaseExplanationViewModel
    {
        public event EventHandler<EventArgs> UpdateProgressEvent;

        #region constructor
        public SortViewModel(IQueryService queryService) : base(queryService)
        {
            _subject = Subjects.SORT;
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Default,
                    Dialog = "Een nieuw onderwerp, een nieuwe tabel. Dit keer gaan we gebruik maken van de tabel liederen, waarin informatie staat over hoe vaak nummers in spotify gestreamed zijn. \n\nBestudeer de afbeelding van de tabel eens en selecteer alles waarbij het verschijningsjaar 2022 of hoger is om verder te gaan.",
                    CanPass = false,
                    AnswerQuery = "select * from liederen where verschijningsjaar >= 2022;"
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Top, de onderdelen select en where beheers je al aardig. Nu gaan we er nog een schepje bovenop doen. \n\nWanneer we zo veel data hebben is het handig deze te sorteren, bijvoorbeeld op naam, dit doen we met de volgende query: \n\n SELECT * FROM liederen ORDER BY titel;",
                    CanPass = false,
                    AnswerQuery = "select * from liederen order by titel;" 
                },
                new Explanation()
                {
                    Image = Avatars.Content,
                    Dialog = "De nummers zijn nu gesorteerd op titel in oplopende volgorde (van a tot z), dit noemen we ASC (ascending). Stel we willen de nummers gesorteerd hebben op aflopende volgorde (z tot a) dan noemen we dat DESC (descending). \n\nVoeg nu DESC toe achteraan je query.",
                    CanPass = false,
                    AnswerQuery = "select * from liederen order by titel desc;" 
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Je kunt ook meerdere kolommen opgeven om bij te sorteren, je moet ze dan scheiden met een komma. De data wordt eerst gesorteerd op de eerste opgegeven kolom. Indien meerdere rijen dezelfde data bevatten in de eerste sorteerkolom, worden die rijen gesorteerd op de tweede sorteerkolom enzovoorts.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Probeer nu eens alle liederen te selecteren \n\nEérst gesorteerd op verschijningsjaar aflopend \n\nEn ten tweede op titel oplopend.",
                    CanPass = false,
                    AnswerQuery = "select * from liederen order by verschijningsjaar desc, titel;"
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Hartstikke goed, Ik heb nog één laatste opdracht voor je over dit onderwerp. \n\nJe kunt de ORDER BY natuurlijk ook combineren met andere SQL statements, zoals de WHERE. \n\nProbeer nu eens alle liedjes op te halen waar de artiest \"Taylor Swift\" in de artiesten staat, gesorteerd op aantal streams (aflopend). Let op dat de ORDER BY altijd na de WHERE komt",
                    CanPass = false,
                    AnswerQuery = "select * from liederen where artiesten LIKE \"%Taylor Swift%\" order by streams desc;"
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
