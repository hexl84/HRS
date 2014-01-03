using System;
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
                    Name = "moto",
                    Address = "aaa"
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
        
    }
}
