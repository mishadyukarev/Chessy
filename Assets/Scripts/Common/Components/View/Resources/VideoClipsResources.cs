using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Scripts.Common
{
    public struct VideoClipsResources
    {
        private static Dictionary<VideoClipTypes, VideoClip> _videoClips;

        internal VideoClipsResources(bool needUpload) : this()
        {
            if (needUpload)
            {
                _videoClips = new Dictionary<VideoClipTypes, VideoClip>();

                var nameVideoClips = "VideoClips/";

                _videoClips.Add(VideoClipTypes.Start, Resources.Load<VideoClip>(nameVideoClips + "Start"));
                _videoClips.Add(VideoClipTypes.BuldFarms, Resources.Load<VideoClip>(nameVideoClips + "Farm"));
                _videoClips.Add(VideoClipTypes.SeedFire, Resources.Load<VideoClip>(nameVideoClips + "FireSeed"));
                _videoClips.Add(VideoClipTypes.Woodcutter, Resources.Load<VideoClip>(nameVideoClips + "Woodcutter"));
                _videoClips.Add(VideoClipTypes.MineTools, Resources.Load<VideoClip>(nameVideoClips + "Mine"));
                _videoClips.Add(VideoClipTypes.Vision, Resources.Load<VideoClip>(nameVideoClips + "Vision"));
                _videoClips.Add(VideoClipTypes.SpeciesAttack, Resources.Load<VideoClip>(nameVideoClips + "SpeciesAttack"));
            }
        }

        public static VideoClip VideoClip(VideoClipTypes videoClipType) => _videoClips[videoClipType];
    }
}
