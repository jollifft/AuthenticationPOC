using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MaterialTest
{
	public interface IAuthenticate
	{
		IPlatformParameters GetParams ();
	}
}

