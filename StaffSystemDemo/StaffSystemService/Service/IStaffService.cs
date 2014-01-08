using System.Collections.Generic;
using StaffSystemViewModel;

namespace StaffSystemService.Service
{
    public interface IStaffService
    {
        List<IndexViewModel.Staff> QueryAllStaffs();
        void Add(IndexViewModel.Staff vmStaff);
        IndexViewModel.Staff FindInfo(int id);
        void Edit(IndexViewModel.Staff staff);
        void Lock(int id, string state);
        List<IndexViewModel.Staff> Search(string name);
    }
}