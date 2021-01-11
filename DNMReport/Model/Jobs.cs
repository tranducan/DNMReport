using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNMReport.Model
{
   public class Jobs
    {
        public string ID { get; set; }
        public ScheduleJob Schedule { get; set; }
        public TaskProfile Task { get; set; }
         public Jobs()
        {
            Schedule = new ScheduleJob();
            Task = new TaskProfile();
        }
    }
}
