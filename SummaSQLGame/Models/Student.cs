using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("student")]
    [PrimaryKey("Id")]
    public class Student : ObservableObject
    {
        private readonly int _id;
        private string _name;
        private int _group;
        private int _mathScore;
        private int _englishScore;
        private int _historyScore;
        private int _geographyScore;
        private int _chemistryScore;
        private int _artScore;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("naam")]
        [StringLength(255)]
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        [Column("klas")]
        public int Group { get { return _group; } set { _group = value; OnPropertyChanged(); } }

        [Column("rekenen")]
        public int MathScore { get { return _mathScore; } set { _mathScore = value; OnPropertyChanged(); } }

        [Column("engels")]
        public int EnglishScore { get { return _englishScore; } set { _englishScore = value; OnPropertyChanged(); } }

        [Column("geschiedenis")]
        public int HistoryScore { get { return _historyScore; } set { _historyScore = value; OnPropertyChanged(); } }

        [Column("aardrijkskunde")]
        public int GeographyScore { get { return _geographyScore; } set { _geographyScore = value; OnPropertyChanged(); } }

        [Column("scheikunde")]
        public int ChemistryScore { get { return _chemistryScore; } set { _chemistryScore = value; OnPropertyChanged(); } }
        
        [Column("kunst")]
        public int ArtScore { get { return _artScore; } set { _artScore = value; OnPropertyChanged(); } }
    }
}
