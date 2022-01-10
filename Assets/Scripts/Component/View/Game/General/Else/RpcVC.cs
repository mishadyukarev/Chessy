using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class RpcVC : MonoBehaviour
    {
        int _idx_cur;

        public static RpcVC Instance { get; private set; }
        public static List<string> NamesMethods
        {
            get
            {
                var list = new List<string>();
                list.Add(nameof(MasterRPC));
                list.Add(nameof(GeneralRPC));
                list.Add(nameof(OtherRPC));
                return list;
            }
        }

        public void Init()
        {
            Instance = this;

            PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);

            //SyncAllMaster();

            //if (!PhotonNetwork.IsMasterClient)
            //{
            //    SyncAllToMast();
            //}
        }


        [PunRPC]
        void MasterRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var rpcT = (RpcMasterTypes)objects[_idx_cur++];

            InfoC.AddInfo(MGOTypes.Master, infoFrom);

            if (rpcT == RpcMasterTypes.UniqAbil)
            {
                var uniqAbil = (UniqueAbilTypes)objects[_idx_cur++];

                switch (uniqAbil)
                {
                    case UniqueAbilTypes.None: throw new Exception();

                    case UniqueAbilTypes.CircularAttack:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilTypes.BonusNear:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilTypes.FirePawn:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilTypes.PutOutFirePawn:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilTypes.Seed:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        EnvDoingMC.Set((EnvTypes)objects[_idx_cur++]);
                        break;

                    case UniqueAbilTypes.FireArcher:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilTypes.GrowAdultForest:
                        ForGrowAdultForestMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilTypes.StunElfemale:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilTypes.ChangeDirWind:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilTypes.ChangeCornerArcher:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    default: throw new Exception();
                }

                UniqueAbilityMC.Set(uniqAbil);
                DataMastSC.InvokeRun(uniqAbil);
            }

            else
            {
                switch (rpcT)
                {
                    case RpcMasterTypes.None:
                        throw new Exception();

                    case RpcMasterTypes.Ready:
                        break;

                    case RpcMasterTypes.Done:
                        break;

                    case RpcMasterTypes.Build:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        BuildDoingMC.Set((BuildTypes)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.DestroyBuild:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.Shift:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.Attack:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.ConditionUnit:
                        CondDoingMC.Set((CondUnitTypes)objects[_idx_cur++]);
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.CreateUnit:
                        UnitDoingMC.Set((UnitTypes)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.MeltOre:
                        break;

                    case RpcMasterTypes.SetUnit:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        UnitDoingMC.Set((UnitTypes)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.BuyRes:
                        ForBuyResMasC.Res = (ResTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.ToNewUnit:
                        UnitDoingMC.Set((UnitTypes)objects[_idx_cur++]);
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.FromToNewUnit:
                        UnitDoingMC.Set((UnitTypes)objects[_idx_cur++]);
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.UpgradeUnit:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.GiveTakeToolWeapon:
                        TWDoingMC.Set((TWTypes)objects[_idx_cur++], (LevelTypes)objects[_idx_cur++]);
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.GetHero:
                        UnitDoingMC.Set((UnitTypes)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.UpgUnits:
                        UnitDoingMC.Set((UnitTypes)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.UpgBuilds:
                        BuildDoingMC.Set((BuildTypes)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.UpgWater:
                        break;

                    default:
                        throw new Exception();
                }

                DataMastSC.InvokeRun(rpcT);
            }

            SyncAllMaster();
        }

        [PunRPC]
        void GeneralRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;
            var rpcT = (RpcGeneralTypes)objects[_idx_cur++];

            InfoC.AddInfo(MGOTypes.General, infoFrom);

            switch (rpcT)
            {
                case RpcGeneralTypes.None:
                    throw new Exception();

                case RpcGeneralTypes.Mistake:
                    var mistakeType = (MistakeTypes)objects[_idx_cur++];
                    MistakeC.MistakeType = mistakeType;
                    MistakeC.CurTime = default;

                    if (mistakeType == MistakeTypes.Economy)
                    {
                        MistakeC.ClearAllNeeds();

                        var needRes = (int[])objects[_idx_cur++];

                        MistakeC.AddNeedRes(ResTypes.Food, needRes[0]);
                        MistakeC.AddNeedRes(ResTypes.Wood, needRes[1]);
                        MistakeC.AddNeedRes(ResTypes.Ore, needRes[2]);
                        MistakeC.AddNeedRes(ResTypes.Iron, needRes[3]);
                        MistakeC.AddNeedRes(ResTypes.Gold, needRes[4]);
                    }

                    SoundEffectVC.Play(ClipTypes.Mistake);
                    break;

                case RpcGeneralTypes.SoundEff:
                    var soundEffectType = (ClipTypes)objects[_idx_cur++];
                    SoundEffectVC.Play(soundEffectType);
                    break;

                case RpcGeneralTypes.SoundUniq:
                    SoundEffectVC.Play((UniqueAbilTypes)objects[_idx_cur++]);
                    break;

                case RpcGeneralTypes.ActiveMotion:
                    MotionsC.IsActivated = true;
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        void OtherRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;
            var rpcT = (RpcOtherTypes)objects[_idx_cur++];
            InfoC.AddInfo(MGOTypes.Other, infoFrom);

            switch (rpcT)
            {
                case RpcOtherTypes.None:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }


        #region SyncData

        //public void SyncAllToMast() => Photon<PhotonVC>().RPC(SyncMasterRPCName, RpcTarget.MasterClient);

        //[PunRPC]
        public void SyncAllMaster()
        {
            var objs = new List<object>();


            foreach (byte idx_0 in Idxs)
            {
                objs.Add(Unit<UnitC>(idx_0).Unit);
                objs.Add(Unit<LevelC>(idx_0).Level);
                objs.Add(Unit<OwnerC>(idx_0).Owner);

                objs.Add(Unit<HpC>(idx_0).Hp);
                objs.Add(Unit<StepC>(idx_0).Steps);
                objs.Add(Unit<WaterC>(idx_0).Water);

                objs.Add(Unit<ConditionC>(idx_0).Condition);
                foreach (var item in Unit<EffectsC>(idx_0).Effects) objs.Add(item.Value);


                objs.Add(UnitTW<ToolWeaponC>(idx_0).ToolWeapon);
                objs.Add(UnitTW<LevelC>(idx_0).Level);
                objs.Add(UnitTW<ProtectionC>(idx_0).Protection);

                objs.Add(Unit<StunC>(idx_0).IsStunned);
                objs.Add(Unit<StunC>(idx_0).StepsInStun);

                objs.Add(Unit<CornerArcherC>(idx_0).IsCornered);

                foreach (var item in Unit<CooldownUniqC>(idx_0).Cooldowns)
                    objs.Add(item.Value);





                objs.Add(Build<BuildC>(idx_0).Build);
                objs.Add(Build<OwnerC>(idx_0).Owner);



                ref var env_0 = ref Environment<EnvironmentC>(idx_0);
                ref var envRes_0 = ref Environment<EnvResC>(idx_0);
                foreach (var item_0 in env_0.Envronments)
                    foreach (var item_1 in envRes_0.Resources)
                    {
                        objs.Add(item_0.Value);
                        objs.Add(item_1.Value);
                    }




                objs.Add(River<RiverC>(idx_0).River);
                foreach (var item_0 in River<RiverC>(idx_0).DirectsDict)
                    objs.Add(item_0.Value);


                foreach (var item_0 in Trail<TrailCellEC>(idx_0).Health)
                    objs.Add(item_0.Value);


                objs.Add(Cloud<HaveEffectC>(idx_0).Have);


                objs.Add(Fire<HaveEffectC>(idx_0).Have);



            }


            foreach (var item_0 in ScoutHeroCooldownC.Cooldowns) objs.Add(item_0.Value);


            foreach (var item_0 in UnitUpgC.Upgrades) objs.Add(item_0.Value);
            foreach (var item_0 in BuildsUpgC.HaveUpgrades) objs.Add(item_0.Value);


            foreach (var item_0 in InvResC.Resources) objs.Add(item_0.Value);
            foreach (var item_0 in InvUnitsC.Units) objs.Add(item_0.Value);
            foreach (var item_0 in InvTWC.ToolWeapons) objs.Add(item_0.Value);


            foreach (var item in WhereUnitsC.Units) objs.Add(item.Value);
            foreach (var item in WhereBuildsC.Cells) objs.Add(item.Value);
            foreach (var item in WhereEnvC.Envs) objs.Add(item.Value);


            foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);


            #region Other

            objs.Add(WhoseMoveC.WhoseMove);
            objs.Add(PlayerWinnerC.PlayerWinner);
            objs.Add(ReadyC.IsStartedGame);
            objs.Add(ReadyC.IsReady(PlayerTypes.Second));

            objs.Add(MotionsC.AmountMotions);

            objs.Add(CloudCenterC.Idx);
            foreach (var item in WindC.Directs) objs.Add(item.Value);
            objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            EntityPool.Rpc<RpcC>().RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            EntityPool.Rpc<RpcC>().RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] {});
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            foreach (byte idx_0 in Idxs)
            {
                Unit<UnitCellEC>(idx_0).Sync((UnitTypes)objects[_idx_cur++], (LevelTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++], (int)objects[_idx_cur++], (int)objects[_idx_cur++], (int)objects[_idx_cur++]);

                Unit<ConditionC>(idx_0).Sync((CondUnitTypes)objects[_idx_cur++]);
                foreach (var item in Unit<EffectsC>(idx_0).Effects) Unit<EffectsC>(idx_0).Sync(item.Key, (bool)objects[_idx_cur++]);

                UnitTW<UnitTWCellEC>(idx_0).Sync((TWTypes)objects[_idx_cur++], (LevelTypes)objects[_idx_cur++], (int)objects[_idx_cur++]);


                Unit<StunC>(idx_0).Sync((bool)objects[_idx_cur++], (int)objects[_idx_cur++]);

                Unit<CornerArcherC>(idx_0).Sync((bool)objects[_idx_cur++]);

                foreach (var item in Unit<CooldownUniqC>(idx_0).Cooldowns)
                    Unit<CooldownUniqC>(idx_0).Sync(item.Key, (int)objects[_idx_cur++]);





                Build<BuildCellEC>(idx_0).Sync((BuildTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++]);


                ref var envCell_0 = ref Environment<EnvCellEC>(idx_0);
                ref var env_0 = ref Environment<EnvironmentC>(idx_0);
                ref var envRes_0 = ref Environment<EnvResC>(idx_0);
                foreach (var item_0 in env_0.Envronments)
                    foreach (var item_1 in envRes_0.Resources)
                        envCell_0.Sync(item_1.Key, (bool)objects[_idx_cur++], (int)objects[_idx_cur++]);


                ref var river_0 = ref River<RiverC>(idx_0);
                river_0.Sync((RiverTypes)objects[_idx_cur++]);
                foreach (var item_0 in river_0.DirectsDict)
                    river_0.Sync(item_0.Key, (bool)objects[_idx_cur++]);



                ref var trail_0 = ref Trail<TrailCellEC>(idx_0);
                foreach (var item_0 in trail_0.Health)
                    trail_0.SyncTrail(item_0.Key, (int)objects[_idx_cur++]);



                Cloud<HaveEffectC>(idx_0).Sync((bool)objects[_idx_cur++]);
                Fire<HaveEffectC>(idx_0).Have = (bool)objects[_idx_cur++];
            }


            foreach (var item_0 in ScoutHeroCooldownC.Cooldowns) ScoutHeroCooldownC.Sync(item_0.Key, (int)objects[_idx_cur++]);


            foreach (var item_0 in UnitUpgC.Upgrades) UnitUpgC.Sync(item_0.Key, (bool)objects[_idx_cur++]);
            foreach (var item_0 in BuildsUpgC.HaveUpgrades) BuildsUpgC.Sync(item_0.Key, (bool)objects[_idx_cur++]);


            foreach (var item_0 in InvResC.Resources) InvResC.Sync(item_0.Key, (int)objects[_idx_cur++]);
            foreach (var item_0 in InvUnitsC.Units) InvUnitsC.Sync(item_0.Key, (int)objects[_idx_cur++]);
            foreach (var item_0 in InvTWC.ToolWeapons) InvTWC.Sync(item_0.Key, (int)objects[_idx_cur++]);


            foreach (var item_0 in WhereUnitsC.Units) WhereUnitsC.Sync(item_0.Key, (bool)objects[_idx_cur++]);
            foreach (var item_0 in WhereBuildsC.Cells) WhereBuildsC.Sync(item_0.Key, (bool)objects[_idx_cur++]);
            foreach (var item_0 in WhereEnvC.Envs) WhereEnvC.Sync(item_0.Key, (bool)objects[_idx_cur++]);


            foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);


            #region Other

            WhoseMoveC.SetWhoseMove((PlayerTypes)objects[_idx_cur++]);
            PlayerWinnerC.PlayerWinner = (PlayerTypes)objects[_idx_cur++];
            ReadyC.IsStartedGame = (bool)objects[_idx_cur++];
            ReadyC.SetIsReady(WhoseMoveC.CurPlayerI, (bool)objects[_idx_cur++]);


            MotionsC.AmountMotions = (int)objects[_idx_cur++];

            CloudCenterC.Sync((byte)objects[_idx_cur++]);
            foreach (var item in WindC.Directs) WindC.Sync(item.Key, (byte)objects[_idx_cur++]);
            WindC.Sync((DirectTypes)objects[_idx_cur++]);

            #endregion
        }


        [PunRPC]
        void UpdateDataAndView(object[] objects)
        {
            DataSC.Run(DataSTypes.RunAfterUpdate);
        }

        #endregion


        #region Serialize

        public static object DeserializeVector2Int(byte[] data)
        {
            Vector2Int result = new Vector2Int();

            result.x = BitConverter.ToInt32(data, 0);
            result.y = BitConverter.ToInt32(data, 4);

            return result;

        }
        public static byte[] SerializeVector2Int(object obj)
        {
            Vector2Int vector = (Vector2Int)obj;
            byte[] result = new byte[8];

            BitConverter.GetBytes(vector.x).CopyTo(result, 0);
            BitConverter.GetBytes(vector.y).CopyTo(result, 4);

            return result;
        }

        #endregion
    }
}