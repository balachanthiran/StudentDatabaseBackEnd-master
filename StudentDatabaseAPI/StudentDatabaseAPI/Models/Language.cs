﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDatabaseAPI.Models
{
    public class Language
    {
        public int LanguageId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
    }
}
