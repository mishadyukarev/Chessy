using System;
using UnityEngine;

internal class SoundManager
{
    private AudioSource _audioSource;


    internal Action MistakeSoundDelegate;

    internal SoundManager(StartSpawnGame startSpawnManager)
    {
        _audioSource = startSpawnManager.AudioSource;
        MistakeSoundDelegate = MistakeSound;
    }

    private void MistakeSound() => _audioSource.Play();
}
