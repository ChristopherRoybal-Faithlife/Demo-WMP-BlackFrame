using Demo_WMP_BlackFrame.Models;
using Demo_WMP_BlackFrame.Settings;
using Demo_WMP_BlackFrame.Utility;

namespace Demo_WMP_BlackFrame.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
		public MainWindowViewModel()
		{
			var settings = SettingsUtility.Get<VideoPlayerSettings>(@"Config\VideoPlayerSettings.json");
			m_videoPlayerViewModel = VideoPlayerKind.QuickShowVideoKind.Create(settings);
		}

		public VideoPlayerViewModel VideoPlayerViewModel => m_videoPlayerViewModel;

		readonly VideoPlayerViewModel m_videoPlayerViewModel;
    }
}
