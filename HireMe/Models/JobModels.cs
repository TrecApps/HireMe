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
        public string Title;
        public string description;
        public string skills;
        public string Location;
        public string salary;
        public float upperSalaryRange;
        public float lowerSalaryRange;
        public List<ApplicationModels> applications;

        private JobModels()
        {
            id = countId++;
            applications = new List<ApplicationModels>();
        }

        private static List<JobModels> jobList;
        public static void prepJobList()
        {
            if (jobList == null)
                jobList = new List<JobModels>();
            else
                return;
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|/Database1.mdf;Integrated Security=True");
            connect.Open();
            SqlCommand comm = new SqlCommand("SELECT COUNT(*) from Job;");

            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                JobModels newMod = new JobModels();
                jobList.Add(newMod);

            }

            connect.Close();

            foreach (JobModels am in jobList)
            {
                am.LoadFromTable();
            }
        }


        public static void addNewJob(string title, string desc, string skillReq,
            string city, string salaryRange)
        {
            JobModels jm = new JobModels();
            jobList.Add(jm);
            jm.description = desc;
            jm.Location = city;
            jm.salary = salaryRange;
            jm.skills = skillReq;
            jm.Title = title;

            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|/Database1.mdf;Integrated Security=True");
            connect.Open();
            string commandStr = "INSERT INTO Job (JobTitle,Jobdescription,JobReqSkill,JobCity,JobSalaryRange)\n";
            commandStr += " VALUES ('"+jm.Title+"','"+jm.description+"','"+jm.skills+"','"+jm.Location+"','"+jm.salary+"')";

            SqlCommand comm = new SqlCommand(commandStr, connect);
            comm.ExecuteNonQuery();
            connect.Close();

        }

        public static JobModels getJobModel(int c)
        {
            if (c < 0 || c > jobList.Count())
                return null;
            return jobList.ElementAt(c);

        }

        public int getId()
        {
            return id;
        }



        public void SaveToTable()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|/Database1.mdf;Integrated Security=True");
            connect.Open();
            string commandStr = "UPDATE " + id + "from Job\n";
            commandStr += " SET JobTitle='" + Title + "', Jobdescription='" + description + "', \n";
           
            commandStr += " JobReqSkill='" + skills + "', JobCity='" + Location + "', JobSalaryRange='" + salary+ "'\n";

            commandStr += " WHERE JobID=" + id + ";";
            SqlCommand command = new SqlCommand(commandStr,connect);

            command.ExecuteNonQuery();
        }

        public void LoadFromTable()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|/Database1.mdf;Integrated Security=True");
            connect.Open();
            SqlCommand command = new SqlCommand("SELECT " + id + "from Job;", connect);
            SqlDataReader reader = command.ExecuteReader();
            Title = reader.GetString(1);
            description = reader.GetString(2);
            skills = reader.GetString(3);

            Location = reader.GetString(4);
            salary = reader.GetString(5);

        }
    }
}