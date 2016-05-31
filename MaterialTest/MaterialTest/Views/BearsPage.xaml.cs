using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MaterialTest
{
	public partial class BearsPage : ContentPage
	{
		BearsViewModel vm;
		public BearsPage()
		{

			InitializeComponent ();
			BindingContext = vm = new BearsViewModel ();

		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			if (vm.IsBusy)
				return;
			bool successful = await vm.LoginAsync ();
			if (successful) {
				//await App.AppService.GetRemoteData ();
				await vm.LoadBearsAsync ();
			} else
				vm.IsBusy = true;
				await Navigation.PushModalAsync (new LoginFailedPage ());
		}
	}
}

