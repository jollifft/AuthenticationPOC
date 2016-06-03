using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MAD.Plugin.MessagingService.PubSubPCL;
using MAD.Plugin.MessagingService.Core;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MaterialTest
{
	public class App : Application
	{
		public static AppService AppService { get; private set;}
		//public static IPlatformParameters Params { get; private set; }
		public static IAuthenticate Authenticate { get; private set; }
		public static IConnectivityService ConnectionService { get; private set; }

		public App(IAuthenticate authenticate)
		{
			//Params = platformParams;
			Authenticate = authenticate;
			ConnectionService = new ConnectivityService ();

			var azure = new AzureService(new MobileServiceClient("https://azureauthbackend.azurewebsites.net"), new MobileServiceSQLiteStore("MaterialDesign.db3"), 30000);
			AppService = new AppService(azure, new PubSubPCLMessaging());
			MainPage = new RootPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			OnResume();
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
			if(!loggingIn)
				hasRan = false;
		}

		bool hasRan;
		bool loggingIn;
		protected async override void OnResume()
		{
			// Handle when your app resumes
			if (hasRan)
				return;
			hasRan = true;
			loggingIn = true;

			await AppService.LoginAsync ();
			await AppService.GetRemoteData ();
			loggingIn = false;
		}
	}
}

