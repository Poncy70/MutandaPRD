using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mutanda.Pages.Clienti;
using Mutanda.Models;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.DeviceInfo;
namespace Mutanda.Pages
{
    public class RootPage : MasterDetailPage
    {
        private Dictionary<MenuType, NavigationPage> Pages { get; set; }

        private bool loginShown;

		public RootPage()
        {

            this.MasterBehavior = MasterBehavior.Popover;

            Title = "Order €ntry";
            Pages = new Dictionary<MenuType, NavigationPage>();
            Master = new MenuPage(this);

            //SE NON C'è CONNESSIONE SALTO COMPLETAMENTE L'AUTENTICAZIONE
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if (networkConnection.IsConnected)
            {
                MessagingCenter.Subscribe<LoginPage>(this, "LoginPageShow", (sender) => { loginShown = true; CloseLogin();  });
            }
            
            NavigateAsync(MenuType.Home);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //SE NON C'è CONNESSIONE SALTO COMPLETAMENTE L'AUTENTICAZIONE
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if (networkConnection.IsConnected)
            {
                if ((Device.OS == TargetPlatform.iOS) || (Device.OS == TargetPlatform.Android))
                {
                    if (!loginShown)
                    {
                        // Se la tabella authorization è già popolata non lancio la maschera di login con la scelta del provider ma effettuo la chiamata di
                        // autenticazione di OAuth precompilando il provider di connessione.
                        IDataService dataService;
                        dataService = DependencyService.Get<IDataService>();
                        Authorization authorizationModel = await dataService.GetAuthorizationAsync(); // Lettura tabella in locale
                        if (authorizationModel == null)
                            await Navigation.PushModalAsync(new LoginPage());
                        else
                        {
                            AzureService.Instance.AUTH_PROVIDER = (MobileServiceAuthenticationProvider)authorizationModel.OAuthProvider;
                            CloseLogin();
                        }

                        loginShown = true;
                    }
                }
            }
        }

        private async void CloseLogin()
        {
            var authorization = DependencyService.Get<IAuthorization>();
            Task<MobileServiceUser> userAuthenticate = authorization.DisplayOAuthWebView();

            await userAuthenticate;

            if (userAuthenticate.IsCompleted && !userAuthenticate.IsFaulted)
            {
                if (Navigation.ModalStack.Count > 0)
                    await Navigation.PopModalAsync();

                CloseAuth();
            }
        }

        private async void CloseAuth()
        {
            IDataService dataService;
            dataService = DependencyService.Get<IDataService>();

            // Autorizzazione dell'utente autenticato da parte del backend
            Task<Authorization> authorizationModel = dataService.GetAuthorizationAsync();
            await authorizationModel;
            if (authorizationModel.IsCompleted && !authorizationModel.IsFaulted)
            {
                if (!authorizationModel.Result.AccesDenied)
                {
                    // Leggo i permessi
                    await dataService.SyncPermission(true);
                    await dataService.SyncGEST_ParametriDevice(true);
                }
                else
                {
                    await DisplayAlert("Autorizzazione", "Non sono presenti database associati all'utente selezioanto", "Ok");
                    await Navigation.PushModalAsync(new LoginPage());
                }
            }
            else
            {
                await DisplayAlert("Autorizzazione", "Autorizzazione non effettuata. Riprovare", "Ok");
                await Navigation.PushModalAsync(new LoginPage());
            }
        }

        void SetDetailIfNull(Page page)
        {
            if (Detail == null && page != null)
                Detail = page;
        }

        public async Task NavigateAsync(MenuType id)
        {
            Page newPage;
            if (!Pages.ContainsKey(id))
            {
                switch (id)
                {
                    case MenuType.Home:
                        var page = new CRMNavigationPage(new HomePage());
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;

                    case MenuType.Clienti:
                        page = new CRMNavigationPage(new ClientiRootPage());
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;

                    case MenuType.Ordini:
                        page = new CRMNavigationPage(new OrdiniLista());
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case MenuType.Parametri:
                        page = new CRMNavigationPage(new Parametri());
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case MenuType.Sync:
                        page = new CRMNavigationPage(new SyncRootPages());

                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;

                    case MenuType.Logout:
                        page = new CRMNavigationPage(new SyncRootPages());

                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                }
            }

            newPage = Pages[id];
            if (newPage == null)
                return;

            this.MasterBehavior = MasterBehavior.Popover;
            Detail = newPage;
            IsPresented = true;
        } 
    }

    //private void LogOut()
    //{
    //    AzureService.Instance.GetMobileServiceClient().LogoutAsync();
    //    var authorizationCookie = DependencyService.Get<IAuthorization>();
    //    authorizationCookie.ClearCookies();
    //}

    public enum MenuType
    {
        Home,
        Clienti,
        Ordini,
		NuovoOrdine,
        Sync,
        Parametri,
        Logout
    }

    public class CRMNavigationPage : NavigationPage
    {
        public CRMNavigationPage(Page root) : base(root)
        {
            Init();
        }

        public CRMNavigationPage()
        {
            Init();
        }

        void Init()
        {
            Icon = "";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }

    public class AndroidCRMNavigationPage : NavigationPage
    {
        public AndroidCRMNavigationPage(Page root) : base(root)
        {
            Init();
        }

        public AndroidCRMNavigationPage()
        {
            Init();
        }

        void Init()
        {
        }
    }

    public class HomeMenuItem
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.Home;
        }

        public MenuType MenuType { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public int Id { get; set; }
    }
}
