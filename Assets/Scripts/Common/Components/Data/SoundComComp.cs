using UnityEngine;

namespace Assets.Scripts.ECS.Component.Common
{
    public struct SoundComComp
    {
        private static AudioSource _audioSource;

        public static float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        public static float SavedVolume;

        public SoundComComp(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
    }
}
