using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using StaffSystemData.DataContext;
using StaffSystemData.DataModel;
using StaffSystemDemo.Web.Controllers;
using StaffSystemService.Service;
using Moq;
using StaffSystemViewModel;
using Repository.Repository;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StaffSystemDemoTest.StaffUnitTest
{
    /// <summary>
    /// Summary description for StaffControllerTest mstest
    /// </summary>
    [TestClass]
    public class StaffControllerTest
    {
        public StaffControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_Index()
        {
            ////Arrange
            //var staffServiceMock = new Mock<IStaffService>();
            //var staffServiceObject = staffServiceMock.Object;
            //staffServiceMock.Setup(x => x.QueryAllStaffs()).Returns(new List<IndexViewModel.Staff>
            //{
            //    new IndexViewModel.Staff
            //    {
            //        Name = "moto",
            //        Address = "aaa"
            //    }
            //});
            //var controller = new StaffController(staffServiceObject);


            ////Act
            //var result = controller.Index();

            ////Assert
            //var viewResult =(ViewResult)result;
            //viewResult.Model.Should().BeOfType<IndexViewModel>();//返回的是否是model
            //(viewResult.Model as IndexViewModel).StaffList.Count.Should().Be(1);//测试返回的是否是返回list的count
        }

        //httppost
        [TestMethod]
        public void Test_Add_when_correctly()
        {
            ////Arrange
            //IndexViewModel.Staff vmStaff = new IndexViewModel.Staff();
            //vmStaff.Name = "yg";
            //vmStaff.BirthDay = DateTime.Parse("2014-01-02");
            //vmStaff.School = "School";
            //vmStaff.Address = "Address";
            //vmStaff.WorkExperience = "WorkExperience";

            //var staffServiceMock = new Mock<IStaffService>();
            //var staffServiceObject = staffServiceMock.Object;
            //var controller = new StaffController(staffServiceObject);

            //int count = 0;

            ////Act
            //staffServiceMock.Setup(t => t.Add(vmStaff)).Callback(() => count++);
            //controller.Add(vmStaff);

            ////Assert
            //count.Should().Be(1);

        }

        [TestMethod]
        public void Test_Add_When_Mistakenly()
        {
            ////Arrange
            //IndexViewModel.Staff vmStaff = new IndexViewModel.Staff();
            //vmStaff.Name = "yg";
            //vmStaff.BirthDay = DateTime.Parse("2014-01-02");
            //vmStaff.School = "School";
            //vmStaff.Address = "Address";
            //vmStaff.WorkExperience = "WorkExperience";

            //IndexViewModel.Staff vmStaffEmpty = new IndexViewModel.Staff();

            //var staffServiceMock = new Mock<IStaffService>();
            //var staffServiceObject = staffServiceMock.Object;
            //var controller = new StaffController(staffServiceObject);

            //var staffServiceMock1 = new Mock<IStaffService>();


            ////Act
            //staffServiceMock.Setup(t => t.Add(vmStaff)).Throws(new Exception("save failed !"));
            //controller.Add(vmStaff);

            //Assert

        }



    }
}
