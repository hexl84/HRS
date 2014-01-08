﻿using System;
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
        public ActionResult Edit(IndexViewModel.Staff vmStaff)
        {
            _staffService.Edit(vmStaff);
            return RedirectToAction("Index");
        }


        public ActionResult Lock(string state, int id = 0)
        {
                _staffService.Lock(id, state);
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
        public ActionResult UploadPicture(HttpPostedFileBase head, int staffId)
        {

            if (head == null)
            {
                return View("Error");
            }

            var staff = _staffService.FindInfo(staffId);

            var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
            var extension = Path.GetExtension(head.FileName);
            if (!supportedTypes.Contains(extension) || head.ContentLength > 1024 * 1000 * 3)
            {
                return View("Error");
            }

            var filename = staffId + extension;
            var filepath = Path.Combine(Server.MapPath("~/Images/StaffImage"), filename);
            head.SaveAs(filepath);

            staff.Picture = filename;
            _staffService.Edit(staff);
            return Edit(staffId);
            
        }

        [HttpPost]
        public ActionResult UploadAttachment(HttpPostedFileBase head, int staffId)
        {

            if (head == null)
            {
                return View("Error");
            }

            var staff = _staffService.FindInfo(staffId);

            var supportedTypes = new[] { ".doc", ".docx"};
            var extension = Path.GetExtension(head.FileName);
            if (!supportedTypes.Contains(extension) || head.ContentLength > 1024 * 1000 * 10)
            {
                return View("Error");
            }

            var filename = staffId + extension;
            var filepath = Path.Combine(Server.MapPath("~/Doc"), filename);
            head.SaveAs(filepath);

            staff.Attachment = filename;
            _staffService.Edit(staff);
            return Edit(staffId);
            
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
