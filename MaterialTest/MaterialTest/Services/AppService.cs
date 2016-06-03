using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using MAD.Plugin.MessagingService.Core;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;

namespace MaterialTest
{
	public class AppService
	{
		static IBaaS _baas;
		static IMessagingService _messaging;
		public IMessagingService Messaging
		{
			get { return _messaging; }

		}

		public bool IsInitialized { get; private set; }

		public Settings Settings
		{
			get { return Settings.Current; }
		}

		public AppService(IBaaS baas, IMessagingService messaging)
		{
			_baas = baas;
			_messaging = messaging;
		}

		async Task Init()
		{
			if (IsInitialized)
				return;
			IsInitialized = true;

			//Local storage init
			_baas.DefineTable<Bears>();
			await _baas.InitializeAsync();
		}

		public async Task GetRemoteData()
		{
			try {
				if(Settings.IsLoggedIn)
				{
					await Init();

					await _baas.SyncDataAsync<Bears> ();

					Messaging.PublishMessage<NewDataMessage>(this, new NewDataMessage());
				}
			} catch (Exception ex) {
				string error = ex.Message;
			}
		}

		public async Task GetBears()
		{
			await Init();

			IEnumerable<Bears> bears;
			bears = await _baas.GetDataAsync<Bears> ();
			_messaging.PublishMessage<BearsMessage>(this, new BearsMessage(bears));
		
		}

		public async Task LoginAsync()
		{
			AuthenticationResult result = null;
			Settings.IsLoggedIn = false;

			//if not connected, fail login
			if (await App.ConnectionService.CheckConnection () == false) {
				Messaging.PublishMessage<UserNotificationMessage> (this, new UserNotificationMessage (new UserNotification{ Message = "Login Failed :(" }));
				return;
			}
			
			try
			{
				AuthenticationContext ac = new AuthenticationContext (Constants.Authority);
				result = await ac.AcquireTokenAsync (Constants.ResourceId, Constants.ClientId, new Uri (Constants.RedirectUri), App.Authenticate.GetParams());
				if(result != null)
					Settings.IsLoggedIn = true;

				//sync with mobile service client
				if(Settings.IsLoggedIn == true)
				{
					await _baas.LoginAsync (result.AccessToken);
				}
			}
			catch(Exception ex){
				string message = ex.Message;
			}
			if (Settings.IsLoggedIn == false)
				Messaging.PublishMessage<UserNotificationMessage> (this, new UserNotificationMessage (new UserNotification{ Message = "Login Failed :(" }));

		}

		public async Task LogoutAsync()
		{
			AuthenticationContext ac = new AuthenticationContext(Constants.Authority);
			ac.TokenCache.Clear();
			string requestUrl = $"{Constants.Authority}/oauth2/logout?post_logout_redirect_uri={Constants.RedirectUri}";

			HttpClient client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
			await client.SendAsync(request);
		}
	}
}

