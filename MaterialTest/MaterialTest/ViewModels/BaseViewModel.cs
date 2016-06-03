using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaterialTest
{
	public class BaseViewModel : INotifyPropertyChanged
	{

		private bool _isBusy;
		private bool _noResults;
		private bool _isConnected;
		private UserNotification _userNotification;
		private Color _userNotificationColor;

		public BaseViewModel()
		{
			App.AppService.Messaging.SubscribeMessage<UserNotificationMessage>(this, args => UserNotification = args.UserNotification);
		}

		#region properties

		public UserNotification UserNotification {
			get { return _userNotification; }
			set
			{
				_userNotification = value;
				if (string.IsNullOrEmpty (value.Color)) {
					value.Color = Constants.USER_NOTIFICATION_BACKGROUND_COLOR;
				}
				UserNotificationColor = Color.FromHex(value.Color);
				OnPropertyChanged ();
			}
		}

		public Color UserNotificationColor
		{
			get { return _userNotificationColor; }
			set {
				_userNotificationColor = value;
				OnPropertyChanged ();
			}
		}

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged();
			}
		}

		public bool NoResults
		{
			get { return _noResults; }
			set
			{
				if (_noResults != value)
				{
					_noResults = value;
					OnPropertyChanged();
				}
			}
		}

		public bool IsConnected
		{
			get { return _isConnected; }
			set
			{
				_isConnected = value;
				OnPropertyChanged();
			}
		}

		#endregion

		public Settings Settings
		{
			get { return Settings.Current; }
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}

