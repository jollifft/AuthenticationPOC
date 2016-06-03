using Xamarin.Forms;

namespace MaterialTest
{
    class UserNotificationView : StackLayout
    {
		public UserNotificationView() //TODO: Do we still want/need the ability to throw static text at this guy instead of using textBinding?
        {
			var notification = new Label()
            {
				TextColor = Color.FromHex (Constants.USER_NOTIFICATION_TEXT_COLOR),
				HeightRequest = Constants.USER_NOTIFICATION_HEIGHT,
				TranslationY = Constants.USER_NOTIFICATION_HEIGHT * -1, //hidden by default
				IsVisible = false, //hidden by default
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.Center,
                FontSize = 13,
            };
			notification.SetBinding (Label.TextProperty, new Binding ("UserNotification", BindingMode.Default, new UserNotificationAnimation (), notification));
			notification.SetBinding (Label.BackgroundColorProperty, "UserNotificationColor");
            base.Orientation = StackOrientation.Vertical;
            base.Spacing = 0;
            base.Children.Add(notification);
        }
    }
}
