using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;

namespace FMCGWebApp.Manager
{
    public class ShopInfoManager
    {
        private ShopInfoGateway _shopInfoGateway = new ShopInfoGateway();

        public string SaveShopInfo(ShopInfo shopInfo)
        {
            if (_shopInfoGateway.IsShopNameExists(shopInfo))
            {
                return "Shop Name Already Exists.";
            }
            if (_shopInfoGateway.SaveShopInfo(shopInfo) > 0)
            {
                return "Shop Name Saved Successfully";
            }

            return "Shop Name Save Faild";
        }
        public List<Area> GetAreaList()
        {
            return _shopInfoGateway.GetAreaList();
        }

        public List<Employee> GetAllEmployees()
        {
            return _shopInfoGateway.GetAllEmployees();
        }
    }
}