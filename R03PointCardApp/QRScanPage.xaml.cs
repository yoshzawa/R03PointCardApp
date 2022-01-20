﻿using System;
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
    public partial class QRScanPage : ContentPage
    {
        private ChooseShopAndServer parent= null;

        public QRScanPage()
        {
            InitializeComponent();
        }
        void Handle_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                zxing.IsAnalyzing = false;  //読み取り停止
                                            // await DisplayAlert("通知", "次の値を読み取りました：" + result.Text, "OK");
                Analytics.TrackEvent("次の値を読み取りました", new Dictionary<string, string> {
    { "result", result.Text }
});
                if (parent != null)
                {
                    parent.setTENPO_NO(result.Text);
                }
                await Navigation.PopAsync();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
        }

        internal void setParent(ChooseShopAndServer chooseShopAndServer)
        {
            parent = chooseShopAndServer;
        }
        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            base.OnDisappearing();
        }
    }
}