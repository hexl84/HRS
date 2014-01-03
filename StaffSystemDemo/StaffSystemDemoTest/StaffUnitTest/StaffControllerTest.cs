using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffSystemData.DataContext;
using StaffSystemDemo.Web.Controllers;
using StaffSystemService.Service;
using Moq;
using StaffSystemViewModel;
using Repository.Repository;
namespace StaffSystemDemoTest.StaffUnitTest
{
    /// <summary>
    /// Summary description for StaffControllerTest
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
            //Arrange
            var staffServiceMock = new Mock<IStaffService>();
            var staffServiceObject = staffServiceMock.Object;
            var controller = new StaffController(staffServiceObject);
            
            //Act
            var result = controller.Index();

            //Assert
            var viewResult =(ViewResult)result;
            viewResult.ViewName.Equals("Index");
        }

         [TestMethod]
        public void Test_Add()
        {
            ////Arrange
            //var staffServiceMock = new Mock<IStaffService>();
            //var staffServiceObject = staffServiceMock.Object;
            //var controller = new StaffController(staffServiceObject);

            //var staffRepositoryMock = new Mock<IDbAccess>();
            //var staffRepositoryMockObject = staffRepositoryMock.Object;
            //var repository = new StaffRepository(staffRepositoryMockObject);
            //staffRepositoryMock.Setup(x => x.QueryAllStaffs()).Returns();
            ////Act
            //VM_Staff vmStaff = new VM_Staff();
            //vmStaff.Name = "yg";
            //vmStaff.BirthDay = DateTime.Parse("2014-01-02");
            //vmStaff.School = "School";
            //vmStaff.Address = "Address";
            // vmStaff.WorkExperience = "WorkExperience";
            //var result = controller.Add(vmStaff);

            ////Assert
            //var viewResult = repository.FindInfo(repository.QueryAllStaffs().Max(t => t.Id));
            //vmStaff.Name.Equals(viewResult.Name);
            //vmStaff.BirthDay.Equals(viewResult.BirthDay);
            //vmStaff.School.Equals(viewResult.School);
            //vmStaff.Address.Equals(viewResult.Address);
            //vmStaff.WorkExperience.Equals(viewResult.WorkExperience);
            //vmStaff.SelfAssessment.Equals(viewResult.SelfAssessment);
            //vmStaff.Lock.Equals(viewResult.Lock);
        }


    }
}
