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

                        var filename = IdUpload + "." + fileExt;
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
            if (FileType == ".doc")
            {
                return File(new FileStream(path, FileMode.Open), "application/msword", Attachment);
            }
            else
            {
                return File(new FileStream(path, FileMode.Open), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", Attachment);
            }

        }

        public ViewResult Error(string msg,string id)
        {
            ViewBag.Msg = msg;
            ViewBag.Id = int.Parse(id);
            return View();
        }

    }
}
