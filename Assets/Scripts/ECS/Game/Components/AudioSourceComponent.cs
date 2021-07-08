using UnityEngine;

namespace Assets.Scripts.ECS.Game.Components
{
    internal struct AudioSourceComponent
    {
        private AudioSource _audioSource;

        internal void StartFill(AudioSource audioSource) => _audioSource = audioSource;
        internal void Play() => _audioSource.Play();
    }











    




}
