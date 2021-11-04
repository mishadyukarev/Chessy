using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;

namespace Scripts.Common
{
    public class SpawnInitComSys : IEcsInitSystem
    {
        private EcsWorld _curComWorld = default;

        public SpawnInitComSys(EcsSystems comSysts, Action<SceneTypes> toggleScene, GameObject main_GO)
        {
            var launchAdd = new LaunchAdComSys();
            new CommonDataC(toggleScene, launchAdd.Run);
            new MainGOComC(main_GO);

            var photScene = MainGOComC.Main_GO.AddComponent<PhotonSceneSys>();

            comSysts
                .Add(this)
                .Add(photScene)
                .Add(launchAdd);
        }

        public void Init()
        {
            var commonZoneEnt = _curComWorld.NewEntity()
                .Replace(new VideoClipsResCom(true))
                .Replace(new SpritesResComC(true))
                .Replace(new PrefabsResComCom(true))
                .Replace(new ClipsResComCom(true));



            var camera = UnityEngine.Object.Instantiate(PrefabsResComCom.Camera, MainGOComC.Main_GO.transform.position, MainGOComC.Main_GO.transform.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            var goES = new GameObject("EventSystem");

            var canvas = GameObject.Instantiate(PrefabsResComCom.Canvas);
            canvas.name = "Canvas";

            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
            audioSource.clip = ClipsResComCom.AudioClip(ClipComTypes.Music);
            audioSource.volume = 0.2f;
            audioSource.loop = true;
            audioSource.Play();

            commonZoneEnt
                //Common
                .Replace(new ComZoneComp(new GameObject(NameConst.COMMON_ZONE)))
                //.Replace(new CameraComC(camera))
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()))
                .Replace(new CanvasCom(canvas))
                .Replace(new SoundComC(audioSource))
                .Replace(new LanguageComCom(LanguageTypes.English))
                .Replace(new AdComCom(DateTime.Now))
                .Replace(new TimeStartGameComCom(DateTime.Now))
                .Replace(new HintComC(true))

                //Toggle
                .Replace(new ToggleZoneComponent(new GameObject()));


            ref var comZoneCom = ref commonZoneEnt.Get<ComZoneComp>();
            comZoneCom.Attach(camera.transform);
            comZoneCom.Attach(MainGOComC.Main_GO.transform);
            comZoneCom.Attach(goES.transform);
            comZoneCom.Attach(canvas.transform);


            //camera.transform.position += CameraComComp._gamePosCamera;


            ref var commonZoneCom = ref commonZoneEnt.Get<ComZoneComp>();
            commonZoneCom.Attach(audioSource.transform);



            if (Advertisement.isSupported) Advertisement.Initialize("4097313", false);
        }
    }
}
