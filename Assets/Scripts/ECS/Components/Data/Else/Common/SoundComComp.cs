﻿using UnityEngine;

namespace Assets.Scripts.ECS.Component.Common
{
    internal struct SoundComComp
    {
        private static AudioSource _audioSource;

        internal static float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        internal static float SavedVolume;

        internal SoundComComp(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
    }
}
