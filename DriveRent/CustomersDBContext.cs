using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace DriveRent
{
    internal class CustomersDBContext:DbContext
    {
        private int field;

        public CustomersDBContext() : base("name=CustomersDBContext")
        {

        }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Rentals> Rentals { get; set; }



    }
}
