using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace MaterialTest
{
	public class UserNotificationAnimation : IValueConverter
	{
		Label _label;
		UserNotification _notification;

		#region IValueConverter implementation
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null) {
				return value;
			}
			_label = (Label)parameter;
			_notification = (UserNotification)value;

			LabelAnimation ();

			return _notification.Message;
		}
		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}
		#endregion

		public async Task LabelAnimation()
		{
			double finish;
			bool isVisible;

			if (string.IsNullOrEmpty (_notification.Message)) { //label is currently visible, this will push it off the screen
				finish = Constants.USER_NOTIFICATION_HEIGHT * -1;
				isVisible = false;
			} else {
				finish = 0;
				isVisible = true;
				_label.IsVisible = isVisible; //make it visible here so we see the transition
			}

			await _label.TranslateTo (0, finish, 1000, Easing.CubicOut);
			_label.IsVisible = isVisible;
		}
		
	}
}

