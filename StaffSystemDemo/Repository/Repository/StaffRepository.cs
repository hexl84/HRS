using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffSystemData.DataContext;
using StaffSystemData.DataModel;

namespace Repository.Repository
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> QueryAllStaffs();
        void Add(Staff staff);
        Staff FindInfo(int Id);
        void Edit(Staff staff);
       
    }

    public class StaffRepository : IStaffRepository
    {
        private readonly IDbAccess _dbAccess;

        public StaffRepository(IDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }


        public IEnumerable<Staff> QueryAllStaffs()
        {
            return _dbAccess.QueryAllStaffs();
        }

        public void Add(Staff staff)
        {
            _dbAccess.Add(staff);
        }

        public Staff FindInfo(int Id)
        {
            return _dbAccess.FindInfo(Id);
        }

        public void Edit(Staff staff)
        {
            _dbAccess.Edit(staff);
        }

       
    }
}
