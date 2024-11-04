using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("spotify")]
    [PrimaryKey("Id")]
    public class Songs : ObservableObject
    {
        private readonly int _id;
        private string _track;
        private string _artists;
        private int _releasedYear;
        private Int64? _streams;
        private int _bpm;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("titel")]
        [StringLength(255)]
        public string Track { get { return _track; } set { _track = value; OnPropertyChanged(); } }

        [Column("artiesten")]
        [StringLength(255)]
        public string Artists { get { return _artists; } set { _artists = value; OnPropertyChanged(); } }

        [Column("verschijningsjaar")]
        public int ReleasedYear { get { return _releasedYear; } set { _releasedYear = value; OnPropertyChanged(); } }

        [Column("streams")]
        public Int64? Streams { get { return _streams; } set { _streams = value; OnPropertyChanged(); } }

        [Column("bpm")]
        public int Bpm { get { return _bpm; } set { _bpm = value; OnPropertyChanged(); }}
    }
}
