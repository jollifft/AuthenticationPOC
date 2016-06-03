using System;

namespace MaterialTest
{
	public static class Constants
	{
		public static string Authority = "https://login.windows.net/tylerjolliffkbslp.onmicrosoft.com";
		public static string ResourceId = "023bf722-9c1e-47da-89f5-d4054dc5540a";
		public static string ClientId = "8644b90c-962d-456c-8788-5b94f3bfb80c";
		public static string RedirectUri = "http://azureauthbackend.azurewebsites.net/.auth/login/done";
		public static string BackendUrl = "https://azureauthbackend.azurewebsites.net";

		#region Connectivity Checking
		//REMOTE_* are for Xam.Plugin.Connectivity
		public static readonly string REMOTE_HOST = "google.com"; //remote host you wish to use for IsConnected status
		public static readonly int REMOTE_PORT = 80;
		public static readonly int REMOTE_TIMEOUT_MS = 5000;
		#endregion

		#region User Notification 
		public static readonly string USER_NOTIFICATION_BACKGROUND_COLOR = "e1e1e1";
		public static readonly string USER_NOTIFICATION_TEXT_COLOR = "000";
		public static readonly string USER_NOTIFICATION_MSG = "UserNotification";
		public static readonly double USER_NOTIFICATION_HEIGHT = 143; 

		#endregion
	}
}

