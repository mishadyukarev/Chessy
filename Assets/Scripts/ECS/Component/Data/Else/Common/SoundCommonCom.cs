using UnityEngine;

namespace Assets.Scripts.ECS.Component.Common
{
    internal struct SoundCommonCom
    {
        private static AudioSource _audioSource;

        internal static float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        internal SoundCommonCom(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
    }
}
