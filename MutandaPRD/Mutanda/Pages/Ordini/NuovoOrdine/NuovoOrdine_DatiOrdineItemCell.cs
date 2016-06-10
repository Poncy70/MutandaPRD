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
    public class NuovoOrdine_DatiOrdineItemCell : ViewCell
    {
        private readonly NuovoOrdineViewModel _NuovoOrdineViewModel;

        public NuovoOrdine_DatiOrdineItemCell(NuovoOrdineViewModel _ViewModel)
        {
             _NuovoOrdineViewModel = _ViewModel;

            Label lblCodArt = new Label();
			lblCodArt.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x=>x.CodArt,BindingMode.OneWay);
            lblCodArt.HorizontalOptions = LayoutOptions.Start;
            lblCodArt.VerticalOptions = LayoutOptions.Center;
            lblCodArt.HorizontalTextAlignment = TextAlignment.Start;
            lblCodArt.VerticalTextAlignment = TextAlignment.Center;
            lblCodArt.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            Label lblDescrizione = new Label();
			lblDescrizione.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x=>x.Descrizione,BindingMode.OneWay);
            lblDescrizione.HorizontalOptions = LayoutOptions.StartAndExpand;
            lblDescrizione.VerticalOptions = LayoutOptions.Center;
            lblDescrizione.HorizontalTextAlignment = TextAlignment.Start;
            lblDescrizione.VerticalTextAlignment = TextAlignment.Center;
            lblDescrizione.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));

            Label lblQta = new Label();
            lblQta.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.Qta, BindingMode.OneWay);
            lblQta.HorizontalOptions = LayoutOptions.FillAndExpand;
            lblQta.VerticalOptions = LayoutOptions.Center;
            lblQta.HorizontalTextAlignment = TextAlignment.Center;
            lblQta.VerticalTextAlignment = TextAlignment.Center;
            lblQta.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            
            Label lblQtaScMerce = new Label();
            lblQtaScMerce.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.NCP_QtaScontoMerce, BindingMode.OneWay);
            lblQtaScMerce.HorizontalOptions = LayoutOptions.FillAndExpand;
            lblQtaScMerce.VerticalOptions = LayoutOptions.Center;
            lblQtaScMerce.HorizontalTextAlignment = TextAlignment.Center;
            lblQtaScMerce.VerticalTextAlignment = TextAlignment.Center;
            lblQtaScMerce.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            
            Label lblSc1 = new Label();
            lblSc1.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.Sc1, BindingMode.OneWay);
            lblSc1.HorizontalOptions = LayoutOptions.FillAndExpand;
            lblSc1.VerticalOptions = LayoutOptions.Center;
            lblSc1.HorizontalTextAlignment = TextAlignment.Center;
            lblSc1.VerticalTextAlignment = TextAlignment.Center;
            lblSc1.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            lblSc1.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.IsVisibleProperty, x => x.CodArt, BindingMode.OneWay, converter: new CellHideConverter());


            Label lblSc2 = new Label();
            lblSc2.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.Sc2, BindingMode.OneWay);
            lblSc2.HorizontalOptions = LayoutOptions.FillAndExpand;
            lblSc2.VerticalOptions = LayoutOptions.Center;
            lblSc2.HorizontalTextAlignment = TextAlignment.Center;
            lblSc2.VerticalTextAlignment = TextAlignment.Center;
            lblSc2.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            lblSc2.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.IsVisibleProperty, x => x.CodArt, BindingMode.OneWay, converter: new CellHideConverter());
            
            Label lblValUnit = new Label();
            lblValUnit.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.ValUnit, BindingMode.OneWay);
            lblValUnit.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.IsVisibleProperty, x => x.CodArt, BindingMode.OneWay, converter: new CellHideConverter());
            lblValUnit.HorizontalOptions = LayoutOptions.EndAndExpand;
            lblValUnit.VerticalOptions = LayoutOptions.Center;
            lblValUnit.HorizontalTextAlignment = TextAlignment.End;
            lblValUnit.VerticalTextAlignment = TextAlignment.Center;
            lblValUnit.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            
            Label lblTotaleRiga = new Label() { FontAttributes = FontAttributes.Bold };
            lblTotaleRiga.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.Imponibile, BindingMode.OneWay, null, "{0:C2}");
            lblTotaleRiga.HorizontalOptions = LayoutOptions.EndAndExpand;
            lblTotaleRiga.VerticalOptions = LayoutOptions.Center;
            lblTotaleRiga.HorizontalTextAlignment = TextAlignment.End;
            lblTotaleRiga.VerticalTextAlignment = TextAlignment.Center;
            lblTotaleRiga.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));
            
			Label lblDataConsegna = new Label();
			lblDataConsegna.SetBinding< GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x=>x.DataPresuntaConsegna, stringFormat: "{0:dd/MM/yyyy}");
            lblDataConsegna.HorizontalOptions = LayoutOptions.EndAndExpand;
            lblDataConsegna.VerticalOptions = LayoutOptions.Center;
            lblDataConsegna.HorizontalTextAlignment = TextAlignment.End;
            lblDataConsegna.VerticalTextAlignment = TextAlignment.Center;
            lblDataConsegna.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            #region Layout griglia articoli
            Grid gridLayout = new Grid();
            gridLayout.ColumnSpacing = 3;
            gridLayout.RowSpacing = 0;
            gridLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.Fill;
            gridLayout.Padding = new Thickness(5, 0);

            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //0 - lblCodArt
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });      //1 - lblDescrizione
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //2 - txtQtaDaOrdinare
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //3 - txtQtaDaOrdinareScontoMerce
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //4 - lblSc1
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //5 - lblSc2
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //6 - lblValUnit
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //8 - lblTotaleRiga
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //9 - lblDataConsegna

            #endregion
            gridLayout.Children.Add(lblCodArt, 0, 0);
            gridLayout.Children.Add(lblDescrizione, 1, 0);
            gridLayout.Children.Add(lblQta, 2, 0);
            gridLayout.Children.Add(lblQtaScMerce, 3, 0);
            gridLayout.Children.Add(lblSc1, 4, 0);
            gridLayout.Children.Add(lblSc2, 5, 0);
            gridLayout.Children.Add(lblValUnit, 6, 0);
            gridLayout.Children.Add(lblTotaleRiga, 7, 0);
			gridLayout.Children.Add(lblDataConsegna, 8, 0);
            var viewGenerale = new StackLayout()
            {
                Padding = new Thickness(0,8),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { gridLayout }
            };

            View = viewGenerale;
        }
    }
}
