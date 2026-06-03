using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveRent
{
    
    
    [Table("Cars")]
    internal class Car
    {
        [Key]
        public int CarID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        public int ModelYear { get; set; }

        [Required]
        [MaxLength(20)]
        public string PlateNumber { get; set; }

        [MaxLength(30)]
        public string Color { get; set; }

        [MaxLength(30)]
        public string FuelType { get; set; }

        [MaxLength(30)]
        public string Transmission { get; set; }

        public int Mileage { get; set; }

        public bool IsAvailable { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual CarCategory CarCategory { get; set; }
        public virtual ICollection<Rentals> Rentals { get; set; }
    }
}

