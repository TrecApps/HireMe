using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HireMe.Models
{
    public class ApplicationModels
    {
        public string firstName, lastName;
        public List<string> skills;
        public List<string> education;
        public string reason;

        public ApplicationModels(string fn, string ln)
        {
            skills = new List<string>();
            education = new List<string>();
        }
    }
}