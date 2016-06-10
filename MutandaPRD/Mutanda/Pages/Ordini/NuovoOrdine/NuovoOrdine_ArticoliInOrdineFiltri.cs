using Mutanda.Extensions;
using Mutanda.ViewModels;
using Xamarin.Forms;
using Mutanda.Models;
using Mutanda.CustomControl;

namespace Mutanda.Pages
{
    public class NuovoOrdine_ArticoliInOrdineFiltri : ContentPage
    {
        public NuovoOrdine_ArticoliInOrdineFiltri(NuovoOrdineViewModel _NuovoOrdineViewModel)
        {
            this.BindingContext = _NuovoOrdineViewModel;
            
			ListView FamiglieList = new ListView(ListViewCachingStrategy.RetainElement);
            FamiglieList.HasUnevenRows = true;
            FamiglieList.SeparatorVisibility = SeparatorVisibility.None;
            FamiglieList.ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate));
            FamiglieList.SetBinding<NuovoOrdineViewModel>(ListView.ItemsSourceProperty, x=>x.ListaFamigliePerArticoliInOrdine, BindingMode.OneWay);
            FamiglieList.SetBinding<NuovoOrdineViewModel>(ListView.IsVisibleProperty, x => x.FamiglieVisibilePerSelezioneArticoli, BindingMode.OneWay);
            FamiglieList.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                var o = (SelezioneListView)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null; //de-select
            };

            //CLASSI
			ListView ClassiList = new ListView(ListViewCachingStrategy.RetainElement);
            ClassiList.HasUnevenRows = true;
            ClassiList.SeparatorVisibility = SeparatorVisibility.None;
            ClassiList.ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate));
            ClassiList.SetBinding<NuovoOrdineViewModel>(ListView.ItemsSourceProperty, x => x.ListaClassiPerArticoliInOrdine, BindingMode.OneWay);
            ClassiList.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                var o = (SelezioneListView)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null;
            };

            //NATURE
			ListView NatureList = new ListView(ListViewCachingStrategy.RetainElement);
            NatureList.HasUnevenRows = true;
            NatureList.SeparatorVisibility = SeparatorVisibility.None;
            NatureList.ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate));
            NatureList.SetBinding<NuovoOrdineViewModel>(ListView.ItemsSourceProperty, x=>x.ListaNaturePerArticoliInOrdine, BindingMode.OneWay);
            NatureList.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                var o = (SelezioneListView)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null; //de-select
            };
            
            Button btnAzzeraFiltri = new Button();
            btnAzzeraFiltri.Text = "Deselez. Tutti";
            btnAzzeraFiltri.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));
            btnAzzeraFiltri.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_AnnullaFiltriFamigliaClasseCommand, BindingMode.OneWay);
            btnAzzeraFiltri.BackgroundColor = Color.FromHex("f99643");
            btnAzzeraFiltri.TextColor = Color.White;

            Button btnSelezionaTutti = new Button();
            btnSelezionaTutti.Text = "Selez. Tutti";
            btnSelezionaTutti.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));
            btnSelezionaTutti.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_ReimpostaFiltriFamigliaClasseCommand, BindingMode.OneWay);
            btnSelezionaTutti.BackgroundColor = Color.FromHex("f99643");
            btnSelezionaTutti.TextColor = Color.White;

            Button BtnRitorna = new Button();
            BtnRitorna.Text = "Ritorna";
            BtnRitorna.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));
            BtnRitorna.Clicked += BtnRitorna_Clicked;
            BtnRitorna.BackgroundColor = Color.FromHex("f99643");
            BtnRitorna.TextColor = Color.White;

            StackLayout slFiltri = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(50),
                Children =
                {
                    new ContentView() {Margin = new Thickness(330,0), Content = ClassiList },
                    new ContentView() {Margin = new Thickness(330,0), Content = FamiglieList },
                    new ContentView() {Margin = new Thickness(330,0), Content = NatureList },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = { btnAzzeraFiltri, btnSelezionaTutti, BtnRitorna }
                    }
                }
            };
            Content = slFiltri;
        }

        private void BtnRitorna_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {

        }
        
        protected override void OnDisappearing()
        {
        }

    }
}
