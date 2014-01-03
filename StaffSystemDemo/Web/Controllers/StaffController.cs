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
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        public ActionResult Index()
        {
            var staffList = _staffService.QueryAllStaffs();
            if (staffList == null)
            {
                return View();
            }
            return View("Index", staffList);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(VM_Staff staff)
        {
            try
            {
                _staffService.Add(staff);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Msg = "save failed !";
                ViewBag.Id = -1;
                return View("Error");
            }
        }

        //select model
        public ActionResult Edit(int Id=0)
        {
            
            VM_Staff vmStaff = _staffService.FindInfo(Id);
            return View("Edit", vmStaff);
        }

        //update model into db
        [HttpPost]
        public ActionResult Edit(VM_Staff vmStaff)
        {
            try
            {
                _staffService.Edit(vmStaff);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "update failed !";
                ViewBag.Id = -1;
                return View("Error");
            }
            return View();
        }


        public ActionResult Lock(string state, int Id = 0)
        {
            try
            {
               _staffService.Lock(Id,state);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Msg = state+" failed !";
                ViewBag.Id = -1;
                return View("Error");
            }
        }

        public ActionResult Search(string name)
        {

            var staffList = _staffService.QueryAllStaffs().Where(t=>t.Name.Contains(name)).OrderBy(a => a.Id).Select(t => new VM_Staff
            {
                Id = t.Id,
                Name = t.Name,
                BirthDay = t.BirthDay,
                School = t.School,
                Address = t.Address,
                WorkExperience = t.WorkExperience,
                Lock = t.Lock
            });
            return View("Index", staffList);
        }

        //image upload
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase head, string IdUpload, string UploadFile)
        {
            try
            {
                if ((head == null))
                {
                    if (UploadFile == "Picture")
                    {
                        ViewBag.Msg = "please select a picture!";
                    }
                    else
                    {
                        ViewBag.Msg = "please select a Attachment!";
                    }
                    
                    ViewBag.Id = int.Parse(IdUpload);
                    return View("Error");
                }
                else
                {                  
                    var staff = _staffService.FindInfo(int.Parse(IdUpload));

                    if (UploadFile == "Picture")
                    {
                        var supportedTypes = new[] { "jpg", "jpeg" };
                        var fileExt = System.IO.Path.GetExtension(head.FileName).Substring(1);
                        if (!supportedTypes.Contains(fileExt))
                        {

                            ViewBag.Msg = "the file type is wrong!";
                            ViewBag.Id = int.Parse(IdUpload);
                            return View("Error");
                        }

                        if (head.ContentLength > 1024 * 1000 * 3)
                        {
                            ViewBag.Msg = "the file size is so big, please select other picture!";
                            ViewBag.Id = int.Parse(IdUpload);
                            return View("Error");
                        }

                        var filename = IdUpload + "." + fileExt;
                        var filepath = Path.Combine(Server.MapPath("~/Images/StaffImage"), filename);
                        head.SaveAs(filepath);

                        staff.Picture = filename;
                        _staffService.Edit(staff);
                        return Edit(int.Parse(IdUpload));
                    }
                    else if (UploadFile == "Attachment")
                    {
                        var supportedTypes = new[] { "doc", "docx" };
                        var fileExt = System.IO.Path.GetExtension(head.FileName).Substring(1);
                        if (!supportedTypes.Contains(fileExt))
                        {

                            ViewBag.Msg = "the file type is wrong!";
                            ViewBag.Id = int.Parse(IdUpload);
                            return View("Error");
                        }

                        var filename = IdUpload  + "." + fileExt;
                        var filepath = Path.Combine(Server.MapPath("~/Doc"), filename);
                        head.SaveAs(filepath);

                        staff.Attachment = filename;
                        _staffService.Edit(staff);
                        return Edit(int.Parse(IdUpload));
                    }
                    else
                    {
                        ViewBag.Msg = "the file type is wrong!";
                        ViewBag.Id = int.Parse(IdUpload);
                        return View("Error");
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Msg = "save failed !";
                ViewBag.Id = int.Parse(IdUpload);
                return View("Error");
            }

            
        }

        public FileStreamResult OpenFile(string Attachment)
        {
            string path = Path.Combine(Server.MapPath("~/Doc"), Attachment);
            string FileType = Path.GetExtension(Attachment).ToLower();
            if (FileType==".doc")
            {
                return File(new FileStream(path, FileMode.Open), "application/msword", Attachment);
            }
            else
            {
                return File(new FileStream(path, FileMode.Open), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", Attachment);
            }
            
        }

    }
}
