using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repository;
using StaffSystemData.DataBase;
using StaffSystemData.DataContext;
using StaffSystemData.DataModel;

namespace StaffSystemService.Repository
{
    public interface IStaffService
    {
        IEnumerable<Staff> QueryAllStaffs();
        void Add(Staff staff);
        Staff FindInfo(int Id);
        void Edit(Staff staff);
        void Lock(int Id);
    }
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        
        public IEnumerable<Staff> QueryAllStaffs()
        {
            //return staffSystemDBEntities.Staffs;
            return _staffRepository.QueryAllStaffs();
        }

        public void Add(Staff staff)
        {
            //staffSystemDBEntities.Staffs.Add(staff);
            //staffSystemDBEntities.SaveChanges();
            throw new NotImplementedException();
        }

        public Staff FindInfo(int Id)
        {
            //return staffSystemDBEntities.Staffs.Find(Id);
            throw new NotImplementedException();
        }

        public void Edit(Staff staff)
        {
            //staffSystemDBEntities.Entry(staff).State = EntityState.Modified;
            //staffSystemDBEntities.SaveChanges();
            throw new NotImplementedException();
        }

        public void Lock(int Id)
        {
            //var staff = FindInfo(Id);
            //if (staff.Lock == 0)
            //{
            //    staff.Lock = 1;
            //}
            //else
            //{
            //    staff.Lock = 0;
            //}

            //staffSystemDBEntities.Entry(staff).State = EntityState.Modified;
            //staffSystemDBEntities.SaveChanges();
            throw new NotImplementedException();
        }
    }
}
