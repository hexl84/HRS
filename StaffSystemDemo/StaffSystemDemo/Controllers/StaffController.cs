using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StaffSystemService.Repository;
using StaffSystemViewModel;

namespace StaffSystemDemo.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffRepository)
        {
            _staffService = staffRepository;
        }

        public ActionResult Index()
        {
            var staffList = _staffService.QueryAllStaffs().OrderBy(a => a.Id).Select(t => new VM_Staff
            {
                Id = t.Id,
                Name = t.Name,
                BirthDay = t.BirthDay,
                School = t.School,
                Address = t.Address,
                WorkExperience = t.WorkExperience
            });
            if (staffList==null)
            {
                return View();
            }
            return View("Index",staffList);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(VM_Staff staff)
        {
            //try
            //{
            //   // StaffRepository staffRepository = new StaffRepository();
            //    staff.Lock = 0;
            //    _staffRepository.Add(staff);
            //    return RedirectToAction("Index");
            //}
            //catch (Exception)
            //{
            //    ViewBag.Msg = "save failed !";
            //    ViewBag.Id = -1;
            //    return View("Error");
            //}
            return View();
        }

        //select model
        public ActionResult Edit(int Id=0)
        {
            //StaffRepository staffRepository = new StaffRepository();
            //VM_Staff staff = _staffRepository.FindInfo(Id);
            //if (Id!=0)
            //{
            //    ViewBag.IdData = Id;
            //}
            //else
            //{
            //    ViewBag.IdData = 0;
            //}
            //return View("Edit", staff);
            return View();
        }

        //update model into db
        [HttpPost]
        public ActionResult Edit(VM_Staff staff)
        {
            //try
            //{
            //    //StaffRepository staffRepository = new StaffRepository();
            //    staff.Lock = 0;
            //    _staffRepository.Edit(staff);
            //    return RedirectToAction("Index");
            //}
            //catch (Exception)
            //{
            //    ViewBag.Msg = "update failed !";
            //    ViewBag.Id = -1;
            //    return View("Error");
            //}
            return View();
        }


        public ActionResult Lock(string state, int Id = 0)
        {
            try
            {
                //StaffRepository staffRepository = new StaffRepository();
                _staffService.Lock(Id);
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
            //StaffRepository staffRepository = new StaffRepository();
            var staffList = _staffService.QueryAllStaffs().Where(a => a.Lock == 0 && a.Name.Contains(name)).OrderBy(a => a.Id);
            return View("Index", staffList);
           
        }

        //image upload
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase head, string IdData, string UploadFile)
        {
            //try
            //{
                
            //    if ((head == null))
            //    {
            //        ViewBag.Msg = "please select a picture!";
            //        ViewBag.Id = int.Parse(IdData);
            //        return View("Error");
            //    }
            //    else
            //    {
            //        //StaffRepository staffRepository = new StaffRepository();
            //        Staff staff = _staffRepository.FindInfo(int.Parse(IdData));

            //        if (UploadFile == "Picture")
            //        {
            //            var supportedTypes = new[] { "jpg", "jpeg" };
            //            var fileExt = System.IO.Path.GetExtension(head.FileName).Substring(1);
            //            if (!supportedTypes.Contains(fileExt))
            //            {

            //                ViewBag.Msg = "the file type is wrong!";
            //                ViewBag.Id = int.Parse(IdData);
            //                return View("Error");
            //            }

            //            if (head.ContentLength > 1024 * 1000 * 3)
            //            {
            //                ViewBag.Msg = "the file size is so big, please select other picture!";
            //                ViewBag.Id = int.Parse(IdData);
            //                return View("Error");
            //            }

            //            var filename = IdData + "." + fileExt;
            //            var filepath = Path.Combine(Server.MapPath("~/Images/StaffImage"), filename);
            //            head.SaveAs(filepath);

            //            staff.Picture = filename;
            //            _staffRepository.Edit(staff);
            //            return Edit(int.Parse(IdData));
            //        }
            //        else if (UploadFile == "Attachment")
            //        {
            //            var supportedTypes = new[] { "doc", "docx" };
            //            var fileExt = System.IO.Path.GetExtension(head.FileName).Substring(1);
            //            if (!supportedTypes.Contains(fileExt))
            //            {

            //                ViewBag.Msg = "the file type is wrong!";
            //                ViewBag.Id = int.Parse(IdData);
            //                return View("Error");
            //            }

            //            var filename = IdData +"-"+ staff.Name + "." + fileExt;
            //            var filepath = Path.Combine(Server.MapPath("~/Doc"), filename);
            //            head.SaveAs(filepath);

            //            staff.Attachment = filename;
            //            _staffRepository.Edit(staff);
            //            return Edit(int.Parse(IdData));
            //        }
            //        else
            //        {
            //            ViewBag.Msg = "the file type is wrong!";
            //            ViewBag.Id = int.Parse(IdData);
            //            return View("Error");
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    ViewBag.Msg = "save failed !";
            //    ViewBag.Id = int.Parse(IdData);
            //    return View("Error");
            //}

            return null;
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
