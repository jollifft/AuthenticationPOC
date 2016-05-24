using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MaterialTest
{
	public partial class BearsPage : ContentPage
	{
		public BearsPage()
		{

			InitializeComponent ();
			BindingContext = new BearsViewModel ();

		}
	}
}

