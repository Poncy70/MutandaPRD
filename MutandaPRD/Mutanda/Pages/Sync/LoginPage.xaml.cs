using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Mutanda.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void OnLoginGoogle(object sender, EventArgs args)
        {
            AzureService.Instance.AUTH_PROVIDER = Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Google;
            MessagingCenter.Send(this, "LoginPageShow");
        }

        public void OnLoginFacebook(object sender, EventArgs args)
        {
            AzureService.Instance.AUTH_PROVIDER = Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Facebook;
            MessagingCenter.Send(this, "LoginPageShow");
        }

        public void OnLoginLive(object sender, EventArgs args)
        {
            AzureService.Instance.AUTH_PROVIDER = Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.MicrosoftAccount;
            MessagingCenter.Send(this, "LoginPageShow");
        }
    }
}
