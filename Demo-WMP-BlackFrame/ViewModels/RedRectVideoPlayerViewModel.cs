using Demo_WMP_BlackFrame.Models;
using Demo_WMP_BlackFrame.Settings;
using Demo_WMP_BlackFrame.Utility;
using System;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Demo_WMP_BlackFrame.ViewModels
{
	public sealed class RedRectVideoPlayerViewModel : VideoPlayerViewModel
	{
		public RedRectVideoPlayerViewModel()
			: this(null, RedRectType.ImmediatePlay)
		{
		}

		public RedRectVideoPlayerViewModel(VideoPlayerSettings settings, RedRectType redRectType)
			: base(settings)
		{
			m_redVisiblity = Visibility.Visible;
			m_redRectType = redRectType;
			m_cr = c_crStart;
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
			StringBuilder sb = new StringBuilder();
			sb.Append($"Red rect ({m_redRectType}): ");

			if (m_iter > 0)
				sb.Append($"displayed a red rectangle, opened video, and waiting {m_iter} seconds to play video and remove red rect... ");
			else
				sb.Append($"rect removed after {c_waitTimeSeconds} seconds! Video is now playing! ");

			switch (m_redRectType)
			{
			case RedRectType.DelayPlay:
				if (m_iter <= 0)
					sb.Append("Delay to play was 250ms");
				break;
			case RedRectType.ImmediatePlay:
				break;
			case RedRectType.DispatchedPlay:
				break;
			case RedRectType.CompositionTargetRenderPlay:
				if (m_iter <= 0)
					sb.Append($"Video was allowed to play after {c_crStart} CompositionTarget.Renderings!");
				break;
			default:
				break;
			}

			return sb.ToString();
		}

		private void DoPlay()
		{
			RedVisibility = Visibility.Hidden;

			if (m_redRectType == RedRectType.DelayPlay)
				Dispatcher.Delay(Play, TimeSpan.FromMilliseconds(250));
			else if (m_redRectType == RedRectType.ImmediatePlay)
				Play();
			else if (m_redRectType == RedRectType.DispatchedPlay)
				Dispatcher.Delay(Play, TimeSpan.FromTicks(1));
			else if (m_redRectType == RedRectType.CompositionTargetRenderPlay)
				CompositionTarget.Rendering += CompositionTarget_Rendering;
		}

		private void CompositionTarget_Rendering(object sender, EventArgs e)
		{
			m_cr--;
			if (m_cr <= 0)
			{
				CompositionTarget.Rendering -= CompositionTarget_Rendering;
				Play();
			}
		}

		private void Countdown()
		{
			SetPropertyField(nameof(SplashText), (a, b) => a == b, m_iter - 1, ref m_iter);

			if (m_iter > 0)
				Dispatcher.Delay(Countdown, TimeSpan.FromSeconds(1));
			else
				DoPlay();
		}


		const int c_waitTimeSeconds = 1;
		const int c_crStart = 2;
		readonly RedRectType m_redRectType;
		int m_cr;
		int m_iter;
		Visibility m_redVisiblity;
	}
}
