using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Mutanda.Pages;

namespace Mutanda
{
    public class App : Application
    {

        //public static string  m_IdOrdineDiPassagio;
        //public static string m_RagioneSociale= "";

        public App()
        {
            var pageStyle = new Style(typeof(Page))
            {
                Setters = {new Setter { Property = Page.BackgroundColorProperty,   Value = Color.White } }
            };

            Resources = new ResourceDictionary();
            Resources.Add("PageStyle", pageStyle);

            // The root page of your application
			MainPage = new RootPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
