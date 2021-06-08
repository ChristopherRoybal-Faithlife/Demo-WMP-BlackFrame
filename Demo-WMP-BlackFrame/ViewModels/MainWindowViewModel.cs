using Demo_WMP_BlackFrame.Models;
using Demo_WMP_BlackFrame.Settings;
using Demo_WMP_BlackFrame.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Demo_WMP_BlackFrame.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
		public MainWindowViewModel()
		{
			m_settings = SettingsUtility.Get<VideoPlayerSettings>(@"Config\VideoPlayerSettings.json");
			m_kinds = VideoPlayerKind.GetKinds();
			m_selectedKind = m_kinds.First();
			m_videoPlayerViewModel = m_selectedKind.Create(m_settings);
		}

		public VideoPlayerViewModel VideoPlayerViewModel
		{
			get => m_videoPlayerViewModel;
			set
			{
				var oldValue = m_videoPlayerViewModel;
				if (SetPropertyField(nameof(VideoPlayerViewModel), ReferenceEquals, value, ref m_videoPlayerViewModel))
					oldValue?.Dispose();
			}
		}

		public VideoPlayerKind SelectedKind
		{
			get => m_selectedKind;
			set
			{
				if (SetPropertyField(nameof(SelectedKind), ReferenceEquals, value, ref m_selectedKind))
					VideoPlayerViewModel = m_selectedKind.Create(m_settings);
			}
		}

		public ReadOnlyCollection<VideoPlayerKind> VideoPlayerKinds => m_kinds;

		public ICommand RefreshCommand
		{
			get
			{
				if (m_refreshCommand is null)
					m_refreshCommand = CommandUtility.Register(CanRefresh, DoRefresh);

				return m_refreshCommand;
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				VideoPlayerViewModel = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private void DoRefresh()
		{
			VideoPlayerViewModel = SelectedKind.Create(m_settings);
		}

		private bool CanRefresh()
		{
			return SelectedKind is object;
		}

		readonly ReadOnlyCollection<VideoPlayerKind> m_kinds;
		readonly VideoPlayerSettings m_settings;

		ICommand m_refreshCommand;
		VideoPlayerKind m_selectedKind;
		VideoPlayerViewModel m_videoPlayerViewModel;
	}
}
