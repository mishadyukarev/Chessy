using UnityEngine;

namespace Assets.Scripts.ECS.Component.Common
{
    internal struct SoundComComp
    {
        private AudioSource _audioSource;

        internal float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        internal SoundComComp(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
    }
}
