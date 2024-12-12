using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("woorden")]
    [PrimaryKey("Id")]
    public class BattleShip : ObservableObject
    {
        private readonly int _id;
        private string _coordinates;
        private string _description;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("coordinaat")]
        [StringLength(2)]
        public string Coordinates { get { return _coordinates; } set { _coordinates = value; OnPropertyChanged(); } }

        [Column("omschrijving")]
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged(); } }
    }
}
