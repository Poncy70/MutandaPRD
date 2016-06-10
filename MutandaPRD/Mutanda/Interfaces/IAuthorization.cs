using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace Mutanda
{
    public interface IAuthorization
    {
        void ClearCookies();
        Task<MobileServiceUser> DisplayOAuthWebView();
    }
}
