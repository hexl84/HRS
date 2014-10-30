using System.Collections.Generic;
using StaffSystemData.DataModel;

namespace Repository.Repository
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> QueryAllStaffs();
        void Add(Staff staff);
        Staff FindInfo(int Id);
        void Edit(Staff staff);
        void DeleteStaff(int id);
       
    }
}