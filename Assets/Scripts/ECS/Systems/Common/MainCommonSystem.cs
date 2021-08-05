using Assets.Scripts.Abstractions;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ECS.System.Data.Common
{
    internal class MainCommonSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _commonWorld;


        private static EcsEntity _commonZoneEnt;
        internal static ref SaverComponent CommonEnt_SaverCom => ref _commonZoneEnt.Get<SaverComponent>();
        internal static ref CameraComponent CommonEnt_CameraCom => ref _commonZoneEnt.Get<CameraComponent>();
        internal static ref CanvasComponent CommonEnt_CanvasCom => ref _commonZoneEnt.Get<CanvasComponent>();
        internal static ref ResourcesComponent CommonEnt_ResourcesCommonCom => ref _commonZoneEnt.Get<ResourcesComponent>();
        internal static ref ToggleZoneComponent CommonEnt_ParentCom => ref _commonZoneEnt.Get<ToggleZoneComponent>();
        internal static ref AudioSourceComponent CommonEnt_AudioSourceCom => ref _commonZoneEnt.Get<AudioSourceComponent>();


        public void Init()
        {
            _commonZoneEnt = _commonWorld.NewEntity()
                .Replace(new ResourcesComponent(true));


            ref var resourcesCom = ref _commonZoneEnt.Get<ResourcesComponent>();

            var camera = UnityEngine.Object.Instantiate(resourcesCom.PrefabConfig.Camera, Main.Instance.transform.position, Main.Instance.transform.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            var goES = new GameObject("EventSystem");

            var canvas = GameObject.Instantiate(resourcesCom.PrefabConfig.Canvas);
            canvas.name = "Canvas";

            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
            audioSource.clip = resourcesCom.SoundConfig.MusicAudioClip;
            audioSource.volume = CommonEnt_SaverCom.SliderVolume;
            audioSource.loop = true;
            audioSource.Play();

            _commonZoneEnt
                .Replace(new CommonZoneComponent(new GameObject(NameConst.COMMON_ZONE)))
                .Replace(new CameraComponent(camera, new Vector3(7, 4.8f, -2)))
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()))
                .Replace(new SaverComponent(0.15f))
                .Replace(new CanvasComponent(canvas))
                .Replace(new ToggleZoneComponent(new GameObject()))
                .Replace(new AudioSourceComponent(audioSource));


            ref var commZoneCom = ref _commonZoneEnt.Get<CommonZoneComponent>();
            commZoneCom.Attach(camera.transform);
            commZoneCom.Attach(Main.Instance.transform);
            commZoneCom.Attach(goES.transform);
            commZoneCom.Attach(canvas.transform);



            ref var cameraCom = ref _commonZoneEnt.Get<CameraComponent>();
            camera.transform.position += cameraCom.PosForCamera;


            ref var commonZoneCom = ref _commonZoneEnt.Get<CommonZoneComponent>();
            commonZoneCom.Attach(audioSource.transform);


        }

        public void Run()
        {
            CommonEnt_AudioSourceCom.AudioSource.volume = CommonEnt_SaverCom.SliderVolume;

            switch (Main.SceneType)
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
