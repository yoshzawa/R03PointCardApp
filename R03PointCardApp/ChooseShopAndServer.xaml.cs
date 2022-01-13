using R03PointCardApp.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter.Analytics;
using System.Net.Http;
using System.Collections.Specialized;
using System.Web;

namespace R03PointCardApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseShopAndServer : ContentPage
    {
        private User user = null;
        string TENPO_NO = null;

        public ChooseShopAndServer()
        {
            InitializeComponent();

        }

        internal void setUser(User u)
        {
            user = u;
            title.Text = "User : " + u.DisplayName;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Analytics.TrackEvent("Button_Clicked");
            QRScanPage qr = new QRScanPage();
            Navigation.PushAsync(qr );
            qr.setParent(this);

        }

        internal void setTENPO_NO(string text)
        {
            TENPO_NO = text;
        }

        private object QRScanPage()
        {
            throw new NotImplementedException();
        }



        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            String url = "http://192.168.54.190:8080/R03JsonKadai00/";
            await DisplayAlert("URL" ,url + "getPoint?TENPO_ID=" + TENPO_NO + "&USER_ID=" + user.Id ,"OK");
            HttpClient client = new HttpClient();
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri+ "getPoint?TENPO_ID=" + TENPO_NO + "&USER_ID=" + user.Id);
            string s = await response.Content.ReadAsStringAsync();
            await DisplayAlert("response", s, "OK");


        }
    }
}