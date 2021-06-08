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
	public sealed class RedRectVideoPlayerViewModel : VideoPlayerViewModel
	{
		public RedRectVideoPlayerViewModel()
			: this(null)
		{
		}

		public RedRectVideoPlayerViewModel(VideoPlayerSettings settings)
			: base(settings)
		{
			m_redVisiblity = Visibility.Visible;
			Open();
			m_iter = c_waitTimeSeconds;
			Dispatcher.Delay(Countdown, TimeSpan.FromSeconds(1));
		}
		public override string SplashText => GetSplashText();

		public Visibility RedVisibility
		{
			get => m_redVisiblity;
			set => SetPropertyField(nameof(RedVisibility), (a, b) => a == b, value, ref m_redVisiblity);
		}

		private string GetSplashText()
		{
			if (m_iter > 0)
				return $"Red rect: displayed a red rectangle, opened video, and waiting {m_iter} seconds to play video and remove red rect...";
			else
				return $"Red rect removed after {c_waitTimeSeconds} seconds! Video is now playing!";
		}

		private void DoPlay()
		{
			RedVisibility = Visibility.Hidden;
			Play();
		}

		private void Countdown()
		{
			SetPropertyField(nameof(SplashText), (a, b) => a == b, m_iter - 1, ref m_iter);

			if (m_iter > 0)
				Dispatcher.Delay(Countdown, TimeSpan.FromSeconds(1));
			else
				DoPlay();
		}


		const int c_waitTimeSeconds = 5;
		int m_iter;
		Visibility m_redVisiblity;
	}
}
