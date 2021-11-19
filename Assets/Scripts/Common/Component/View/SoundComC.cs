using UnityEngine;

namespace Game.Common
{
    public struct SoundComC
    {
        private static AudioSource _audioSource;

        public static float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        public static float SavedVolume;

        public SoundComC(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
    }
}
