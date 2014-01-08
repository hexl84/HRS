using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StaffSystemService.Service;
using StaffSystemViewModel;

namespace StaffSystemDemo.Web.Controllers
{
    public class StaffController : BaseController
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        public ActionResult Index()
        {
            var indexModel = new IndexViewModel
            {
                StaffList = _staffService.QueryAllStaffs()
            };
            return View("Index", indexModel);

        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(IndexViewModel.Staff staff)
        {
           
            if (staff.Id<0)
            {
                throw new Exception("failed");
            }
            _staffService.Add(staff);
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public ActionResult AddStaff(IndexViewModel.Staff staff)
        {
            _staffService.Add(staff);
            return new EmptyResult();
        }

        //select model
        public ActionResult Edit(int Id = 0)
        {

            IndexViewModel.Staff vmStaff = _staffService.FindInfo(Id);
            return View("Edit", vmStaff);
        }

        //update model into db
        [HttpPost]
        public ActionResult Edit(IndexViewModel.Staff vmStaff)
        {
            _staffService.Edit(vmStaff);
            return RedirectToAction("Index");
        }


        public ActionResult Lock(string state, int Id = 0)
        {
                _staffService.Lock(Id, state);
                return RedirectToAction("Index");
        }

        public ActionResult Search(string name)
        {
            var indexModel = new IndexViewModel
            {
                StaffList = _staffService.Search(name)
            };

            return View("Index", indexModel);
        }

        //image upload
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase head, string IdUpload, string UploadFile)
        {
            
            if ((head == null))
            {
                return View("Error");
            }

            var staff = _staffService.FindInfo(int.Parse(IdUpload));

            if (UploadFile == "Picture")
            {
                var supportedTypes = new[] { "jpg", "jpeg" };
                var extension = Path.GetExtension(head.FileName);
                if (extension != null)
                {
                    var fileExt = extension.Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        return View("Error");
                    }

                    if (head.ContentLength > 1024 * 1000 * 3)
                    {
                        return View("Error");
                    }

                    var filename = IdUpload + "." + fileExt;
                    var filepath = Path.Combine(Server.MapPath("~/Images/StaffImage"), filename);
                    head.SaveAs(filepath);

                    staff.Picture = filename;
                }
                _staffService.Edit(staff);
                return Edit(int.Parse(IdUpload));
            }
            if (UploadFile == "Attachment")
            {
                var supportedTypes = new[] { "doc", "docx" };
                var extension = Path.GetExtension(head.FileName);
                if (extension != null)
                {
                    var fileExt = extension.Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        return View("Error");
                    }

                    var filename = IdUpload + "." + fileExt;
                    var filepath = Path.Combine(Server.MapPath("~/Doc"), filename);
                    head.SaveAs(filepath);

                    staff.Attachment = filename;
                }
                _staffService.Edit(staff);
                return Edit(int.Parse(IdUpload));
            }
            return View("Error");
        }

        public FileStreamResult OpenFile(string attachment)
        {
            var path = Path.Combine(Server.MapPath("~/Doc"), attachment);
            var extension = Path.GetExtension(attachment);
            if (extension != null)
            {
                var fileType = extension.ToLower();
                if (fileType == ".doc")
                {
                    return File(new FileStream(path, FileMode.Open), "application/msword", attachment);
                }
            }
            return File(new FileStream(path, FileMode.Open), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", attachment);
        }

        public ViewResult Error()
        {
            return View();
        }

    }
}
