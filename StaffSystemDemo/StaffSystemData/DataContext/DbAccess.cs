using System;
using System.Collections.Generic;
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
        Staff FindInfo(int Id);
        void Edit(Staff staff);
        void Lock(int Id);
    }
    public class DbAccess :IDbAccess
    {
        //private readonly IDbAccess _dbAccess;
        private readonly StaffSystemDBEntities _dbEntitiesdbContext;
        public DbAccess(StaffSystemDBEntities dbEntitiesdbContext)
        {
            //_dbAccess = dbAccess;
            _dbEntitiesdbContext = dbEntitiesdbContext;
        }


        public IEnumerable<Staff> QueryAllStaffs()
        {
            return _dbEntitiesdbContext.Staffs;
        }

        public void Add(Staff staff)
        {
            throw new NotImplementedException();
        }

        public Staff FindInfo(int Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Staff staff)
        {
            throw new NotImplementedException();
        }

        public void Lock(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
