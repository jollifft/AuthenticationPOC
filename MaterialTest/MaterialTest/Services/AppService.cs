using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using MAD.Plugin.MessagingService.Core;

namespace MaterialTest
{
	public class AppService
	{
		static IBaaS _baas;
		public IBaaS BaaS 
		{
			get { return _baas; }
		}

		static IMessagingService _messaging;
		public static IMessagingService Messaging
		{
			get { return _messaging; }

		}

		public bool IsInitialized { get; private set; }

		public AppService(IBaaS baas, IMessagingService messaging)
		{
			_baas = baas;
			_messaging = messaging;

			//Task.Run(async () => await Init());
		}

		public async Task Init()
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
			await Init ();
			try 
			{
				await _baas.SyncDataAsync<Bears> ();
			} 
			catch (Exception ex) {
				
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
			//await GetRemoteData ();

			IEnumerable<Bears> bears;
			bears = await _baas.GetDataAsync<Bears> ();
			_messaging.PublishMessage<BearsMessage>(this, new BearsMessage(bears));
		
		}
	}
}

