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
		IBaaS _baas;
		static IMessagingService _messaging;
		public static IMessagingService Messaging
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

			//Task.Run(async () => await Init());
		}

		async Task Init()
		{
			if (IsInitialized)
				return;
			IsInitialized = true;

			//Local storage init
			_baas.DefineTable<Bears>();
			await _baas.InitializeAsync();
			
			//Local store end

			//await GetLocalData(); //load up local data
			await GetRemoteData(); //pull any new data from IBaaS
		}

		public async Task GetRemoteData()
		{
			try {
				await _baas.SyncDataAsync<Bears> ();
			} catch (Exception ex) {
				
			}
			//await GetLocalData();
		}

		public async Task GetLocalData()
		{
			await GetBears();
		}

		public async Task GetBears()
		{
			await Init();

			IEnumerable<Bears> bears;
			bears = await _baas.GetDataAsync<Bears> ();
			_messaging.PublishMessage<BearsMessage>(this, new BearsMessage(bears));
		
		}

		public async Task<bool> LoginAsync()
		{
			AuthenticationResult result = null;
			bool successful = false;
			try
			{
				AuthenticationContext ac = new AuthenticationContext (Constants.Authority);
				result = await ac.AcquireTokenAsync (Constants.ResourceId, Constants.ClientId, new Uri (Constants.RedirectUri), App.Params);
				successful = true;

				//check to see if we need to sync MobileServiceClient
				if(Settings.UserAuthToken != result.AccessToken)
				{
					Settings.UserAuthToken = result.AccessToken;
					await _baas.LoginAsync (result.AccessToken);
				}
			}
			catch(Exception ex){

			}
				
			return successful;
		}

		public async Task LogoutAsync()
		{
			AuthenticationContext ac = new AuthenticationContext(Constants.Authority);
			ac.TokenCache.Clear();
			string requestUrl = $"{Constants.Authority}/oauth2/logout?post_logout_redirect_uri={Constants.RedirectUri}";

			HttpClient client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
			var response = await client.SendAsync(request);
		}
	}
}

