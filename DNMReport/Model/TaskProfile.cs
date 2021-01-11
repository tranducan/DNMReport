using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNMReport.Model
{
    public class TaskProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
        public string Extention { get; set; }
        public DateTime CreateTime {get;set;}
        public DateTime LastTimeModified { get; set; }
     
    }
}
