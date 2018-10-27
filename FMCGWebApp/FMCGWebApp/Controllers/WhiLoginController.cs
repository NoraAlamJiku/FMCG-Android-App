using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FMCGWebApp.Manager;
using FMCGWebApp.ViewModels;

namespace FMCGWebApp.Controllers
{
    public class WhiLoginController : ApiController
    {
        private AccountManager _account = new AccountManager();

        public HttpResponseMessage Get(string email, string password)
        {
            LoginInfo login = new LoginInfo();
            login.Email = email;
            login.Password = password;
            try
            {
                var product = _account.WhInchargeLogin(login);
                if (product != null)
                {
                    var message = Request.CreateResponse(HttpStatusCode.Found, product);
                    return message;
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product not found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
