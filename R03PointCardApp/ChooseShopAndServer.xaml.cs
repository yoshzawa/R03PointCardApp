using R03PointCardApp.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter.Analytics;


namespace R03PointCardApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseShopAndServer : ContentPage
    {
        private User user = null;
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

            Navigation.PushAsync(new QRScanPage());

        }

        public static implicit operator ChooseShopAndServer(NavigationPage v)
        {
            throw new NotImplementedException();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}