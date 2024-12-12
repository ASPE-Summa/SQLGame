using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("knoppen_veiligheid")]
    [PrimaryKey("Id")]
    public class ButtonSafety : ObservableObject
    {
        private readonly int _id;
        private bool _isSafe;
        private int _buttonId;
        private Button _button;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("is_veilig")]
        public bool IsSafe { get { return _isSafe; } set { _isSafe = value; OnPropertyChanged(); } }

        [Column("knop_id")]
        public int ButtonId { get { return _buttonId; } set { _buttonId = value; OnPropertyChanged(); } }

        [ForeignKey("ButtonId")]
        public Button Button {  get { return _button; } set { _button = value; OnPropertyChanged(); } }
    }
}
