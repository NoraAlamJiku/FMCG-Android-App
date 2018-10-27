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
    public class WhInchargeController : Controller
    {
        // GET: WhIncharge
    
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

        public ActionResult Stockout()
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
                if (loginInfo.UserTypeId == 3)
                {
                    UserTypeId = 3;
                }
            }
            if (UserTypeId == 3)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fmcg.somee.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/StockOut/").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;

                        var records = JsonConvert.DeserializeObject<List<Stockout>>(res); //  JSON.Net

                        foreach (Stockout record in records)
                        {
                            var category = (string.Format("Id: {0}", record.Id));
                        }

                        ViewBag.orderId = records;
                    }
                }
                //ViewBag.orderId = _stockOut.SellOrderList();

                return View();
            }
            else
            {
                return RedirectToAction("Home", "Account");
            }

        }
        [HttpPost]
        public ActionResult Stockout(Stockout stockout)
         {
            stockout.Status = "Ok";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("api/StockOut/PostStockout", stockout).Result;

                if (response.IsSuccessStatusCode)
                {
                    string msg = response.Content.ReadAsStringAsync().Result;
                    var records = JsonConvert.DeserializeObject(msg); //  JSON.Net
                    ViewBag.msg = records;
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/StockOut/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<Stockout>>(res); //  JSON.Net

                    foreach (Stockout record in records)
                    {
                        var category = (string.Format("Id: {0}", record.Id));
                    }

                    ViewBag.orderId = records;
                }
            }

            return View();
        }

        public JsonResult GetSelOrderInfoById(int departmentId)
        {
           // IEnumerable<SellOrderInfo> sellorder = _stockOut.GetAllSellOrder(departmentId);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/SellOrder/" + departmentId + "").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<SellOrderInfo>>(res); //  JSON.Net

                    foreach (SellOrderInfo record in records)
                    {
                        var category =
                            (string.Format(
                                "Id: {0}, CategoryName: {1}, ItemName: {2}, Quentity: {3}, ShopName: {4}, AreaName: {5}",
                                record.Id, record.CategoryName, record.ItemName, record.Quentity,
                                record.ShopName, record.AreaName));
                    }
                    return Json(records.ToList(), JsonRequestBehavior.AllowGet);

                }
            }
        }

        public ActionResult SaveStockin()
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
                if (loginInfo.UserTypeId == 3)
                {
                    UserTypeId = 3;
                }
            }
            if (UserTypeId == 3)
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
                            var category = (string.Format("Id: {0}, Id: {1}", record.Id, record.CategoryName));
                        }

                        ViewBag.categorys = records;
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
                            var category = (string.Format("Id: {0}, Id: {1}", record.Id, record.ItemName));
                        }

                        ViewBag.item = records;
                    }
                }

               // ViewBag.categorys = _item.GetAllCategory();
               // ViewBag.item = _sellOrder.GetAllItem();

                return View();
            }
            else
            {
                return RedirectToAction("Home", "Account");
            }

        }

        [HttpPost]
        public ActionResult SaveStockin(StockIn stockIn)
        {
            stockIn.EntryDate = DateTime.Now;
            stockIn.EmployeeId = (int)Session["user"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("api/Stockin/PostStockin", stockIn).Result;

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
                        var category = (string.Format("Id: {0}, Id: {1}", record.Id, record.CategoryName));
                    }

                    ViewBag.categorys = records;
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
                        var category = (string.Format("Id: {0}, Id: {1}", record.Id, record.ItemName));
                    }

                    ViewBag.item = records;
                }
            }


            return View();
        }

        public ActionResult CompareOrderandStock()
        {
          //  List<CompareOrderandStock> CompareOrderandStock = _report.GetNeedandStockInfo();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://fmcg.somee.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/SellOrder/").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    var records = JsonConvert.DeserializeObject<List<CompareOrderandStock>>(res); //  JSON.Net

                    foreach (CompareOrderandStock record in records)
                    {
                        var category =
                            (string.Format("Id: {0}, CategoryName: {1}, ItemName: {2}, Need: {3}, Stock: {4}", record.Id, record.CategoryName,
                                record.ItemName, record.Need, record.Stock));
                    }
                    
                   ViewBag.list = records;
                }
            }

            return View();
        }

    }
}