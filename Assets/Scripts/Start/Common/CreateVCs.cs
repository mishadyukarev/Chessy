using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Common
{
    public sealed class CreateVCs
    {
        public CreateVCs(Transform main)
        {
            new MainGoVC(main.gameObject);

            var camera = UnityEngine.Object.Instantiate(PrefabResC.Camera, main.position, main.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            var goES = new GameObject("EventSystem");

            var canvas = GameObject.Instantiate(PrefabResC.Canvas);
            canvas.name = "Canvas";

            var aS = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
            aS.clip = ClipsResC.AudioClip(ClipComTypes.Music);
            aS.volume = 0.2f;
            aS.loop = true;
            aS.Play();


            new ComZoneC(new GameObject(NameConst.COMMON_ZONE));
            new UnityEventC(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>());
            new CanvasC(canvas);
            new SoundC(aS);
            new AdComC(DateTime.Now);
            new TimeStartGameC(DateTime.Now);
            new HintC(true);
            new ToggleZoneVC(new GameObject());
            new ShopUIC(CanvasC.FindUnderComZone<Transform>("ShopZone"));



            ComZoneC.Attach(camera.transform);
            ComZoneC.Attach(main);
            ComZoneC.Attach(goES.transform);
            ComZoneC.Attach(canvas.transform);
            ComZoneC.Attach(aS.transform);
        }
    }
}