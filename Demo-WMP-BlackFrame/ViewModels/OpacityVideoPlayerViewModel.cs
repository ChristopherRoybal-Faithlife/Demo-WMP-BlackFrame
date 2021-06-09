using Demo_WMP_BlackFrame.Settings;
using Demo_WMP_BlackFrame.Utility;
using System;

namespace Demo_WMP_BlackFrame.ViewModels
{
	public sealed class OpacityVideoPlayerViewModel : VideoPlayerViewModel
	{
		public OpacityVideoPlayerViewModel()
			: base(null)
		{
		}

		public OpacityVideoPlayerViewModel(VideoPlayerSettings settings)
			: base(settings)
		{
			m_iters = c_iters;
			m_opacity = 0.0d;
			Open();
			Play();
			Dispatcher.Delay(Countdown, TimeSpan.FromMilliseconds(250));
		}

		public double Opacity
		{
			get => m_opacity;
			private set => SetPropertyField(nameof(Opacity), (a, b) => a == b, value, ref m_opacity);
		}

		public override string SplashText => $"Opens and plays video, waiting {m_iters * 250}ms to change opacity...";

		private void Countdown()
		{
			SetPropertyField(nameof(SplashText), (a, b) => a == b, m_iters - 1, ref m_iters);
			if (m_iters > 0)
				Dispatcher.Delay(Countdown, TimeSpan.FromMilliseconds(250));
			else
				Opacity = 1.0d;
		}

		const int c_iters = 2;
		int m_iters;
		double m_opacity;
	}
}
