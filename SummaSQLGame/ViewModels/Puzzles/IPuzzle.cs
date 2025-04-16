using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels.Puzzles
{
    public interface IPuzzle
    {
        public event EventHandler<EventArgs> PuzzleCompleted;
        public DataTable QueryResult { get; set; }
        public ICommand QueryCommand { get; }
        public int Attempts { get; set; }

        public void InitializePuzzle();
        public string QueryText { get; set; }
        public string PuzzleType {  get; }
    }
}
