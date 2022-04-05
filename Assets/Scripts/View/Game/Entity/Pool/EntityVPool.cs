using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Game.Values;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct EntityVPool
    {


        readonly Dictionary<ClipTypes, AudioSourceVC> _sounds0;
        readonly Dictionary<AbilityTypes, AudioSourceVC> _sounds1;

        public AudioSourceVC SoundV(in ClipTypes clip) => _sounds0[clip];
        public AudioSourceVC SoundV(in AbilityTypes clip) => _sounds1[clip];


        public EntityVPool(out Dictionary<ClipTypes, Action> action0, out Dictionary<AbilityTypes, Action> action1, in Transform generalZone)
        {
            var aSParent = new GameObject("AudioSource");

            aSParent.transform.SetParent(generalZone);


            AudioSource aS = default;


            _sounds0 = new Dictionary<ClipTypes, AudioSourceVC>();

            for (var clipT = ClipTypes.None + 1; clipT < ClipTypes.End; clipT++)
            {
                aS = aSParent.AddComponent<AudioSource>();
                aS.clip = UnityEngine.Resources.Load<AudioClip>(clipT.ToString());

                aS.volume = StartValues.Volume(clipT);
                if (clipT == ClipTypes.Background2)
                {
                    aS.Play();
                    aS.loop = true;
                }


                _sounds0.Add(clipT, new AudioSourceVC(aS));
            }


            _sounds1 = new Dictionary<AbilityTypes, AudioSourceVC>();

            for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
            {
                aS = aSParent.AddComponent<AudioSource>();
                aS.clip = Resources.Load<AudioClip>("Unique/" + unique.ToString());

                _sounds1.Add(unique, new AudioSourceVC(aS));

                aS.volume = StartValues.Volume(unique);
            }


            action0 = new Dictionary<ClipTypes, Action>();
            foreach (var item in _sounds0) action0.Add(item.Key, item.Value.Play);
            action1 = new Dictionary<AbilityTypes, Action>();
            foreach (var item in _sounds1) action1.Add(item.Key, item.Value.Play);
        }
    }
}