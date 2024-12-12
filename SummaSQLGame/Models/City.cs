using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummaSQLGame.Models
{
    [Table("steden")]
    [PrimaryKey("Id")]
    public class City : ObservableObject
    {
        private readonly int _id;
        private string _name;
        private int? _countryId;
        private Country? _country;
        private int? _population;
        private bool _isCapital;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("naam")]
        [StringLength(255)]
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        [Column("land")]
        public int? CountryId { get { return _countryId; } set { _countryId = value; OnPropertyChanged(); } }
        
        [ForeignKey("CountryId")]
        public Country? Country { get { return _country; } set {_country = value; OnPropertyChanged(); } }

        [NotMapped]
        public string? CountryName { get; set; }

        [Column("inwoners")]
        public int? Population { get { return _population; } set { _population = value; OnPropertyChanged(); } }

        [Column("is_hoofdstad")]
        public bool IsCapital { get { return _isCapital; } set { _isCapital = value; OnPropertyChanged(); } }
    }
}
