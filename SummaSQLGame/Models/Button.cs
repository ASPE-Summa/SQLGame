using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("knoppen")]
    [PrimaryKey("Id")]
    public class Button : ObservableObject
    {
        private int _id;
        private string? _emoji;
        private string? _emojiNumber;
        private string _name;
        private ButtonSafety _buttonSafety;
        private bool _isSafe = false;

        [Key]
        [Column("id")]
        public int Id { get { return _id; } set { _id = value; OnPropertyChanged(); } }

        [Column("emoji")]
        [StringLength(255)]
        public string? Emoji { get { return _emoji; } set { _emoji = value; OnPropertyChanged(); } }

        [Column("naam")]
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        [NotMapped]
        public bool IsSafe { get { return _isSafe; } set { _isSafe = value; OnPropertyChanged(); } }

        [InverseProperty("Button")]
        public ButtonSafety  ButtonSafety {  get { return _buttonSafety; } set { _buttonSafety = value; OnPropertyChanged(); } }
    }
}
