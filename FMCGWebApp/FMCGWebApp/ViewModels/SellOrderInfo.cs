using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMCGWebApp.ViewModels
{
    public class SellOrderInfo
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string ShopName { get; set; }
        public string AreaName { get; set; }
        public int Quentity { get; set; }
    }
}