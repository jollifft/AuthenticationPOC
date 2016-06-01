using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using MAD.Plugin.MessagingService.Core;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MaterialTest
{
	public class AppService
	{
		IBaaS _baas;
		IPlatformParameters Params;
		static IMessagingService _messaging;
		public static IMessagingService Messaging
		{
			get { return _messaging; }

		}

		public bool IsInitialized { get; private set; }

		public AppService(IBaaS baas, IMessagingService messaging, IPlatformParameters pParams)
		{
			_baas = baas;
			_messaging = messaging;
			Params = pParams;

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
			//"023bf722-9c1e-47da-89f5-d4054dc5540a"
			AuthenticationContext ac = new AuthenticationContext ("https://login.windows.net/tylerjolliffkbslp.onmicrosoft.com");
			AuthenticationResult result;
			try {
				result = await ac.AcquireTokenAsync ("https://graph.windows.net", "8644b90c-962d-456c-8788-5b94f3bfb80c", 
					new Uri ("http://azureauthbackend.azurewebsites.net/.auth/login/done"), Params);
			} catch (Exception ex) {
				result = default(AuthenticationResult);
			}

			bool successful = await _baas.LoginAsync (result.AccessToken);
			return successful;
			
		}
	}
}

