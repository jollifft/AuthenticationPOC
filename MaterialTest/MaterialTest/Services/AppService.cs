using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAD.Plugin.BaaS.Core;
using Xamarin.Forms;
using MAD.Plugin.MessagingService.Core;

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

		public AppService(IBaaS baas, IMessagingService messaging)
		{
			_baas = baas;
			_messaging = messaging;

			Task.Run(async () => await Init());
		}

		async Task Init()
		{
			//Local storage init
			_baas.DefineTable<Bears>();
			await _baas.InitializeAsync();
			
			//Local store end

			await GetLocalData(); //load up local data
			await GetRemoteData(); //pull any new data from IBaaS
		}

		public async Task GetRemoteData()
		{
			try {
				await _baas.SyncDataAsync<Bears> ();
			} catch (Exception ex) {
				
			}
			await GetLocalData();
		}

		public async Task GetLocalData()
		{
			await GetBears();
		}

		public async Task GetBears()
		{
			IEnumerable<Bears> bears;
			try {
				bears = await _baas.GetDataAsync<Bears> ();
			} catch (Exception ex) {
				bears = default(IEnumerable<Bears>);
			}
			_messaging.PublishMessage<BearsMessage>(this, new BearsMessage(bears));
			//MessagingCenter.Send<AppService, IEnumerable<Bears>>(this, "BearMessage", bears);
		
		}
	}
}

