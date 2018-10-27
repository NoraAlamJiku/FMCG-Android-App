using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.ViewModels;

namespace FMCGWebApp.Manager
{
    public class ReportManager
    {
        private ReportGateway _report = new ReportGateway();

        public List<CompareOrderandStock> GetNeedandStockInfo()
        {
            return _report.GetNeedandStockInfo();
        } 
    }
}