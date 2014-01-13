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
