using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNMReport.Model;
using System.IO;
namespace DNMReport.Controller
{
  public  class ControllerJobs
    {
        public Jobs GetJobs()
        {
            Jobs jobs = new Jobs();
            jobs.ID = "Test01";
            jobs.Schedule = new ScheduleJob{ ID = "Schedule01", Name = "Test", Task = "Test JMP", Action = "Run JMP",TimeRun = DateTime.Now,Status=false };
            jobs.Task = new TaskProfile();
            jobs.Task.Id = "Task01";
            FileInfo fileInfo = new FileInfo(StaticConfigurationcs.JMPfolderInitial + "Test Script.jsl");
            jobs.Task.Name = fileInfo.Name;
            jobs.Task.Direction = fileInfo.DirectoryName;
            jobs.Task.Extention = fileInfo.Extension;
            jobs.Task.LastTimeModified = fileInfo.LastWriteTime;
            jobs.Task.CreateTime = fileInfo.CreationTime;
            return jobs;
        }

    }
}
