//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeTask.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descraption { get; set; }
        public Nullable<bool> IsComplate { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public Nullable<System.DateTime> ComplatedDate { get; set; }
        public string comment { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
