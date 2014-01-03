using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using StaffSystemData.DataModel;

namespace StaffSystemData.DataBase
{
    
        public class StaffSystemDBEntities : DbContext
        {
            public StaffSystemDBEntities()
                : base("name=StaffSystemDBEntities")
            {
            }

            public DbSet<Staff> Staff { get; set; }


            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Staff>().HasKey(t=>t.Id);
                modelBuilder.Entity<Staff>()
                    .Property(t => t.Id)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            }
        }
    
}
