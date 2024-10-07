using ICSharpCode.AvalonEdit.Document;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels
{
    public class SelectViewModel : ObservableObject
    {
        #region fields
        private List<Explanation> _explanations;
        private int _explanationIndex = 0;
        private Explanation _currentExplanation;
        private DataTable _queryResult;
        private string _queryText;
        #endregion

        #region properties
        public Explanation CurrentExplanation
        {
            get { return _currentExplanation; }
            set
            {
                _currentExplanation = value; OnPropertyChanged();
            }
        }

        public DataTable QueryResult
        {
            get { return _queryResult; }
            set { _queryResult = value; OnPropertyChanged(); }
        }

        public string QueryText
        {
            get { return _queryText; }
            set { _queryText = value; OnPropertyChanged(); }
        }
        #endregion

        #region constructor
        public SelectViewModel()
        {
            NextExplanationCommand = new RelayCommand(ExecuteNextDialogue, CanExecuteNext);
            PreviousExplanationCommand = new RelayCommand(ExecutePreviousDialogue, CanExecutePrevious);
            QueryCommand = new RelayCommand(ExecuteQuery);
            _explanations = new List<Explanation>()
            {
                new Explanation() {
                    Image = Avatars.Default,
                    Dialog = "Welkom bij je eerste les over SQL. De Structured Query Language. \n SQL is de taal die we gebruiken om een database aan te spreken. \n\n In dit onderdeel gaan we middels SQL leren gegevens op te halen uit de database.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Explaining,
                    Dialog = "Rechts naast deze dialoog staan links waar je meer informatie kunt vinden over huidige onderwerpen. In dit geval het SELECT statement. Op de website vindt je extra uitleg, zowel als voorbeelden.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Boven de links zie je een afbeelding van wat we noemen een tabel. Een tabel is een soort container waar we aan elkaar verwante data opslaan. In dit geval staat er alle informatie in over honden. \n\n Om de data op te halen moeten we een Query (vraag) aan de database stellen. In die query vertellen we welke informatie we willen hebben en waar die informatie vandaan komt. In dit geval willen we alles ophalen uit de tabel honden, dat doen we met de query : SELECT * FROM honden; \n\n Probeer het eens uit in de TextBox boven mij en klik op Uitvoeren.",
                    CanPass = false,
                    AcceptedQueries = new List<string>(){"SELECT * FROM honden;" }
                },
                new Explanation()
                {
                    Image = Avatars.Smiling,
                    Dialog = "Goed gedaan! Laten we de query analyseren. \n Met het woord SELECT geven we het type query aan, in dit geval willen we data ophalen. \n * geeft aan dat we alle informatie willen. \n Met FROM geven we aan uit welke tabel de informatie komt, in dit geval FROM honden. \n De ; duidt het einde van de query aan.",
                    CanPass = true
                },
                new Explanation()
                {
                    Image = Avatars.Default,
                    Dialog = "Nu hebben we alle informatie uit honden opgehaald. Maar soms wil je maar een beperkte hoeveelheid informatie weten. \n De tabel honden bestaat uit drie kolommen; id, naam en ras. In plaats van een * kunnen we ook een specifieke kolom opgeven. \n\n probeer nu eens de namen op te halen uit de tabel honden.",
                    CanPass = false,
                    AcceptedQueries = new List<string>(){"SELECT naam FROM honden;" }
                },
                new Explanation()
                {
                    Image = Avatars.Content,
                    Dialog = "Top! \n Tot slot is het ook mogelijk meerdere kolommen tegelijk op te halen. Deze moeten dan worden gescheiden met een komma. \n\n Probeer nu eens zowel de naam als het ras van de honden op te halen.",
                    CanPass = false,
                    AcceptedQueries = new List<string>(){"SELECT naam, ras FROM honden;", "SELECT naam,ras FROM honden;", "SELECT ras,naam FROM honden;", "SELECT ras, naam FROM honden;" }
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

        #region commands
        public ICommand NextExplanationCommand { get; }
        public ICommand PreviousExplanationCommand { get; }
        public ICommand QueryCommand { get; }
        #endregion

        #region methods
        private bool CanExecuteNext(object? obj)
        {
            return _explanationIndex < _explanations.Count - 1 && CurrentExplanation.CanPass;
        }

        private bool CanExecutePrevious(object? obj)
        {
            return _explanationIndex > 0;
        }

        private void ExecuteNextDialogue(object? obj)
        {
            _explanationIndex++;
            CurrentExplanation = _explanations[_explanationIndex];
        }

        private void ExecutePreviousDialogue(object? obj)
        {
            _explanationIndex--;
            CurrentExplanation = _explanations[_explanationIndex];
        }

        private void ExecuteQuery(object? obj)
        {
            try
            {
                using (SqliteConnection conn = new SqliteConnection(ConfigurationManager.ConnectionStrings["localDb"].ConnectionString))
                {
                    conn.Open();

                    SqliteCommand command = new SqliteCommand(QueryText, conn);
                    SqliteDataReader reader = command.ExecuteReader();
                    DataTable result = new DataTable();
                    result.Load(reader);
                    QueryResult = result;

                    if(CurrentExplanation.AcceptedQueries.Contains(QueryText))
                    {
                        CurrentExplanation.CanPass = true;
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
