using Assets.Scripts.Abstractions;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Common;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ECS.System.Data.Common
{
    internal class MainComSys : IEcsInitSystem
    {
        private EcsWorld _curCommonWorld = default;

        public void Init()
        {
            var commonZoneEnt = _curCommonWorld.NewEntity()
                .Replace(new ResourcesComponent(true));



            var camera = UnityEngine.Object.Instantiate(ResourcesComponent.PrefabConfig.Camera, Main.Instance.transform.position, Main.Instance.transform.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            var goES = new GameObject("EventSystem");

            var canvas = GameObject.Instantiate(ResourcesComponent.PrefabConfig.Canvas);
            canvas.name = "Canvas";

            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
            audioSource.clip = ResourcesComponent.SoundConfig.MusicAudioClip;
            audioSource.volume = 0.2f;
            audioSource.loop = true;
            audioSource.Play();

            commonZoneEnt
                //Common
                .Replace(new CommonZoneComponent(new GameObject(NameConst.COMMON_ZONE)))
                .Replace(new CameraComponent(camera, new Vector3(7, 4.8f, -2)))
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()))
                .Replace(new StepModComponent(StepModeTypes.ByQueue))
                .Replace(new CanvasComponent(canvas))
                .Replace(new SoundComComp(audioSource))

                //Toggle
                .Replace(new ToggleZoneComponent(new GameObject()));


            ref var commZoneCom = ref commonZoneEnt.Get<CommonZoneComponent>();
            commZoneCom.Attach(camera.transform);
            commZoneCom.Attach(Main.Instance.transform);
            commZoneCom.Attach(goES.transform);
            commZoneCom.Attach(canvas.transform);


            camera.transform.position += CameraComponent.PosForCamera;


            ref var commonZoneCom = ref commonZoneEnt.Get<CommonZoneComponent>();
            commonZoneCom.Attach(audioSource.transform);


        }
    }
}
