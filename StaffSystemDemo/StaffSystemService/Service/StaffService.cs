using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
                SelfAssessment = t.SelfAssessment,
                Lock = t.Lock,
                Picture=t.Picture,
                Attachment = t.Attachment
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
            if (!string.IsNullOrEmpty(vmStaff.Picture))
            {
                staff.Picture = vmStaff.Picture;
            }
            if (!string.IsNullOrEmpty(vmStaff.Attachment))
            {
                staff.Attachment = vmStaff.Attachment;
            }
            _staffRepository.Edit(staff);
        }

        private void UploadPicture(HttpPostedFileBase head, int staffPicId)
        {

            //if (head == null)
            //{
            //    return View("Error");
            //}

            //var staff = _staffService.FindInfo(staffPicId);

            //var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
            //var extension = Path.GetExtension(head.FileName);
            //if (!supportedTypes.Contains(extension) || head.ContentLength > 1024 * 1000 * 3)
            //{
            //    return View("Error");
            //}

            //var filename = staffPicId + extension;
            //var filepath = Path.Combine(Server.MapPath("~/Images/StaffImage"), filename);
            //head.SaveAs(filepath);

            //staff.Picture = filename;
            //_staffService.Edit(staff);
            //return RedirectToAction("Edit", new { id = staff.Id });
        }

        public void Edit(StaffEditModel editStaff, HttpPostedFileBase headPic, HttpPostedFileBase headAtt)
        {
            var staff = _staffRepository.FindInfo(editStaff.editId);
            staff.Id = editStaff.editId;
            staff.Name = editStaff.editName;
            staff.BirthDay = editStaff.editBirthDay;
            staff.School = editStaff.editSchool;
            staff.Address = editStaff.editAddress;
            staff.WorkExperience = editStaff.editWorkExperience;
            staff.SelfAssessment = editStaff.editSelfAssessment;
            staff.Lock = editStaff.editLock;
            //if (!string.IsNullOrEmpty(editStaff.editPicture))
            //{
            //    staff.Picture = editStaff.editPicture;
            //}
            //if (!string.IsNullOrEmpty(editStaff.editAttachment))
            //{
            //    staff.Attachment = editStaff.editAttachment;
            //}
            if (headPic != null)
            {
                
                var extension = Path.GetExtension(headPic.FileName);
                var filename = editStaff.editId + extension;
                var filepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/StaffImage"), filename);
                headPic.SaveAs(filepath);
                staff.Picture = filename;
            }
            if (headAtt != null)
            {
                var filepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Doc"), headAtt.FileName);
                headAtt.SaveAs(filepath);
                staff.Attachment = headAtt.FileName;
            }
            _staffRepository.Edit(staff);
        }


        public void Lock(int id)
        {

            var staff = _staffRepository.FindInfo(id);
            staff.Lock = staff.Lock != true;
            _staffRepository.Edit(staff);
        }

        public List<IndexViewModel.Staff> Search(string parameter)
        {

            var staffList = QueryAllStaffs().Where(t => (t.Name != null && t.Name.Contains(parameter)) || (t.School != null && t.School.Contains(parameter)) ||
                                                         (t.Address != null && t.Address.Contains(parameter)) || (t.WorkExperience != null && t.WorkExperience.Contains(parameter)) ||
                                                         (t.SelfAssessment != null && t.SelfAssessment.Contains(parameter)) ||  t.Id.ToString().Contains(parameter)).ToList();
            return staffList;
        }


    }
}
