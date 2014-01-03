using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Repository;
using StaffSystemData.DataModel;
using StaffSystemViewModel;

namespace StaffSystemService.Service
{
    public interface IStaffService
    {
        IEnumerable<VM_Staff> QueryAllStaffs();
        void Add(VM_Staff staff);
        VM_Staff FindInfo(int Id);
        void Edit(VM_Staff staff);
        void Lock(int Id,string state);
    }
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        
        public IEnumerable<VM_Staff> QueryAllStaffs()
        {
            return _staffRepository.QueryAllStaffs().OrderBy(a=>a.Id).Select(t => new VM_Staff
            {
                Id = t.Id,
                Name = t.Name,
                BirthDay = t.BirthDay,
                School = t.School,
                Address = t.Address,
                WorkExperience = t.WorkExperience,
                Lock = t.Lock
            }); 
        }

        public void Add(VM_Staff vm_staff)
        {
            Staff staff=new Staff();
            staff.Id = vm_staff.Id;
            staff.Name=vm_staff.Name;
            staff.BirthDay=vm_staff.BirthDay;
            staff.School=vm_staff.School;
            staff.Address=vm_staff.Address;
            staff.WorkExperience=vm_staff.WorkExperience;
            staff.SelfAssessment=vm_staff.SelfAssessment;
            staff.Lock=0;
            staff.Picture=vm_staff.Picture;
            staff.Attachment=vm_staff.Attachment;
            _staffRepository.Add(staff);
            
        }

        public VM_Staff FindInfo(int Id)
        {
            Staff staff=_staffRepository.FindInfo(Id);
            VM_Staff vmStaff = new VM_Staff();

            vmStaff.Id = staff.Id;
            vmStaff.Name = staff.Name;
            vmStaff.BirthDay = staff.BirthDay;
            vmStaff.School = staff.School;
            vmStaff.Address = staff.Address;
            vmStaff.WorkExperience = staff.WorkExperience;
            vmStaff.SelfAssessment = staff.SelfAssessment;
            vmStaff.Lock = staff.Lock;
            vmStaff.Picture = staff.Picture;
            vmStaff.Attachment = staff.Attachment;
            
            return vmStaff;
        }

        public void Edit(VM_Staff vmStaff)
        {
            var staff = _staffRepository.FindInfo(vmStaff.Id);
            staff.Id = vmStaff.Id;
            staff.Name = vmStaff.Name;
            staff.BirthDay = vmStaff.BirthDay;
            staff.School = vmStaff.School;
            staff.Address = vmStaff.Address;
            staff.WorkExperience = vmStaff.WorkExperience;
            staff.SelfAssessment = vmStaff.SelfAssessment;
            staff.Lock = vmStaff.Lock;
            staff.Picture = vmStaff.Picture;
            staff.Attachment = vmStaff.Attachment;
            _staffRepository.Edit(staff);
        }

        public void Lock(int Id,string state)
        {
            
            var staff=_staffRepository.FindInfo(Id);
            if (state=="Lock")
            {
                staff.Lock = 1;
            }
            else
            {
                staff.Lock = 0;
            }
            _staffRepository.Edit(staff);
        }


    }
}
