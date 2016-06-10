using Mutanda.ViewModels.Clienti;
using Mutanda.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Mutanda.Views.Clienti
{
    public partial class ClientiDettaglioView : ClientiDettaglioViewXaml
    {
        public ClientiDettaglioView()
        {
            InitializeComponent();
        }
    }

    public abstract class ClientiDettaglioViewXaml : ModelBoundContentView<ClientiDettaglioViewModel>
    {

    }
}
