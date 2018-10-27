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
    public class CategoryController : ApiController
    {
        private CategoryManager _category = new CategoryManager();
        private EmployeeManager _employee = new EmployeeManager();
        public HttpResponseMessage PostCategory(Category category)
        {
            var messages = _category.SaveCategory(category);
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
                var product = _employee.GetGenderList();
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
