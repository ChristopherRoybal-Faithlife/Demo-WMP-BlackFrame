using Demo_WMP_BlackFrame.Settings;
using Demo_WMP_BlackFrame.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Demo_WMP_BlackFrame.Models
{
	public sealed class VideoPlayerKind
    {
		public string Name => m_name;
		public VideoPlayerViewModel Create(VideoPlayerSettings settings) => m_fnCreator(settings);

		public static VideoPlayerKind SimpleVideoKind = Register("Simple", settings => new SimpleVideoPlayerViewModel(settings));
		public static VideoPlayerKind QuickShowVideoKind = Register("Quick Show", settings => new QuickShowVideoPlayerViewModel(settings));

		public static ReadOnlyCollection<VideoPlayerKind> GetKinds() => s_kinds.AsReadOnly();

		public static VideoPlayerKind GetKindByName(string name) => s_kinds.FirstOrDefault(x => x.Name == name);

		private VideoPlayerKind(string name, Func<VideoPlayerSettings, VideoPlayerViewModel> fnCreator)
		{
			m_name = name;
			m_fnCreator = fnCreator;
		}

		private static VideoPlayerKind Register(string name, Func<VideoPlayerSettings, VideoPlayerViewModel> fnCreator)
		{
			if (s_kinds is null)
				s_kinds = new List<VideoPlayerKind>();

			var kind = new VideoPlayerKind(name, fnCreator);
			s_kinds.Add(kind);
			return kind;
		}

		static List<VideoPlayerKind> s_kinds;
		readonly string m_name;
		readonly Func<VideoPlayerSettings, VideoPlayerViewModel> m_fnCreator;
    }
}
