using UnityEngine;

namespace Game.Game
{
    public readonly struct AudioSourceVC : ISoundE
    {
        readonly AudioSource _audioSource;

        public bool IsPlaying => _audioSource.isPlaying;

        public AudioSourceVC(in AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void Play() => _audioSource.Play();
    }
}