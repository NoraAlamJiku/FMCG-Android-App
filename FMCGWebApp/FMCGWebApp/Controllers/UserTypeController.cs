using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FMCGWebApp.Manager;
using FMCGWebApp.Models;

namespace FMCGWebApp.Controllers
{
    public class UserTypeController : ApiController
    {
        private PasswordAndDesignationManager _passwordAndDesignation = new PasswordAndDesignationManager();
        public HttpResponseMessage PostEmployeeUserType(EmployeeUserType employee)
        {
            var messages = _passwordAndDesignation.SetEmployeeUserType(employee);
            if (messages != null)
            {
                var message = Request.CreateResponse(HttpStatusCode.Created, messages);
                return message;
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Category not created");
        }
    }
}
