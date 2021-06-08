using Demo_WMP_BlackFrame.Settings;
using System;
using System.Windows;
using System.Windows.Media;

namespace Demo_WMP_BlackFrame.ViewModels
{
	public sealed class ShowWhenLoadedVideoPlayerViewModel : VideoPlayerViewModel
	{
		public ShowWhenLoadedVideoPlayerViewModel()
			: this(null)
		{
		}

		public ShowWhenLoadedVideoPlayerViewModel(VideoPlayerSettings settings)
			: base(settings)
		{
			m_visiblity = Visibility.Hidden;
			m_crTimes = c_crWait;
			Player.MediaOpened += Player_MediaOpened;
			Open();
		}

		public Visibility Visibility
		{
			get => m_visiblity;
			set => SetPropertyField(nameof(Visibility), (a, b) => a == b, value, ref m_visiblity);
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				Player.MediaOpened -= Player_MediaOpened;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private void Player_MediaOpened(object sender, EventArgs e)
		{
			Visibility = Visibility.Visible;
			m_start = DateTime.Now;
			CompositionTarget.Rendering += CompositionTarget_Rendering;
		}

		private void CompositionTarget_Rendering(object sender, EventArgs e)
		{
			m_crTimes--;
			if (m_crTimes <= 0)
			{
				m_diff = DateTime.Now - m_start;
				RaisePropertyChanged(nameof(SplashText));
				CompositionTarget.Rendering -= CompositionTarget_Rendering;
				Play();
			}
		}


		public override string SplashText => $"Show when loaded: opens video, waits until loaded, then plays it after {c_crWait} composition target render cycles. Took {m_diff.TotalMilliseconds}ms!";

		const int c_crWait = 2;

		DateTime m_start;
		TimeSpan m_diff;
		int m_crTimes;
		Visibility m_visiblity;
	}
}
