using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace DriveRent
{
    [Table("Rentals")]
    internal class Rentals
    {
        [Key]
        public int RentalID { get; set; }

        public int CustomerID { get; set; }

        public int CarID { get; set; }

        [Required]
        public DateTime RentDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        public decimal TotalPrice { get; set; }

        [MaxLength(30)]
        public string Status { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customers Customer { get; set; }

        [ForeignKey("CarID")]
        public virtual Car Car { get; set; }
        
       }
    }
