using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MaterialTest
{
	public partial class LoginFailedPage : ContentPage
	{
		public LoginFailedPage ()
		{
			InitializeComponent ();
			BindingContext = new LoginFailedViewModel (Navigation);
		}
	}
}

