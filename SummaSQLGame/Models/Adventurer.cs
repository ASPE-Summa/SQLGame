using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("avonturiers")]
    [PrimaryKey("Id")]
    public class Adventurer : ObservableObject
    {
        private readonly int _id;
        private string _name;
        private int _level;
        private string _className;
        private int _strength;
        private int _dexterity;
        private int _constitution;
        private int _intelligence;
        private int _wisdom;
        private int _charisma;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("naam")]
        [StringLength(255)]
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        [Column("klasse")]
        [StringLength(255)]
        public string ClassName { get { return _className; } set {_className = value; OnPropertyChanged(); } }

        [Column("niveau")]
        public int Level { get { return _level; } set { _level = value; OnPropertyChanged(); } }

        [Column("kracht")]
        public int Strength { get { return _strength; } set { _strength = value; OnPropertyChanged(); } }

        [Column("behendigheid")]
        public int Dexterity { get { return _dexterity; } set { _dexterity = value; OnPropertyChanged(); } }

        [Column("constitutie")]
        public int Constitution { get { return _constitution; } set { _constitution = value; OnPropertyChanged(); } }

        [Column("intelligentie")]
        public int Intelligence { get { return _intelligence; } set { _intelligence = value; OnPropertyChanged(); } }

        [Column("wijsheid")]
        public int Wisdom { get { return _wisdom; } set { _wisdom = value; OnPropertyChanged(); } }

        [Column("charisma")]
        public int Charisma { get { return _charisma; } set { _charisma = value; OnPropertyChanged(); } }
    }
}
