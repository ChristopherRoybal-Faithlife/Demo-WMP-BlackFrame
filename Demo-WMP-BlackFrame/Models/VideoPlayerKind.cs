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
		public static VideoPlayerKind DelayShowVideoKind = Register("Delay Show", settings => new DelayShowVideoPlayerViewModel(settings));
		public static VideoPlayerKind NoDelayRedRectVideoKind = Register("Red Rect no delay", settings => new RedRectVideoPlayerViewModel(settings, RedRectType.ImmediatePlay));
		public static VideoPlayerKind DelayRedRectVideoKind = Register("Red Rect with delay", settings => new RedRectVideoPlayerViewModel(settings, RedRectType.DelayPlay));
		public static VideoPlayerKind DispatchedRedRectVideoKind = Register("Red Rect dispatched", settings => new RedRectVideoPlayerViewModel(settings, RedRectType.DispatchedPlay));
		public static VideoPlayerKind OnRenderRedRectVideoKind = Register("Red Rect CompTarget.Rendering", settings => new RedRectVideoPlayerViewModel(settings, RedRectType.CompositionTargetRenderPlay));
		public static VideoPlayerKind ShowWhenLoadedVideoKind = Register("Show when loaded + CompTarget.Rendering", settings => new ShowWhenLoadedVideoPlayerViewModel(settings));
		public static VideoPlayerKind OpacityVideoKind = Register("Opacity show", settings => new OpacityVideoPlayerViewModel(settings));

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
