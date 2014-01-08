using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffSystemData.DataBase;
using StaffSystemData.DataModel;

namespace StaffSystemData.DataContext
{
    public interface IDbAccess
    {
        IEnumerable<Staff> QueryAllStaffs();
        void Add(Staff staff);
        Staff FindInfo(int id);
        void Edit(Staff staff);
        
    }
    public class DbAccess :IDbAccess
    {
        private readonly StaffSystemDBEntities _dbEntitiesdbContext;
        public DbAccess(StaffSystemDBEntities dbEntitiesdbContext)
        {
            _dbEntitiesdbContext = dbEntitiesdbContext;
        }

        public IEnumerable<Staff> QueryAllStaffs()
        {
            return _dbEntitiesdbContext.Staff;
        }

        public void Add(Staff staff)
        {
            _dbEntitiesdbContext.Staff.Add(staff);
            _dbEntitiesdbContext.SaveChanges();
        }

        public Staff FindInfo(int id)
        {
            return _dbEntitiesdbContext.Staff.Find(id);
        }

        public void Edit(Staff staff)
        {
            _dbEntitiesdbContext.Entry(staff).State=EntityState.Modified;
            _dbEntitiesdbContext.SaveChanges();

        }
    }
}
