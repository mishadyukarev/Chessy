using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Game.Game
{
    public struct VideoClipsResC
    {
        private static Dictionary<VideoClipTypes, VideoClip> _videoClips;

        public VideoClipsResC(bool needUpload) : this()
        {
            if (needUpload)
            {
                _videoClips = new Dictionary<VideoClipTypes, VideoClip>();

                var nameVideoClips = "VideoClips/";

                _videoClips.Add(VideoClipTypes.Start, Resources.Load<VideoClip>(nameVideoClips + "Start"));
                _videoClips.Add(VideoClipTypes.Start2, Resources.Load<VideoClip>(nameVideoClips + "Start2"));
                _videoClips.Add(VideoClipTypes.Start3, Resources.Load<VideoClip>(nameVideoClips + "Start3"));
                _videoClips.Add(VideoClipTypes.Start4, Resources.Load<VideoClip>(nameVideoClips + "Start4"));
                _videoClips.Add(VideoClipTypes.BuldFarms, Resources.Load<VideoClip>(nameVideoClips + "Farm"));
                _videoClips.Add(VideoClipTypes.SeedFire, Resources.Load<VideoClip>(nameVideoClips + "FireSeed"));
                _videoClips.Add(VideoClipTypes.Woodcutter, Resources.Load<VideoClip>(nameVideoClips + "Woodcutter"));
                _videoClips.Add(VideoClipTypes.BuildMine, Resources.Load<VideoClip>(nameVideoClips + "Mine"));
                _videoClips.Add(VideoClipTypes.Vision, Resources.Load<VideoClip>(nameVideoClips + "Vision"));
                _videoClips.Add(VideoClipTypes.SpeciesAttack, Resources.Load<VideoClip>(nameVideoClips + "SpeciesAttack"));
                _videoClips.Add(VideoClipTypes.ProtRelax, Resources.Load<VideoClip>(nameVideoClips + "ProtRelax"));
                _videoClips.Add(VideoClipTypes.CircularAttack, Resources.Load<VideoClip>(nameVideoClips + "CircularAttack"));
                _videoClips.Add(VideoClipTypes.BonusKing, Resources.Load<VideoClip>(nameVideoClips + "BonusKing"));
                _videoClips.Add(VideoClipTypes.UpgToolWeapon, Resources.Load<VideoClip>(nameVideoClips + "UpgToolWeapon"));
                _videoClips.Add(VideoClipTypes.Pick, Resources.Load<VideoClip>(nameVideoClips + "Pick"));
            }
        }

        public static VideoClip VideoClip(VideoClipTypes videoClipType) => _videoClips[videoClipType];
    }
}
