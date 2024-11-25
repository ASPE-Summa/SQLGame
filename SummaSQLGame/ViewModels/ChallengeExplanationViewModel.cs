using SummaSQLGame.ViewModels.Puzzles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels
{
    class ChallengeExplanationViewModel : IPuzzle
    {
        public DataTable QueryResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICommand QueryCommand => throw new NotImplementedException();

        public int Attempts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string QueryText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<EventArgs> PuzzleCompleted;
    }
}
