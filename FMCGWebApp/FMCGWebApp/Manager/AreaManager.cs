using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;

namespace FMCGWebApp.Manager
{
    public class AreaManager
    {
        private AreaGateway _area = new AreaGateway();

        public string SaveArea(Area area)
        {
            if (_area.IsAreaNameExists(area))
            {
                return "Area Name Already Exists.";
            }
            if (_area.SaveArea(area) > 0)
            {
                return "Area Name Saved Successfully";
            }

            return "Area Name Save Faild";
        }
    }
}