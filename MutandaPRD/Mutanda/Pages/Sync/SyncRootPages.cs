using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using Mutanda.Models;
using Mutanda.Services;
using Newtonsoft.Json;

namespace Mutanda.Pages
{
    public class SyncRootPages : ContentPage
    {
        private bool loginShown;
        private ActivityIndicator leadListActivityIndicator = new ActivityIndicator();
        private Button btnSync;
        private Button btnPush;
        private Label lblStatus;
        private bool IsBusy=false;

        public object JSONConvert { get; private set; }

        public SyncRootPages()
        {
            btnSync = new Button
            {
                Text = "Sincronizzazione Completa",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.FromHex("f99643"),
                TextColor = Color.White
            };

            btnSync.Clicked += OnButtonSyncClicked;

            btnPush = new Button
            {
                Text = "Invia Ordini",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.FromHex("f99643"),
                TextColor = Color.White
            };

            lblStatus = new Label { };

            btnPush.Clicked += OnPushButtonClicked;

            StackLayout layout = new StackLayout();
            layout.Children.Add(leadListActivityIndicator);
            layout.Children.Add(btnSync);
            layout.Children.Add(btnPush);
            layout.Children.Add(lblStatus);

            Content = layout;

            MessagingCenter.Subscribe<DataService,string>(this, "REFRESHLBLSTATUS", (param,stringtoshow) =>
            {
                lblStatus.Text = stringtoshow;
            });
        }

        private async void OnButtonSyncClicked(object sender, EventArgs e)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            leadListActivityIndicator.IsVisible = true;
            leadListActivityIndicator.IsEnabled = true;
            leadListActivityIndicator.IsRunning = true;

            IDataService dataService;
            dataService = DependencyService.Get<IDataService>();

            await dataService.SyncAll(true);
            leadListActivityIndicator.IsVisible = false;
            leadListActivityIndicator.IsEnabled = false;
            leadListActivityIndicator.IsRunning = false;

            //Caso: eseguo sync e quindi riapro la pagina ordini - devo forzare refresh list ordini sincronizzato o meno
            MessagingCenter.Send(this, "REFRESHLISTAORDINI");
            IsBusy = false;
        }

        private async void OnPushButtonClicked(object sender, EventArgs e)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            IDataService dataService;
            dataService = DependencyService.Get<IDataService>();
            //await dataService.PushAllChanged();

            await dataService.PushAllGEST_Ordini_Async();

            //Caso: eseguo sync e quindi riapro la pagina ordini - devo forzare refresh list ordini sincronizzato o meno
            MessagingCenter.Send(this, "REFRESHLISTAORDINI");

            IsBusy = false;
        }
    }
}
