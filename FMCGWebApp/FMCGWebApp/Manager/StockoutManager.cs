using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;
using FMCGWebApp.ViewModels;

namespace FMCGWebApp.Manager
{
    public class StockoutManager
    {
        private StockOutGateway _stockOut = new StockOutGateway();

        public string ConfirmSellOrder(Stockout stockout)
        {
            if (_stockOut.ConfirmSellOrder(stockout) > 0)
            {
                return "Aprove Sell Ordered";
            }

            return "Oparetion Faild";
        }

        public List<Stockout> SellOrderList()
        {
            return _stockOut.SellOrderList();
        }

        public List<SellOrderInfo> GetAllSellOrder(int id)
        {
            return _stockOut.GetAllSellOrder(id);
        } 
    }
}