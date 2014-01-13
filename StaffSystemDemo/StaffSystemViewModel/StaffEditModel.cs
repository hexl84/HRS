using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSystemViewModel
{
    public class StaffEditModel
    {
        public int EditId { get; set; }

        public string EditName { get; set; }
        public DateTime? EditBirthDay { get; set; }
        public string EditSchool { get; set; }
        public string EditAddress { get; set; }
        public string EditWorkExperience { get; set; }
        public string EditSelfAssessment { get; set; }
        public bool EditLock { get; set; }
        public string EditPicture { get; set; }
        public string EditAttachment { get; set; }
    }
}
