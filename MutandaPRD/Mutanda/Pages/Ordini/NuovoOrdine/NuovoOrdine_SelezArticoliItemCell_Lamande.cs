using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutanda.ViewModels;
using Xamarin.Forms;
using Mutanda.Models;
using Mutanda.Converters;

namespace Mutanda.Pages
{
    public class NuovoOrdine_SelezArticoliItemCell_Lamande : ViewCell
    {
        private readonly NuovoOrdineViewModel _NuovoOrdineViewModel;

        public NuovoOrdine_SelezArticoliItemCell_Lamande(NuovoOrdineViewModel _ViewModel)
        {
             _NuovoOrdineViewModel = _ViewModel;

            //var fsCodArt = new FormattedString();
            //fsCodArt.Spans.Add(new Span { ForegroundColor = Color.FromHex("f99643"), FontAttributes = FontAttributes.Bold});
            Label lblCodArt = new Label();
            lblCodArt.SetBinding(Label.TextProperty, "CodArt" );
            lblCodArt.HorizontalOptions = LayoutOptions.Start;

            //var fsDescrizione = new FormattedString();
            //fsDescrizione.Spans.Add(new Span { ForegroundColor = Color.FromHex("f99643"), FontAttributes = FontAttributes.Bold });
            Label lblDescrizione = new Label();
            lblDescrizione.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Label.TextProperty, x=>x.Descrizione,BindingMode.OneWay);
            lblDescrizione.HorizontalOptions = LayoutOptions.StartAndExpand;
            
            Entry txtQtaDaOrdinare = new Entry();
            txtQtaDaOrdinare.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Entry.TextProperty, x => x.QtaDaOrdinare);
            txtQtaDaOrdinare.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtQtaDaOrdinare.VerticalOptions = LayoutOptions.End;
            txtQtaDaOrdinare.HorizontalTextAlignment = TextAlignment.Center;
            txtQtaDaOrdinare.Keyboard = Keyboard.Numeric;
            txtQtaDaOrdinare.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            Entry txtQtaDaOrdinareScontoMerce = new Entry();
            txtQtaDaOrdinareScontoMerce.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Entry.TextProperty, x => x.NCP_QtaScontoMerce);
            txtQtaDaOrdinareScontoMerce.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtQtaDaOrdinareScontoMerce.VerticalOptions = LayoutOptions.End;
            txtQtaDaOrdinareScontoMerce.HorizontalTextAlignment = TextAlignment.Center;
            txtQtaDaOrdinareScontoMerce.Keyboard = Keyboard.Numeric;
            txtQtaDaOrdinareScontoMerce.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            Label lblValUnit = new Label();
            lblValUnit.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Label.TextProperty, x => x.ValUnit, BindingMode.OneWay, null, "{0:C2}");
            lblValUnit.HorizontalOptions = LayoutOptions.FillAndExpand;
            lblValUnit.VerticalOptions = LayoutOptions.Center;
            lblValUnit.HorizontalTextAlignment = TextAlignment.End;
            lblValUnit.VerticalTextAlignment = TextAlignment.Center;

            Entry txtSconto1 = new Entry();
            txtSconto1.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Entry.TextProperty, x => x.PercSconto1);
            txtSconto1.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtSconto1.HorizontalTextAlignment = TextAlignment.Center;
            txtSconto1.VerticalOptions = LayoutOptions.Center;
            txtSconto1.Keyboard = Keyboard.Numeric;
            txtSconto1.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            Label lblTotaleRiga = new Label() { FontAttributes = FontAttributes.Bold };
            lblTotaleRiga.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Label.TextProperty, x => x.Imponibile, BindingMode.OneWay, null, "{0:C2}");
            lblTotaleRiga.HorizontalOptions = LayoutOptions.FillAndExpand;
            lblTotaleRiga.VerticalOptions = LayoutOptions.FillAndExpand;
            lblTotaleRiga.HorizontalTextAlignment = TextAlignment.End;
            lblTotaleRiga.VerticalTextAlignment = TextAlignment.Center;
        
            Grid gridLayout = new Grid();
            gridLayout.ColumnSpacing = 3;
            gridLayout.RowSpacing = 5;
            gridLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.Fill;
            gridLayout.Padding = new Thickness(5, 0);
            
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblCodArt
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });      //lblDescrizione
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //txtQtaDaOrdinare
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //txtQtaDaOrdinareScontoMerce
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblValUnit
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //txtSconto1
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblTotaleRiga

            //COLONNA 0
            gridLayout.Children.Add(lblCodArt, 0, 0);
            //COLONNA 1
            gridLayout.Children.Add(lblDescrizione, 1, 0);
            //COLONNA 2
            gridLayout.Children.Add(txtQtaDaOrdinare, 2, 0);
            //COLONNA 3
            gridLayout.Children.Add(txtQtaDaOrdinareScontoMerce, 3, 0);
            //COLONNA 4
            gridLayout.Children.Add(new ContentView() { Padding = new Thickness(0, 0, 5, 0), Content = lblValUnit }, 4, 0);
            //COLONNA 5
            gridLayout.Children.Add(txtSconto1, 5, 0);
            // COLONNA 6
            gridLayout.Children.Add(lblTotaleRiga, 6, 0);

            var viewGenerale = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { gridLayout }
            };

            viewGenerale.SetBinding<GEST_Articoli_Anagrafica>(StackLayout.BackgroundColorProperty, x => x.ArticoloVendibile, BindingMode.OneWay, converter: new CellSelectedBackgroundConverter(true));
            viewGenerale.SetBinding<GEST_Articoli_Anagrafica>(StackLayout.IsEnabledProperty,x => x.ArticoloVendibile,BindingMode.OneWay);
            
            View = gridLayout;

        }


        
    }
}
