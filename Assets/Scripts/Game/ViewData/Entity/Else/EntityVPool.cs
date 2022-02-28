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


        public EntityVPool(out List<object> actions, out Dictionary<ClipTypes, System.Action> action0, out Dictionary<AbilityTypes, System.Action> action1)
        {
            _sounds0 = new Dictionary<ClipTypes, AudioSourceVC>();
            _sounds1 = new Dictionary<AbilityTypes, AudioSourceVC>();


            ToggleZoneVC.ReplaceZone(SceneTypes.Game);

            var genZone = new GameObject("GeneralZone");
            ToggleZoneVC.Attach(genZone.transform);
            GenegalZone = new GameObjectVC(genZone);

            SoundC.SavedVolume = SoundC.Volume;


            var backGroundGO = GameObject.Instantiate(PrefabResC.BackGroundCollider2D,
                MainGoVC.Pos + new Vector3(7, 5.5f, 2), MainGoVC.Rot);

            backGroundGO.transform.SetParent(GenegalZone.Transform);






            //        public void Attach(in Transform transForAttach)
            //=> transForAttach.transform.SetParent(EntityVPool.GeneralZone<GameObjectVC>().Transform);










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


            var mistake_AS = aSParent.AddComponent<AudioSource>();
            mistake_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Mistake);
            mistake_AS.volume = 0.4f;
            _sounds0.Add(ClipTypes.Mistake, new AudioSourceVC(mistake_AS));


            var attack_AS = aSParent.AddComponent<AudioSource>();
            attack_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AttackMelee);
            _sounds0.Add(ClipTypes.AttackMelee, new AudioSourceVC(attack_AS));


            var attackArcher_AS = aSParent.AddComponent<AudioSource>();
            attackArcher_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AttackArcher);
            attackArcher_AS.volume = 0.6f;
            _sounds0.Add(ClipTypes.AttackArcher, new AudioSourceVC(attackArcher_AS));


            var pickArcher_AS = aSParent.AddComponent<AudioSource>();
            pickArcher_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickArcher);
            pickArcher_AS.volume = 0.7f;
            _sounds0.Add(ClipTypes.PickArcher, new AudioSourceVC(pickArcher_AS));


            var pickMelee_AS = aSParent.AddComponent<AudioSource>();
            pickMelee_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickMelee);
            pickMelee_AS.volume = 0.1f;
            _sounds0.Add(ClipTypes.PickMelee, new AudioSourceVC(pickMelee_AS));


            var build_AS = aSParent.AddComponent<AudioSource>();
            build_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Building);
            build_AS.volume = 0.1f;
            _sounds0.Add(ClipTypes.Building, new AudioSourceVC(build_AS));


            var clickToTable_AS = aSParent.AddComponent<AudioSource>();
            clickToTable_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.ClickToTable);
            _sounds0.Add(ClipTypes.ClickToTable, new AudioSourceVC(clickToTable_AS));


            var createUnit_AS = aSParent.AddComponent<AudioSource>();
            createUnit_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.SoundGoldPack);
            createUnit_AS.volume = 0.3f;
            _sounds0.Add(ClipTypes.SoundGoldPack, new AudioSourceVC(createUnit_AS));


            var melt_AS = aSParent.AddComponent<AudioSource>();
            melt_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Melting);
            melt_AS.volume = 0.3f;
            _sounds0.Add(ClipTypes.Melting, new AudioSourceVC(melt_AS));


            var destroy_AS = aSParent.AddComponent<AudioSource>();
            destroy_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Destroy);
            destroy_AS.volume = 0.3f;
            _sounds0.Add(ClipTypes.Destroy, new AudioSourceVC(destroy_AS));


            var upgradeUnitMelee_AS = aSParent.AddComponent<AudioSource>();
            upgradeUnitMelee_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.UpgradeMelee);
            upgradeUnitMelee_AS.volume = 0.2f;
            _sounds0.Add(ClipTypes.UpgradeMelee, new AudioSourceVC(upgradeUnitMelee_AS));


            var shiftUnit_AS = aSParent.AddComponent<AudioSource>();
            shiftUnit_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.ClickToTable);
            shiftUnit_AS.volume = 0.6f;


            var afterBuildTown_AS = aSParent.AddComponent<AudioSource>();
            afterBuildTown_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AfterBuildTown);
            afterBuildTown_AS.volume = 0.2f;
            _sounds0.Add(ClipTypes.AfterBuildTown, new AudioSourceVC(afterBuildTown_AS));


            var truce_AS = aSParent.AddComponent<AudioSource>();
            truce_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Truce);
            truce_AS.volume = 0.6f;
            _sounds0.Add(ClipTypes.Truce, new AudioSourceVC(truce_AS));


            var cur_AS = aSParent.AddComponent<AudioSource>();
            cur_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickUpgrade);
            cur_AS.volume = 0.3f;
            _sounds0.Add(ClipTypes.PickUpgrade, new AudioSourceVC(cur_AS));


            cur_AS = aSParent.AddComponent<AudioSource>();
            cur_AS.clip = UnityEngine.Resources.Load<AudioClip>("GetHero_Clip");
            cur_AS.volume = 0.25f;
            _sounds0.Add(ClipTypes.GetHero, new AudioSourceVC(cur_AS));



            for (var uniq = AbilityTypes.None + 1; uniq < AbilityTypes.End; uniq++)
            {
                if (uniq != AbilityTypes.CircularAttack
                    && uniq != AbilityTypes.PutOutFirePawn
                    && uniq != AbilityTypes.PutOutFirePawn
                    && uniq != AbilityTypes.ChangeCornerArcher)
                {
                    cur_AS = aSParent.AddComponent<AudioSource>();
                    cur_AS.clip = ClipResourcesVC.AudioClip(uniq);
                    _sounds1.Add(uniq, new AudioSourceVC(cur_AS));

                    var volume = 0f;
                    switch (uniq)
                    {
                        case AbilityTypes.CircularAttack: throw new Exception();
                        case AbilityTypes.BonusNear: volume = 0.3f; break;

                        case AbilityTypes.DestroyBuilding: volume = 0.1f; break;
                        case AbilityTypes.SetFarm: volume = 0.1f; break;
                        case AbilityTypes.SetCity: volume = 0.1f; break;
                        case AbilityTypes.Seed: volume = 0.2f; break;
                        case AbilityTypes.FirePawn: volume = 0.2f; break;
                        case AbilityTypes.PutOutFirePawn: throw new Exception();

                        case AbilityTypes.FireArcher: volume = 0.2f; break;
                        case AbilityTypes.ChangeCornerArcher: throw new Exception();

                        case AbilityTypes.GrowAdultForest: volume = 0.3f; break;
                        case AbilityTypes.StunElfemale: volume = 0.3f; break;
                        case AbilityTypes.ChangeDirectionWind: volume = 0.1f; break;

                        case AbilityTypes.IceWall: volume = 0.1f; break;
                        case AbilityTypes.ActiveAroundBonusSnowy: volume = 0.1f; break;
                        case AbilityTypes.DirectWave: volume = 0.1f; break;

                        case AbilityTypes.Resurrect: volume = 0.1f; break;
                        case AbilityTypes.SetTeleport: volume = 0.1f; break;
                        case AbilityTypes.Teleport: volume = 0.1f; break;
                        case AbilityTypes.InvokeSkeletons: volume = 0.1f; break;
                        default: throw new Exception();
                    }

                    cur_AS.volume = volume;
                }
            }


            action0 = new Dictionary<ClipTypes, Action>();
            foreach (var item in _sounds0) action0.Add(item.Key, item.Value.Play);
            action1 = new Dictionary<AbilityTypes, Action>();
            foreach (var item in _sounds1) action1.Add(item.Key, item.Value.Play);
        }
    }
}