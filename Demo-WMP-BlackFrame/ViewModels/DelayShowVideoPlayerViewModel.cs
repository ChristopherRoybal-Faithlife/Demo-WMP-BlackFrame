using Demo_WMP_BlackFrame.Models;
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
			m_iter = c_secondsDelay;
			Dispatcher.Delay(Countdown, TimeSpan.FromSeconds(1));
		}

		public override string SplashText => GetSplashText();

		public Visibility Visibility
		{
			get => m_visibility;
			set => SetPropertyField(nameof(Visibility), (a, b) => a == b, value, ref m_visibility);
		}

		private void Countdown()
		{
			SetPropertyField(nameof(SplashText), (a, b) => a == b, m_iter - 1, ref m_iter);

			if (m_iter > 0)
				Dispatcher.Delay(Countdown, TimeSpan.FromSeconds(1));
			else
				BeginShow();
		}

		private string GetSplashText()
		{
			if (m_iter > 0)
				return $"Delay show: Waiting {m_iter} seconds to open video... then waits {c_beginShowDelayMs}ms to make visible, and then waits {c_showDelayMs}ms to play it!";
			else
				return $"Delay show: Opened video! And then waits {c_beginShowDelayMs}ms to make visible, and then waits {c_showDelayMs}ms to play it!";

		}

		private void BeginShow()
		{
			Open();
			Dispatcher.Delay(Show, TimeSpan.FromMilliseconds(c_beginShowDelayMs));
		}

		private void Show()
		{
			Visibility = Visibility.Visible;
			Dispatcher.Delay(Play, TimeSpan.FromMilliseconds(c_showDelayMs));
		}

		const int c_secondsDelay = 3;
		const int c_beginShowDelayMs = 250;
		const int c_showDelayMs = 1000;
		Visibility m_visibility;
		int m_iter;
	}
}
