using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMCGWebApp.Models
{
    public class W_h_info
    {
        public int Id { get; set; }
        public string WhName { get; set; }
        public double Capacity { get; set; }
        public string Location { get; set; }
        public int? EmployeeId { get; set; }
    }
}