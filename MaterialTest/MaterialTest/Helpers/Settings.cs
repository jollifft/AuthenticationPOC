// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaterialTest
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
	public class Settings : INotifyPropertyChanged
	{
		private static ISettings AppSettings
		{
		  get
		  {
		    return CrossSettings.Current;
		  }
		}

		static Settings settings;
		public static Settings Current
		{
			get { return settings ?? (settings = new Settings()); }
		}


		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;


		public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
			}
		}

		const string BearsListCountKey = "bearslistcount_key";
		static readonly int BearsListCountDefault = 0;

		public int BearsListCount
		{
			get
			{
				return AppSettings.GetValueOrDefault<int> (BearsListCountKey, BearsListCountDefault);
			}
			set {
				if (AppSettings.AddOrUpdateValue<int> (BearsListCountKey, value)) {
					OnPropertyChanged ();
				}
			}
		}

		const string UserAuthTokenKey = "userauthtoken_key";
		static readonly string UserAuthTokenDefault = string.Empty;

		public string UserAuthToken
		{
			get
			{
				return AppSettings.GetValueOrDefault<string> (UserAuthTokenKey, UserAuthTokenDefault);
			}
			set {
				if (AppSettings.AddOrUpdateValue<string> (UserAuthTokenKey, value)) {
					OnPropertyChanged ();
				}
			}
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		void OnPropertyChanged([CallerMemberName]string name = "") =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		#endregion

	}
}