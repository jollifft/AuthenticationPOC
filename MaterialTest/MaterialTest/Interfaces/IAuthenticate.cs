using System;
using System.Threading.Tasks;

namespace MaterialTest
{
	public interface IAuthenticate
	{
		Task<bool> Authenticate();
	};
}

