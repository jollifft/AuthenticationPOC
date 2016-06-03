using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;

namespace MaterialTest.iOS
{
	public class Authenticate : IAuthenticate
	{
		public Authenticate ()
		{
		}

		#region IAuthenticate implementation

		public IPlatformParameters GetParams ()
		{
			return new PlatformParameters (UIApplication.SharedApplication.KeyWindow.RootViewController);
		}

		#endregion
	}
}

