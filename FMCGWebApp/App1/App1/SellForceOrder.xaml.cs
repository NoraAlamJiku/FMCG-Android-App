using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellForceOrder : ContentPage
    {
		public SellForceOrder ()
		{
			InitializeComponent ();
		    GetAllArea();
		    GetAllShop();
		    GetAllCategory();
		    GetAllItem();

		}
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            var shop = (Picker)sender;
            int selectedShop = shop.SelectedIndex;

            var category = (Picker)sender;
            int selectedCategory = shop.SelectedIndex;

            var item = (Picker)sender;
            int selectedItem = shop.SelectedIndex;

            //if (selectedIndex != -1)
            //{
            //    monkeyNameLabel.Text = picker.Items[selectedIndex];
            //}
        }
        private async void AddButton_OnClicked(Object sender, EventArgs e)
        {
            int qun = Convert.ToInt32(Quantity.Text);
            if (qun != 0)
            {
                try
                {
                    Item item = (Item)ItemListView.SelectedItem;
                    Category category = (Category)CategoryListView.SelectedItem;
                    Shop shop = (Shop)ShopListView.SelectedItem;
                    Area area = (Area)AreaListView.SelectedItem;
                    SendSellOrder sellOrder = new SendSellOrder
                    {
                        AreaId = area.Id,
                        ShopId = shop.Id,
                        CategoryId = category.Id,
                        ItemId = item.Id,
                        Quantity = Convert.ToInt32(Quantity.Text),
                        EntryDate = DateTime.Now,
                        Status = "Ordered",
                        EmployeeId = 2

                    };

                    using (var client = new HttpClient())
                    {
                        var jsonRequest = JsonConvert.SerializeObject(sellOrder);
                        var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");
                        client.BaseAddress = new Uri("http://fmcg.somee.com");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage response = client.PostAsync("api/SendSellOrder/PostSellOrder", content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string msg = response.Content.ReadAsStringAsync().Result;
                            var records = JsonConvert.DeserializeObject(msg); //  JSON.Net
                            DisplayAlert("Congratulations", "'" + records + "'", "OK");
                            ItemListView.SelectedItem = 0;
                            Quantity.Text = null;

                        }
                    }
                }
                catch
                {
                    DisplayAlert("Alert!", "Data Save Failed", "OK");
                }
            }
            else
            {
                DisplayAlert("Alert!", "Check all required fild", "OK");
            }
           
        }

        public async void GetAllArea()
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


                    AreaListView.ItemsSource = records;
                   // EmployeeView.ItemsSource = records;
                  
                }
            }
        }

        public async void GetAllShop()
        {
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


                    ShopListView.ItemsSource = records;
                    // EmployeeView.ItemsSource = records;

                }
            }
        }

        public async void GetAllCategory()
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


                    CategoryListView.ItemsSource = records;
                    // EmployeeView.ItemsSource = records;

                }
            }
        }

        public async void GetAllItem()
        {
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


                    ItemListView.ItemsSource = records;
                    // EmployeeView.ItemsSource = records;

                }
            }
        }


    }
}