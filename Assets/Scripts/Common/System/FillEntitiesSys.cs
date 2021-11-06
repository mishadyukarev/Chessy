using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;

namespace Scripts.Common
{
    public class FillEntitiesSys : IEcsInitSystem
    {
        private EcsWorld _curComWorld = default;

        public FillEntitiesSys(EcsWorld comWorld, Action<SceneTypes> toggleScene, GameObject main_GO)
        {
            var comSysts = new EcsSystems(comWorld);
            var runUpdate = new EcsSystems(comWorld);



            var launchAdd = new EcsSystems(comWorld)
                .Add(new LaunchAdComSys());



            var dict = new Dictionary<ActionDataTypes, Action>();
            dict.Add(ActionDataTypes.RunUpdate, runUpdate.Run);
            dict.Add(ActionDataTypes.LaunchAdd, launchAdd.Run);

            new ComSysDataC(dict, toggleScene);
            new MainGoVC(main_GO);


            var photScene = MainGoVC.Main_GO.AddComponent<PhotonSceneSys>();


            comSysts
                .Add(this)
                .Add(new EventSys())
                .Add(photScene)
                .Add(launchAdd)
                .Add(runUpdate);

            comSysts.Init();
        }

        public void Init()
        {
            var commonZoneEnt = _curComWorld.NewEntity()
                .Replace(new VideoClipsResCom(true))
                .Replace(new SpritesResComC(true))
                .Replace(new PrefabResComC(true))
                .Replace(new ClipsResComCom(true));



            var camera = UnityEngine.Object.Instantiate(PrefabResComC.Camera, MainGoVC.Main_GO.transform.position, MainGoVC.Main_GO.transform.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            var goES = new GameObject("EventSystem");

            var canvas = GameObject.Instantiate(PrefabResComC.Canvas);
            canvas.name = "Canvas";

            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
            audioSource.clip = ClipsResComCom.AudioClip(ClipComTypes.Music);
            audioSource.volume = 0.2f;
            audioSource.loop = true;
            audioSource.Play();

            commonZoneEnt
                //Common
                .Replace(new ComZoneC(new GameObject(NameConst.COMMON_ZONE)))
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()))
                .Replace(new CanvasC(canvas))
                .Replace(new SoundComC(audioSource))
                .Replace(new LanguageComCom(LanguageTypes.English))
                .Replace(new AdComCom(DateTime.Now))
                .Replace(new TimeStartGameComCom(DateTime.Now))
                .Replace(new HintComC(true))

                //Toggle
                .Replace(new ToggleZoneComponent(new GameObject()));


            var shopZoneUI_Trans = CanvasC.FindUnderComZone<Transform>("ShopZone");




            commonZoneEnt
                .Replace(new ShopZoneUICom(shopZoneUI_Trans));



            ref var comZoneCom = ref commonZoneEnt.Get<ComZoneC>();
            comZoneCom.Attach(camera.transform);
            comZoneCom.Attach(MainGoVC.Main_GO.transform);
            comZoneCom.Attach(goES.transform);
            comZoneCom.Attach(canvas.transform);


            //camera.transform.position += CameraComComp._gamePosCamera;


            ref var commonZoneCom = ref commonZoneEnt.Get<ComZoneC>();
            commonZoneCom.Attach(audioSource.transform);



            if (Advertisement.isSupported) Advertisement.Initialize("4097313", false);
        }
    }
}
