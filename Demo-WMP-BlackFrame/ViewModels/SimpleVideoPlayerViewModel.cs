using Demo_WMP_BlackFrame.Settings;
using System.Windows;
using System.Windows.Media;

namespace Demo_WMP_BlackFrame.ViewModels
{
    public sealed class SimpleVideoPlayerViewModel : VideoPlayerViewModel
    {
		public SimpleVideoPlayerViewModel()
			: this(null)
		{
		}

		public SimpleVideoPlayerViewModel(VideoPlayerSettings settings)
			: base(settings)
		{
			Open();
			Play();
		}

		public string SplashText => "Simple video player - just opens and plays video!";
    }
}
