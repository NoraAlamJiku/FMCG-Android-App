using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class SendSellOrder
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
