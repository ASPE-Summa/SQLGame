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
                    Dialog = "De grootste kracht van Relationale databases zoals MySQL, SQL Server en SQLITE zit hem in de naam. Relaties. Dit wil zeggen dat je data die aan elkaar verwant is kunt koppelen aan elkaar. In plaats van data dubbel of onoverzichtelijk opslaan in de data wordt het gestructureerd opgeslagen in tabellen met koppelingen ertussen. Dergelijke koppelingen worden ook wel relaties genoemd."
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "In dit voorbeeld hebben we twee tabellen. In de tabel land staat alle informatie over landen en in de tabel stad staat alle informatie over steden, inclusief een koppeling naar land in de vorm van het land id. Je zou natuurlijk ook de naam van het land in de tabel stad kunnen zetten, maar dan krijg je heel veel dubbele data."
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Het nadeel is dat een id je natuurlijk niet zo veel zegt als de naam van een land. Daarvoor hebben we de JOIN. JOIN koppelt de data van twee tabellen aan elkaar maar je moet wel zelf opgeven op welke kolommen je die koppeling maakt, dat doen we met ON. Onderstaand een voorbeeld \n\nSELECT * FROM stad JOIN land ON stad.land = land.id;",
                    CanPass = false,
                    AcceptedQueries = { "select * from stad join land on stad.land = land.id;" }
                } ,
                new Explanation()
                {
                    Image = Avatars.Content,
                    Dialog = "In dit voorbeeld geven we aan dat de koppeling ligt tussen de land kolom in de tabel stad en het id uit de tabel land. Je ziet dus ook in het resultaat dat de inhoud van deze kolommen bij elke rij exact hetzelfde is.\n\nEr zijn meerdere soorten JOIN maar de standaard geeft alleen alle data terug waarbij de opgegeven ON kolommen met elkaar overeenkomen."
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Meestal gebruik je id's om te koppelen. Dit is het meest secuur gezien het id van een tabel altijd uniek moet zijn. Je kunt echter ook op andere kolommen joinen. Stel we willen alle landen hebben waar de naam van de hoofdstad hetzelfde is als de naam van het land. Dat kunnen we op twee manieren opvragen."
                },
                new Explanation()
                {
                    Image = Avatars.Fear,
                    Dialog = "Bedenk welke optie voor jouw het meest logisch is en probeer de bijbehorende query te schrijven: \n\n1. Selecteer alles, JOIN land en stad op id en schrijf een WHERE waarin is_hoofdstad waar is en de namen met elkaar vergeleken worden. \n\n2. Selecteer alles, JOIN land en stad op naam en schrijf een WHERE waarin is_hoofdstad waar is.",
                    CanPass = false,
                    AcceptedQueries = { 
                        "select * from stad join land on stad.naam = land.naam where ishoofdstad = true;",
                        "select * from stad join land on land.naam = stad.naam where ishoofdstad = true;",
                        "select * from land join stad on land.naam = stad.naam where ishoofdstad = true;",
                        "select * from land join stad on stad.naam = land.naam where ishoofdstad = true;",
                        "select * from stad join land on stad.land = land.id where ishoofdstad = true and land.naam = stad.naam;",
                        "select * from stad join land on stad.land = land.id where ishoofdstad = true and stad.naam = land.naam;",
                        "select * from stad join land on land.id = stad.land where ishoofdstad = true and land.naam = stad.naam;",
                        "select * from stad join land on land.id = stad.land where ishoofdstad = true and stad.naam = land.naam;",
                        "select * from land join stad on stad.land = land.id where ishoofdstad = true and land.naam = stad.naam;",
                        "select * from land join stad on stad.land = land.id where ishoofdstad = true and stad.naam = land.naam;",
                        "select * from land join stad on land.id = stad.land where ishoofdstad = true and land.naam = stad.naam;",
                        "select * from land join stad on land.id = stad.land where ishoofdstad = true and stad.naam = land.naam;",
                        "select * from stad join land on stad.land = land.id where land.naam = stad.naam and ishoofdstad = true;",
                        "select * from stad join land on stad.land = land.id where stad.naam = land.naam and ishoofdstad = true;",
                        "select * from stad join land on land.id = stad.land where land.naam = stad.naam and ishoofdstad = true;",
                        "select * from stad join land on land.id = stad.land where stad.naam = land.naam and ishoofdstad = true;",
                        "select * from land join stad on stad.land = land.id where land.naam = stad.naam and ishoofdstad = true;",
                        "select * from land join stad on stad.land = land.id where stad.naam = land.naam and ishoofdstad = true;",
                        "select * from land join stad on land.id = stad.land where land.naam = stad.naam and ishoofdstad = true;",
                        "select * from land join stad on land.id = stad.land where stad.naam = land.naam and ishoofdstad = true;",
                    }
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Gefeliciteerd! Joins zijn vaak voor beginnende gebruikers van SQL een van de lastigste onderwerpen, maar het is je gelukt! Dit onderdeel is hiermee afgerond. Voel je vrij om te oefenen met queries op verschillende tabellen of toon je bekwaamheid in de challenge."
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
