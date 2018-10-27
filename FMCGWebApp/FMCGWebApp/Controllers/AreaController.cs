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
    public class AreaController : ApiController
    {
        private AreaManager _area = new AreaManager();
        public HttpResponseMessage PostArea(Area area)
        {
            var messages = _area.SaveArea(area);
            if (messages != null)
            {
                var message = Request.CreateResponse(HttpStatusCode.Created, messages);
                return message;
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Category not created");
        }
    }
}
