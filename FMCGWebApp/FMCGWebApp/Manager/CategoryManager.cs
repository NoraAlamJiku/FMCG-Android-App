using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;

namespace FMCGWebApp.Manager
{
    public class CategoryManager
    {
        private CategoryGateway _category = new CategoryGateway();

        public string SaveCategory(Category category)
        {
            if (_category.IsCategoryNameExists(category))
            {
                return "Category Name Already Exists.";
            }
            if (_category.SaveCategory(category) > 0)
            {
                return "Category Name Saved Successfully";
            }

            return "Category Name Save Faild";
        }
    }
}