using ECS;
using Game.Common;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct EntityVPool
    {
        static Dictionary<string, Entity> _ents;
        static Dictionary<ClipTypes, Entity> _sounds0;
        static Dictionary<AbilityTypes, Entity> _sounds1;

        public static ref C Background<C>() where C : struct, IBackgroundE => ref _ents[nameof(IBackgroundE)].Get<C>();
        public static ref C GeneralZone<C>() where C : struct => ref _ents[nameof(IGeneralZoneE)].Get<C>();
        public static ref C Photon<C>() where C : struct, IPhotonE => ref _ents[nameof(IPhotonE)].Get<C>();

        public static ref C SoundV<C>(in ClipTypes clip) where C : struct, ISoundE
        {
            if (!_sounds0.ContainsKey(clip)) throw new Exception();
            return ref _sounds0[clip].Get<C>();
        }
        public static ref C SoundV<C>(in AbilityTypes clip) where C : struct, ISoundE
        {
            if (!_sounds1.ContainsKey(clip)) throw new Exception();
            return ref _sounds1[clip].Get<C>();
        }


        public EntityVPool(in EcsWorld gameW, out List<object> actions, out Dictionary<ClipTypes, System.Action> action0, out Dictionary<AbilityTypes, System.Action> action1)
        {
            _ents = new Dictionary<string, Entity>();
            _sounds0 = new Dictionary<ClipTypes, Entity>();
            _sounds1 = new Dictionary<AbilityTypes, Entity>();


            ToggleZoneVC.ReplaceZone(SceneTypes.Game);

            var genZone = new GameObject("GeneralZone");
            ToggleZoneVC.Attach(genZone.transform);
            _ents[nameof(IGeneralZoneE)] = gameW.NewEntity()
                .Add(new GeneralZoneVEC())
                .Add(new GameObjectVC(genZone));

            SoundC.SavedVolume = SoundC.Volume;


            var backGroundGO = GameObject.Instantiate(PrefabResC.BackGroundCollider2D,
                MainGoVC.Pos + new Vector3(7, 5.5f, 2), MainGoVC.Rot);
            GeneralZone<GeneralZoneVEC>().Attach(backGroundGO.transform);
            backGroundGO.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);

            _ents[nameof(IBackgroundE)] = gameW.NewEntity()
                .Add(new GameObjectVC(backGroundGO));


            var aSParent = new GameObject("AudioSource");
            GeneralZone<GeneralZoneVEC>().Attach(aSParent.transform);


            var photonView_Rpc = new GameObject("PhotonView_Rpc");
            GeneralZone<GeneralZoneVEC>().Attach(photonView_Rpc.transform);

            var photonV = photonView_Rpc.AddComponent<PhotonView>();

            if (PhotonNetwork.IsMasterClient) PhotonNetwork.AllocateViewID(photonV);
            else photonV.ViewID = 1001;
            _ents[nameof(IPhotonE)] = gameW.NewEntity()
                .Add(new PhotonVC(photonV, out actions));


            var mistake_AS = aSParent.AddComponent<AudioSource>();
            mistake_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Mistake);
            mistake_AS.volume = 0.4f;
            _sounds0.Add(ClipTypes.Mistake, gameW.NewEntity().Add(new AudioSourceVC(mistake_AS)));


            var attack_AS = aSParent.AddComponent<AudioSource>();
            attack_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AttackMelee);
            _sounds0.Add(ClipTypes.AttackMelee, gameW.NewEntity().Add(new AudioSourceVC(attack_AS)));


            var attackArcher_AS = aSParent.AddComponent<AudioSource>();
            attackArcher_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AttackArcher);
            attackArcher_AS.volume = 0.6f;
            _sounds0.Add(ClipTypes.AttackArcher, gameW.NewEntity().Add(new AudioSourceVC(attackArcher_AS)));


            var pickArcher_AS = aSParent.AddComponent<AudioSource>();
            pickArcher_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickArcher);
            pickArcher_AS.volume = 0.7f;
            _sounds0.Add(ClipTypes.PickArcher, gameW.NewEntity().Add(new AudioSourceVC(pickArcher_AS)));


            var pickMelee_AS = aSParent.AddComponent<AudioSource>();
            pickMelee_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickMelee);
            pickMelee_AS.volume = 0.1f;
            _sounds0.Add(ClipTypes.PickMelee, gameW.NewEntity().Add(new AudioSourceVC(pickMelee_AS)));


            var build_AS = aSParent.AddComponent<AudioSource>();
            build_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Building);
            build_AS.volume = 0.1f;
            _sounds0.Add(ClipTypes.Building, gameW.NewEntity().Add(new AudioSourceVC(build_AS)));


            var clickToTable_AS = aSParent.AddComponent<AudioSource>();
            clickToTable_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.ClickToTable);
            _sounds0.Add(ClipTypes.ClickToTable, gameW.NewEntity().Add(new AudioSourceVC(clickToTable_AS)));


            var createUnit_AS = aSParent.AddComponent<AudioSource>();
            createUnit_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.SoundGoldPack);
            createUnit_AS.volume = 0.3f;
            _sounds0.Add(ClipTypes.SoundGoldPack, gameW.NewEntity().Add(new AudioSourceVC(createUnit_AS)));


            var melt_AS = aSParent.AddComponent<AudioSource>();
            melt_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Melting);
            melt_AS.volume = 0.3f;
            _sounds0.Add(ClipTypes.Melting, gameW.NewEntity().Add(new AudioSourceVC(melt_AS)));


            var destroy_AS = aSParent.AddComponent<AudioSource>();
            destroy_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Destroy);
            destroy_AS.volume = 0.3f;
            _sounds0.Add(ClipTypes.Destroy, gameW.NewEntity().Add(new AudioSourceVC(destroy_AS)));


            var upgradeUnitMelee_AS = aSParent.AddComponent<AudioSource>();
            upgradeUnitMelee_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.UpgradeMelee);
            upgradeUnitMelee_AS.volume = 0.2f;
            _sounds0.Add(ClipTypes.UpgradeMelee, gameW.NewEntity().Add(new AudioSourceVC(upgradeUnitMelee_AS)));


            var shiftUnit_AS = aSParent.AddComponent<AudioSource>();
            shiftUnit_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.ClickToTable);
            shiftUnit_AS.volume = 0.6f;


            var afterBuildTown_AS = aSParent.AddComponent<AudioSource>();
            afterBuildTown_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AfterBuildTown);
            afterBuildTown_AS.volume = 0.2f;
            _sounds0.Add(ClipTypes.AfterBuildTown, gameW.NewEntity().Add(new AudioSourceVC(afterBuildTown_AS)));


            var truce_AS = aSParent.AddComponent<AudioSource>();
            truce_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Truce);
            truce_AS.volume = 0.6f;
            _sounds0.Add(ClipTypes.Truce, gameW.NewEntity().Add(new AudioSourceVC(truce_AS)));


            var cur_AS = aSParent.AddComponent<AudioSource>();
            cur_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickUpgrade);
            cur_AS.volume = 0.3f;
            _sounds0.Add(ClipTypes.PickUpgrade, gameW.NewEntity().Add(new AudioSourceVC(cur_AS)));


            cur_AS = aSParent.AddComponent<AudioSource>();
            cur_AS.clip = Resources.Load<AudioClip>("GetHero_Clip");
            cur_AS.volume = 0.25f;
            _sounds0.Add(ClipTypes.GetHero, gameW.NewEntity().Add(new AudioSourceVC(cur_AS)));



            for (var uniq = AbilityTypes.None + 1; uniq < AbilityTypes.End; uniq++)
            {
                if (uniq != AbilityTypes.CircularAttack
                    && uniq != AbilityTypes.PutOutFirePawn
                    && uniq != AbilityTypes.PutOutFirePawn
                    && uniq != AbilityTypes.ChangeCornerArcher)
                {
                    cur_AS = aSParent.AddComponent<AudioSource>();
                    cur_AS.clip = ClipResourcesVC.AudioClip(uniq);
                    _sounds1.Add(uniq, gameW.NewEntity().Add(new AudioSourceVC(cur_AS)));

                    var volume = 0f;
                    switch (uniq)
                    {
                        case AbilityTypes.CircularAttack: throw new Exception();
                        case AbilityTypes.BonusNear: volume = 0.3f; break;
                        case AbilityTypes.FirePawn: volume = 0.2f; break;
                        case AbilityTypes.PutOutFirePawn: throw new Exception();
                        case AbilityTypes.Seed: volume = 0.2f; break;
                        case AbilityTypes.FireArcher: volume = 0.2f; break;
                        case AbilityTypes.ChangeCornerArcher: throw new Exception();
                        case AbilityTypes.GrowAdultForest: volume = 0.3f; break;
                        case AbilityTypes.StunElfemale: volume = 0.3f; break;
                        case AbilityTypes.ChangeDirectionWind: volume = 0.1f; break;
                        case AbilityTypes.IceWall: volume = 0.1f; break;
                        case AbilityTypes.DestroyBuilding: volume = 0.1f; break;
                        case AbilityTypes.Farm: volume = 0.1f; break;
                        case AbilityTypes.Mine: volume = 0.1f; break;
                        case AbilityTypes.City: volume = 0.1f; break;
                        default: throw new Exception();
                    }

                    cur_AS.volume = volume;
                }
            }


            action0 = new Dictionary<ClipTypes, Action>();
            foreach (var item in _sounds0) action0.Add(item.Key, item.Value.Get<AudioSourceVC>().Play);
            action1 = new Dictionary<AbilityTypes, Action>();
            foreach (var item in _sounds1) action1.Add(item.Key, item.Value.Get<AudioSourceVC>().Play);
        }
    }

    public interface IPhotonE { }
    public interface ISoundE { }
    public interface IBackgroundE { }
    public interface IGeneralZoneE { }
}