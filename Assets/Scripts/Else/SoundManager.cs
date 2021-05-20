using System;
using UnityEngine;
using static MainGame;

internal class SoundManager
{
    private AudioSource _audioSource;


    internal Action MistakeSoundDelegate;

    internal SoundManager()
    {
        _audioSource = Instance.GameObjectPool.AudioSourceGO.GetComponent<AudioSource>();
        MistakeSoundDelegate = MistakeSound;
    }

    private void MistakeSound() => _audioSource.Play();
}
