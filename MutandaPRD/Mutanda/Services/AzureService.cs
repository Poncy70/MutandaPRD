using Microsoft.WindowsAzure.MobileServices;

namespace Mutanda
{
    public class AzureService
    {
        #if DEBUG
            const string applicationURL = @"https://orderentrynet-test.azurewebsites.net";
        #else
            const string applicationURL = @"https://orderentrynet.azurewebsites.net";
        #endif

        private static AzureService instance;
        private MobileServiceClient client;

        public MobileServiceAuthenticationProvider AUTH_PROVIDER = MobileServiceAuthenticationProvider.Google;

        public AzureService()
        {
            client = new MobileServiceClient(applicationURL);
        }

        public static AzureService Instance
        {
            get
            {
                if (instance == null)
                    instance = new AzureService();

                return instance;
            }
        }

        public MobileServiceUser User { get; set; }

        public MobileServiceClient GetMobileServiceClient()
        {
            return client;
        }
    }
}
