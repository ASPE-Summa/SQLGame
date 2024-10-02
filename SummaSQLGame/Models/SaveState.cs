using SummaSQLGame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaSQLGame.Models
{
    public class SaveState : ObservableObject
    {
        #region fields
        private string _name;
        private int _selectCompletion;
        private int _whereCompletion;
        private int _sortCompletion;
        private int _wildcardCompletion;
        private int _joinCompletion;
        #endregion

        public SaveState(string name)
        {
            _name = name;
        }

        #region properties
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public int SelectCompletion
        {
            get { return _selectCompletion; }
            set { _selectCompletion = value; OnPropertyChanged(); }
        }

        public int WhereCompletion
        {
            get { return _whereCompletion; }
            set { _whereCompletion = value; OnPropertyChanged(); }
        }

        public int SortCompletion
        {
            get { return _sortCompletion; }
            set { _sortCompletion = value; OnPropertyChanged(); }
        }

        public int WildcardCompletion
        {
            get { return _wildcardCompletion; }
            set { _wildcardCompletion = value; OnPropertyChanged(); }
        }

        public int JoinCompletion
        {
            get { return _joinCompletion; }
            set { _joinCompletion = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
