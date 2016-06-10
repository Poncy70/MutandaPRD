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
    public class NuovoOrdine_SelezClienteIndSpedizItemCell : ViewCell
    {
        
        
        public NuovoOrdine_SelezClienteIndSpedizItemCell()
        {
            
			Label lblRagSociale = new Label(){
               HorizontalOptions = LayoutOptions.Start,
               FontAttributes = FontAttributes.Bold,
				LineBreakMode = LineBreakMode.TailTruncation
			};
            lblRagSociale.SetBinding<GEST_Clienti_Anagrafica_Indirizzi>(Label.TextProperty, x=>x.RagioneSociale);

			Label lblIndirizzo = new Label(){
                HorizontalOptions = LayoutOptions.StartAndExpand,
                LineBreakMode = LineBreakMode.TailTruncation
			};

            lblIndirizzo.SetBinding<GEST_Clienti_Anagrafica_Indirizzi>(Label.TextProperty, x => x.Indirizzo);


            Label lblCitta = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            lblCitta.SetBinding<GEST_Clienti_Anagrafica_Indirizzi>(Label.TextProperty, x => x.Citta);

            



            var view = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10),
                Children =  { lblRagSociale, lblIndirizzo, lblCitta }
            };

            View = view;

        }

    }
}
