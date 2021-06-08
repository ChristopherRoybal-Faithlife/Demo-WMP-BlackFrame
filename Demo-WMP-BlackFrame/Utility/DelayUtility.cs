using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Demo_WMP_BlackFrame.Utility
{
	public static class DelayUtility
    {
        public static void Delay(this Dispatcher dispatcher, Action action, TimeSpan timeSpan)
		{
            Task.Delay(timeSpan).ContinueWith(t => dispatcher.Invoke(action));
		}
    }
}
