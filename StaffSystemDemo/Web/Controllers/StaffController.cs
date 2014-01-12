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
            _staffService.Add(staff);
            return RedirectToAction("Index");

        }

        //by ajax request
        [HttpPost]
        public ActionResult AddStaff(IndexViewModel.Staff staff)
        {
            _staffService.Add(staff);
            return new EmptyResult();
        }

        //select model
        public ActionResult Edit(int id = 0)
        {
            IndexViewModel.Staff vmStaff = _staffService.FindInfo(id);
            return View("Edit", vmStaff);
        }

        //update model into db
        [HttpPost]
        public ActionResult Edit(StaffEditModel editStaff, HttpPostedFileBase headPic, HttpPostedFileBase headAtt)
        {
            _staffService.Edit(editStaff, headPic, headAtt);
            return RedirectToAction("Index");
        }

        public ActionResult Lock(int id = 0)
        {
            _staffService.Lock(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(string parameter)
        {
            var indexModel = new IndexViewModel
            {
                StaffList = _staffService.Search(parameter)
            };

            return View("Index", indexModel);
        }

        //image upload
        //[HttpPost]
        //public ActionResult UploadPicture(HttpPostedFileBase head, int staffPicId)
        //{

        //    if (head == null)
        //    {
        //        return View("Error");
        //    }

        //    var staff = _staffService.FindInfo(staffPicId);

        //    var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
        //    var extension = Path.GetExtension(head.FileName);
        //    if (!supportedTypes.Contains(extension) || head.ContentLength > 1024 * 1000 * 3)
        //    {
        //        return View("Error");
        //    }

        //    var filename = staffPicId + extension;
        //    var filepath = Path.Combine(Server.MapPath("~/Images/StaffImage"), filename);
        //    head.SaveAs(filepath);

        //    staff.Picture = filename;
        //    _staffService.Edit(staff);
        //    return RedirectToAction("Edit", new { id = staff.Id });
        //}



        [HttpPost]
        public ActionResult UploadAttachment(HttpPostedFileBase head, int staffAttId)
        {

            if (head == null)
            {
                return View("Error");
            }

            var staff = _staffService.FindInfo(staffAttId);

            var supportedTypes = new[] { ".doc", ".docx" };
            var extension = Path.GetExtension(head.FileName);
            if (!supportedTypes.Contains(extension) || head.ContentLength > 1024 * 1000 * 10)
            {
                return View("Error");
            }

            var filename = head.FileName;
            var filepath = Path.Combine(Server.MapPath("~/Doc"), filename);
            head.SaveAs(filepath);

            staff.Attachment = filename;
            _staffService.Edit(staff);
            return RedirectToAction("Edit", new { id = staff.Id });

        }

        public FileStreamResult OpenFile(string attachment)
        {
            var path = Path.Combine(Server.MapPath("~/Doc"), attachment);
            var extension = Path.GetExtension(attachment);
            return File(new FileStream(path, FileMode.Open), "application/octet-stream", Server.UrlEncode(attachment));
        }

        public ViewResult Error()
        {
            return View();
        }

    }
}
