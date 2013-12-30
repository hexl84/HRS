﻿using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using StaffSystemDemo.Models;
using StaffSystemDemo.Repository;

namespace StaffSystemDemo.Controllers
{
    public class StaffController : Controller
    {
        //
        // GET: /Staff/

        public ActionResult Index()
        {
            StaffRepository staffRepository = new StaffRepository();
            var staffList = staffRepository.QueryAllStaffs().Where(a=>a.Lock==0);

            if (staffList==null)
            {
                return View();
            }
            return View("Index",staffList.OrderBy(a=>a.Id));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Staff staff)
        {
            StaffRepository staffRepository = new StaffRepository();
            staff.Lock = 0;
            staffRepository.Add(staff);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id=0)
        {
            StaffRepository staffRepository = new StaffRepository();
            Staff staff=staffRepository.FindInfo(Id);
            if (Id!=0)
            {
                ViewBag.IdData = Id;
            }
            else
            {
                ViewBag.IdData = 0;
            }
            return View("Edit", staff);
        }

        [HttpPost]
        public ActionResult Edit(Staff staff)
        {
            StaffRepository staffRepository = new StaffRepository();
            staff.Lock = 0;
            staffRepository.Edit(staff);
            return RedirectToAction("Index");
        }


        public ActionResult Lock(int Id = 0)
        {
            StaffRepository staffRepository = new StaffRepository();
            staffRepository.Lock(Id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(string name)
        {
            StaffRepository staffRepository = new StaffRepository();
            var staffList = staffRepository.QueryAllStaffs().Where(a => a.Lock == 0 && a.Name.Contains(name)).OrderBy(a=>a.Id);
            return View("Index", staffList);
           
        }

        //image upload
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase head, string IdData)
        {
            try
            {
                
                if ((head == null))
                {
                    ViewBag.Msg = "please select a picture!";
                    ViewBag.Id = int.Parse(IdData);
                    return View("Error");
                }
                else
                {
                    var supportedTypes = new[] { "jpg", "jpeg" };
                    var fileExt = System.IO.Path.GetExtension(head.FileName).Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        
                        ViewBag.Msg = "the file type is wrong!";
                        ViewBag.Id = int.Parse(IdData);
                        return View("Error");
                    }

                    if (head.ContentLength > 1024 * 1000 * 3)
                    {
                        ViewBag.Msg = "the file size is so big, please select other picture!";
                        ViewBag.Id = int.Parse(IdData);
                        return View("Error");
                    }

                    Random r = new Random();
                    //var filename = DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(10000) + "." + fileExt;
                    var filename = IdData + "." + fileExt;
                    var filepath = Path.Combine(Server.MapPath("~/Images/StaffImage"), filename);
                    head.SaveAs(filepath);

                    StaffRepository staffRepository = new StaffRepository();

                    Staff staff = staffRepository.FindInfo(int.Parse(IdData));
                    staff.Picture = filename;
                    staffRepository.Edit(staff);
                    return Edit(int.Parse(IdData));

                }
            }
            catch (Exception)
            {
                ViewBag.Msg = "save failed !";
                ViewBag.Id = int.Parse(IdData);
                return View("Error");
            }

        }

    }
}