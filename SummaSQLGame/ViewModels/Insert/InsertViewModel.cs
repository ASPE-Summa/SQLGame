using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using System;
using System.Collections.Generic;

namespace SummaSQLGame.ViewModels.Insert
{
    public class InsertViewModel : BaseExplanationViewModel
    {
        public event EventHandler<EventArgs> UpdateProgressEvent;

        public InsertViewModel()
        {
            _subject = Subjects.INSERT;
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Default,
                    Dialog = "Welkom bij het onderdeel INSERT. Hier leer je hoe je nieuwe rijen toevoegt aan een tabel in SQL.",
                    CanPass = true
                },
                new Explanation() {
                    Image = Avatars.Explaining,
                    Dialog = "De basis van een INSERT statement is: INSERT INTO tabelnaam (kolom1, kolom2) VALUES (waarde1, waarde2);\n\nProbeer een nieuwe hond toe te voegen aan de tabel honden met de naam 'Bobby' en ras 'Beagle'.",
                    CanPass = false,
                    AnswerQuery = "insert into honden (naam, ras) values (\"Bobby\", \"Beagle\");"
                },
                new Explanation() {
                    Image = Avatars.Content,
                    Dialog = "Goed gedaan! Je kunt meerdere rijen tegelijk toevoegen door meerdere VALUES toe te voegen.\n\nProbeer nu twee honden toe te voegen: 'Max' (Labrador) en 'Luna' (Poodle).",
                    CanPass = false,
                    AnswerQuery = "insert into honden (naam, ras) values (\"Max\", \"Labrador\"), (\"Luna\", \"Poodle\");"
                },
                new Explanation() {
                    Image = Avatars.Smiling,
                    Dialog = "Gefeliciteerd! Je hebt het onderdeel INSERT afgerond. Je kunt nu rijen toevoegen aan tabellen in SQL."
                }
            };
            CurrentExplanation = _explanations[0];
        }

        protected override void UpdateProgress()
        {
            UpdateProgressEvent?.Invoke(this, new EventArgs());
        }
    }
}
