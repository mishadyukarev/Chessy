using System;
using UnityEngine;
using UnityEngine.UI;

internal sealed class SoundManager
{
    private SaverData _saverData;
    private Slider _slider;
    private AudioSource _musicAudioSource;

    internal SoundManager(ResourcesLoad resourcesLoad, Builder builder, SaverData saverData)
    {
        _saverData = saverData;

        _musicAudioSource = builder.CreateGameObject("MusicAudioSource", new Type[] { typeof(AudioSource) }).GetComponent<AudioSource>();
        _musicAudioSource.clip = resourcesLoad.SoundConfig.MusicAudioClip;
        _musicAudioSource.volume = 0.2f;
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }

    internal void ToggleScene(SceneTypes sceneType, GameObject inMenuZoneCanvasGO)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                _slider = inMenuZoneCanvasGO.transform.Find("Slider").GetComponent<Slider>();
                break;

            case SceneTypes.Game:
                break;

            default:
                break;
        }
    }

    internal void SyncValues()
    {
        if (_slider.value != _saverData.SliderVolume)
        {
            _saverData.SliderVolume = _slider.value;
            _musicAudioSource.volume = _saverData.SliderVolume;
        }
    }
}
