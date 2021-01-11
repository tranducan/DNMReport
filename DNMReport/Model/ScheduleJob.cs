using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNMReport.Model
{
  public  class ScheduleJob
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Task { get; set; }
        public string Action { get; set; }
        public DateTime TimeRun { get; set; }
        public bool Status { get; set; }
    }
}
