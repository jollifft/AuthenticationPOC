using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaterialTest
{
	public static class TaskConnectivityExtensions
	{
		private static IConnectivityService Connectivity { get { return DependencyService.Get<IConnectivityService> ();} }

		//inspired by https://github.com/xamarin/app-crm/blob/master/src/MobileApp/XamarinCRM/Services/DataService.cs

		/// <summary>
		/// Executes the passed task only after verifying an active data connection.
		/// </summary>
		/// <returns>The with connection.</returns>
		/// <param name="execute">Execute.</param>
		public static async Task<bool> ExecuteWithConnection(this Task execute)
		{
			var wasSuccessful = false;
			if (await Connectivity.CheckConnection ()) {
				await execute;
				wasSuccessful = true;
			}
			return wasSuccessful;
		}

		/// <summary>
		/// Executes the passed task only after verifying an active wifi connection.
		/// </summary>
		/// <returns>The with wifi only.</returns>
		/// <param name="execute">Execute.</param>
		public static async Task<bool> ExecuteWithWifiOnly(this Task execute)
		{
			var wasSuccessful = false;
			if (await Connectivity.CheckWifiEnabled () && await Connectivity.CheckConnection ()) {
				await execute;
				wasSuccessful = true;
			}
			return wasSuccessful;
		}
	}
}

