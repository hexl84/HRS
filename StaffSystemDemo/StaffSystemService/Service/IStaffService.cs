using System.Collections.Generic;
using System.Web;
using StaffSystemViewModel;

namespace StaffSystemService.Service
{
    public interface IStaffService
    {
        List<IndexViewModel.Staff> QueryAllStaffs();
        void Add(IndexViewModel.Staff vmStaff);
        IndexViewModel.Staff FindInfo(int id);
        void Edit(IndexViewModel.Staff staff);
        void Edit(StaffEditModel staff, HttpPostedFileBase headPic, HttpPostedFileBase headAtt);
        void Lock(int id);
        List<IndexViewModel.Staff> Search(string name);
    }
}