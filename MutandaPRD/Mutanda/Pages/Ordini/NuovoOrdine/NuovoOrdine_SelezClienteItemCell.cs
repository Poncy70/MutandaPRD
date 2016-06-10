using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mutanda.ViewModels;
using Mutanda.Models;

namespace Mutanda.Pages
{
    public class NuovoOrdine_SelezClienteItemCell : ViewCell
    {


        public NuovoOrdine_SelezClienteItemCell()
        {

            Label lblRagSociale = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Bold,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            lblRagSociale.SetBinding<GEST_Clienti_Anagrafica>(Label.TextProperty, x => x.RagioneSociale);

            Label lblIndirizzo = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            lblIndirizzo.SetBinding(Label.TextProperty, "Indirizzo");

            Label lblCitta = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            lblCitta.SetBinding(Label.TextProperty, "Citta");

            Label lblPersonaRif = new Label()
            {
                HorizontalOptions = LayoutOptions.End
            };
            lblCitta.SetBinding(Label.TextProperty, "RifInterno");

            //Label lblSconto1 = new Label()
            //{
            //    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            //    VerticalTextAlignment = TextAlignment.End,
            //    LineBreakMode = LineBreakMode.TailTruncation
            //};

            //lblSconto1.SetBinding<GEST_Clienti_Anagrafica>(Label.TextProperty, x => x.PercSconto1);



            var view = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10),
                Children = { lblRagSociale, lblIndirizzo, lblCitta }
            };

            View = view;

        }

    }
}