using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("honden")]
    [PrimaryKey("Id")]
    public class Dog : ObservableObject
    {
        private readonly int _id;
        private string _name;
        private string _race;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("naam")]
        [StringLength(255)]
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        [Column("ras")]
        [StringLength(255)]
        public string Race { get { return _race; } set {_race = value; OnPropertyChanged(); } }
    }
}
