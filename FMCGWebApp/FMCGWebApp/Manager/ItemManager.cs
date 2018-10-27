using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;

namespace FMCGWebApp.Manager
{
    public class ItemManager
    {
        private ItemGateway _item = new ItemGateway();
        public string SaveItem(Item item)
        {
            if (_item.IsItemNameExists(item))
            {
                return "Item Name Already Exists.";
            }
            if (_item.SaveItem(item) > 0)
            {
                return "Item Name Saved Successfully";
            }

            return "Item Name Save Faild";
        }

        public List<Category> GetAllCategory()
        {
            return _item.GetAllCategory();
        }
    }
}