﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaterialTest
{
	public class BaseViewModel : INotifyPropertyChanged
	{

		private bool _isBusy;
		private bool _noResults;
		private bool _isConnected;

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged();
			}
		}

		public bool NoResults
		{
			get { return _noResults; }
			set
			{
				if (_noResults != value)
				{
					_noResults = value;
					OnPropertyChanged();
				}
			}
		}

		public bool IsConnected
		{
			get { return _isConnected; }
			set
			{
				_isConnected = value;
				OnPropertyChanged();
			}
		}

		public Settings Settings
		{
			get { return Settings.Current; }
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}

