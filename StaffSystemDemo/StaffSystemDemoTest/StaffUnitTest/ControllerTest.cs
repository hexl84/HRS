using System;
using System.Threading;
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
            viewResult.Model.Should().BeOfType<IndexViewModel>();//返回的是否是model
            (viewResult.Model as IndexViewModel).StaffList.Count.Should().Be(1);//测试返回的是否是返回list的count
        }

        [Test]
        public void Test_Add_when_correctly()
        {
            //Arrange
            IndexViewModel.Staff vmStaff = new IndexViewModel.Staff();
            vmStaff.Name = "yg";
            vmStaff.BirthDay = DateTime.Parse("2014-01-02");
            vmStaff.School = "School";
            vmStaff.Address = "Address";
            vmStaff.WorkExperience = "WorkExperience";

            IndexViewModel.Staff vmStaffEmpty = new IndexViewModel.Staff();

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            var staffServiceMock1 = new Mock<IStaffService>();

            int count = 0;

            //Act
            staffServiceMock.Setup(t => t.Add(vmStaff)).Callback(() => count++);
            controller.Add(vmStaff);

            //Assert
            count.Should().Be(1);

        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Test_Add_When_Mistakenly()
        {
            //Arrange
            IndexViewModel.Staff vmStaff = new IndexViewModel.Staff();
            vmStaff.Id = -1;
            vmStaff.Name = "yg";
            vmStaff.BirthDay = DateTime.Parse("2014-01-02");
            vmStaff.School = "School";
            vmStaff.Address = "Address";
            vmStaff.WorkExperience = "WorkExperience";

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);
             
            //Act
            staffServiceMock.Setup(t => t.Add(vmStaff)).Throws(new Exception("save failed !"));
            controller.Add(vmStaff);
            //Assert

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
            IndexViewModel.Staff vmStaff = new IndexViewModel.Staff();
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
           controller.Edit(vmStaff);

            //Assert
            count.Should().Be(1);

        }

        [Test]
        public void Test_Lock()
        {
            //Arrange
            var state = "Lock";
            var id = 1;

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            int count = 0;
            staffServiceMock.Setup(x => x.Lock(id,state)).Callback(() => count++);

            //Act
            controller.Lock(state, id);

            //Assert
            count.Should().Be(1);

        }

        [Test]
        public void Test_UnLock()
        {
            //Arrange
            var state = "UnLock";
            var id = 1;

            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);

            int count = 0;
            staffServiceMock.Setup(x => x.Lock(id, state)).Callback(() => count++);

            //Act
            controller.Lock(state, id);

            //Assert
            count.Should().Be(1);

        }

        [Test]
        public void Test_Search()
        {
            //Arrange
            var name = "1";
            
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
            (viewResult.Model as IndexViewModel).StaffList.Count.Should().Be(1);
        }

       
    }
}
