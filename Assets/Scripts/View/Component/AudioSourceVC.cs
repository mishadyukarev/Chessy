using UnityEngine;

namespace Chessy.Common.Component
{
    public struct AudioSourceVC
    {
        public AudioSource AS;

        public bool IsPlaying => AS.isPlaying;

        public AudioSourceVC(in AudioSource audioSource) => AS = audioSource;

        public void Play() => AS.Play();
    }
}