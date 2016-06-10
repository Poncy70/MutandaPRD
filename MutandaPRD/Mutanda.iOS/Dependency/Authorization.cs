using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Mutanda.Authorization;
using UIKit;
using Foundation;
using Xamarin;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Authorization))]
namespace Mutanda.Authorization
{
	public class Authorization: IAuthorization
	{
		public async Task<MobileServiceUser> DisplayOAuthWebView()
		{
			try
			{
				var window = UIKit.UIApplication.SharedApplication.KeyWindow;
				var root = window.RootViewController;
				if(root != null)
				{
					var current = root;
					while(current.PresentedViewController != null)
					{
						current = current.PresentedViewController;
					}

					return await AzureService.Instance.GetMobileServiceClient().LoginAsync(current, MobileServiceAuthenticationProvider.Google);
				}
			}
			catch(Exception e)
			{
//				InsightsManager.Report(e);
			}

			return null;
		}


		public void ClearCookies()
		{
			var store = NSHttpCookieStorage.SharedStorage;
			var cookies = store.Cookies;

			foreach(var c in cookies)
			{
				store.DeleteCookie(c);
			}
		}
	}
}