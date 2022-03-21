﻿using UnityEngine;

namespace Chessy.Common.Component
{
    public struct AudioSourceVC
    {
        public AudioSource AudioSource;

        public bool IsPlaying => AudioSource.isPlaying;

        public AudioSourceVC(in AudioSource audioSource) => AudioSource = audioSource;

        public void Play() => AudioSource.Play();
    }
}