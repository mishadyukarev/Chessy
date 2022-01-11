﻿using UnityEngine.Video;

namespace Game.Game
{
    public readonly struct VideoPlayerVC
    {
        readonly VideoPlayer _vP;

        public VideoPlayerVC(in VideoPlayer vP) => _vP = vP;

        public void SetVideoClip(in string url)
        {
            _vP.url = url;
        }
    }
}