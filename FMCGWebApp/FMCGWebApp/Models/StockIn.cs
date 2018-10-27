using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMCGWebApp.Models
{
    public class StockIn
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public int TotalQuantity { get; set; }
        public int BigQuantity { get; set; }
        public int MediumQuantity { get; set; }
        public int SmallQuantity { get; set; }
        public DateTime EntryDate { get; set; }
        public int EmployeeId { get; set; }
    }
}