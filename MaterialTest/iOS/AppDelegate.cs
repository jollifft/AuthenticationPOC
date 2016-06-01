using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Microsoft.IdentityModel.Clients.ActiveDirectory;



namespace MaterialTest.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			SQLitePCL.CurrentPlatform.Init();

			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App(new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController)));

			return base.FinishedLaunching(app, options);
		}
	}
}

