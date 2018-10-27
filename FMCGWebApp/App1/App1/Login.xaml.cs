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
    public partial class Login : ContentPage
    {
		public Login ()
		{
			InitializeComponent ();
		}
        private async void AddButton_OnClicked(Login login)
        {
            var email = Username.Text;
            var password = Password.Text;
            if (email == null)
            {
                DisplayAlert("Alert", "Please enter Email", "ok");
            }
            else if (password == null)
            {
                DisplayAlert("Alert", "Please enter password", "ok");
            }
            else
            {

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

                        var records = JsonConvert.DeserializeObject<List<Login>>(res); //  JSON.Net
                        if (records.Count > 0)
                        {
                            Navigation.PushModalAsync(new SellForceOrder());
                        }
                        else
                        {
                            DisplayAlert("Sorry!", "Your Email and Password Mismatch", "OK");
                            Navigation.PushModalAsync(new Login());
                        }
                    }




                }

            }
        }

    }
}