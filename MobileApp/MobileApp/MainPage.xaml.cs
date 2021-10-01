using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BtnUpdate.Clicked += BtnUpdate_Clicked;
        }

        private void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            SingleInsert(new Setting
            {
                Id = 1, DistanceInMeters = "250 meters", ContinuousAlertDistance = "50 meters",
                IsContinuousAlert = false
            });
        }

        private static async void SingleInsert(Setting item)
        {
            try
            {
                await using var context = new AppDbContext();
                await context.SingleInsertAsync(item);
                item.ContinuousAlertDistance = "sabari";
                await context.SingleUpdateAsync(item);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }
    }
}