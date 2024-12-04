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
            _subject = Subjects.AGGREGATES;
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Explaining,
                    Dialog = "Rekenen in databases doen we aan de hand van zogenaamde Aggregaatfuncties. Dit zijn min, max, count, sum en avg. Deze functies kunnen we op enkele kolommen in de database uitvoeren. Denk bijvoorbeeld aan de laagste score of een gemiddelde uitrekenen. \n MIN - Geeft de laagste waarde in een kolom terug \n MAX - Geeft de hoogste waarde in een kolom terug \n COUNT - Telt de hoeveelheid rijen in je selectie \n SUM - Telt numerieke waardes bij elkaar op en geeft het totaal \n AVG - Berekend een gemiddelde van een numerieke kolom.",
                },
                new Explanation()
                {
                    Image = Avatars.Fear,
                    Dialog = "Dat zijn veel functies, maar laten we ze één voor één behandelen. \n\nAls we zo'n bereking gaan uitvoeren, doen we dit in de SELECT. In het geval dat we willen weten wat de hoogst scorende anime is kunnen we bijvoorbeeld de volgende query gebruiken: \n\nSELECT naam, MAX(score) FROM anime;\n\nWe willen zowel de naam als de hoogste score, anders weten we niet bij welke anime die score hoort. Probeer het eens zelf.",
                    CanPass = false,
                    AcceptedQueries = { "select naam, max(score) from anime;"}
                },
                new Explanation()
                {
                    Image = Avatars.Content,
                    Dialog = "Heel goed, maar kun je nu ook achterhalen wat de laagst scorende anime is?",
                    CanPass = false,
                    AcceptedQueries = { "select naam, min(score) from anime;"}
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Hartstikke goed! In een later onderwerp leren we ook groeperen zodat je bijvoorbeeld de hoogste score per studio kan vinden. Maar laten we het voor nu houden bij de aggregaatfuncties."
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Om het gemiddelde uit te rekenen kun je de AVG functie gebruiken. In dit geval gaan we hem ook combineren met de WHERE. \n\nSelecteer de gemiddelde score van anime waarbij genres \"Sports\" bevat.\n\nHint: selecteer enkel de gemiddelde score en maak gebruik van wildcards.",
                    CanPass = false,
                    AcceptedQueries = { "select avg(score) from anime where genres like \"%sports%\";" }
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Netjes! Je hebt het querien inmiddels aardig onder de knie."
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Nog 2 aggregaatfuncties te gaan, COUNT & SUM.\n\nCOUNT geeft het aantal rijen wat je hebt geselecteerd weer. Het maakt vaak dus ook niet uit welke kolom je gebruikt. COUNT(score), COUNT(naam) of COUNT(*) zou alle drie het resultaat geven gezien het niet kijkt naar de inhoud, maar de hoeveelheid rijen."
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Gebruik nu eens de COUNT (samen met een WHERE LIKE) om antwoord te geven op de volgende vraag:\n\nHoeveel anime zijn er waar studio Sunrise bij betrokken is?",
                    CanPass = false,
                    AcceptedQueries = { 
                        "select count(*) from anime where studios like \"%sunrise%\";", 
                        "select count(id) from anime where studios like \"%sunrise%\";", 
                        "select count(naam) from anime where studios like \"%sunrise%\";",
                        "select count(engelsenaam) from anime where studios like \"%sunrise%\";", 
                        "select count(score) from anime where studios like \"%sunrise%\";",
                        "select count(genres) from anime where studios like \"%sunrise%\";",
                        "select count(omschrijving) from anime where studios like \"%sunrise%\";",
                        "select count(type) from anime where studios like \"%sunrise%\";",
                        "select count(studios) from anime where studios like \"%sunrise%\";"
                    }
                },
                new Explanation()
                {
                    Image = Avatars.Content,
                    Dialog = "Goed gedaan. Dan gaan we gelijk door naar de laatste aggregaatfunctie, SUM. SUM kijkt wél naar de inhoud van geselecteerde kolommen en telt die bij elkaar op. Het geeft dus een totaal terug, dit werkt natuurlijk alleen op numerieke kolommen. \n\nDe laatste opdracht: selecteer de som van scores van anime waarbij de genres \"Action\" of \"Adventure\" bevatten",
                    CanPass = false,
                    AcceptedQueries = { "select sum(score) from anime where genres like \"%anime%\" or genres like \"%adventure%\";", "select sum(score) from anime where genres like \"%adventure%\" or genres like \"%action%\";" }
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Dat was stieken best een pittige query. Maar je hebt hem opgelost, gefeliciteerd! Dit onderdeel is daarmee ook afgerond. In het onderdeel groeperen leer je de aggregaatfuncties combineren met GROUP BY om eenzelfde berekening resultaten per groep te bekijken."
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
