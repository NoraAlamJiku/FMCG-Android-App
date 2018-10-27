using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMCGWebApp.ViewModels
{
    public class CompareOrderandStock
    {
        public int Id { get; set; }
        public String CategoryName { get; set; }
        public String ItemName { get; set; }
        public string Need { get; set; }
        public string Stock { get; set; }
    }
}