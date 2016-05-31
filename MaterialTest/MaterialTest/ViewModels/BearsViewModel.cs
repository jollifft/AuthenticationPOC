using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaterialTest
{
	public class BearsViewModel : BaseViewModel
	{
		private ObservableCollection<Bears> _bears;
		public ObservableCollection<Bears> BearsList
		{
			get
			{
				return _bears;
			}
			set
			{
				_bears = value;
				base.OnPropertyChanged();
			}
		}

		private Command increase;
		public Command IncreaseCommand
		{
			get 
			{ 
				return increase ?? (increase = new Command(() =>Settings.BearsListCount++));
				//return increase ?? (increase = new Command(async () => await App.AppService.GetBears()));
			}
		}

		public async Task LoadBearsAsync()
		{
			if (IsBusy)
				return;
			IsBusy = true;
			await App.AppService.GetBears ();
		}

		public BearsViewModel()
		{
			AppService.Messaging.SubscribeMessage<BearsMessage>(this, results =>
			{
				BearsList = new ObservableCollection<Bears>(results.bears);
				if(BearsList.Count > 0){
					IsBusy = false;
				}
			});

		}
	}
}

