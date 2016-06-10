using Mutanda.Extensions;
using Mutanda.Models;
using Mutanda.ViewModels.Clienti;
using Mutanda.Views.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mutanda.Views.Clienti
{
    public class ClientiListaView : ModelBoundContentView<ClientiListaViewModel>
    {
        protected ListViewExt listView;
        public ClientiListaView()
        {

            listView = new ListViewExt() { ItemTemplate = new DataTemplate(typeof(ClientiListaItemView)) };
            listView.SetBinding(ListViewExt.ItemsSourceProperty, "ListaClienti", BindingMode.TwoWay);
            listView.SetBinding(ListViewExt.ItemSelezionatoCommandProperty, new Binding("ItemSelezionatoCommand", BindingMode.OneWay));

            listView.HasUnevenRows = true;
            listView.SeparatorColor = Color.FromHex("f99643");
            
            listView.HorizontalOptions = LayoutOptions.StartAndExpand;
            listView.VerticalOptions = LayoutOptions.Center;

            StackLayout stackLayout = new StackLayout()
            {
                Spacing = 0,
                Padding = new Thickness(10, 3),
                Children = { listView }
            };
            
            Content = stackLayout;
        }
    }
}
