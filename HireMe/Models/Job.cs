using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4EF.Models
{
    public class Job
    {
        public int JobID { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public string RequiredSkills { get; set; }

        public string JobLocation { get; set; }

        public string SalaryRange { get; set; }

       public string RequiredKnowledge { get; set; }
    }
}