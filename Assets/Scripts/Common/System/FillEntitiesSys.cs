using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;

namespace Chessy.Common
{
    public class FillEntitiesSys : IEcsInitSystem
    {
        private EcsWorld _curComWorld = default;

        public FillEntitiesSys(EcsWorld comWorld, Action<SceneTypes> toggleScene, GameObject main_GO)
        {
            var comSysts = new EcsSystems(comWorld);
            var runUpdate = new EcsSystems(comWorld);



            var launchAdd = new EcsSystems(comWorld)
                .Add(new AdLaunchS());



            var dict = new Dictionary<ActionDataTypes, Action>();
            dict.Add(ActionDataTypes.RunUpdate, runUpdate.Run);
            dict.Add(ActionDataTypes.LaunchAdd, launchAdd.Run);

            new DataSC(dict, toggleScene);
            new MainGoVC(main_GO);


            var photScene = MainGoVC.Main_GO.AddComponent<PhotonSceneSys>();


            comSysts
                .Add(this)
                .Add(new EventSys())
                .Add(photScene)
                .Add(launchAdd)
                .Add(runUpdate)
                .Add(new IAPCore());

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

            var aS = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
            aS.clip = ClipsResComCom.AudioClip(ClipComTypes.Music);
            aS.volume = 0.2f;
            aS.loop = true;
            aS.Play();

            commonZoneEnt
                //Common
                .Replace(new ComZoneC(new GameObject(NameConst.COMMON_ZONE)))
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()))
                .Replace(new CanvasC(canvas))
                .Replace(new SoundComC(aS))
                .Replace(new LanguageComC(LanguageTypes.English))
                .Replace(new AdComCom(DateTime.Now))
                .Replace(new TimeStartGameComCom(DateTime.Now))
                .Replace(new HintComC(true))

                //Toggle
                .Replace(new ToggleZoneVC(new GameObject()));


            var shopZoneUI_Trans = CanvasC.FindUnderComZone<Transform>("ShopZone");




            commonZoneEnt
                .Replace(new ShopUIC(shopZoneUI_Trans));



            ref var comZoneCom = ref commonZoneEnt.Get<ComZoneC>();
            comZoneCom.Attach(camera.transform);
            comZoneCom.Attach(MainGoVC.Main_GO.transform);
            comZoneCom.Attach(goES.transform);
            comZoneCom.Attach(canvas.transform);


            //camera.transform.position += CameraComComp._gamePosCamera;


            ref var commonZoneCom = ref commonZoneEnt.Get<ComZoneC>();
            commonZoneCom.Attach(aS.transform);



            //if (Advertisement.isSupported) Advertisement.Initialize("4097313", false);
        }
    }
}
