﻿using System;

using Xamarin.Forms;

namespace MaterialTest
{
	public class RootPage : MasterDetailPage
	{
		MenuPage menuPage = new MenuPage();
		public RootPage()
		{
			
			Master = menuPage;
			Detail = new NavigationPage(new BearsPage());

			menuPage.ListView.ItemSelected += OnItemSelected;
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MenuItems;
			if (item != null)
			{
				Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
				menuPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
}
}


