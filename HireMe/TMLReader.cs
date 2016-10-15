using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HireMe.Models;
using System.IO;

namespace HireMe
{
    public class TMLReader
    {
        private List<JobModels> jobs;
        private List<ApplicationModels> applications;
        private FileStream file;

        private static TMLReader theReader = null;

        string fileName;
        private TMLReader()
        {
            jobs = new List<JobModels>();
            applications = new List<ApplicationModels>();
            fileName = "App_Data/Data.tml";
        }

        public static TMLReader getTMLReader()
        {
            if (theReader == null)
                theReader = new TMLReader();
            return theReader;
        }


        public void addNewJobModel(JobModels jm)
        {
            if (jm != null)
                jobs.Add(jm);
        }

        public void addNewApplicationModel(ApplicationModels am)
        {
            if (am != null)
                applications.Add(am);
        }

        public bool removeJob(JobModels jm)
        {
            return jobs.Remove(jm);
        }

        public bool removeApplication(ApplicationModels am)
        {
            return applications.Remove(am);
        }
        private int getIntFromString(string str)
        {
            if (str == null)
                return -1;
            int returnable = 0;
            foreach(char ch in str)
            {
                returnable *= 10;
                switch(ch)
                {
                    case '1':
                        returnable += 1;
                        break;
                    case '2':
                        returnable += 2;
                        break;
                    case '3':
                        returnable += 3;
                        break;
                    case '4':
                        returnable += 4;
                        break;
                    case '5':
                        returnable += 5;
                        break;
                    case '6':
                        returnable += 6;
                        break;
                    case '7':
                        returnable += 7;
                        break;
                    case '8':
                        returnable += 8;
                        break;
                    case '9':
                        returnable += 9;
                        break;
                    case '0':
                        returnable += 0;
                        break;
                    default:
                        return -1;
                }
            }
            return returnable;
        }
        public void read()
        {
            try {
                file = File.Open(fileName, FileMode.Open);
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine("ERROR! " + fileName + " could not open the dataFile!");
                Console.WriteLine(ex.Message);
                return;
            }
            file.Close();
            IEnumerable<string> lines = File.ReadLines(fileName);

            JobModels curJob = null;
            ApplicationModels curApp = null;
            string[] splitter = null;

            foreach(string line in lines)
            {
                // Test for new objects
                if(line.Contains("->Job"))
                {
                    if (curApp != null)
                        applications.Add(curApp);
                    curApp = null;
                    if (curJob != null)
                        jobs.Add(curJob);
                    curJob = new JobModels();
                    continue;
                }
                if(line.Contains("->Application"))
                {
                    if (curApp != null)
                        applications.Add(curApp);
                    curApp = new ApplicationModels();
                    if (curJob != null)
                        jobs.Add(curJob);
                    curJob = null;
                    continue;
                }

                splitter = line.Split(new char[] { ':' });
                if (splitter.Count() < 2)
                    continue;



                switch(splitter[0])
                {
                    case "-|ID":
                        if (curJob != null)
                            curJob.id = getIntFromString(splitter[1]);
                        else if (curApp != null)
                            curApp.id = getIntFromString(splitter[1]);
                        break;
                    case "-|Title":
                        if (curJob != null)
                            curJob.Title = splitter[1];
                        break;
                    case "-|Description":
                        if (curJob != null)
                            curJob.description = splitter[1];
                        break;
                    case "-|Skills":
                        if (curApp != null)
                            curApp.skills = splitter[1];
                        else if (curJob != null)
                            curJob.skills = splitter[1];
                        break;
                    case "-|Location":
                        if (curJob != null)
                            curJob.Location = splitter[1];
                        break;
                    case "-|Salary":
                        if (curJob != null)
                            curJob.salary = splitter[1];
                        break;
                    case "-|Name":
                        if (curApp != null)
                            curApp.Name = splitter[1];
                        break;
                    case "-|City":
                        if (curApp != null)
                            curApp.City = splitter[1];
                        break;
                    case "-|State":
                        if (curApp != null)
                            curApp.State = splitter[1];
                        break;
                    case "-|Education":
                        if (curApp != null)
                            curApp.education = splitter[1];
                        break;
                    case "-|Reason":
                        if (curApp != null)
                            curApp.reason = splitter[1];
                        break;
                    case "-|JobID":
                        if (curApp != null)
                            curApp.jobID = getIntFromString(splitter[1]);
                        break;
                }
            } // End of foreach


            foreach(JobModels jm in jobs)
            {
                foreach(ApplicationModels am in applications)
                {
                    if (jm.id == am.jobID)
                        jm.applications.Add(am);
                }
            }
        }

        public void write()
        {
            try
            {
                file = File.Open(fileName, FileMode.OpenOrCreate);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERROR! " + fileName + " could not open the dataFile!");
                Console.WriteLine(ex.Message);
                return;
            }
            file.Close();
            File.WriteAllText(fileName, "");

            StreamWriter writer = File.AppendText(fileName);

            foreach(JobModels jm in jobs)
            {
                writer.WriteLine("->Job");
                writer.WriteLine("-|ID:" + jm.id);
                writer.WriteLine("-|Title:" + jm.Title);

                jm.description.Replace('\n', ';');

                writer.WriteLine("-|Description:" + jm.description);

                jm.skills.Replace('\n', ';');
                writer.WriteLine("-|Skills:" + jm.skills);

                writer.WriteLine("-|Location:" + jm.Location);

                writer.WriteLine("-|Salary:" + jm.salary);

                writer.WriteLine("-/\n");
            }

            foreach(ApplicationModels am in applications)
            {
                writer.WriteLine("->Application");
                writer.WriteLine("-|ID:" + am.id);
                am.skills.Replace('\n', ';');

                writer.WriteLine("-|Skills:" + am.skills);
                writer.WriteLine("-|Name:" + am.Name);
                writer.WriteLine("-|City:" + am.City);
                writer.WriteLine("-|State:" + am.State);

                am.education.Replace('\n', ';');

                writer.WriteLine("-|Education:"+am.education);
                writer.WriteLine("-|JobID:" + am.jobID);
            }
        }
    }
}