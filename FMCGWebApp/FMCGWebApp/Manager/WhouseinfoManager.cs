using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;

namespace FMCGWebApp.Manager
{
    public class WhouseinfoManager
    {
        private WhouseinfoGateway _whouseinfo = new WhouseinfoGateway();

        public string SaveWhouse(W_h_info wHInfo)
        {
            if (_whouseinfo.IsWhouseExists(wHInfo))
            {
                return "W H Name Already Exists.";
            }
            if (_whouseinfo.SaveWhouse(wHInfo) > 0)
            {
                return "W H Name Saved Successfully";
            }

            return "W H Name Save Faild";
        }
    }
}