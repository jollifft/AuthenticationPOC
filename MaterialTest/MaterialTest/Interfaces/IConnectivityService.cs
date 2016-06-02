using System.Threading.Tasks;

namespace MaterialTest
{
    public interface IConnectivityService
    {
		bool IsConnected { get; }
		bool IsWifiEnabled { get; }
		Task<bool> CheckConnection ();
		Task<bool> CheckWifiEnabled ();
		Task<bool> IsRemoteReachable (string host, int port = 80, int msTimeout = 5000);
    }
}
