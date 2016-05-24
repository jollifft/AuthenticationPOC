using System;
using MAD.Plugin.BaaS.AzureService;
using MAD.Plugin.BaaS.Core;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MaterialTest
{
	public class App : Application
	{
		public static AppService AppService { get; private set;}
		public App()
		{
			// The root page of your application
			//var content = new ContentPage
			//{
			//	Title = "MaterialTest",
			//	Content = new StackLayout
			//	{
			//		VerticalOptions = LayoutOptions.Center,
			//		Children = {
			//			new Label {
			//				HorizontalTextAlignment = TextAlignment.Center,
			//				Text = "Welcome to Xamarin Forms!"
			//			}
			//		}
			//	}
			//};
			var azure = new AzureService(new MobileServiceClient("http://azureauthbackend.azurewebsites.net"), new MobileServiceSQLiteStore("MaterialDesign.db3"), 14000);
			AppService = new AppService(azure, new PubSubPCLMessaging(new SubscriptionManager()));
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

