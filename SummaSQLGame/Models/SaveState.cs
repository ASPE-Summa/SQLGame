using SummaSQLGame.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int _mathCompletion;
        private int _wildcardCompletion;
        private int _groupCompletion;
        private int _joinCompletion;
        private Dictionary<string, int> _progress;

        public Dictionary<string, int> Progress
        {
            get { return _progress; }
            set {  _progress = value; OnPropertyChanged(); }
        }
        #endregion

        public SaveState(string name)
        {
            _name = name;
            UpdateProgress();
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

        public int MathCompletion
        {
            get { return _mathCompletion; }
            set { _mathCompletion = value; OnPropertyChanged(); }
        }

        public int WildcardCompletion
        {
            get { return _wildcardCompletion; }
            set { _wildcardCompletion = value; OnPropertyChanged(); }
        }

        public int GroupCompletion
        {
            get { return _groupCompletion; }
            set { _groupCompletion = value; OnPropertyChanged(); }
        }

        public int JoinCompletion
        {
            get { return _joinCompletion; }
            set { _joinCompletion = value; OnPropertyChanged(); }
        }
        #endregion

        public void UpdateProgress()
        {
            Progress = new Dictionary<string, int>
            {
                {Subjects.SELECT,SelectCompletion},
                {Subjects.WHERE, WhereCompletion},
                {Subjects.SORT, SortCompletion },
                {Subjects.MATH, MathCompletion },
                {Subjects.WILDCARDS,WildcardCompletion },
                {Subjects.GROUP, GroupCompletion },
                {Subjects.JOIN, JoinCompletion}
            };
        }
    }
}
