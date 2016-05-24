using System;

using Xamarin.Forms;

namespace MaterialTest
{
	public class BearsPageCS : ContentPage
	{
		private BearsViewModel viewModel;

		public BearsPageCS()
		{
			viewModel = new BearsViewModel();
			BindingContext = viewModel;

			ListView bearsListView = new ListView
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None,

			};
			bearsListView.SetBinding(ListView.ItemsSourceProperty, "BearsList");
			bearsListView.ItemTemplate = new DataTemplate(typeof(TextCell));
			bearsListView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
			bearsListView.ItemTemplate.SetBinding(TextCell.DetailProperty, "Location");

			ActivityIndicator ai = new ActivityIndicator();
			ai.HeightRequest = 50;
			ai.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
			ai.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");

			StackLayout layout = new StackLayout()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { ai, bearsListView }
			};

			Content = layout;

		}
	}
}


