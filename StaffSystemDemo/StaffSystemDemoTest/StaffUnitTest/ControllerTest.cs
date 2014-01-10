using System;
using System.Threading;
using System.Web;
using System.Web.Routing;
using System.Windows.Forms;
using FluentAssertions;
using StaffSystemService.Service;
using StaffSystemViewModel;
using Moq;
using System.Collections.Generic;
using StaffSystemDemo.Web.Controllers;
using System.Web.Mvc;
using NUnit.Framework;

namespace StaffSystemDemoTest.StaffUnitTest
{
    [TestFixture]
    public class ControllerTest
    {
        [Test]
        public void Test_Index()
        {
            //Arrange
            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            staffServiceMock.Setup(x => x.QueryAllStaffs()).Returns(new List<IndexViewModel.Staff>
            {
                new IndexViewModel.Staff
                {
                    Name = "yg",
                    Address = "Address"
                }
            });
            var controller = new StaffController(staffServiceObject);
            
            //Act
            var result = controller.Index();

            //Assert
            var viewResult = (ViewResult)result;
            viewResult.Model.Should().BeOfType<IndexViewModel>();
            var indexViewModel = viewResult.Model as IndexViewModel;
            if (indexViewModel != null)
                indexViewModel.StaffList.Count.Should().Be(1);
        }

        [Test]
        public void Test_AddStaff_when_correctly()
        {
            //Arrange
            var vmStaff = new IndexViewModel.Staff();
            vmStaff.Name = "yg";
            vmStaff.BirthDay = DateTime.Parse("2014-01-02");
            vmStaff.School = "School";
            vmStaff.Address = "Address";
            vmStaff.WorkExperience = "WorkExperience";

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            var count = 0;

            //Act
            staffServiceMock.Setup(t => t.Add(vmStaff)).Callback(() => count++);
            controller.AddStaff(vmStaff);

            //Assert
            count.Should().Be(1);

        }

        [Test]
        public void Test_Add()
        {
            //Arrange
            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            //Act
            var result=controller.Add();
            
            //Assert
            var viewResult = (ViewResult)result;
            viewResult.ViewName.Should().BeEmpty();
        }

        [Test]
        public void Test_Add_when_correctly()
        {
            //Arrange
            var vmStaff = new IndexViewModel.Staff();
            vmStaff.Name = "yg";
            vmStaff.BirthDay = DateTime.Parse("2014-01-02");
            vmStaff.School = "School";
            vmStaff.Address = "Address";
            vmStaff.WorkExperience = "WorkExperience";

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            var count = 0;

            //Act
            staffServiceMock.Setup(t => t.Add(vmStaff)).Callback(() => count++);
            controller.Add(vmStaff);

            //Assert
            count.Should().Be(1);

        }

        [Test]
        public void Test_Edit_By_Id()
        {
            //Arrange
            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            staffServiceMock.Setup(x => x.FindInfo(1)).Returns(new IndexViewModel.Staff()
            {
                Name = "yg",
                Address = "Address"
            });

            //Act
            var controller = new StaffController(staffServiceObject);
            var result=controller.Edit(1);

            //Assert
            var viewResult = (ViewResult) result;
            viewResult.Model.Should().BeOfType<IndexViewModel.Staff>();

        }

        [Test]
        public void Test_Edit_By_Staff()
        {
            //Arrange
            var vmStaff = new IndexViewModel.Staff();
            vmStaff.Id = -1;
            vmStaff.Name = "yg";
            vmStaff.BirthDay = DateTime.Parse("2014-01-02");
            vmStaff.School = "School";
            vmStaff.Address = "Address";
            vmStaff.WorkExperience = "WorkExperience";

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            int count = 0;
            staffServiceMock.Setup(x => x.Edit(vmStaff)).Callback(()=>count++);
            
            //Act
           //controller.Edit(vmStaff);

            //Assert
            count.Should().Be(1);

        }

        [Test]
        public void Test_Lock()
        {
            //Arrange
            //const string state = "Lock";
            //const int id = 1;

            //var staffServiceMock = new Mock<IStaffService>();
            //var staffServiceObject = staffServiceMock.Object;
            //var controller = new StaffController(staffServiceObject);

            //int count = 0;
            //staffServiceMock.Setup(x => x.Lock(id,state)).Callback(() => count++);

            ////Act
            //controller.Lock(state, id);

            ////Assert
            //count.Should().Be(1);

        }

        [Test]
        public void Test_UnLock()
        {
            //Arrange
            //const string state = "UnLock";
            //const int id = 1;

            //var staffServiceMock = new Mock<IStaffService>();
            //var staffServiceObject = staffServiceMock.Object;
            //var controller = new StaffController(staffServiceObject);

            //int count = 0;
            //staffServiceMock.Setup(x => x.Lock(id, state)).Callback(() => count++);

            ////Act
            //controller.Lock(state, id);

            ////Assert
            //count.Should().Be(1);

        }

        [Test]
        public void Test_Search()
        {
            //Arrange
            const string name = "1";
            
            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            staffServiceMock.Setup(x => x.Search(name)).Returns(new List<IndexViewModel.Staff>
            {
                new IndexViewModel.Staff()
                {
                    Id = 1,
                    Name = "yg"
                }
            });

            //Act
            var serchResult=controller.Search(name);

            //Assert
            var viewResult = (ViewResult)serchResult;
            viewResult.Model.Should().BeOfType<IndexViewModel>();
            var indexViewModel = viewResult.Model as IndexViewModel;
            if (indexViewModel != null)
                indexViewModel.StaffList.Count.Should().Be(1);
        }

        //if file is empty
        [Test]
        public void Test_UploadPicture_When_EmptyFile()
        {
            //Arrange
            //const int staffId = 1;
            
            //var staffServiceMock = new Mock<IStaffService>();
            //var staffServiceObject = staffServiceMock.Object;
            //var controller = new StaffController(staffServiceObject);

            //var httpPostedFileBase = new Mock<HttpPostedFileBase>();

            //var file = httpPostedFileBase.Object;
            //file = null;
            ////Act
            //var result = controller.UploadPicture(file, staffId);

            ////Assert
            //var viewResult = (ViewResult)result;
            //viewResult.ViewName.Should().Be("Error");
        }


        [Test]
        public void Test_UploadPicture_ByFileType()
        {
            ////Arrange
            //const int staffId = 1;
            
            //var staffServiceMock = new Mock<IStaffService>();
            //var controller = new StaffController(staffServiceMock.Object);

            //var httpPostedFileBaseMock = new Mock<HttpPostedFileBase>();
            //httpPostedFileBaseMock.Setup(h => h.FileName).Returns(".gif");
            //var file = httpPostedFileBaseMock.Object;

            ////Act
            //var result = controller.UploadPicture(file, staffId);

            ////Assert  
            //var viewResult = (ViewResult)result;
            //viewResult.ViewName.Should().Be("Error");
        }

        [Test]
        public void Test_UploadPicture_ByFileTypeAndLength()
        {
            ////Arrange
            //const int staffId = 1;
            
            //var staffServiceMock = new Mock<IStaffService>();
            //var controller = new StaffController(staffServiceMock.Object);

            //var httpPostedFileBaseMock = new Mock<HttpPostedFileBase>();
            //httpPostedFileBaseMock.Setup(h => h.FileName).Returns(".png");
            //httpPostedFileBaseMock.Setup(h => h.ContentLength).Returns(30720001);
            //var file = httpPostedFileBaseMock.Object;

            ////Act
            //var result = controller.UploadPicture(file, staffId);

            ////Assert  
            //var viewResult = (ViewResult)result;
            //viewResult.ViewName.Should().Be("Error");
        }

        [Test]
        public void Test_UploadPicture_When_Success()
        {
            //Arrange
            //const int staffId = 2;
            
            //var httpContextMock = new Mock<HttpContextBase>();
            //var httpServerMock = new Mock<HttpServerUtilityBase>();
            //httpServerMock.Setup(x => x.MapPath("~/Images/StaffImage")).Returns(@"D:\work\HRS\StaffSystemDemo\Web\Images\StaffImage");
            //httpContextMock.Setup(x => x.Server).Returns(httpServerMock.Object);

            //var staffServiceMock = new Mock<IStaffService>();
            //var controller = new StaffController(staffServiceMock.Object);
            //controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);

            //staffServiceMock.Setup(x => x.FindInfo(staffId)).Returns(new IndexViewModel.Staff()
            //{
            //    Id = staffId,
            //    Name = "yg",
            //    Address = "Address",

            //});

            //var httpPostedFileBaseMock = new Mock<HttpPostedFileBase>();
            //httpPostedFileBaseMock.Setup(h => h.FileName).Returns(".jpeg");
            //var file = httpPostedFileBaseMock.Object;

            ////Act
            //var result = controller.UploadPicture(file, staffId);
            //var redirectToRouteResult = (RedirectToRouteResult)result;
            

            ////Assert
            //redirectToRouteResult.RouteValues["action"].Should().Be("Edit");
            //redirectToRouteResult.RouteValues["id"].Should().Be(staffId);



            ////Act
            //var result = controller.UploadPicture(file, staffId);
            //var viewResult = (ViewResult)result;
            //var indexViewModelStaff = viewResult.Model as IndexViewModel.Staff;

            ////Assert

            //indexViewModelStaff.Should().NotBeNull();
            //viewResult.Model.Should().BeOfType<IndexViewModel.Staff>();
            //indexViewModelStaff.Picture.Should().Be("2.jpeg");
            //viewResult.ViewName.Should().Be("Edit");

            //httpPostedFileBaseMock.Verify(x => x.SaveAs(@"D:\work\HRS\StaffSystemDemo\Web\Images\StaffImage\2.jpeg"));
        }

        //if file is empty
        [Test]
        public void Test_UploadAttachment_When_EmptyFile()
        {
            //Arrange
            const int staffId = 1;

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            var httpPostedFileBase = new Mock<HttpPostedFileBase>();

            var file = httpPostedFileBase.Object;
            file = null;
            //Act
            var result = controller.UploadAttachment(file, staffId);

            //Assert
            var viewResult = (ViewResult)result;
            viewResult.ViewName.Should().Be("Error");
        }


        [Test]
        public void Test_UploadAttachment_ByFileType()
        {
            //Arrange
            const int staffId = 1;

            var staffServiceMock = new Mock<IStaffService>();
            var controller = new StaffController(staffServiceMock.Object);

            var httpPostedFileBaseMock = new Mock<HttpPostedFileBase>();
            httpPostedFileBaseMock.Setup(h => h.FileName).Returns(".docxx");
            var file = httpPostedFileBaseMock.Object;

            //Act
            var result = controller.UploadAttachment(file, staffId);

            //Assert  
            var viewResult = (ViewResult)result;
            viewResult.ViewName.Should().Be("Error");
        }

        [Test]
        public void Test_UploadAttachment_ByFileTypeAndLength()
        {
            //Arrange
            const int staffId = 1;

            var staffServiceMock = new Mock<IStaffService>();
            var controller = new StaffController(staffServiceMock.Object);

            var httpPostedFileBaseMock = new Mock<HttpPostedFileBase>();
            httpPostedFileBaseMock.Setup(h => h.FileName).Returns(".doc");
            httpPostedFileBaseMock.Setup(h => h.ContentLength).Returns(102400001);
            var file = httpPostedFileBaseMock.Object;

            //Act
            var result = controller.UploadAttachment(file, staffId);

            //Assert  
            var viewResult = (ViewResult)result;
            viewResult.ViewName.Should().Be("Error");
        }

        [Test]
        public void Test_UploadAttachment_When_Success()
        {
            //Arrange
            const int staffId = 2;

            var httpContextMock = new Mock<HttpContextBase>();
            var httpServerMock = new Mock<HttpServerUtilityBase>();
            httpServerMock.Setup(x => x.MapPath("~/Doc")).Returns(@"D:\work\HRS\StaffSystemDemo\Web\Doc");
            httpContextMock.Setup(x => x.Server).Returns(httpServerMock.Object);

            var staffServiceMock = new Mock<IStaffService>();
            var controller = new StaffController(staffServiceMock.Object);
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);

            staffServiceMock.Setup(x => x.FindInfo(staffId)).Returns(new IndexViewModel.Staff()
            {
                Id = staffId,
                Name = "yg",
                Address = "Address",
            });

            var httpPostedFileBaseMock = new Mock<HttpPostedFileBase>();
            httpPostedFileBaseMock.Setup(h => h.FileName).Returns(".doc");
            var file = httpPostedFileBaseMock.Object;

            //Act
            var result = controller.UploadAttachment(file, staffId);
            var redirectToRouteResult = (RedirectToRouteResult)result;


            //Assert
            redirectToRouteResult.RouteValues["action"].Should().Be("Edit");
            redirectToRouteResult.RouteValues["id"].Should().Be(staffId);

            ////Act
            //var result = controller.UploadAttachment(file, staffId);
            //var viewResult = (ViewResult)result;
            //var indexViewModelStaff = viewResult.Model as IndexViewModel.Staff;

            ////Assert
            //indexViewModelStaff.Should().NotBeNull();
            //viewResult.Model.Should().BeOfType<IndexViewModel.Staff>();
            //indexViewModelStaff.Attachment.Should().Be("yg.doc");
            //viewResult.ViewName.Should().Be("Edit");

            //httpPostedFileBaseMock.Verify(x => x.SaveAs(@"D:\work\HRS\StaffSystemDemo\Web\Doc\yg.doc"));
        }

        [Test]
        public void Test_OpenFile_When_Success()
        {
            //Arrange
            const string attachment = "test.docx";

            var httpContextMock = new Mock<HttpContextBase>();
            var httpServerMock = new Mock<HttpServerUtilityBase>();
            httpServerMock.Setup(x => x.MapPath("~/Doc")).Returns(@"D:\work\HRS\StaffSystemDemo\Web\Doc");
            httpContextMock.Setup(x => x.Server).Returns(httpServerMock.Object);

            var staffServiceMock = new Mock<IStaffService>();
            var controller = new StaffController(staffServiceMock.Object);
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            //Act
            var result = controller.OpenFile(attachment);
            
            //Assert
            result.FileStream.Should().NotBeNull();
        }

        [Test]
        public void Test_Error()
        {
            //Arrange
            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            //Act
            var result = controller.Error();

            //Assert
            var viewResult = (ViewResult)result;
            viewResult.ViewName.Should().BeEmpty();
        }
        
    }
}
