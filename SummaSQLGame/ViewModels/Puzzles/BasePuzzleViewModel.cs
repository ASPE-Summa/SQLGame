using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels.Puzzles
{
    public abstract class BasePuzzleViewModel : ObservableObject, IPuzzle
    {
        public abstract event EventHandler<EventArgs> PuzzleCompleted;

        public DataTable QueryResult { get { return _queryResult; } set { _queryResult = value; OnPropertyChanged(); } }
        public ICommand QueryCommand { get; }

        protected int _attempts = 0;
        protected string _queryText = "";
        protected DataTable _queryResult;


        public int Attempts { get { return _attempts; } set { _attempts = value; OnPropertyChanged(); } }
        public string QueryText { get { return _queryText; } set { _queryText = value; OnPropertyChanged(); } }

        public BasePuzzleViewModel()
        {
            QueryCommand = new RelayCommand(ExecuteQuery);
            QueryResult = new DataTable();
        }

        protected void ExecuteQuery(object? sender)
        {
            using(AppDbContext context = new AppDbContext())
            {
                QueryResult = context.ExecuteQuery(QueryText);
            }
        }
    }
}
