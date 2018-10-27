using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMCGWebApp.Models
{
    public class ShopInfo
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string ShopkeeperName { get; set; }
        public int AreaId { get; set; }
        public int? EmployeeId { get; set; }
        public string PhoneNumber { get; set; }
    }
}