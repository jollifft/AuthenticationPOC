using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MaterialTest
{
	public partial class MenuPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public MenuPage()
		{
			InitializeComponent();

			var masterPageItems = new List<MenuItems>();
			masterPageItems.Add(new MenuItems
			{
				Title = "Bears",
				TargetType = typeof(BearsPage)
			});
			masterPageItems.Add(new MenuItems
			{
				Title = "Settings",
				TargetType = typeof(SettingsPage)
			});

			listView.ItemsSource = masterPageItems;
		}
	}
}

