using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace Mutanda.Extensions
{
    public class ListViewExt : ListView
    {
        //public static BindableProperty ItemSelezionatoCommandProperty = BindableProperty.Create<ListViewExt, ICommand>(x => x.ItemSelezionatoCommand, null);
        public static BindableProperty ItemSelezionatoCommandProperty = BindableProperty.Create(nameof(ItemSelezionatoCommand),typeof(ICommand),typeof(ListViewExt), null);



//        BindableProperty.Create<BindablePicker, IList>(p => p.ItemsSource, null,
//    propertyChanged: OnItemsSourcePropertyChanged);

//with

//BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(BindablePicker), null,
//    propertyChanged: OnItemsSourcePropertyChanged);


        public ICommand ItemSelezionatoCommand
        {
            get { return (ICommand)GetValue(ItemSelezionatoCommandProperty); }
            set { SetValue(ItemSelezionatoCommandProperty, value); }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem;
            if (item != null && ItemSelezionatoCommand != null && ItemSelezionatoCommand.CanExecute(item))
            {
                ItemSelezionatoCommand.Execute(e.SelectedItem);
            }

            SelectedItem = null;
        }

        public ListViewExt(): base()
        {
            
            ItemSelected += OnItemSelected;
        }

        public ListViewExt(ListViewCachingStrategy _Type):base (_Type)
        {
            ItemSelected += OnItemSelected;
        }
    }
}
