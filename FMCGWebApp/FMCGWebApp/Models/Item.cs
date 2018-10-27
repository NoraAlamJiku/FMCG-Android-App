using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMCGWebApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int CategoryId { get; set; }
    }
}