using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveRent
{
   
    
        [Table("CarCategories")]
        internal class CarCategory
        {
            [Key]
            public int CategoryID { get; set; }

            [Required]
            [MaxLength(50)]
            public string CategoryName { get; set; }

            public decimal DailyPrice { get; set; }

            [MaxLength(200)]
            public string Description { get; set; }

            public virtual ICollection<Car> Cars { get; set; }
        }
}
