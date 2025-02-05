using SummaSQLGame.Helpers;

namespace SummaSQLGame.Models
{
    public class Explanation : ObservableObject
    {
        private Uri? _image;
        private string? _dialog;
        private bool _canPass = true;
        private string? _answerQuery;

        public Uri? Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }
        public string? Dialog
        {
            get { return _dialog; }
            set { _dialog = value; OnPropertyChanged(); }
        }

        public bool CanPass
        {
            get { return _canPass; }
            set { _canPass = value; OnPropertyChanged(); }
        }

        public string? AnswerQuery
        {
            get { return _answerQuery; }
            set { _answerQuery = value; OnPropertyChanged(); }
        }
    }
}
