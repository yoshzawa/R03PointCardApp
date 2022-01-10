﻿using R03PointCardApp.Identity;
using System;
using Xamarin.Forms;



namespace R03PointCardApp
{
    public partial class MainPage : ContentPage
    {
        private readonly IMicrosoftAuthService microsoftAuthService;
        private User user;
        public MainPage()
        {
            InitializeComponent();
            microsoftAuthService = new MicrosoftAuthService();

        }

        public IMicrosoftAuthService MicrosoftAuthService => microsoftAuthService;

        public User User { get => user; set => user = value; }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            this.MicrosoftAuthService.Initialize();
            User = await this.MicrosoftAuthService.OnSignInAsync();
            message.Text = User.Id;
            logoutButton.IsVisible = true;
        }

        private async void Button_Clicked_CLOSE(object sender, EventArgs e)
        {
            await this.MicrosoftAuthService.OnSignOutAsync();
            user = null;
            message.Text = "Microsoftアカウントでログインしてください";
            logoutButton.IsVisible = false;
        }

    }
}
