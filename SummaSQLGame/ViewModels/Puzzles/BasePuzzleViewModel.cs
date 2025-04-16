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
        public abstract event EventHandler<EventArgs>? PuzzleCompleted;
        public abstract void InitializePuzzle();
        
        public DataTable QueryResult { get { return _queryResult; } set { _queryResult = value; OnPropertyChanged(); } }
        public ICommand QueryCommand { get; }

        protected int _attempts = 0;
        protected string _queryText = "";
        protected DataTable _queryResult;
        protected string _puzzleType;
        protected IAppDbContext _context;


        public int Attempts { get { return _attempts; } set { _attempts = value; OnPropertyChanged(); } }
        
        public string QueryText { get { return _queryText; } set { _queryText = value; OnPropertyChanged(); } }
        public string PuzzleType { get { return _puzzleType; } }
        public IAppDbContext Context
        { get { return _context; } set { _context = value; OnPropertyChanged(); } }

        public BasePuzzleViewModel()
        {
            QueryCommand = new RelayCommand(ExecuteQuery);
            QueryResult = new DataTable();
            Context = new AppDbContext();
        }

        protected void ExecuteQuery(object? sender)
        {
            using (AppDbContext context = new AppDbContext())
            {
                QueryResult = context.ExecuteQuery(QueryText);
            }
        }
    }
}
