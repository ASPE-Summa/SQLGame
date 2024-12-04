using SummaSQLGame.Helpers;

namespace SummaSQLGame.Models
{
    public class SaveState : ObservableObject
    {
        #region fields
        private string _name;
        private int _selectCompletion;
        private int _whereCompletion;
        private int _sortCompletion;
        private int _aggregateCompletion;
        private int _wildcardCompletion;
        private int _groupCompletion;
        private int _joinCompletion;
        private int _highScore;
        private int _battleshipPuzzlesEncountered;
        private int _battleshipPuzzlesCompleted;

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

        public int AggregateCompletion
        {
            get { return _aggregateCompletion; }
            set { _aggregateCompletion = value; OnPropertyChanged(); }
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

        public void UpdateCompletion(string subject, int value)
        {
            switch (subject)
            {
                case Subjects.SELECT: SelectCompletion = value; break;
                case Subjects.WHERE: WhereCompletion = value; break;
                case Subjects.WILDCARDS: WildcardCompletion = value; break;
                case Subjects.SORT: SortCompletion = value; break;
                case Subjects.AGGREGATES: AggregateCompletion = value; break;
                case Subjects.GROUP: GroupCompletion = value; break;
                case Subjects.JOIN: JoinCompletion = value; break;

            }
        }
    }
}
