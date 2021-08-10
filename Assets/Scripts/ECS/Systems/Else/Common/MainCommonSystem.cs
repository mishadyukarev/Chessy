using Assets.Scripts.Abstractions;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ECS.System.Data.Common
{
    internal class MainCommonSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _commonWorld;


        private static EcsEntity _commonZoneEnt;
        internal static ref AudioSourceComponent CommonEnt_AudioSourceCom => ref _commonZoneEnt.Get<AudioSourceComponent>();


        public void Init()
        {
            _commonZoneEnt = _commonWorld.NewEntity()
                .Replace(new ResourcesComponent(true));



            var camera = UnityEngine.Object.Instantiate(ResourcesComponent.PrefabConfig.Camera, Main.Instance.transform.position, Main.Instance.transform.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            var goES = new GameObject("EventSystem");

            var canvas = GameObject.Instantiate(ResourcesComponent.PrefabConfig.Canvas);
            canvas.name = "Canvas";

            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
            audioSource.clip = ResourcesComponent.SoundConfig.MusicAudioClip;
            audioSource.volume = SaverComponent.SliderVolume;
            audioSource.loop = true;
            audioSource.Play();

            _commonZoneEnt
                .Replace(new CommonZoneComponent(new GameObject(NameConst.COMMON_ZONE)))
                .Replace(new CameraComponent(camera, new Vector3(7, 4.8f, -2)))
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()))
                .Replace(new SaverComponent(StepModeTypes.ByQueue, 0.15f))
                .Replace(new CanvasComponent(canvas))
                .Replace(new ToggleZoneComponent(new GameObject()))
                .Replace(new SoundCommonCom(audioSource))
                .Replace(new PhotonViewComponent(Main.Instance.gameObject.AddComponent<PhotonView>()));


            ref var commZoneCom = ref _commonZoneEnt.Get<CommonZoneComponent>();
            commZoneCom.Attach(camera.transform);
            commZoneCom.Attach(Main.Instance.transform);
            commZoneCom.Attach(goES.transform);
            commZoneCom.Attach(canvas.transform);


            camera.transform.position += CameraComponent.PosForCamera;


            ref var commonZoneCom = ref _commonZoneEnt.Get<CommonZoneComponent>();
            commonZoneCom.Attach(audioSource.transform);


        }

        public void Run()
        {
            SoundCommonCom.Volume = SaverComponent.SliderVolume;

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
