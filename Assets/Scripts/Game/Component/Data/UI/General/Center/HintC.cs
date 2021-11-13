using Chessy.Common;
using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct HintC
    {
        private static Dictionary<VideoClipTypes, bool> _wasActivated;
        public static int CurStartNumber;

        public static bool WasActived(VideoClipTypes video)
        {
            if (!_wasActivated.ContainsKey(video)) throw new Exception();
            return _wasActivated[video];
        }

        public HintC(Dictionary<VideoClipTypes, bool> hint)
        {
            CurStartNumber = 1;
            _wasActivated = hint;

            for (var video = (VideoClipTypes)1; video < (VideoClipTypes)typeof(VideoClipTypes).GetEnumNames().Length; video++)
            {
                _wasActivated.Add(video, false);
            }
        }

        public static void SetWasActived(VideoClipTypes video, bool isActive)
        {
            if (!_wasActivated.ContainsKey(video)) throw new Exception();
            _wasActivated[video] = isActive;
        }
    }
}