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

            var vmStaff = new StaffEditModel();
            vmStaff.EditId = -1;
            vmStaff.EditName = "yg";
            vmStaff.EditBirthDay = DateTime.Parse("2014-01-02");
            vmStaff.EditSchool = "School";
            vmStaff.EditAddress = "Address";
            vmStaff.EditWorkExperience = "WorkExperience";

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            int count = 0;
            staffServiceMock.Setup(x => x.Edit(vmStaff,null,null)).Callback(()=>count++);
            
            //Act
            controller.Edit(vmStaff, null, null);

            //Assert
            count.Should().Be(1);

        }

        [Test]
        public void Test_Lock()
        {
            //Arrange
           
            const int id = 1;

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            int count = 0;
            staffServiceMock.Setup(x => x.Lock(id)).Callback(() => count++);

            //Act
            controller.Lock(id);

            //Assert
            count.Should().Be(1);

        }

        [Test]
        public void Test_UnLock()
        {
            //Arrange
            const int id = 1;

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            int count = 0;
            staffServiceMock.Setup(x => x.Lock(id)).Callback(() => count++);

            //Act
            controller.Lock(id);

            //Assert
            count.Should().Be(1);

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
