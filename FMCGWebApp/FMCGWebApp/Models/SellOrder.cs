using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMCGWebApp.Models
{
    public class SellOrder
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int AreaId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime EntryDate { get; set; }
        public string Status { get; set; }
    }
}