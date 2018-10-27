using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using FMCGWebApp.Manager;
using FMCGWebApp.Models;
using FMCGWebApp.ViewModels;
using Newtonsoft.Json;

namespace FMCGWebApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult AdminLogin()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                ;
            }
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AdminLogin(LoginInfo login)
        {
            string email = login.Email;
            string password = login.Password;
            try
            {
                List<LoginInfo> status = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Login?email=" + email + "&password="+password+"").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<LoginInfo>>(res); //  JSON.Net
                        status = records;
                    }
                }

                if (status.Count != 0)
                {
                    if (status.Count() > 0)
                    {
                        var name = status[0].EmployeeName;
                        Session["Admin"] = "Admin";
                        Session["User1"] = name;
                        Session["user"] = status[0].Id;
                        Session["status"] = true;
                        Session["userTypeId"] = status[0].UserTypeId;
                        if (status[0].UserTypeId == 1)
                        {
                            return RedirectToAction("../Admin/Index");

                        }

                    }

                }
                else
                {
                    ViewBag.Msg = "User name or passwoer mismatch!";
                }
            }
            catch (Exception exception)
            {
                ViewBag.Msg = exception.Message;
            }
            return View();
        }

        public ActionResult AssistantLogin()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                ;
            }
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AssistantLogin(LoginInfo login)
        {
            string email = login.Email;
            string password = login.Password;
            try
            {
               
                List<LoginInfo> status = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/AssLogin?email=" + email + "&password=" + password + "").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<LoginInfo>>(res); //  JSON.Net
                        status = records;
                    }
                }

                if (status.Count != 0)
                {
                    if (status.Count() > 0)
                    {
                        var name = status[0].EmployeeName;
                        Session["Admin"] = "Admin";
                        Session["User1"] = name;
                        Session["user"] = status[0].Id;
                        Session["status"] = true;
                        if (status[0].UserTypeId == 2)
                        {
                            return RedirectToAction("../OfficeAssistant/Index");

                        }

                    }

                }
                else
                {
                    ViewBag.Msg = "User name or passwoer mismatch!";
                }
            }
            catch (Exception exception)
            {
                ViewBag.Msg = exception.Message;
            }
            return View();
        }

        public ActionResult WhInchargeLogin()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                ;
            }
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult WhInchargeLogin(LoginInfo login)
        {
            string email = login.Email;
            string password = login.Password;
            try
            {
               // List<LoginInfo> status = _account.WhInchargeLogin(login);
                List<LoginInfo> status = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/WhiLogin?email=" + email + "&password=" + password + "").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<LoginInfo>>(res); //  JSON.Net
                        status = records;
                    }
                }
                if (status.Count != 0)
                {
                    if (status.Count() > 0)
                    {
                        var name = status[0].EmployeeName;
                        Session["Admin"] = "Admin";
                        Session["User1"] = name;
                        Session["user"] = status[0].Id;
                        Session["status"] = true;
                        if (status[0].UserTypeId == 3)
                        {
                            return RedirectToAction("../WhIncharge/Index");

                        }

                    }

                }
                else
                {
                    ViewBag.Msg = "User name or passwoer mismatch!";
                }
            }
            catch (Exception exception)
            {
                ViewBag.Msg = exception.Message;
            }
            return View();
        }

        public ActionResult SellForceLogin()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                ;
            }
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SellForceLogin(LoginInfo login)
        {
            string email = login.Email;
            string password = login.Password;
            try
            {
               // List<LoginInfo> status = _account.SellForceLogin(login);
                List<LoginInfo> status = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/SfLogin?email=" + email + "&password=" + password + "").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<LoginInfo>>(res); //  JSON.Net
                        status = records;
                    }
                }

                if (status.Count != 0)
                {
                    if (status.Count() > 0)
                    {
                        var name = status[0].EmployeeName;
                        Session["Admin"] = "Admin";
                        Session["User1"] = name;
                        Session["user"] = status[0].Id;
                        Session["status"] = true;
                        if (status[0].UserTypeId == 4)
                        {
                            return RedirectToAction("../SellForce/Index");

                        }

                    }

                }
                else
                {
                    ViewBag.Msg = "User name or passwoer mismatch!";
                }
            }
            catch (Exception exception)
            {
                ViewBag.Msg = exception.Message;
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Home");
        }

    }
}