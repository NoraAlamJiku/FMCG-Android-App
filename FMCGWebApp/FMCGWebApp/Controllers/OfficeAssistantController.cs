using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FMCGWebApp.Manager;
using FMCGWebApp.Models;
using FMCGWebApp.ViewModels;
using Newtonsoft.Json;

namespace FMCGWebApp.Controllers
{
    public class OfficeAssistantController : Controller
    {
        // GET: OfficeAssistant

        private AccountManager _account = new AccountManager();

        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Account");
                ;
            }

            return View();
        }

        public ActionResult SaveCategory()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Account");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = _account.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 2)
                {
                    UserTypeId = 2;
                }
            }
            if (UserTypeId == 2)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Account");
            }
        }

        [HttpPost]
        public ActionResult SaveCategory(Category category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("api/Category/PostCategory", category).Result;
                if (response.IsSuccessStatusCode)
                {
                    string msg = response.Content.ReadAsStringAsync().Result;
                    var records = JsonConvert.DeserializeObject(msg); //  JSON.Net

                    ViewBag.ShowMsg = records;
                }
            }



            return View();
        }

        public ActionResult SaveItem()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Account");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = _account.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 2)
                {
                    UserTypeId = 2;
                }
            }
            if (UserTypeId == 2)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Item/").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<Category>>(res); //  JSON.Net

                        foreach (Category record in records)
                        {
                            var category = (string.Format("Id: {0}, CategoryName: {1}", record.Id, record.CategoryName));
                        }


                        ViewBag.categorys = records;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Account");
            }

        }

        [HttpPost]
        public ActionResult SaveItem(Item item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("api/Item/PostItem", item).Result;

                if (response.IsSuccessStatusCode)
                {
                    string msg = response.Content.ReadAsStringAsync().Result;
                    var records = JsonConvert.DeserializeObject(msg); //  JSON.Net
                    ViewBag.ShowMsg = records;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Item/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<Category>>(res); //  JSON.Net

                    foreach (Category record in records)
                    {
                        var category = (string.Format("Id: {0}, CategoryName: {1}", record.Id, record.CategoryName));
                    }

                    ViewBag.categorys = records;
                }
            }

            return View();
        }

        public ActionResult SaveWhouse()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Account");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = _account.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 2)
                {
                    UserTypeId = 2;
                }
            }
            if (UserTypeId == 2)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Whouse/").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<Employee>>(res); //  JSON.Net

                        foreach (Employee record in records)
                        {
                            var category = (string.Format("Id: {0}, EmployeeName: {1}", record.Id, record.EmployeeName));
                        }

                        ViewBag.employees = records;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Account");
            }

        }
        [HttpPost]
        public ActionResult SaveWhouse(W_h_info wHInfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("api/Whouse/PostWhouse", wHInfo).Result;

                if (response.IsSuccessStatusCode)
                {
                    string msg = response.Content.ReadAsStringAsync().Result;
                    var records = JsonConvert.DeserializeObject(msg); //  JSON.Net
                    ViewBag.ShowMsg = records;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Whouse/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<Employee>>(res); //  JSON.Net

                    foreach (Employee record in records)
                    {
                        var category = (string.Format("Id: {0}, EmployeeName: {1}", record.Id, record.EmployeeName));
                    }

                    ViewBag.employees = records;
                }
            }

            return View();
        }

        public ActionResult SaveEmployee()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Account");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = _account.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 2)
                {
                    UserTypeId = 2;
                }
            }
            if (UserTypeId == 2)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Employee/").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<Disignation>>(res); //  JSON.Net

                        foreach (Disignation record in records)
                        {
                            var category = (string.Format("Id: {0}, DisignationName: {1}", record.Id, record.DisignationName));
                        }

                        ViewBag.designations = records;
                    }
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Category/").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<Gender>>(res); //  JSON.Net

                        foreach (Gender record in records)
                        {
                            var category = (string.Format("Id: {0}, GenderName: {1}", record.Id, record.GenderName));
                        }

                        ViewBag.gender = records;
                    }
                }

                return View();
            }
            else
            {
                return RedirectToAction("Home", "Account");
            }

        }
        [HttpPost]
        public ActionResult SaveEmployee(Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("api/Employee/PostEmployee", employee).Result;

                if (response.IsSuccessStatusCode)
                {
                    string msg = response.Content.ReadAsStringAsync().Result;
                    var records = JsonConvert.DeserializeObject(msg); //  JSON.Net
                    ViewBag.ShowMsg = records;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Employee/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<Disignation>>(res); //  JSON.Net

                    foreach (Disignation record in records)
                    {
                        var category = (string.Format("Id: {0}, DisignationName: {1}", record.Id, record.DisignationName));
                    }

                    ViewBag.designations = records;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Category/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<Gender>>(res); //  JSON.Net

                    foreach (Gender record in records)
                    {
                        var category = (string.Format("Id: {0}, GenderName: {1}", record.Id, record.GenderName));
                    }

                    ViewBag.gender = records;
                }
            }

            return View();
        }

        public ActionResult SaveArea()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Account");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = _account.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 2)
                {
                    UserTypeId = 2;
                }
            }
            if (UserTypeId == 2)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Home", "Account");
            }

        }

        [HttpPost]
        public ActionResult SaveArea(Area area)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("api/Area/PostArea", area).Result;
                if (response.IsSuccessStatusCode)
                {
                    string msg = response.Content.ReadAsStringAsync().Result;
                    var records = JsonConvert.DeserializeObject(msg); //  JSON.Net

                    ViewBag.ShowMsg = records;
                }
            }

            return View();
        }

        public ActionResult SaveShopInfo()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Account");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = _account.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 2)
                {
                    UserTypeId = 2;
                }
            }
            if (UserTypeId == 2)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Shop/").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<Area>>(res); //  JSON.Net

                        foreach (Area record in records)
                        {
                            var category = (string.Format("Id: {0}, AreaName: {1}", record.Id, record.AreaName));
                        }

                        ViewBag.area = records;
                    }
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Whouse/").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<Employee>>(res); //  JSON.Net

                        foreach (Employee record in records)
                        {
                            var category = (string.Format("Id: {0}, EmployeeName: {1}", record.Id, record.EmployeeName));
                        }

                        ViewBag.employees = records;
                    }
                }

                return View();
            }
            else
            {
                return RedirectToAction("Home", "Account");
            }

        }

        [HttpPost]
        public ActionResult SaveShopInfo(ShopInfo shopInfo)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("api/Shop/PostEmployee", shopInfo).Result;

                if (response.IsSuccessStatusCode)
                {
                    string msg = response.Content.ReadAsStringAsync().Result;
                    var records = JsonConvert.DeserializeObject(msg); //  JSON.Net
                    ViewBag.ShowMsg = records;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Shop/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<Area>>(res); //  JSON.Net

                    foreach (Area record in records)
                    {
                        var category = (string.Format("Id: {0}, ShopName: {1}", record.Id, record.AreaName));
                    }

                    ViewBag.area = records;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Whouse/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<Employee>>(res); //  JSON.Net

                    foreach (Employee record in records)
                    {
                        var category = (string.Format("Id: {0}, EmployeeName: {1}", record.Id, record.EmployeeName));
                    }

                    ViewBag.employees = records;
                }
            }

            return View();
        }

    }
}