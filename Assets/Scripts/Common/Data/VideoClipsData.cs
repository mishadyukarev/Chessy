using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts.Abstractions.Data
{
    [CreateAssetMenu(menuName = "VideoClipsData", fileName = "VideoClipsData")]
    public sealed class VideoClipsData : ScriptableObject
    {
        [SerializeField] private VideoClip _start_VC = default;
        [SerializeField] private VideoClip _building_VC = default;

        public VideoClip Start_VC => _start_VC;
        public VideoClip Building_VC => _building_VC;
    }
}
