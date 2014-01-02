using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffSystemData.DataModel;

namespace StaffSystemData.DataBase
{
    
        public partial class StaffSystemDBEntities : DbContext
        {
            public StaffSystemDBEntities()
                : base("name=StaffSystemDBEntities")
            {
            }

            public DbSet<Staff> Staffs { get; set; }
        }
    
}
