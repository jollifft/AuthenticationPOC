using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace MaterialTest.Droid
{
	[Activity(Label = "MaterialTest.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
	{
		protected override void OnCreate(Bundle bundle)
		{



			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			global::Xamarin.Forms.Forms.Init(this, bundle);

			App.Init((IAuthenticate)this);

			LoadApplication(new App());
		}

		// Define a authenticated user.
		private MobileServiceUser user;

		public async Task<bool> Authenticate()
		{
			var success = false;
			try
			{
				// Sign in with Facebook login using a server-managed flow.
				user = await App.AppService.BaaS.AzureClient.LoginAsync(this, MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
//				CreateAndShowDialog(string.Format("you are now logged in - {0}",
//					user.UserId), "Logged in!");

				success = true;
			}
			catch (Exception ex)
			{
				CreateAndShowDialog(ex.Message, "Authentication failed");
			}
			return success;
		}

		private void CreateAndShowDialog(String message, String title)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);

			builder.SetMessage(message);
			builder.SetTitle(title);
			builder.Create().Show();
		}
	}
}

