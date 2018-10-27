using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.ViewModels;

namespace FMCGWebApp.Manager
{
    public class AccountManager
    {
        private AccountGateway _account = new AccountGateway();

        public List<LoginInfo> AdminLogin(LoginInfo login)
        {
            return _account.AdminLogin(login);
        }

        public List<LoginInfo> AssistantLogin(LoginInfo login)
        {
            return _account.AssistantLogin(login);
        }

        public List<LoginInfo> WhInchargeLogin(LoginInfo login)
        {
            return _account.WhInchargeLogin(login);
        }

        public List<LoginInfo> SellForceLogin(LoginInfo login)
        {
            return _account.SellForceLogin(login);
        }

        public List<LoginInfo> GetUserRole(int id)
        {
            return _account.GetUserRole(id);
        }
    }
}