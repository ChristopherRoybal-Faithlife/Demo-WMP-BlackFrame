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
	public sealed class QuickShowVideoPlayerViewModel : VideoPlayerViewModel
	{
		public QuickShowVideoPlayerViewModel()
			: this(null)
		{
		}

		public QuickShowVideoPlayerViewModel(VideoPlayerSettings settings)
			: base(settings)
		{
			m_visibility = Visibility.Hidden;
			Dispatcher.Delay(ShowIt, TimeSpan.FromSeconds(3));
		}

		public override string SplashText => "Quick show video player - waits 3 seconds, then opens, plays, and makes the video visible!";

		public Visibility Visibility
		{
			get => m_visibility;
			set => SetPropertyField(nameof(Visibility), (a, b) => a == b, value, ref m_visibility);
		}

		private void ShowIt()
		{
			Open();
			Play();
			Visibility = Visibility.Visible;
		}

		Visibility m_visibility;
	}
}
