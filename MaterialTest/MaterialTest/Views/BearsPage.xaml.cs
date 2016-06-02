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

			//checking for zero incase we reuse views (performance)
			if (vm.BearsList.Count == 0) 
				await vm.LoadBearsAsync();



//			if (vm.IsBusy)
//				return;
//
//			bool success = await vm.LoginAsync ();
//			if (success) {
//				await vm.LoadBearsAsync ();
//			} else {
//				//alert
//			}
		}
	}
}

