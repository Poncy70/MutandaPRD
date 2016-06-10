using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

[assembly: Dependency(typeof(Mutanda.Authorization))]
namespace Mutanda
{
    public class Authorization : IAuthorization
    {
        public async Task<MobileServiceUser> DisplayOAuthWebView()
        {
            try
            {
                MobileServiceClient client = AzureService.Instance.GetMobileServiceClient();
                return await client.LoginAsync(Forms.Context, AzureService.Instance.AUTH_PROVIDER);


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ClearCookies()
        {
            global::Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
        }
    }
}