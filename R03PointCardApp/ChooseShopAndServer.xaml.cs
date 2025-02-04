﻿using R03PointCardApp.Identity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter.Analytics;
using System.Net.Http;
using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            labelUser.Text = "User : " + u.DisplayName;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Analytics.TrackEvent("Button_Clicked");
            QRScanPage qr = new QRScanPage();
            Navigation.PushAsync(qr);
            qr.setParent(this);

        }

        internal void setTENPO_NO(string text)
        {
            TENPO_NO = text;
            labelTenpo.Text = "Tenpo : " + text;

        }





        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            {
                string[] urls = {
                    "http://192.168.54.190:8080/R03JsonKadai00/",
                    "http://192.168.54.190:8080/R03JsonKadai01/",
                    "http://192.168.54.190:8080/R03JsonKadai02/",
                    "http://192.168.54.190:8080/R03JsonKadai03/",
                    "http://192.168.54.190:8080/R03JsonKadai04/",
                    "http://192.168.54.190:8080/R03JsonKadai05/",
                    "http://192.168.54.190:8080/R03JsonKadai06/",
                    "http://192.168.54.190:8080/R03JsonKadai07/",
                    "http://192.168.54.190:8080/R03JsonKadai08/",
                    "http://192.168.54.190:8080/R03JsonKadai09/",
                    "http://192.168.54.190:8080/R03JsonKadai10/",
                    "http://192.168.54.190:8080/R03JsonKadai11/",
                    "http://192.168.54.190:8080/R03JsonKadai12/",
                    "http://192.168.54.190:8080/R03JsonKadai13/",
                };
                int selectedIndex = picker.SelectedIndex;
                if (selectedIndex < 0)
                {
                    selectedIndex = 0;
                    picker.SelectedIndex = 0;
                }
                String url = urls[selectedIndex];
                await DisplayAlert("URL", url + "getPoint?TENPO_ID=" + TENPO_NO + "&USER_ID=" + user.Id, "OK");
                HttpClient client = new HttpClient();
                Uri uri = new Uri(string.Format(url, string.Empty));
                HttpResponseMessage response = await client.GetAsync(uri + "getPoint?TENPO_ID=" + TENPO_NO + "&USER_ID=" + user.Id);
                if (response.IsSuccessStatusCode)
                {
                    string s = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("response", s, "OK");

                    StackLayout sl = new StackLayout();
                    sl.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    sl.VerticalOptions = LayoutOptions.CenterAndExpand;
                    sl.BackgroundColor = Color.Azure;
                    //                    result = sl;

                    JsonPoint sp = JsonPoint.getPoint(s);

                    Label pointLabel = new Label();
                    pointLabel.Text = "ポイントは" + sp.point + "ポイントです";

                    result.Children.Add(pointLabel);

                    await DisplayAlert("URL", url + "getTicketList?TENPO_ID=" + TENPO_NO + "&USER_ID=" + user.Id, "OK");
                    response = await client.GetAsync(uri + "getTicketList?TENPO_ID=" + TENPO_NO + "&USER_ID=" + user.Id);
                    if (response.IsSuccessStatusCode)
                    {
                        string s2 = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("response", s2, "OK");
                        JsonTicketList ticketList = JsonTicketList.getTicketList(s2);
                        await DisplayAlert("progress", "job1", "OK");

                        List<JsonTicket> r = ticketList.ticketArray;
                        await DisplayAlert("progress", "job2", "OK");

                        foreach (JsonTicket jt in r)
                        {
                            Label ticketLabel = new Label();
                            ticketLabel.Text = jt.OptName +"を"+ jt.POINT + "ポイントで交換できます";
                            await DisplayAlert("progress", "job3", "OK");
                            result.Children.Add(ticketLabel);
                        }
                    }
                    else
                    {
                        await DisplayAlert("getTicketListを唱えた！しかし、何も起きなかった", "status=" + response.StatusCode.ToString(), "OK");
                    }
                }
                else
                {
                    await DisplayAlert("へんじがない。ただのしかばねのようだ", "status=" + response.StatusCode.ToString(), "OK");
                }


            }
        }
    }

    internal class JsonPoint
    {
        public int point
        {
            get;
            set;
        }
        internal static JsonPoint getPoint(string s)
        {
            return JsonConvert.DeserializeObject<JsonPoint>(s);

        }
    }
    public class JsonTicketList
    {
        public List<JsonTicket> ticketArray { get; set; }

        internal static JsonTicketList getTicketList(string s)
        {
            return JsonConvert.DeserializeObject<JsonTicketList>(s);
        }
    }

    public class JsonTicket
    {
        public int ID { get; set; }
        public string OptName { get; set; }
        public int POINT { get; set; }
    }



}