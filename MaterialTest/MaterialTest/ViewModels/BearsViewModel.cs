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

		public BearsViewModel()
		{
			AppService.Messaging.SubscribeMessage<BearsMessage>(this, results =>
			{
				BearsList = new ObservableCollection<Bears>(results.bears);
				IsBusy = false;
			});

//			MessagingCenter.Subscribe<AppService, IEnumerable<Bears>> (this, "BearMessage", (sender, args) => {
//				BearsList = new ObservableCollection<Bears>(args);
//				IsBusy = false;
//			});

			if (IsBusy)
				return;

			IsBusy = true;
			Task.Run(async () => await App.AppService.GetBears());

			//App.AppService.GetBears ();

		}
	}
}

