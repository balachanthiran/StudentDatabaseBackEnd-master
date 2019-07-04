using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDatabaseAPI.Models
{
    public class Social
    {
        public int UserID { get; set; }
        public int SocialID { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
    }
}
