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
    public class EmployeeController : ApiController
    {
        private EmployeeManager _employee = new EmployeeManager();
        private PasswordAndDesignationManager _passwordAndDesignation = new PasswordAndDesignationManager();
        public HttpResponseMessage PostEmployee(Employee employee)
        {
            var messages = _employee.SaveEmployee(employee);
            if (messages != null)
            {
                var message = Request.CreateResponse(HttpStatusCode.Created, messages);
                return message;
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Category not created");
        }

        public HttpResponseMessage Get()
        {
            try
            {
                var product = _employee.GetDesignationList();
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
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var product = _passwordAndDesignation.GetEmployeeById(id);
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
