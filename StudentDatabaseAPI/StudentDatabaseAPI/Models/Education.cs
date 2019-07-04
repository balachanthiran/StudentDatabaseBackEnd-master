using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDatabaseAPI.Models
{
    public class Education
    {
        public int UserID { get; set; }
        public int EducationID { get; set; }
        public string Faculty { get; set; }
        public string Study { get; set; }
    }
}
