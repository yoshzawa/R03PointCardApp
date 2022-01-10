using R03PointCardApp.Identity;
using System;
using Xamarin.Forms;



namespace R03PointCardApp
{
    public partial class MainPage : ContentPage
    {
        private readonly IMicrosoftAuthService microsoftAuthService;
        private bool isLoading;
        private User user;
        public MainPage()
        {
            InitializeComponent();
            microsoftAuthService = new MicrosoftAuthService();

        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            this.microsoftAuthService.Initialize();
            user = await this.microsoftAuthService.OnSignInAsync();
            message.Text = user.Id;


        }

    }
}
