using Assets.Scripts.Abstractions;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ECS.System.Data.Common
{
    internal class SpawnInitComSys : IEcsInitSystem
    {
        private EcsWorld _curComWorld = default;

        public void Init()
        {
            var commonZoneEnt = _curComWorld.NewEntity()
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
                .Replace(new ComZoneComp(new GameObject(NameConst.COMMON_ZONE)))
                .Replace(new CameraComComp(camera, new Vector3(7, 4.8f, -2)))
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()))
                .Replace(new CanvasCom(canvas))
                .Replace(new SoundComComp(audioSource))
                .Replace(new LanguageComCom(LanguageTypes.Russian))

                //Toggle
                .Replace(new ToggleZoneComponent(new GameObject()));


            ref var comZoneCom = ref commonZoneEnt.Get<ComZoneComp>();
            comZoneCom.Attach(camera.transform);
            comZoneCom.Attach(Main.Instance.transform);
            comZoneCom.Attach(goES.transform);
            comZoneCom.Attach(canvas.transform);


            //camera.transform.position += CameraComComp._gamePosCamera;


            ref var commonZoneCom = ref commonZoneEnt.Get<ComZoneComp>();
            commonZoneCom.Attach(audioSource.transform);


        }
    }
}
