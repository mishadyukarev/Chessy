using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Common
{
    public sealed class FillEntitiesSys : IEcsInitSystem
    {
        private readonly EcsWorld _curComW = default;

        public FillEntitiesSys(EcsSystems comSysts, Action<SceneTypes> toggleScene, GameObject main_GO)
        {
            var comW = comSysts.World;

            var runUpdate = new EcsSystems(comW)
                .Add(new MyYodo())
                .Add(new AdLaunchS());


            new DataSC(runUpdate.Run, toggleScene);
            new MainGoVC(main_GO);


            var photScene = MainGoVC.AddComponent<PhotonSceneSys>();

            comSysts
                .Add(this)
                .Add(new EventSys())
                .Add(photScene)
                .Add(runUpdate)
                .Add(new IAPCore());

            comSysts.Init();
        }

        public void Init()
        {
            var commonZoneEnt = _curComW.NewEntity()
                .Replace(new PrefabResC(true))
                .Replace(new ClipsResComCom(true));



            var camera = UnityEngine.Object.Instantiate(PrefabResC.Camera, MainGoVC.Pos, MainGoVC.Rot);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            var goES = new GameObject("EventSystem");

            var canvas = GameObject.Instantiate(PrefabResC.Canvas);
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
                .Replace(new SoundC(aS))
                .Replace(new LanguageComC(LanguageTypes.English))
                .Replace(new AdComC(DateTime.Now))
                .Replace(new TimeStartGameComCom(DateTime.Now))
                .Replace(new HintComC(true))

                //Toggle
                .Replace(new ToggleZoneVC(new GameObject()));


            var shopZoneUI_Trans = CanvasC.FindUnderComZone<Transform>("ShopZone");




            commonZoneEnt
                .Replace(new ShopUIC(shopZoneUI_Trans));



            ref var comZoneCom = ref commonZoneEnt.Get<ComZoneC>();
            comZoneCom.Attach(camera.transform);
            comZoneCom.Attach(MainGoVC.Trans);
            comZoneCom.Attach(goES.transform);
            comZoneCom.Attach(canvas.transform);


            //camera.transform.position += CameraComComp._gamePosCamera;


            ref var commonZoneCom = ref commonZoneEnt.Get<ComZoneC>();
            commonZoneCom.Attach(aS.transform);



            //if (Advertisement.isSupported) Advertisement.Initialize("4097313", false);
        }
    }
}
