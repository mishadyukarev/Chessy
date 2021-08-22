using UnityEngine;

namespace Assets.Scripts.ECS.Component.Common
{
    internal struct SoundCommComp
    {
        private static AudioSource _audioSource;

        internal static float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        internal SoundCommComp(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
    }
}
