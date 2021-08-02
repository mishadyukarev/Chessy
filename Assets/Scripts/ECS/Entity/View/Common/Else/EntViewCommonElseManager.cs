using Assets.Scripts.ECS.Entity.Data.Common.Else.Container;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.Workers.Common;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Entity.View.Common
{
    public sealed class EntViewCommonElseManager
    {
        private EcsEntity _soundEnt;
        internal ref AudioSourceComponent StandartMusicEnt_AudioSourceCom => ref _soundEnt.Get<AudioSourceComponent>();

        internal EntViewCommonElseManager(EcsWorld commonWorld, EntDataCommonElseManager entDataCommonElseManager)
        {
            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();///////////
            audioSource.transform.SetParent(DataCommContainerElseZone.ParentGO.transform);

            _soundEnt = commonWorld.NewEntity()
                .Replace(new AudioSourceComponent(audioSource));
            audioSource.clip = entDataCommonElseManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.MusicAudioClip;
            audioSource.volume = DataCommContainerElseSaver.SliderVolume;
            audioSource.loop = true;
            audioSource.Play();
        }

        internal void OwnUpdate(SceneTypes sceneType)
        {
            StandartMusicEnt_AudioSourceCom.AudioSource.volume = DataCommContainerElseSaver.SliderVolume;

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
