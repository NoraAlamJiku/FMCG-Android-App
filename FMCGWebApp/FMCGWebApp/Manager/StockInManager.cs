using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;

namespace FMCGWebApp.Manager
{
    public class StockInManager
    {
        private StockInGateway _stockIn = new StockInGateway();

        public string SaveStockin(StockIn stockIn)
        {
            int bigQuantity = stockIn.BigQuantity * 50;
            int mediumQuantity = stockIn.MediumQuantity * 30;
            int smallQuantity = stockIn.SmallQuantity * 15;
            stockIn.TotalQuantity = bigQuantity + mediumQuantity + smallQuantity;

            if (_stockIn.SaveStockin(stockIn) > 0)
            {
                return "Item Entry Successfully";
            }

            return "Item Entry Faild";
        }
    }
}