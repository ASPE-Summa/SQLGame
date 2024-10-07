using SummaSQLGame.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaSQLGame.Models
{
    public class Hond : ObservableObject
    {
        private readonly int _id;
        private string _naam;
        private string _ras;

        [Key]
        public int Id { get { return _id; } }

        public string Naam { get { return _naam; } set { _naam = value; OnPropertyChanged(); } }

        public string Ras { get { return _ras; } set {_ras = value; OnPropertyChanged(); } }
    }
}
