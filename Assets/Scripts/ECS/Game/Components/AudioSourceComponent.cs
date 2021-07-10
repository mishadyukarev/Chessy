using UnityEngine;

namespace Assets.Scripts.ECS.Game.Components
{
    internal struct AudioSourceComponent
    {
        private AudioSource _audioSource;

        internal bool IsPlaying => _audioSource.isPlaying;

        internal void StartFill(AudioSource audioSource) => _audioSource = audioSource;
        internal void Play() => _audioSource.Play();
    }
}
