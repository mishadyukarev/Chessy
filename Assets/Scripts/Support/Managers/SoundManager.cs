using System;
using UnityEngine;

internal class SoundManager
{
    private AudioSource _audioSource;


    internal Action MistakeSoundDelegate;

    internal SoundManager(SupportManager supportManager)
    {
        _audioSource = UnityEngine.Object.Instantiate(supportManager.ResourcesLoadManager.AudioSource);
        MistakeSoundDelegate = MistakeSound;
    }

    private void MistakeSound() => _audioSource.Play();
}
