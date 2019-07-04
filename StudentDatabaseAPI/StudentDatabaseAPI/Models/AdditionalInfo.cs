using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDatabaseAPI.Models
{
    public class AdditionalInfo
    {
        public AdditionalInfo(Education education, Social social,  List<Language> languages, List<Skill> skills)
        {
            Education = education;
            Social = social;
            Languages = languages;
            Skills = skills;
        }
        public Education Education { get; set; }
        public Social Social{ get; set; }
        public List<Language> Languages { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
