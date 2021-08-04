using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.Workers.Common;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.System.View.Common
{
    internal sealed class MainCommonViewSys : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _commonWorld;
        private EcsFilter<CommonZoneComponent> _commZoneFilter;


        private static EcsEntity _soundEnt;
        internal static ref AudioSourceComponent StandartMusicEnt_AudioSourceCom => ref _soundEnt.Get<AudioSourceComponent>();

        public void Init()
        {
            ref var commonZoneCom = ref _commZoneFilter.Get1(0);


            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();///////////
            commonZoneCom.Attach(audioSource.transform);

            _soundEnt = _commonWorld.NewEntity()
                .Replace(new AudioSourceComponent(audioSource));
            audioSource.clip = MainDataCommSys.ResourcesEnt_ResourcesCommonCom.SoundConfig.MusicAudioClip;
            audioSource.volume = MainDataCommSys.CommonZoneEnt_SaverCom.SliderVolume;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void Run()
        {
            StandartMusicEnt_AudioSourceCom.AudioSource.volume = MainDataCommSys.CommonZoneEnt_SaverCom.SliderVolume;

            switch (Main.Instance.SceneType)
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
