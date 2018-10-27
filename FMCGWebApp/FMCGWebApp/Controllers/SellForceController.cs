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
    public class SellForceController : Controller
    {
        // GET: SellForce

        private SellOrderManager _sellOrder = new SellOrderManager();
        private ShopInfoManager _shopInfo = new ShopInfoManager();
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

        public ActionResult SendSellOrder()
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
                if (loginInfo.UserTypeId == 4)
                {
                    UserTypeId = 4;
                }
            }
            if (UserTypeId == 4)
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
                    HttpResponseMessage response = client.GetAsync("api/SendSellOrder/").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<Shop>>(res); //  JSON.Net

                        foreach (Shop record in records)
                        {
                            var category = (string.Format("Id: {0}, ShopName: {1}", record.Id, record.ShopName));
                        }

                        ViewBag.shop = records;
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

                        ViewBag.category = records;
                    }
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Stockin/").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<Item>>(res); //  JSON.Net

                        foreach (Item record in records)
                        {
                            var category = (string.Format("Id: {0}, ItemName: {1}", record.Id, record.ItemName));
                        }

                        ViewBag.item = records;
                    }
                }

                //ViewBag.area = _shopInfo.GetAreaList();
                //ViewBag.shop = _sellOrder.GetAllShop();
                //ViewBag.category = _sellOrder.GetAllCategory();
                //ViewBag.item = _sellOrder.GetAllItem();

                return View();
            }
            else
            {
                return RedirectToAction("Home", "Account");
            }

        }
        [HttpPost]
        public ActionResult SendSellOrder(SellOrder sellOrder)
        {
            sellOrder.EntryDate = DateTime.Now;
            sellOrder.Status = "Ordered";
            sellOrder.EmployeeId = (int)Session["user"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("api/SendSellOrder/PostSellOrder", sellOrder).Result;
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
                HttpResponseMessage response = client.GetAsync("api/SendSellOrder/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<Shop>>(res); //  JSON.Net

                    foreach (Shop record in records)
                    {
                        var category = (string.Format("Id: {0}, ShopName: {1}", record.Id, record.ShopName));
                    }

                    ViewBag.shop = records;
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

                    ViewBag.category = records;
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Stockin/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<Item>>(res); //  JSON.Net

                    foreach (Item record in records)
                    {
                        var category = (string.Format("Id: {0}, ItemName: {1}", record.Id, record.ItemName));
                    }

                    ViewBag.item = records;
                }
            }

           // ViewBag.area = _shopInfo.GetAreaList();
          //  ViewBag.shop = _sellOrder.GetAllShop();
          //  ViewBag.category = _sellOrder.GetAllCategory();
          //  ViewBag.item = _sellOrder.GetAllItem();
            return View();
        }

        public JsonResult InsertCustomers(List<SellOrder> sellOrders)
        {
            int flag = 0;
            string message = string.Empty;
            foreach (SellOrder customer in sellOrders)
            {
                customer.EntryDate = DateTime.Now;
                customer.Status = "Ordered";
                customer.EmployeeId = (int)Session["user"];
                message = _sellOrder.SendSellOrder(customer);
                flag = 1;


            }
            if (flag == 1)
            {
                return Json(message);
            }
            return Json(message);
        }

    }
}