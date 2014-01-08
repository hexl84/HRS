using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Repository;
using StaffSystemData.DataModel;
using StaffSystemViewModel;

namespace StaffSystemService.Service
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }


        public List<IndexViewModel.Staff> QueryAllStaffs()
        {
            return _staffRepository.QueryAllStaffs().OrderBy(a => a.Id).Select(t => new IndexViewModel.Staff
            {
                Id = t.Id,
                Name = t.Name,
                BirthDay = t.BirthDay,
                School = t.School,
                Address = t.Address,
                WorkExperience = t.WorkExperience,
                Lock = t.Lock
            }).ToList();
        }

        public void Add(IndexViewModel.Staff vmStaff)
        {
            var staff = new Staff();
            staff.Id = vmStaff.Id;
            staff.Name = vmStaff.Name;
            staff.BirthDay = vmStaff.BirthDay;
            staff.School = vmStaff.School;
            staff.Address = vmStaff.Address;
            staff.WorkExperience = vmStaff.WorkExperience;
            staff.SelfAssessment = vmStaff.SelfAssessment;
            staff.Lock = false;
            staff.Picture = vmStaff.Picture;
            staff.Attachment = vmStaff.Attachment;
            _staffRepository.Add(staff);
        }

        public IndexViewModel.Staff FindInfo(int id)
        {
            var staff = _staffRepository.FindInfo(id);
            var vmStaff = new IndexViewModel.Staff
            {
                Id = staff.Id,
                Name = staff.Name,
                BirthDay = staff.BirthDay,
                School = staff.School,
                Address = staff.Address,
                WorkExperience = staff.WorkExperience,
                SelfAssessment = staff.SelfAssessment,
                Lock = staff.Lock,
                Picture = staff.Picture,
                Attachment = staff.Attachment
            };

            return vmStaff;
        }

        public void Edit(IndexViewModel.Staff vmStaff)
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

        public void Lock(int id, string state)
        {

            var staff = _staffRepository.FindInfo(id);
            staff.Lock = state == "Lock";
            _staffRepository.Edit(staff);
        }

        public List<IndexViewModel.Staff> Search(string name)
        {
            var staffList = QueryAllStaffs().Where(t => t.Name!=null && t.Name.Contains(name)).ToList();
            return staffList;
        }


    }
}
