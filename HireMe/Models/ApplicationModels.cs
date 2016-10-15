using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace HireMe.Models
{
    public class ApplicationModels
    {
        public int id;
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string skills { get; set; }
        public string education { get; set; }
        public string reason { get; set; }
        public int jobID { set; get; }

        private static int idCount= 1;
        private static List<ApplicationModels> ModelList;

        public ApplicationModels()
        {
            id = idCount++;
        }

        public static void prepFromDatabase()
        {
            if (ModelList == null)
                ModelList = new List<ApplicationModels>();
            else
                return;
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|/Database1.mdf;Integrated Security=True");
            connect.Open();
            SqlCommand comm = new SqlCommand("SELECT COUNT(*) from App;");

            SqlDataReader reader = comm.ExecuteReader();

            while(reader.Read())
            {
                ApplicationModels newMod = new ApplicationModels();
                ModelList.Add(newMod);

            }

            connect.Close();

            foreach(ApplicationModels am in ModelList)
            {
                am.loadFromTable();
            }
        }

        public static void AddNewApplication(string name,string city,string state,
            string skills, string education, string reason, int jID)
        {
            ApplicationModels am = new ApplicationModels();
            ModelList.Add(am);
            am.City = city;
            am.education = education;
            am.jobID = jID;
            am.Name = name;
            am.reason = reason;
            am.skills = skills;
            am.State = state;

            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|/Database1.mdf;Integrated Security=True");
            connect.Open();
            string commandStr = "INSERT INTO App (AName, ACity, AState, ASkills, AEducation, Areason, JobID)\n";
            commandStr += " VALUES ('"+am.Name+"','"+am.City+"','"+am.State+"','"+am.skills+"','"+am.education+"','"+am.reason+"',"+am.jobID+");\n";
            SqlCommand comm = new SqlCommand(commandStr, connect);
            comm.ExecuteNonQuery();

            connect.Close();
        }

        public static ApplicationModels getApplicationModel(int c)
        {
            if (c < 0 || c >= ModelList.Count())
                return null;
            return ModelList.ElementAt(c);
        }

        public void SaveToTable()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|/Database1.mdf;Integrated Security=True");
            connect.Open();
            string commandStr = "UPDATE " + id + "from App\n";
            commandStr += " SET AName='" + Name + "', ACity='" + City + "', AState='" + State + "', ASkills='" + skills + "', AEducation='" +
                education + "', Areason='" + reason + "', JobID=" + jobID + "\n";
            commandStr += " WHERE ACRN=" + id + ";";

            SqlCommand command = new SqlCommand(commandStr, connect);

            command.ExecuteNonQuery();
        }

        public void loadFromTable()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|/Database1.mdf;Integrated Security=True");
            connect.Open();
            SqlCommand command = new SqlCommand("SELECT " + id + "from App;", connect);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Name = reader.GetString(1);
            City = reader.GetString(2);
            State = reader.GetString(3);
            skills = reader.GetString(4);
            education = reader.GetString(5);
            reason = reader.GetString(6);
            jobID = reader.GetInt32(7);
        }
    }
}