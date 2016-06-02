using System;
using System.Threading.Tasks;
using Plugin.Connectivity.Abstractions;
using Plugin.Connectivity;
using System.Threading;
using System.Linq;

namespace MaterialTest
{
	public class ConnectivityService : IConnectivityService
	{
		private IConnectivity _connectivity = CrossConnectivity.Current;
		private bool _isConnected;
		private bool _isWifiEnabled;

		public ConnectivityService ()
		{
			//TODO: do we need any of these 3? or should we only care about connectivity when we NEED connectivity
			_connectivity.ConnectivityChanged += ConnectivityChanged; //TODO: do we really need this? not sure what value "live" connectivity updates would provide unless we are currently making a call
			CheckConnection ();
			CheckWifiEnabled ();
		}

		/// <summary>
		/// Event triggers when connectivity on the device changes e.g. wifi on/off cellular network changes etc.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void ConnectivityChanged (object sender, ConnectivityChangedEventArgs e)
		{
			CheckConnection ();
			CheckWifiEnabled ();
		}

		public bool IsConnected { 
			get 
			{
				return _isConnected;
			}
			private set 
			{
				if (value != _isConnected) {
					_isConnected = value;
				}
			}
		}

		public bool IsWifiEnabled { 
			get
			{
				return _isWifiEnabled;
			}
			private set
			{
				if (value != _isWifiEnabled) {
					_isWifiEnabled = value;
				}
			}
		}

		#region IConnectivity implementation

		public async Task<bool> CheckConnection ()
		{
//			var isConnected = Task.Run(async () => await IsRemoteReachable (Constants.REMOTE_HOST, Constants.REMOTE_PORT, Constants.REMOTE_TIMEOUT_MS)).Result;
			var isConnected = await IsRemoteReachable (Constants.REMOTE_HOST, Constants.REMOTE_PORT, Constants.REMOTE_TIMEOUT_MS);

			IsConnected = isConnected;
			return isConnected;
		}

		public async Task<bool> CheckWifiEnabled ()
		{
			var result = false;
			var connectionTypes = _connectivity.ConnectionTypes.ToList ();
			if (connectionTypes.Contains (ConnectionType.WiFi)) {
				result = true;
			}
			IsWifiEnabled = result;
			return result;
		}

		public async Task<bool> IsRemoteReachable (string host, int port = 80, int msTimeout = 5000)
		{
			var result = false;
			var cts = new CancellationTokenSource ();
			if(_connectivity.IsConnected){
				var task = new Task<bool> (() => _connectivity.IsRemoteReachable (host, port, msTimeout).Result);
				task.Start ();
				if (await Task.WhenAny(task, Task.Delay (msTimeout, cts.Token)) == task && task.Status == TaskStatus.RanToCompletion) {
					result = task.Result;
					cts.Cancel ();
				}
			}
			return result;
		}

		#endregion
	}
}

