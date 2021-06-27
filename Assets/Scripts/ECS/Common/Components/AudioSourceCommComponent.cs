using UnityEngine;

namespace Assets.Scripts
{
    internal struct AudioSourceCommComponent
    {
        private AudioSource _audioSource;

        internal float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        internal bool Loop
        {
            get => _audioSource.loop;
            set => _audioSource.loop = value;
        }

        internal void SetAudioSource(AudioSource audioSource) => _audioSource = audioSource;
        internal void SetClip(AudioClip audioClip) => _audioSource.clip = audioClip;
        internal void Play() => _audioSource.Play();
    }
}