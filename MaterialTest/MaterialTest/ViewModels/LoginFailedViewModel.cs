using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialTest
{
	public class LoginFailedViewModel : BaseViewModel
	{
		private INavigation Nav;

		private ICommand retryLogin;
		public ICommand RetryLoginCommand
		{
			get { return retryLogin ?? (retryLogin = new Command(async () => await TryToLogin()));}
		}

		private async Task TryToLogin ()
		{
			bool successful = await base.LoginAsync();
			if (successful)
				await Nav.PopModalAsync ();
		}

		public LoginFailedViewModel (INavigation nav)
		{
			Nav = nav;
			IsBusy = false;
		}
	}
}

