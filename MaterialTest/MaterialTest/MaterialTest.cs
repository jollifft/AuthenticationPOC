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
		public static IPlatformParameters Params { get; private set; }

		public App(IPlatformParameters platformParams)
		{
			Params = platformParams;
			var azure = new AzureService(new MobileServiceClient("https://azureauthbackend.azurewebsites.net"), new MobileServiceSQLiteStore("MaterialDesign.db3"), 30000);
			AppService = new AppService(azure, new PubSubPCLMessaging(), platformParams);
			MainPage = new RootPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

