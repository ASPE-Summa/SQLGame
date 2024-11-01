using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaSQLGame.Models
{
    [Table("bieren")]
    [PrimaryKey("Id")]
    public class Beer : ObservableObject
    {
        private readonly int _id;
        private string _name;
        private string _type;
        private double _alcoholPercentage;
        private string _brewery;
        private string _country;

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return _id; } }

        [Column("naam")]
        [StringLength(255)]
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        [Column("soort")]
        [StringLength(255)]
        public string Type { get { return _type; } set { _type = value; OnPropertyChanged(); } }

        [Column("alcoholpercentage")]
        public double AlcoholPercentage { get { return _alcoholPercentage; }   set { _alcoholPercentage = value;   OnPropertyChanged(); } }

        [Column("brouwerij")]
        [StringLength(255)]
        public string Brewery { get { return _brewery; } set { _brewery = value; OnPropertyChanged(); } }

        [Column("land")]
        [StringLength(255)]
        public string Country { get { return _country;  } set { _country = value; OnPropertyChanged(); } }
    }
}