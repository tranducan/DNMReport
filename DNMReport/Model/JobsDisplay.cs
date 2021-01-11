using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNMReport.Model
{
  public  class JobsDisplay
    {
        public string JobId { get; set; }
        public string TaskId { get; set; }
        public string ScheduleId { get; set; }
        public string OwnerID { get; set; }
        public string OwnerName{ get; set; }
        public DateTime TimeRun { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastTimeModified { get; set; }
        public string Status { get; set; }
    }
}
