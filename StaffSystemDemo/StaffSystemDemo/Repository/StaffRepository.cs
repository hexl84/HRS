using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using StaffSystemDemo.Models;

namespace StaffSystemDemo.Repository
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> QueryAllStaffs();
    }
    public class StaffRepository : IStaffRepository
    {
        StaffSystemDBEntities staffSystemDBEntities=new StaffSystemDBEntities();
        public IEnumerable<Staff> QueryAllStaffs()
        {
            return staffSystemDBEntities.Staffs;
        }

        public void Add(Staff staff)
        {
            staffSystemDBEntities.Staffs.Add(staff);
            staffSystemDBEntities.SaveChanges();
        }

        public Staff FindInfo(int Id)
        {
           return staffSystemDBEntities.Staffs.Find(Id);
        }

        public void Edit(Staff staff)
        {
            staffSystemDBEntities.Entry(staff).State=EntityState.Modified;
            staffSystemDBEntities.SaveChanges();
        }

        public void Lock(int Id)
        {
            var staff = FindInfo(Id);
            if (staff.Lock == 0)
            {
                staff.Lock = 1;
            }
            else
            {
                staff.Lock = 0;
            }
            
            staffSystemDBEntities.Entry(staff).State = EntityState.Modified;
            staffSystemDBEntities.SaveChanges();
        }
    }
}