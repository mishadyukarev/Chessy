using UnityEngine;
using UnityEngine.Video;

namespace Chessy.Common.View.UI.Component
{
    public struct VideoPlayerVC
    {
        public readonly VideoPlayer VP;

        public Transform Transform => VP.transform;
        public GameObject GO => VP.gameObject;

        internal VideoPlayerVC(in VideoPlayer videoPlayer) => VP = videoPlayer;

        public void SetVideoClip(in string url) => VP.url = url;
    }
}