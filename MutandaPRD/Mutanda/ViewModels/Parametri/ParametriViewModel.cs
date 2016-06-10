using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutanda.Models;
using Xamarin.Forms;
using System.Windows.Input;
using Mutanda.Services;
using System.Collections.ObjectModel;
using Mutanda.Extensions;
using Mutanda.ViewModels.Base;
using System.Threading;
using Plugin.DeviceInfo;

namespace Mutanda.ViewModels
{
	public class ParametriViewModel:BaseViewModel
	{
		

		IDataService _DataClient;


        

        public ParametriViewModel()
		{
			_DataClient = DependencyService.Get<IDataService>();
		

            IsInitialized = false;



		}
        

        public async Task ExecuteLoadSeedDataCommand()
		{
			

			if (IsBusy)
				return;

			IsBusy = true;

			//if (!_DataClient.IsSeeded)
			//{
			//	await _DataClient.SeedLocalDataAsync(false);
			//}
			



			

			IsInitialized = true;
			IsBusy = false;
			
		}		

	}
}

