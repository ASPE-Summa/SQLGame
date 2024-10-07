using SummaSQLGame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaSQLGame.Models
{
    public class Explanation : ObservableObject
    {
        private Uri _image;
        private string _dialog;
        private bool _canPass;
        private List<String> _acceptedQueries = new List<String>();

        public Uri Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }
        public string Dialog
        {
            get { return _dialog; }
            set { _dialog = value; OnPropertyChanged(); }
        }

        public bool CanPass
        {
            get { return _canPass; }
            set { _canPass = value; OnPropertyChanged(); }
        }

        public List<String> AcceptedQueries
        {
            get { return _acceptedQueries; }
            set { _acceptedQueries = value; OnPropertyChanged(); }
        }
    }
}
