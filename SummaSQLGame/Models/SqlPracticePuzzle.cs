using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("doolhoven")]
    [PrimaryKey("Id")]
    public class SqlPracticePuzzle : ObservableObject
    {
        private readonly int _id;
        private string _contents;
        private int _pattern;
        private int _sequence;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("inhoud")]
        [StringLength(255)]
        public string Contents { get { return _contents; } set { _contents = value; OnPropertyChanged(); } }

        [Column("patroon")]
        public int Pattern { get { return _pattern; } set { _pattern = value; OnPropertyChanged(); } }

        [Column("regelnummer")]
        public int Sequence { get { return _sequence; } set { _sequence = value; OnPropertyChanged(); } }

    }
}
