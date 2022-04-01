using Chessy.Common.Component;
using Chessy.Common.Enum;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Common.Entity.View
{
    public sealed class EntitiesViewCommon
    {
        readonly Dictionary<Enum.ClipCommonTypes, AudioSourceVC> _sound;

        public readonly GameObjectVC MainGOC;
        public readonly GameObjectVC Background;
        public readonly UnityEventC UnityEventC;
        public readonly CameraVC CameraVC;

        public GameObjectVC ToggleZoneGOC;

        public AudioSourceVC Sound(in Enum.ClipCommonTypes clip) => _sound[clip];

        public EntitiesViewCommon(in Transform main, in TestModes testMode, out Dictionary<Enum.ClipCommonTypes, Action> sound, out Transform commonZone)
        {
            MainGOC = new GameObjectVC(main.gameObject);

            var camera = UnityEngine.Object.Instantiate(Resources.Load<Camera>("Camera"), main.position, main.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            CameraVC = new CameraVC(camera);




            _sound = new Dictionary<Enum.ClipCommonTypes, AudioSourceVC>();
            sound = new Dictionary<Enum.ClipCommonTypes, Action>();

            var parent = new GameObject("AudioSources");

            for (var clipT = Enum.ClipCommonTypes.None + 1; clipT < Enum.ClipCommonTypes.End; clipT++)
            {
                var aS = parent.AddComponent<AudioSource>();
                aS.clip = UnityEngine.Resources.Load<AudioClip>(clipT.ToString());

                var volume = 1f;

                switch (clipT)
                {
                    case ClipCommonTypes.Music: volume = testMode == TestModes.Standart ? 0 : 0.2f; break;
                    case ClipCommonTypes.Click: volume = 0.25f; break;
                }

                aS.volume = volume;

                if (clipT == Enum.ClipCommonTypes.Music)
                {
                    aS.Play();
                    aS.loop = true;
                }


                _sound.Add(clipT, new AudioSourceVC(aS));
                sound.Add(clipT, aS.Play);
            }

            commonZone = new GameObject(NameConst.COMMON_ZONE).transform;

            var goES = new GameObject("EventSystem");
            UnityEventC = new UnityEventC(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>());


            ToggleZoneGOC = new GameObjectVC(new GameObject());



            camera.transform.SetParent(commonZone);
            main.SetParent(commonZone);
            goES.transform.SetParent(commonZone);
            parent.transform.SetParent(commonZone);


            var backGroundGO = GameObject.Instantiate(Resources.Load<GameObject>("Background"), main.position + new Vector3(7, 5.5f, 2), main.rotation);
            backGroundGO.transform.SetParent(commonZone);
            backGroundGO.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
            Background = new GameObjectVC(backGroundGO);
        }
    }
}