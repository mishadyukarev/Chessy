using Chessy.Common.Component;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.Enum;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Common.Entity.View
{
    public sealed class EntitiesView
    {
        readonly Dictionary<ClipTypes, AudioSourceVC> _sound;

        public readonly BookUIE BookE;

        public readonly CanvasC CanvasC;
        public readonly GameObjectVC MenuGOC;
        public readonly GameObjectVC GameGOC;


        public AudioSourceVC Sound(in ClipTypes clip) => _sound[clip];

        public EntitiesView(in Transform main, in TestModes testMode)
        {
            new MainGoVC(main.gameObject);



            var camera = UnityEngine.Object.Instantiate(Resources.Load<Camera>("Camera"), main.position, main.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            var goES = new GameObject("EventSystem");




            _sound = new Dictionary<ClipTypes, AudioSourceVC>();

            var parent = new GameObject("AudioSources");

            for (var clipT = ClipTypes.None + 1; clipT < ClipTypes.End; clipT++)
            {
                var aS = parent.AddComponent<AudioSource>();
                aS.clip = UnityEngine.Resources.Load<AudioClip>(clipT.ToString());

                var volume = 1f;

                switch (clipT)
                {
                    case ClipTypes.Music: volume = testMode == TestModes.Standart ? 0 : 0.2f; break;
                }

                aS.volume = volume;

                if (clipT == ClipTypes.Music)
                {
                    aS.Play();
                    aS.loop = true;
                }


                _sound.Add(clipT, new AudioSourceVC(aS));
            }









            new TestModeC(testMode);

            var commonZone = new GameObject(NameConst.COMMON_ZONE).transform;

            new UnityEventC(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>());

            new AdComC(DateTime.Now);
            new TimeStartGameC(DateTime.Now);
            new ToggleZoneVC(new GameObject());



            camera.transform.SetParent(commonZone);
            main.SetParent(commonZone);
            goES.transform.SetParent(commonZone);
            parent.transform.SetParent(commonZone);

            


                #region UI

                var canvas = GameObject.Instantiate(Resources.Load<Canvas>("Canvas"));
            canvas.name = "Canvas";

            canvas.transform.SetParent(commonZone);

            CanvasC = new CanvasC(canvas);

            MenuGOC = new GameObjectVC(canvas.transform.Find("Menu+").gameObject);
            GameGOC = new GameObjectVC(canvas.transform.Find("Game+").gameObject);


            commonZone = canvas.transform.Find("Common+");
            BookE = new BookUIE(commonZone);
            new ShopUIC(commonZone.Find("ShopZone"));

            #endregion
        }
    }
}