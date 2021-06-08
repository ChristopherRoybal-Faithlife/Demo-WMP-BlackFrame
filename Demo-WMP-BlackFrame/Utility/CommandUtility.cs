using System;
using System.Windows.Input;

namespace Demo_WMP_BlackFrame.Utility
{
	public class CommandUtility : ICommand
	{
		public static ICommand Register(Func<bool> fnCanExecute, Action fnExecute)
		{
			return new CommandUtility(fnCanExecute, fnExecute);
		}

		private CommandUtility(Func<bool> fnCanExecute, Action fnExecute)
		{
			m_fnCanExecute = fnCanExecute;
			m_fnExecute = fnExecute;
		}

		public event EventHandler CanExecuteChanged = (sender, e) => { };

		public bool CanExecute(object parameter)
		{
			return m_fnCanExecute();
		}

		public void Execute(object parameter)
		{
			m_fnExecute();
		}

		readonly Func<bool> m_fnCanExecute;
		readonly Action m_fnExecute;
	}
}
