//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DNMReport.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ScheduleTrigger
    {
        public long TriggerId { get; set; }
        public string TrigerName { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime CreateTimeUtc { get; set; }
        public System.DateTime LastModifiedTime { get; set; }
        public System.DateTime LastModifiedTimeUtc { get; set; }
        public string LastModifiedUser { get; set; }
    }
}
