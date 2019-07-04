using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDatabaseAPI.Models
{
    public class SearchResult
    {
        public SearchResult(User user, string study, List<string> skills, List<string> languages)
        {
            this.User = user;
            this.Study = study;
            this.Skills = skills;
            this.Languages = languages;
        }

        public User User { get; set; }

        public string Study { get; set; }

        public List<string> Skills { get; set; }

        public List<string> Languages { get; set; }

    }
}
