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
    public class NuovoOrdine_SelezArticoliItemCell : ViewCell
    {
        private readonly NuovoOrdineViewModel _NuovoOrdineViewModel;

        public NuovoOrdine_SelezArticoliItemCell(NuovoOrdineViewModel _ViewModel)
        {
             _NuovoOrdineViewModel = _ViewModel;

            //var fsCodArt = new FormattedString();
            //fsCodArt.Spans.Add(new Span { ForegroundColor = Color.FromHex("f99643"), FontSize = 20, FontAttributes = FontAttributes.Bold });
            Label lblCodArt = new Label() { FontSize = 18, FontAttributes = FontAttributes.Bold };
            lblCodArt.SetBinding(Label.TextProperty, "CodArt" );
            lblCodArt.HorizontalOptions = LayoutOptions.Start;

            Label lblDescrizione = new Label() { FontSize = 18, FontAttributes = FontAttributes.Bold };
			lblDescrizione.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Label.TextProperty, x=>x.Descrizione,BindingMode.OneWay);

            Label lblQtaGiaInOrdine = new Label() { FontAttributes = FontAttributes.Bold };
            lblQtaGiaInOrdine.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Label.TextProperty,x => x.QtaGiaInOrdine);
            lblQtaGiaInOrdine.HorizontalOptions = LayoutOptions.EndAndExpand;
            lblQtaGiaInOrdine.HorizontalTextAlignment = TextAlignment.End;

            Label lblValUnit = new Label() { FontAttributes = FontAttributes.Bold };
            lblValUnit.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Label.TextProperty, x => x.ValUnit, BindingMode.OneWay, null, "{0:C2}");
            lblValUnit.HorizontalOptions = LayoutOptions.FillAndExpand;
            lblValUnit.VerticalOptions = LayoutOptions.FillAndExpand;
            lblValUnit.HorizontalTextAlignment = TextAlignment.End;
            lblValUnit.VerticalTextAlignment = TextAlignment.Center;

            Label lblCodFamiglia = new Label();
            lblCodFamiglia.SetBinding(Label.TextProperty, "CodFamiglia");
            lblCodFamiglia.HorizontalOptions = LayoutOptions.EndAndExpand;

            Label lblCodClasse = new Label();
            lblCodClasse.SetBinding(Label.TextProperty, "CodClasse");         

            Label lblCodNatura = new Label();
            lblCodNatura.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Label.TextProperty, x => x.CodNatura);

            var stackDescrizioneArticolo = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(8, 0),
                Children = {
                    lblCodArt, new ContentView() { Padding = new Thickness(10,0,0,0), Content = lblDescrizione,HorizontalOptions = LayoutOptions.StartAndExpand },
                    new Label() { Text = "Qta In Ordine:", HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.EndAndExpand }
            //new ContentView() { Padding = new Thickness(10,0,0,0), Content = new Label() { Text = "(" } }
            //new Label() { Text="Classe: "},
            //lblCodClasse,
            //new Label() { Text="Natura: "},
            //lblCodNatura,
            //new Label() { Text = ")" }
                }
            };
            
            #region Entry Qta - Qta Sconto Merce - Sc1 - Sc2
            Entry txtQtaDaOrdinare = new Entry();
            txtQtaDaOrdinare.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Entry.TextProperty, x => x.QtaDaOrdinare);
            txtQtaDaOrdinare.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtQtaDaOrdinare.HorizontalTextAlignment = TextAlignment.Center;
            txtQtaDaOrdinare.VerticalOptions = LayoutOptions.Center;
            txtQtaDaOrdinare.Keyboard = Keyboard.Numeric;
            txtQtaDaOrdinare.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            Entry txtQtaDaOrdinareScontoMerce = new Entry();
            txtQtaDaOrdinareScontoMerce.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Entry.TextProperty, x => x.NCP_QtaScontoMerce);
            txtQtaDaOrdinareScontoMerce.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtQtaDaOrdinareScontoMerce.HorizontalTextAlignment = TextAlignment.Center;
            txtQtaDaOrdinareScontoMerce.VerticalOptions = LayoutOptions.Center;
            txtQtaDaOrdinareScontoMerce.Keyboard = Keyboard.Numeric;
            txtQtaDaOrdinareScontoMerce.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            Entry txtSconto1 = new Entry();
            txtSconto1.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Entry.TextProperty, x => x.PercSconto1);
            txtSconto1.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtSconto1.HorizontalTextAlignment = TextAlignment.Center;
            txtSconto1.VerticalOptions = LayoutOptions.Center;
            txtSconto1.Keyboard = Keyboard.Numeric;
            txtSconto1.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));

            Entry txtSconto2 = new Entry();
            txtSconto2.SetBinding<GEST_Articoli_Anagrafica_SelezioneArticolo>(Entry.TextProperty, x => x.PercSconto2);
            txtSconto2.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtSconto2.HorizontalTextAlignment = TextAlignment.Center;
            txtSconto2.VerticalOptions = LayoutOptions.Center;
            txtSconto2.Keyboard = Keyboard.Numeric;
            txtSconto2.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));
            #endregion

            DatePicker dteDataConsegna = new DatePicker();
            dteDataConsegna.SetBinding(DatePicker.DateProperty, "DataConsegna");
            dteDataConsegna.Format = "dd/MM/yyyy";
            dteDataConsegna.HorizontalOptions = LayoutOptions.EndAndExpand;
            dteDataConsegna.VerticalOptions = LayoutOptions.Center;
            #region Bottoni + e - Qta e Qta Sconto Merce            
            Button BtnQtaUp = new Button
			{
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Image = "plus.png"
            };

            Button BtnQtaDown = new Button
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Image = "minus.png"
            };

            BtnQtaUp.SetBinding(Button.CommandProperty,new Binding( "SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnUpCommand", BindingMode.OneWay, null, null, null, _NuovoOrdineViewModel));
            BtnQtaUp.SetBinding(Button.CommandParameterProperty, new Binding() { Source = this.BindingContext, Path = "." });
            BtnQtaDown.SetBinding(Button.CommandProperty, new Binding("SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnDownCommand", BindingMode.OneWay, null , null , null , _NuovoOrdineViewModel));
			BtnQtaDown.SetBinding(Button.CommandParameterProperty,new Binding() { Source = this.BindingContext, Path = "." });
            
            Button BtnQtaUpScontoMerce = new Button
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Image = "plus.png"
            };

            Button BtnQtaDownScontoMerce = new Button
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Image = "minus.png"
            };

            BtnQtaUpScontoMerce.SetBinding(Button.CommandProperty, new Binding("SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand", BindingMode.OneWay, null, null, null, _NuovoOrdineViewModel));
            BtnQtaUpScontoMerce.SetBinding(Button.CommandParameterProperty, new Binding() { Source = this.BindingContext, Path = "." });
            BtnQtaDownScontoMerce.SetBinding(Button.CommandProperty, new Binding("SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand", BindingMode.OneWay, null, null, null, _NuovoOrdineViewModel));
            BtnQtaDownScontoMerce.SetBinding(Button.CommandParameterProperty, new Binding() { Source = this.BindingContext, Path = "." });
            #endregion            

            Grid gridLayout = new Grid();
            gridLayout.ColumnSpacing = 15;
            gridLayout.RowSpacing = 5;
            gridLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.Fill;
            
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //BtnQtaDown
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //txtQtaDaOrdinare
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //BtnQtaUp
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //BtnQtaDownScontoMerce
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //txtQtaDaOrdinareScontoMerce
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //BtnQtaUpScontoMerce
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //lblValUnit
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //txtSconto1
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //txtSconto2
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //dteDataConsegna

            //gridLayout.Children.Add(new Label() { Text="Prezzo", HorizontalTextAlignment=TextAlignment.Center, HorizontalOptions=LayoutOptions.Center }, 0, 0);
            //gridLayout.Children.Add(new ContentView() { Padding=new Thickness(0,0,5,0), Content = lblValUnit }, 0, 1);
            gridLayout.Children.Add(BtnQtaDown, 0, 1);
            gridLayout.Children.Add(new Label() { Text = "Qta", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center }, 1, 0);
            gridLayout.Children.Add(txtQtaDaOrdinare, 1, 1);
            gridLayout.Children.Add(BtnQtaUp, 2, 1);
            gridLayout.Children.Add(BtnQtaDownScontoMerce, 3, 1);
            Label lblScontoMerce = new Label() { Text = "Sconto Merce", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center };
            gridLayout.Children.Add(lblScontoMerce, 3, 0);
            Grid.SetColumnSpan(lblScontoMerce, 3);

            gridLayout.Children.Add(txtQtaDaOrdinareScontoMerce, 4, 1);
            gridLayout.Children.Add(BtnQtaUpScontoMerce, 5, 1);

            BoxView bv = new BoxView() { BackgroundColor = Color.FromHex("dddddd") };
            gridLayout.Children.Add(bv, 6, 0);
            Grid.SetRowSpan(bv, 2);

            gridLayout.Children.Add(new Label() { Text = "Prezzo", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold }, 6, 0);
            gridLayout.Children.Add(new ContentView() { Padding = new Thickness(0, 0, 5, 0), Content = lblValUnit }, 6, 1);

            gridLayout.Children.Add(new Label() { Text = "Sconto 1", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center }, 7, 0);
            gridLayout.Children.Add(txtSconto1, 7, 1);
            gridLayout.Children.Add(new Label() { Text = "Sconto 2", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center }, 8, 0);
            gridLayout.Children.Add(txtSconto2, 8, 1);
            //gridLayout.Children.Add(new Label() { Text = "Data Cons.", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center }, 9, 0);
            //gridLayout.Children.Add(dteDataConsegna, 9, 1);

            //Label lblQtaInOrdine = new Label() { Text = "Qta In Ordine:", HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.End };
            //Label lblQtaInOrdine = new Label() { Text = "Data Consegna:", HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.End };
            //gridLayout.Children.Add(lblQtaInOrdine, 0, 2);
            //Grid.SetColumnSpan(lblQtaInOrdine, 8);

            //gridLayout.Children.Add(lblQtaGiaInOrdine, 9, 2);
            //gridLayout.Children.Add(dteDataConsegna, 8, 2);
            //Grid.SetColumnSpan(dteDataConsegna, 2);

            var viewGenerale = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { stackDescrizioneArticolo, gridLayout }
            };

            viewGenerale.SetBinding<GEST_Articoli_Anagrafica>(StackLayout.BackgroundColorProperty, x => x.ArticoloVendibile, BindingMode.OneWay, converter: new CellSelectedBackgroundConverter(false));
            viewGenerale.SetBinding<GEST_Articoli_Anagrafica>(StackLayout.IsEnabledProperty,x => x.ArticoloVendibile,BindingMode.OneWay);


            View = viewGenerale;

        }
    }
}
