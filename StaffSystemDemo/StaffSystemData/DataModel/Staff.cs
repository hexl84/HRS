using System;
using System.ComponentModel.DataAnnotations;

namespace StaffSystemData.DataModel
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public string School { get; set; }
        public string Address { get; set; }
        public string WorkExperience { get; set; }
        public string SelfAssessment { get; set; }
        public bool Lock { get; set; }
        public string Picture { get; set; }
        public string Attachment { get; set; }
    }
}
