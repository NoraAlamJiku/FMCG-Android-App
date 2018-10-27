using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;
using FMCGWebApp.ViewModels;

namespace FMCGWebApp.Manager
{
    public class SellOrderManager
    {
        private SellOrderGateway _sellOrder = new SellOrderGateway();

        public string SendSellOrder(SellOrder sellOrder)
        {
            if (_sellOrder.IsSellOrderExists(sellOrder))
            {
                sellOrder.Id = _sellOrder.GetOrderId(sellOrder);
                if (_sellOrder.UpdateSendSellOrder(sellOrder) > 0)
                {
                    return "Sell Order Successfully Updated.";
                }

            }
            if (_sellOrder.SendSellOrder(sellOrder) > 0)
            {
                return "Sell Order Send Successfully.";
            }

            return "Sell Order Sending Faild.";
        }

        public List<Shop> GetAllShop()
        {
            return _sellOrder.GetAllShop();
        }
        public List<Category> GetAllCategory()
        {
            return _sellOrder.GetAllCategory();
        }
        public List<Item> GetAllItem()
        {
            return _sellOrder.GetAllItem();
        }
    }
}