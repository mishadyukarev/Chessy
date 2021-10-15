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


        #region

        private static event Action<SceneTypes> _toggleScene_Event;
        public static GameObject Main_GO { get; private set; }

        public static void ToggleScene(SceneTypes sceneType) => _toggleScene_Event.Invoke(sceneType);

        public SpawnInitComSys(Action<SceneTypes> toggleScene_Action, GameObject main_GO)
        {
            _toggleScene_Event += toggleScene_Action;
            Main_GO = main_GO;
        }

        #endregion


        public void Init()
        {
            var commonZoneEnt = _curComWorld.NewEntity()
                .Replace(new VideoClipsResCom(true))
                .Replace(new SpritesResCom(true))
                .Replace(new PrefabsResComCom(true))
                .Replace(new ClipsResComCom(true));



            var camera = UnityEngine.Object.Instantiate(PrefabsResComCom.Camera, Main_GO.transform.position, Main_GO.transform.rotation);
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
                .Replace(new CameraComComp(camera, new Vector3(7.4f, 4.8f, -2)))
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()))
                .Replace(new CanvasCom(canvas))
                .Replace(new SoundComComp(audioSource))
                .Replace(new LanguageComCom(LanguageTypes.English))
                .Replace(new AdComCom(DateTime.Now))
                .Replace(new TimeStartGameComCom(DateTime.Now))

                //Toggle
                .Replace(new ToggleZoneComponent(new GameObject()));


            ref var comZoneCom = ref commonZoneEnt.Get<ComZoneComp>();
            comZoneCom.Attach(camera.transform);
            comZoneCom.Attach(Main_GO.transform);
            comZoneCom.Attach(goES.transform);
            comZoneCom.Attach(canvas.transform);


            //camera.transform.position += CameraComComp._gamePosCamera;


            ref var commonZoneCom = ref commonZoneEnt.Get<ComZoneComp>();
            commonZoneCom.Attach(audioSource.transform);



            if (Advertisement.isSupported) Advertisement.Initialize("4097313", false);
        }
    }
}
