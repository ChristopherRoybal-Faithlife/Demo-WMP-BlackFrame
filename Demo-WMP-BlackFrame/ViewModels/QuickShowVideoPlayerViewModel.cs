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
			m_iters = 3;
			Dispatcher.Delay(Countdown, TimeSpan.FromSeconds(1));
		}

		public override string SplashText => GetSplashText();

		public Visibility Visibility
		{
			get => m_visibility;
			set => SetPropertyField(nameof(Visibility), (a, b) => a == b, value, ref m_visibility);
		}

		private string GetSplashText()
		{
			if (m_iters > 0)
				return $"Quickshow: Opening, playing, and making visible in {m_iters} seconds...";
			else
				return "Quickshow: Opened and played!";
		}

		private void Countdown()
		{
			SetPropertyField(nameof(SplashText), (x, y) => x == y, m_iters - 1, ref m_iters);

			if (m_iters > 0)
				Dispatcher.Delay(Countdown, TimeSpan.FromSeconds(1));
			else
				ShowIt();
		}

		private void ShowIt()
		{
			Open();
			Play();
			Visibility = Visibility.Visible;
		}

		Visibility m_visibility;
		int m_iters;
	}
}
