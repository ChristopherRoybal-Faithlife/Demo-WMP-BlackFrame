using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Demo_WMP_BlackFrame.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
	{
		public ViewModelBase()
		{
			Dispatcher = Dispatcher.CurrentDispatcher;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

		protected virtual void Dispose(bool disposing)
		{
		}

		protected void RaisePropertyChanged(params string[] props)
		{
			foreach (var prop in props)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		protected bool SetPropertyField<T>(string propName, Func<T, T, bool> fnEqual, T value, ref T field)
		{
			return SetPropertyField(new[] { propName }, fnEqual, value, ref field);
		}

		protected bool SetPropertyField<T>(string[] propNames, Func<T, T, bool> fnEqual, T value, ref T field)
		{
			if (fnEqual(value, field))
				return false;

			field = value;

			foreach (var prop in propNames)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));

			return true;
		}

		protected Dispatcher Dispatcher { get; }
	}
}
