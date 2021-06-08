using Demo_WMP_BlackFrame.Settings;
using Demo_WMP_BlackFrame.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Demo_WMP_BlackFrame.ViewModels
{
	public sealed class DelayShowVideoPlayerViewModel : VideoPlayerViewModel
	{
		public DelayShowVideoPlayerViewModel()
			:base(null)
		{
		}

		public DelayShowVideoPlayerViewModel(VideoPlayerSettings settings)
			: base(settings)
		{
			m_visibility = Visibility.Hidden;
			Dispatcher.Delay(BeginShow, TimeSpan.FromSeconds(3));
		}

		public override string SplashText => "Waits 3 seconds, opens and starts video, waits 250ms, and then makes visible!";

		public Visibility Visibility
		{
			get => m_visibility;
			set => SetPropertyField(nameof(Visibility), (a, b) => a == b, value, ref m_visibility);
		}

		private void BeginShow()
		{
			Open();
			Play();
			Dispatcher.Delay(Show, TimeSpan.FromMilliseconds(250));
		}

		private void Show()
		{
			Visibility = Visibility.Visible;
		}

		Visibility m_visibility;
	}
}
