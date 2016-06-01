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
			vm.IsBusy = true;
			bool success = await App.AppService.LoginAsync ();
			vm.IsBusy = false;
//			bool success = await vm.LoginAsync ();
			if (success) {
				await vm.LoadBearsAsync ();
			} else {
				//alert
			}
		}
	}
}

