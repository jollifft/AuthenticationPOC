﻿using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MAD.Plugin.MessagingService.PubSubPCL;
using MAD.Plugin.MessagingService.Core;

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
			var azure = new AzureService(new MobileServiceClient("http://azureauthbackend.azurewebsites.net"), new MobileServiceSQLiteStore("MaterialDesign.db3"), 30000);
			AppService = new AppService(azure, new PubSubPCLMessaging());
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

