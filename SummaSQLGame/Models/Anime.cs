using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("anime")]
    [PrimaryKey("Id")]
    public class Anime : ObservableObject
    {
        private readonly int _id;
        private string _name;
        private string _englishName;
        private int? _score;
        private string _genres;
        private string _synopsis;
        private string _type;
        private string _studios;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("naam")]
        [StringLength(255)]
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        [Column("engelsenaam")]
        [StringLength(255)]
        public string EnglishName { get { return _englishName; } set {_englishName = value; OnPropertyChanged(); } }

        [Column("score")]
        public int? Score { get { return _score; } set { _score = value; OnPropertyChanged(); } }

        [Column("genres")]
        [StringLength(255)]
        public string Genres { get { return _genres; } set { _genres = value; OnPropertyChanged(); } }

        [Column("omschrijving")]
        public string Synopsis { get { return _synopsis; } set { _synopsis = value; OnPropertyChanged(); } }

        [Column("type")]
        [StringLength(255)]
        public string Type { get { return _type; } set { _type = value; OnPropertyChanged(); } }

        [Column("studios")]
        [StringLength(255)]
        public string Studios { get { return _studios; } set { _studios = value; OnPropertyChanged(); } }
    }
}
