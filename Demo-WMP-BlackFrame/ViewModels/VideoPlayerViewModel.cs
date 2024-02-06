using Demo_WMP_BlackFrame.Settings;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace Demo_WMP_BlackFrame.ViewModels
{
    public abstract class VideoPlayerViewModel : ViewModelBase
    {
		public VideoPlayerViewModel(VideoPlayerSettings settings)
		{
			m_settings = settings ?? throw new ArgumentNullException(nameof(settings));
			m_player = new MediaPlayer();
			m_player.ScrubbingEnabled = true;
		}

		public void Open()
		{
			var filePath = Path.GetFullPath(m_settings.VideoFilePath);

			m_player.Open(new Uri(filePath));
			m_player.Position = TimeSpan.FromTicks(1);
			m_player.Pause();
		}

		public void Play() => m_player.Play();
		public void Pause() => m_player.Pause();

		public MediaPlayer Player => m_player;

		public Rect NaturalRect => new Rect(new Size(1920, 1080));

		public abstract string SplashText { get; }

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
					m_player.Close();
			}
			finally
			{
				base.Dispose(disposing);
			}
		}


		readonly MediaPlayer m_player;
		readonly VideoPlayerSettings m_settings;
    }
}
