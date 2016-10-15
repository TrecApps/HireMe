using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HireMe.Models
{
    public class JobModels
    {
        static int countId = 1;

        private int id;
        private string Title;
        private string description;
        private List<string> skills;
        private string Location;
        private float upperSalaryRange;
        private float lowerSalaryRange;
        private List<ApplicationModels> applications;

        public JobModels(string title)
        {
            id = countId++;
            Title = title;
            skills = new List<string>();
        }

        public int getId()
        {
            return id;
        }

        public string getTitle()
        { return Title; }

        public void setDescription(string desc)
        {
            description = desc;
        }

        public string getDescription()
        {
            return description;
        }

        public void addSkill(string skill)
        {
            skills.Add(skill);
        }

        public string getSkill(int c)
        {
            if (c > skills.Count() || c < 1)
                return null;
            return skills.ElementAt(c-1);
        }

        public string removeSkill(int c)
        {
            if (c > skills.Count() || c < 1)
                return null;
            string retunable = skills.ElementAt(c-1);
            skills.RemoveAt(c - 1);
            return retunable;
        }

        public bool removeSkill(string str)
        {
            return skills.Remove(str);
        }

        public void setLocation(string str)
        {
            Location = str;
        }

        public string getLocation()
        {
            return Location;
        }

        public float getUpperSalary()
        {
            return upperSalaryRange;
        }

        public float getLowerSalary()
        {
            return lowerSalaryRange;
        }

        public void setUpperSalary(float money)
        {
            if (money < 0)
                throw new IndexOutOfRangeException("You need to Pay workers, not charge them!");
            if (money < lowerSalaryRange)
                lowerSalaryRange = money;
            upperSalaryRange = money;
        }

        public void setLowerSalary(float money)
        {
            if (money < 0)
                throw new IndexOutOfRangeException("You need to Pay workers, not charge them!");
            if (money > upperSalaryRange)
                upperSalaryRange = money;
            lowerSalaryRange = money;
        }

        public void AddApplication(ApplicationModels am)
        {
            if (am == null)
                return;

            applications.Add(am);
        }

        public ApplicationModels getApplicationModel(int c)
        {
            if (c > applications.Count() || c < 1)
                return null;

            return applications.ElementAt(c - 1);
        }

        public List<ApplicationModels> getApplications()
        {
            return new List<ApplicationModels>(applications);
        }

        public void SaveToTable()
        {

        }

        public void LoadFromTable()
        {
            //SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename='';Integrated Security=True");
        }
    }
}