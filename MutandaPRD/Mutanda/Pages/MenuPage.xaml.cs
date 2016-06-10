using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Mutanda.Pages
{
    public partial class MenuPage : ContentPage
    {
        RootPage root;
        List<HomeMenuItem> menuItems;
        public MenuPage(RootPage root)
        {
            this.root = root;
            InitializeComponent();
            Title = "Menu";

            ListViewMenu.ItemsSource = menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem { Title = "Home", MenuType = MenuType.Home },
                new HomeMenuItem { Title = "Clienti", MenuType = MenuType.Clienti },
                new HomeMenuItem { Title = "Ordini", MenuType = MenuType.Ordini },
                new HomeMenuItem { Title = "Aggiorna e Invia", MenuType = MenuType.Sync },
                //new HomeMenuItem { Title = "Parametri", MenuType = MenuType.Parametri },
                //new HomeMenuItem { Title = "Logout", MenuType = MenuType.Logout }
            };

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (ListViewMenu.SelectedItem == null)
                    return;
                
                await this.root.NavigateAsync(((HomeMenuItem)e.SelectedItem).MenuType);
                this.root.IsPresented = false;
            };
        }
    }
}
