using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDatabaseAPI.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string Nationality { get; set; }

        public string City { get; set; }

        public string Image { get; set; }

        public static bool operator ==(User a, User b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if((object)a == null || (object)b == null)
            {
                return false;
            }
            return a.UserID == b.UserID;
        }

        public static bool operator !=(User a, User b)
        {
            if (ReferenceEquals(a, b))
            {
                return false;
            }
            if ((object)a == null || (object)b == null)
            {
                return true;
            }
            return a.UserID != b.UserID;
        }

    }
}
