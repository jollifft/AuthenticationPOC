using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.App;
using Xamarin.Forms;

namespace MaterialTest.Droid
{
	public class Authenticate : IAuthenticate
	{
		public Authenticate ()
		{
		}

		#region IAuthenticate implementation

		public IPlatformParameters GetParams ()
		{
			return new PlatformParameters ((Activity)Forms.Context);
		}

		#endregion
	}
}

