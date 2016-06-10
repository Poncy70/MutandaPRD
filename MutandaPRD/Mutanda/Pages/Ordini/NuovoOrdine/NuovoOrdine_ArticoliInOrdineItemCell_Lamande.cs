using Mutanda.ViewModels;
using Xamarin.Forms;
using Mutanda.Models;
using Mutanda.Converters;

namespace Mutanda.Pages
{
    public class NuovoOrdine_ArticoliInOrdineItemCell_Lamande : ViewCell
    {
        private readonly NuovoOrdineViewModel _NuovoOrdineViewModel;

        public NuovoOrdine_ArticoliInOrdineItemCell_Lamande(NuovoOrdineViewModel _ViewModel)
        {
            _NuovoOrdineViewModel = _ViewModel;
            
            Label lblCodArt = new Label(){ HorizontalOptions = LayoutOptions.Start };
            lblCodArt.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.CodArt);
            lblCodArt.HorizontalOptions = LayoutOptions.Start;
            lblCodArt.VerticalOptions = LayoutOptions.Start;
            lblCodArt.HorizontalTextAlignment = TextAlignment.Start;

            Label lblDescrizione = new Label() { HorizontalOptions = LayoutOptions.StartAndExpand };
            lblDescrizione.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.Descrizione);
            lblDescrizione.HorizontalOptions = LayoutOptions.StartAndExpand;
            lblDescrizione.VerticalOptions = LayoutOptions.Start;
            lblDescrizione.HorizontalTextAlignment = TextAlignment.Start;

            Entry txtQtaDaOrdinare = new Entry();
            txtQtaDaOrdinare.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Entry.TextProperty, x => x.Qta);
            txtQtaDaOrdinare.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtQtaDaOrdinare.VerticalOptions = LayoutOptions.Start;
            txtQtaDaOrdinare.HorizontalTextAlignment = TextAlignment.Center;
            txtQtaDaOrdinare.Keyboard = Keyboard.Numeric;
            txtQtaDaOrdinare.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));
            
            Entry txtQtaDaOrdinareScontoMerce = new Entry();
            txtQtaDaOrdinareScontoMerce.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Entry.TextProperty, x => x.NCP_QtaScontoMerce);
            txtQtaDaOrdinareScontoMerce.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtQtaDaOrdinareScontoMerce.VerticalOptions = LayoutOptions.Start;
            txtQtaDaOrdinareScontoMerce.HorizontalTextAlignment = TextAlignment.Center;
            txtQtaDaOrdinareScontoMerce.Keyboard = Keyboard.Numeric;
            txtQtaDaOrdinareScontoMerce.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));
            
            Label lblValUnit = new Label() { FontAttributes = FontAttributes.Bold };
            lblValUnit.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.ValUnit, BindingMode.OneWay, null, "{0:C2}");
            lblValUnit.HorizontalOptions = LayoutOptions.FillAndExpand;
            lblValUnit.VerticalOptions = LayoutOptions.Start;
            lblValUnit.HorizontalTextAlignment = TextAlignment.End;
            lblValUnit.VerticalTextAlignment = TextAlignment.Start;

            Entry txtSconto1 = new Entry();
            txtSconto1.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Entry.TextProperty, x => x.Sc1);
            txtSconto1.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtSconto1.HorizontalTextAlignment = TextAlignment.Center;
            txtSconto1.VerticalOptions = LayoutOptions.Start;
            txtSconto1.Keyboard = Keyboard.Numeric;
            txtSconto1.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            Entry txtSconto2 = new Entry();
            txtSconto2.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Entry.TextProperty, x => x.Sc2);
            txtSconto2.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtSconto2.VerticalOptions = LayoutOptions.Start;
            txtSconto2.HorizontalTextAlignment = TextAlignment.Center;
            txtSconto2.Keyboard = Keyboard.Numeric;
            txtSconto2.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            Label lblImponibile = new Label() { HorizontalOptions = LayoutOptions.End };
            lblImponibile.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.Imponibile, BindingMode.OneWay, null, "{0:C2}");
            lblImponibile.HorizontalOptions = LayoutOptions.EndAndExpand;
            lblImponibile.VerticalOptions = LayoutOptions.Start;
            lblImponibile.HorizontalTextAlignment = TextAlignment.End;
            lblImponibile.VerticalTextAlignment = TextAlignment.Start;
            
            Label lblDataConsegna = new Label();
            lblDataConsegna.SetBinding< GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x=>x.DataPresuntaConsegna, stringFormat: "{0:dd/MM/yyyy}");
            lblDataConsegna.HorizontalOptions = LayoutOptions.End;
            lblDataConsegna.VerticalOptions = LayoutOptions.Start;
            lblDataConsegna.HorizontalTextAlignment = TextAlignment.Center;
            lblDataConsegna.VerticalTextAlignment = TextAlignment.Start;
            
            #region Layout griglia articoli
            Grid gridLayout = new Grid();
            gridLayout.ColumnSpacing = 3;
            gridLayout.RowSpacing = 0;
            gridLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.Fill;
            gridLayout.Padding = new Thickness(5, 0);

            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //0 - lblCodArt
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });      //1 - lblDescrizione
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //2 - txtQtaDaOrdinare
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //3 - txtQtaDaOrdinareScontoMerce
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //4 - lblValUnit
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //5 - txtSconto1
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //6 - txtSconto2
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //8 - lblTotaleRiga
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //9 - BtnDelete

            //COLONNA 0
            gridLayout.Children.Add(lblCodArt, 0, 0);
            //COLONNA 1
            gridLayout.Children.Add(lblDescrizione, 1, 0);
            //COLONNA 2
            gridLayout.Children.Add(txtQtaDaOrdinare, 2, 0);
            //COLONNA 3
            gridLayout.Children.Add(txtQtaDaOrdinareScontoMerce, 3, 0);
            //COLONNA 4
            gridLayout.Children.Add(lblValUnit, 4, 0);
            //COLONNA 5
            gridLayout.Children.Add(txtSconto1, 5, 0);
            //COLONNA 6
            gridLayout.Children.Add(txtSconto2, 6, 0);
            //COLONNA 7
            gridLayout.Children.Add(lblImponibile, 7, 0);
            //COLONNA 8
            gridLayout.Children.Add(lblDataConsegna, 8, 0);
            #endregion Layout griglia articoli

            #region Layout generale articoli
            var stackGenerale = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { gridLayout }
            };
            stackGenerale.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(StackLayout.BackgroundColorProperty, x => x.IsSelected, converter: new CellSelectedBackgroundConverter(false));
            #endregion

            View = stackGenerale;

            

        }
    }
}
