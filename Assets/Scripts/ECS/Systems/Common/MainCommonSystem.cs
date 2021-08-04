using Assets.Scripts.Abstractions;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.System.View.Menu;
using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ECS.System.Data.Common
{
    internal class MainCommonSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _commonWorld;

        private EcsFilter<CommonZoneComponent> _commZoneFilter;


        private static EcsEntity _resourcesEnt;
        internal static ref ResourcesComponent ResourcesEnt_ResourcesCommonCom => ref _resourcesEnt.Get<ResourcesComponent>();


        private static EcsEntity _commonZoneEnt;
        internal static ref CommonZoneComponent CommonEnt_CommonZoneCom => ref _commonZoneEnt.Get<CommonZoneComponent>();
        internal static ref SaverComponent CommonZoneEnt_SaverCom => ref _commonZoneEnt.Get<SaverComponent>();
        internal static ref CameraComponent CameraEnt_CameraCom => ref _commonZoneEnt.Get<CameraComponent>();
        internal static ref CanvasComponent CanvasEnt_CanvasCom => ref _commonZoneEnt.Get<CanvasComponent>();


        private static EcsEntity _toggleZoneEnt;
        internal static ref ToggleZoneComponent ToggleZoneEnt_ParentCom => ref _toggleZoneEnt.Get<ToggleZoneComponent>();


        private static EcsEntity _soundEnt;
        internal static ref AudioSourceComponent StandartMusicEnt_AudioSourceCom => ref _soundEnt.Get<AudioSourceComponent>();



        public void Init()
        {
            _resourcesEnt = _commonWorld.NewEntity()
                .Replace(new ResourcesComponent(true));


            var camera = UnityEngine.Object.Instantiate(ResourcesEnt_ResourcesCommonCom.PrefabConfig.Camera, Main.Instance.transform.position, Main.Instance.transform.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            var goES = new GameObject("EventSystem");

            var canvas = GameObject.Instantiate(ResourcesEnt_ResourcesCommonCom.PrefabConfig.Canvas);
            canvas.name = "Canvas";

            _commonZoneEnt = _commonWorld.NewEntity()
                .Replace(new CommonZoneComponent(new GameObject(NameConst.COMMON_ZONE)))
                .Replace(new CameraComponent(camera, new Vector3(7, 4.8f, -2)))
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()))
                .Replace(new SaverComponent(0.15f))
                .Replace(new CanvasComponent(canvas));


            ref var commZoneCom = ref _commonZoneEnt.Get<CommonZoneComponent>();

            commZoneCom.Attach(camera.transform);
            commZoneCom.Attach(Main.Instance.transform);
            commZoneCom.Attach(goES.transform);
            commZoneCom.Attach(canvas.transform);


            ref var cameraCom = ref _commonZoneEnt.Get<CameraComponent>();

            camera.transform.position += cameraCom.PosForCamera;

            _toggleZoneEnt = _commonWorld.NewEntity()
                .Replace(new ToggleZoneComponent(new GameObject()));


            ref var commonZoneCom = ref _commZoneFilter.Get1(0);


            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();///////////
            commonZoneCom.Attach(audioSource.transform);

            _soundEnt = _commonWorld.NewEntity()
                .Replace(new AudioSourceComponent(audioSource));
            audioSource.clip = ResourcesEnt_ResourcesCommonCom.SoundConfig.MusicAudioClip;
            audioSource.volume = MainCommonSystem.CommonZoneEnt_SaverCom.SliderVolume;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void Run()
        {
            StandartMusicEnt_AudioSourceCom.AudioSource.volume = CommonZoneEnt_SaverCom.SliderVolume;

            switch (Main.Instance.SceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    CommonZoneEnt_SaverCom.StepModeType = MainMenuSystem.StepModUIEnt_DropDownTMPCom.StepModValue;
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
