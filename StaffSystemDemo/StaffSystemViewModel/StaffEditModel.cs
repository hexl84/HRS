using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSystemViewModel
{
    public class StaffEditModel
    {
        public int editId { get; set; }

        public string editName { get; set; }
        public DateTime? editBirthDay { get; set; }
        public string editSchool { get; set; }
        public string editAddress { get; set; }
        public string editWorkExperience { get; set; }
        public string editSelfAssessment { get; set; }
        public bool editLock { get; set; }
        public string editPicture { get; set; }
        public string editAttachment { get; set; }
    }
}
