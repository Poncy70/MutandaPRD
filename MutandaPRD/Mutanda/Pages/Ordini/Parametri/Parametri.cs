using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mutanda.Models;
using Mutanda.ViewModels;
using Mutanda.Views;

namespace Mutanda.Pages
{
	public class Parametri: ContentPage
	{
        ParametriViewModel _ParametriViewModel;
		INavigation _navi;
		public Parametri()
		{

			Title = "Parametri";

            _ParametriViewModel = new ParametriViewModel();


		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			if (!_ParametriViewModel.IsInitialized)
			{
				await _ParametriViewModel.ExecuteLoadSeedDataCommand();
                _ParametriViewModel.IsInitialized = true;
			}
		}
	}
}

