using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace MaterialTest.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IAuthenticate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			SQLitePCL.CurrentPlatform.Init();

			global::Xamarin.Forms.Forms.Init();

			App.Init(this);

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

		// Define a authenticated user.
		private MobileServiceUser user;

		public async Task<bool> Authenticate()
		{
			var success = false;
			try
			{
				// Sign in with Facebook login using a server-managed flow.
				if (user == null)
				{
					user = await App.AppService.BaaS.AzureClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController,
						MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
					if (user != null)
					{
						UIAlertView avAlert = new UIAlertView("Authentication", "You are now logged in " + user.UserId, null, "OK", null);
						avAlert.Show();
					}
				}

				success = true;
			}
			catch (Exception ex)
			{
				UIAlertView avAlert = new UIAlertView("Authentication failed", ex.Message, null, "OK", null);
				avAlert.Show();
			}
			return success;
		}
	}
}

