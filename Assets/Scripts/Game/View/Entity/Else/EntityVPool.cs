using Chessy.Common;
using Chessy.Common.Component;
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

                var volume = 1f;

                switch (clipT)
                {
                    case ClipTypes.AttackArcher: volume = 0.6f; break;
                    case ClipTypes.AttackMelee: volume = 1; break;
                    case ClipTypes.Building: volume = 0.1f; break;
                    case ClipTypes.Mistake: volume = 0.4f; break;
                    case ClipTypes.SoundGoldPack: volume = 0.3f; break;
                    case ClipTypes.Melting: volume = 0.3f; break;
                    case ClipTypes.Destroy: volume = 0.3f; break;
                    case ClipTypes.ClickToTable: volume = 0.6f; break;
                    case ClipTypes.Truce: volume = 0.6f; break;
                    case ClipTypes.PickMelee: volume = 0.1f; break;
                    case ClipTypes.PickArcher: volume = 0.7f; break;
                    case ClipTypes.Click: volume = 0.25f; break;
                    case ClipTypes.WritePensil: volume = 0.2f; break;
                    case ClipTypes.Leaf: volume = 0.4f; break;
                    case ClipTypes.KickGround: volume = 0.1f; break;
                    case ClipTypes.Rock: volume = 0.2f; break;
                    case ClipTypes.ShortWind: volume = 0.2f; break;
                    case ClipTypes.ShortRain: volume = 0.2f; break;

                    case ClipTypes.Background1: volume = 1; break;
                    case ClipTypes.Background2: volume = 0.05f; break;
                }

                aS.volume = volume;
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

                var volume = 1f;
                switch (unique)
                {
                    case AbilityTypes.KingPassiveNearBonus: volume = 0.3f; break;

                    case AbilityTypes.DestroyBuilding: volume = 0.1f; break;
                    case AbilityTypes.SetFarm: volume = 0.1f; break;
                    case AbilityTypes.Seed: volume = 0.2f; break;
                    case AbilityTypes.FirePawn: volume = 0.2f; break;

                    case AbilityTypes.FireArcher: volume = 0.2f; break;

                    case AbilityTypes.GrowAdultForest: volume = 0.3f; break;
                    case AbilityTypes.StunElfemale: volume = 0.3f; break;
                    case AbilityTypes.ChangeDirectionWind: volume = 0.1f; break;

                    //case AbilityTypes.IceWall: volume = 0.1f; break;
                    //case AbilityTypes.ActiveAroundBonusSnowy: volume = 0.1f; break;
                    //case AbilityTypes.DirectWave: volume = 0.1f; break;

                    case AbilityTypes.Resurrect: volume = 0.1f; break;
                    case AbilityTypes.SetTeleport: volume = 0.1f; break;
                    case AbilityTypes.Teleport: volume = 0.1f; break;
                    case AbilityTypes.InvokeSkeletons: volume = 0.1f; break;
                }

                aS.volume = volume;
            }


            action0 = new Dictionary<ClipTypes, Action>();
            foreach (var item in _sounds0) action0.Add(item.Key, item.Value.Play);
            action1 = new Dictionary<AbilityTypes, Action>();
            foreach (var item in _sounds1) action1.Add(item.Key, item.Value.Play);
        }
    }
}