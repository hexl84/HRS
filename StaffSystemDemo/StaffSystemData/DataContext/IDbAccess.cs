using System.Collections.Generic;
using StaffSystemData.DataModel;

namespace StaffSystemData.DataContext
{
    public interface IDbAccess
    {
        IEnumerable<Staff> QueryAllStaffs();
        void Add(Staff staff);
        Staff FindInfo(int id);
        void Edit(Staff staff);
        void DeleteStaff(int id);
    }
}