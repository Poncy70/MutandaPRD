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
    public class NuovoOrdine_ArticoliInOrdineItemCell : ViewCell
    {
        private readonly NuovoOrdineViewModel _NuovoOrdineViewModel;

        public NuovoOrdine_ArticoliInOrdineItemCell(NuovoOrdineViewModel _ViewModel)
        {
            _NuovoOrdineViewModel = _ViewModel;

            #region Descrizione Articolo
            Label lblCodArt = new Label()
            {
                HorizontalOptions = LayoutOptions.Start
            };
            lblCodArt.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.CodArt);

            Label lblDescrizione = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start
            };
            lblDescrizione.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.Descrizione);

            Label lblClasse = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start
            };
            lblClasse.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.CodClasse);


            Label lblNatura = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start
            };
            lblNatura.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.CodNatura);


            Label lblImponibile = new Label() { FontAttributes = FontAttributes.Bold };
            lblImponibile.HorizontalOptions = LayoutOptions.End;
            lblImponibile.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Label.TextProperty, x => x.Imponibile);

            var stackDescrArticolo = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(8, 0),
                Children =
                {
                    lblClasse,lblNatura,lblCodArt,lblDescrizione, new Label() { Text="Imponibile:", HorizontalOptions=LayoutOptions.EndAndExpand}, lblImponibile
                }
            };
            #endregion Descrizione Articolo

            Button BtnQtaDown = new Button
            {
                BackgroundColor = Color.White,
                Image = "minus.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            BtnQtaDown.SetBinding(Button.CommandProperty, new Binding("ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnDownCommand", 
					BindingMode.OneWay, null, null, null, _NuovoOrdineViewModel));
            BtnQtaDown.SetBinding(Button.CommandParameterProperty, new Binding() { Source = this.BindingContext, Path = "." });

            Entry txtQtaDaOrdinare = new Entry();
            txtQtaDaOrdinare.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Entry.TextProperty, x => x.Qta);
            txtQtaDaOrdinare.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtQtaDaOrdinare.VerticalOptions = LayoutOptions.Center;
            txtQtaDaOrdinare.HorizontalTextAlignment = TextAlignment.Center;
            txtQtaDaOrdinare.Keyboard = Keyboard.Numeric;

            Button BtnQtaUp = new Button
            {
                BackgroundColor = Color.White,
                Image = "plus.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            BtnQtaUp.SetBinding(Button.CommandProperty, new Binding("ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnUpCommand", 
				BindingMode.OneWay, null, null, null, _NuovoOrdineViewModel));
            BtnQtaUp.SetBinding(Button.CommandParameterProperty, new Binding() { Source = this.BindingContext, Path = "." });

            Button BtnQtaDownScontoMerce = new Button
            {
                BackgroundColor = Color.White,
                Image = "minus.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            BtnQtaDownScontoMerce.SetBinding(Button.CommandProperty, new Binding("ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand", 
				BindingMode.OneWay, null, null, null, _NuovoOrdineViewModel));
            BtnQtaDownScontoMerce.SetBinding(Button.CommandParameterProperty, new Binding() { Source = this.BindingContext, Path = "." });

            Entry txtQtaDaOrdinareScontoMerce = new Entry();
            txtQtaDaOrdinareScontoMerce.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Entry.TextProperty, 
				x => x.NCP_QtaScontoMerce);
            txtQtaDaOrdinareScontoMerce.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtQtaDaOrdinareScontoMerce.VerticalOptions = LayoutOptions.Center;
            txtQtaDaOrdinareScontoMerce.HorizontalTextAlignment = TextAlignment.Center;
            txtQtaDaOrdinareScontoMerce.Keyboard = Keyboard.Numeric;


            


            Button BtnQtaUpScontoMerce = new Button
            {
                BackgroundColor = Color.White,
                Image = "plus.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
			BtnQtaUpScontoMerce.SetBinding(Button.CommandProperty, 
				new Binding("ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand", BindingMode.OneWay, null, null, null, _NuovoOrdineViewModel));
            BtnQtaUpScontoMerce.SetBinding(Button.CommandParameterProperty, new Binding() { Source = this.BindingContext, Path = "." });

            Entry txtSconto1 = new Entry();
            txtSconto1.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Entry.TextProperty, x => x.Sc1);
            txtSconto1.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtSconto1.VerticalOptions = LayoutOptions.Center;
            txtSconto1.HorizontalTextAlignment = TextAlignment.Center;
            txtSconto1.Keyboard = Keyboard.Numeric;

            Entry txtSconto2 = new Entry();
            txtSconto2.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(Entry.TextProperty, x => x.Sc2);
            txtSconto2.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtSconto2.VerticalOptions = LayoutOptions.Center;
            txtSconto2.HorizontalTextAlignment = TextAlignment.Center;
            txtSconto2.Keyboard = Keyboard.Numeric;

            DatePicker dteDataConsegna = new DatePicker();
            dteDataConsegna.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(DatePicker.DateProperty, x => x.DataPresuntaConsegna);
            dteDataConsegna.Format = "dd/MM/yyyy";
            dteDataConsegna.VerticalOptions = LayoutOptions.Center;

            //var BtnDelete = new Button
            //{
            //    BackgroundColor = Color.FromHex("f99643"),
            //    Image = "delete.png",
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center,
            //};
            //BtnDelete.SetBinding(Button.CommandProperty, new Binding("ArticoliInOrdineItemCell_CancellaItemCommand", BindingMode.OneWay, null, null, null, _NuovoOrdineViewModel));
            //BtnDelete.SetBinding(Button.CommandParameterProperty, new Binding() { Source = this.BindingContext, Path = "." });

            #region Layout griglia articoli
            Grid gridLayout = new Grid();
            gridLayout.ColumnSpacing = 8;
            gridLayout.RowSpacing = 2;
            gridLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.Fill;

            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

            //gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //0 - lblValUnit
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //0 - BtnQtaDown
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //1 - txtQtaDaOrdinare
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //2 - BtnQtaUp
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //3 - BtnQtaDownScontoMerce
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //4 - txtQtaDaOrdinareScontoMerce
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //5 - BtnQtaUpScontoMerce
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //6 - txtSconto1
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //7 - txtSconto2
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });      //8 - dteDataConsegna
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //9 - BtnDelete

            //gridLayout.Children.Add(new Label() { Text = "Val. Unit", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center }, 0, 0);
            //gridLayout.Children.Add(new ContentView() { Padding = new Thickness(0, 0, 5, 0), Content = lblValUnit }, 0, 1);
            gridLayout.Children.Add(BtnQtaDown, 0, 1);
            gridLayout.Children.Add(new Label()
            {
                Text = "Qta",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand
            }, 1, 0);
            gridLayout.Children.Add(txtQtaDaOrdinare, 1, 1);
            gridLayout.Children.Add(BtnQtaUp, 2, 1);
            gridLayout.Children.Add(BtnQtaDownScontoMerce, 3, 1);

            Label lblQtaScontoMerce = new Label()
            {
                Text = "Qta Sc. Merce",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            gridLayout.Children.Add(lblQtaScontoMerce, 3, 0);
            Grid.SetColumnSpan(lblQtaScontoMerce, 3);

            gridLayout.Children.Add(txtQtaDaOrdinareScontoMerce, 4, 1);
            gridLayout.Children.Add(BtnQtaUpScontoMerce, 5, 1);
            gridLayout.Children.Add(new Label()
            {
                Text = "Sc. 1",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand
            }, 6, 0);
            gridLayout.Children.Add(txtSconto1, 6, 1);
            gridLayout.Children.Add(new Label()
            {
                Text = "Sc. 2",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand
            }, 7, 0);
            gridLayout.Children.Add(txtSconto2, 7, 1);
            gridLayout.Children.Add(new Label()
            {
                Text = "Data Cons.",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand
            }, 8, 0);
            gridLayout.Children.Add(dteDataConsegna, 8, 1);
            //gridLayout.Children.Add(BtnDelete, 9, 1);
            #endregion Layout griglia articoli

            #region Layout generale artiocoli
            var stackGenerale = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(8, 0),
                Children = { stackDescrArticolo, gridLayout }
            };
            stackGenerale.SetBinding<GEST_Ordini_Righe_DettaglioOrdine>(StackLayout.BackgroundColorProperty, x => x.IsSelected, converter: new CellSelectedBackgroundConverter(false));
            #endregion

            View = stackGenerale;

            

        }
    }
}
