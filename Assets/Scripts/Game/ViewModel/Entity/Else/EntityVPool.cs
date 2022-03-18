using Chessy.Common;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct EntityVPool
    {
        public readonly GameObjectVC GenegalZone;
        public readonly GameObjectVC Background;
        public readonly PhotonViewC Photon;

        readonly Dictionary<ClipTypes, AudioSourceVC> _sounds0;
        readonly Dictionary<AbilityTypes, AudioSourceVC> _sounds1;

        public AudioSourceVC SoundV(in ClipTypes clip) => _sounds0[clip];
        public AudioSourceVC SoundV(in AbilityTypes clip) => _sounds1[clip];


        public EntityVPool(out List<object> actions, out Dictionary<ClipTypes, global::System.Action> action0, out Dictionary<AbilityTypes, global::System.Action> action1)
        {
            ToggleZoneVC.ReplaceZone(SceneTypes.Game);

            var genZone = new GameObject("GeneralZone");
            ToggleZoneVC.Attach(genZone.transform);
            GenegalZone = new GameObjectVC(genZone);

            SoundC.SavedVolume = SoundC.Volume;


            var backGroundGO = GameObject.Instantiate(PrefabResC.BackGroundCollider2D,
                MainGoVC.Pos + new Vector3(7, 5.5f, 2), MainGoVC.Rot);

            backGroundGO.transform.SetParent(GenegalZone.Transform);


            backGroundGO.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);

            Background = new GameObjectVC(backGroundGO);


            var aSParent = new GameObject("AudioSource");

            aSParent.transform.SetParent(GenegalZone.Transform);


            var photonView_Rpc = new GameObject("PhotonView_Rpc");
            photonView_Rpc.transform.SetParent(GenegalZone.Transform);

            var photonV = photonView_Rpc.AddComponent<PhotonView>();

            if (PhotonNetwork.IsMasterClient) PhotonNetwork.AllocateViewID(photonV);
            else photonV.ViewID = 1001;
            Photon = new PhotonViewC(photonV, out actions);


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
                    case ClipTypes.UpgradeMelee: volume = 0.2f; break;
                    case ClipTypes.UpgradeUnitArcher: volume = 1; break;
                    case ClipTypes.ClickToTable: volume = 0.6f; break;
                    case ClipTypes.Truce: volume = 0.6f; break;
                    case ClipTypes.AfterBuildTown: volume = 0.2f; break;
                    case ClipTypes.PickMelee: volume = 0.1f; break;
                    case ClipTypes.PickArcher: volume = 0.7f; break;
                    case ClipTypes.PickUpgrade: volume = 0.3f; break;
                    case ClipTypes.GetHero: volume = 0.25f; break;
                    //case ClipTypes.HeroAbility: volume = 0.25f; break;
                    case ClipTypes.BackgroundInGame: volume = 1; break;
                    case ClipTypes.Click: volume = 0.25f; break;
                    case ClipTypes.WritePensil: volume = 0.2f; break;
                }

                aS.volume = volume;

                if (clipT == ClipTypes.BackgroundInGame)
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