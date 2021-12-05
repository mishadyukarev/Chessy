using Game.Common;
using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class RpcSys : MonoBehaviour, IEcsInitSystem
    {
        static PhotonView PhotonView => RpcVC.PhotonView;

        static string MasterRPCName => nameof(MasterRPC);
        static string GeneralRPCName => nameof(GeneralRPC);
        static string OtherRPCName => nameof(OtherRPC);
        static string SyncMasterRPCName => nameof(SyncAllMaster);

        int _idx_cur;

        public static RpcSys Instance { get; private set; }

        public void Init()
        {
            Instance = this;

            PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);

            SyncAllMaster();

            //if (!PhotonNetwork.IsMasterClient)
            //{
            //    SyncAllToMast();
            //}
        }

        #region ToMaster

        #region Methods

        #region Uniq

        public static void FireArcherToMas(byte fromIdx, byte toIdx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.FireArcher, fromIdx, toIdx });
        public static void FirePawnToMas(byte idx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.FirePawn, idx });
        public static void PutOutFirePawnToMas(byte idx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.PutOutFirePawn, idx });
        public static void SeedEnvToMaster(byte idxCell, EnvTypes env) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.Seed, idxCell, env });
        public static void ChangeCornerArchToMas(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.ChangeCornerArcher, idxCell });

        public static void BonusNearUnits(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.BonusNear, idxCell });

        public static void StunElfemaleToMas(byte fromIdx, byte toIdx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.StunElfemale, fromIdx, toIdx });

        public static void GrowAdultForest(byte idx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.GrowAdultForest, idx });
        public static void PutOutFireElffToMas(byte fromIdx, byte toIdx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.ChangeDirWind, fromIdx, toIdx });

        public static void CircularAttackKingToMaster(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqueAbilTypes.CircularAttack, idxCell });

        #endregion


        #region Upgrades

        public static void PickUpgUnitToMas(UnitTypes unit) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UpgUnits, new object[] { unit });
        public static void PickUpgBuildToMas(BuildTypes build) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UpgBuilds, new object[] { build });
        public static void UpgWater() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UpgWater, new object[] { });

        #endregion


        public static void ReadyToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Ready, new object[default]);

        public static void DoneToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Done, new object[default]);

        public static void BuyResToMaster(ResTypes res) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.BuyRes, new object[] { res });
        
        public static void GetHero(UnitTypes unit) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.GetHero, new object[] { unit });

        public static void ShiftUnitToMaster(byte idx_from, byte idx_to) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Shift, new object[] { idx_from, idx_to });
        public static void AttackUnitToMaster(byte idx_from, byte idx_to) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Attack, new object[] { idx_from, idx_to });

        public static void BuildToMaster(byte idxCellForBuild, BuildTypes buildingType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Build, new object[] { idxCellForBuild, buildingType });
        public static void DestroyBuildingToMaster(byte xyCellForDestroy) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.DestroyBuild, new object[] { xyCellForDestroy });

        public static void ConditionUnitToMaster(CondUnitTypes neededCondtionType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.ConditionUnit, new object[] { neededCondtionType, idxCell });

        public static void FromNewUnitToMas(UnitTypes unitType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.ToNewUnit, new object[] { unitType, idxCell });
        public static void FromToNewUnitToMas(UnitTypes unitType, byte idxFrom, byte idxTo) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.FromToNewUnit, new object[] { unitType, idxFrom, idxTo });
        public static void UpgradeUnitToMaster(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UpgradeUnit, new object[] { idxCell });
        public static void GiveTakeToolWeapon(TWTypes tw, LevelTypes level, byte idx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.GiveTakeToolWeapon, new object[] { tw, level, idx });

        public static void CreateUnitToMaster(UnitTypes unitType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.CreateUnit, new object[] { unitType });

        public static void MeltOreToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.MeltOre, new object[] { });

        public static void SetUniToMaster(byte idxCell, UnitTypes unitType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SetUnit, new object[] { idxCell, unitType });

        #endregion


        [PunRPC]
        private void MasterRPC(RpcMasterTypes rpcType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            InfoC.AddInfo(MGOTypes.Master, infoFrom);

            if(rpcType == RpcMasterTypes.UniqAbil)
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
                switch (rpcType)
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

                DataMastSC.InvokeRun(rpcType);
            }

            SyncAllMaster();
        }

        #endregion


        #region General

        public static void MistakeEconomyToGeneral(Player playerTo, Dictionary<ResTypes, int> needRes)
        {
            int[] needRes2 = new int[(int)ResTypes.End];
            needRes2[0] = needRes[ResTypes.Food];
            needRes2[1] = needRes[ResTypes.Wood];
            needRes2[2] = needRes[ResTypes.Ore];
            needRes2[3] = needRes[ResTypes.Iron];
            needRes2[4] = needRes[ResTypes.Gold];

            PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.Economy, needRes2 });
        }
        public static void SimpleMistakeToGeneral(MistakeTypes mistakeType, Player playerTo) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { mistakeType });



        public static void ActiveMotionZoneToGen(Player player) => PhotonView.RPC(GeneralRPCName, player, RpcGeneralTypes.ActiveMotion, new object[] { });

        public static void SoundToGeneral(RpcTarget rpcTarget, ClipTypes soundEffectType) => PhotonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.SoundEff, new object[] { soundEffectType });
        public static void SoundToGeneral(RpcTarget rpcTarget, UniqueAbilTypes uniq) => PhotonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.SoundUniq, new object[] { uniq });
        public static void SoundToGeneral(Player playerTo, ClipTypes eff) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.SoundEff, new object[] { eff });
        public static void SoundToGeneral(Player playerTo, UniqueAbilTypes uniq) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.SoundUniq, new object[] { uniq });

        [PunRPC]
        private void GeneralRPC(RpcGeneralTypes rpcGeneralType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;
            InfoC.AddInfo(MGOTypes.General, infoFrom);

            switch (rpcGeneralType)
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

        #endregion


        #region Other

        [PunRPC]
        private void OtherRPC(RpcOtherTypes rpcOtherType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;
            InfoC.AddInfo(MGOTypes.Other, infoFrom);

            switch (rpcOtherType)
            {
                case RpcOtherTypes.None:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        #endregion


        #region SyncData

        //public void SyncAllToMast() => PhotonView.RPC(SyncMasterRPCName, RpcTarget.MasterClient);

        //[PunRPC]
        public void SyncAllMaster()
        {
            var objs = new List<object>();


            foreach (byte idx_0 in Idxs)
            {
                objs.Add(Unit<UnitC>(idx_0).Unit);
                objs.Add(Unit<LevelC>(idx_0).Level);
                objs.Add(Unit<OwnerC>(idx_0).Owner);

                objs.Add(Unit<HpC>(idx_0).HP);
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



                ref var env_0 = ref Environment<EnvC>(idx_0);
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


                foreach (var item_0 in Trail<TrailC>(idx_0).Health)
                    objs.Add(item_0.Value);


                objs.Add(Cloud<CloudC>(idx_0).Have);


                objs.Add(Fire<FireC>(idx_0).Have);


                
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
            objs.Add(PlyerWinnerC.PlayerWinner);
            objs.Add(ReadyC.IsStartedGame);
            objs.Add(ReadyC.IsReady(PlayerTypes.Second));

            objs.Add(MotionsC.AmountMotions);

            objs.Add(CloudCenterC.Idx);
            foreach (var item in WindC.Directs) objs.Add(item.Value);
            objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            PhotonView.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            PhotonView.RPC(nameof(UpdateDataAndView), RpcTarget.All);
        }

        [PunRPC]
        private void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            foreach (byte idx_0 in Idxs)
            {
                Unit<UnitCellWC>(idx_0).Sync((UnitTypes)objects[_idx_cur++], (LevelTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++]);
                Unit<HpUnitWC>(idx_0).Sync((int)objects[_idx_cur++]);
                Unit<StepUnitWC>(idx_0).Sync((int)objects[_idx_cur++]);
                Unit<WaterUnitC>(idx_0).Sync((int)objects[_idx_cur++]);

                Unit<ConditionC>(idx_0).Sync((CondUnitTypes)objects[_idx_cur++]);
                foreach (var item in Unit<EffectsC>(idx_0).Effects) Unit<EffectsC>(idx_0).Sync(item.Key, (bool)objects[_idx_cur++]);

                UnitTW<UnitTWCellC>(idx_0).Sync((TWTypes)objects[_idx_cur++], (LevelTypes)objects[_idx_cur++], (int)objects[_idx_cur++]);


                Unit<StunC>(idx_0).Sync((bool)objects[_idx_cur++], (int)objects[_idx_cur++]);

                Unit<CornerArcherC>(idx_0).Sync((bool)objects[_idx_cur++]);

                foreach (var item in Unit<CooldownUniqC>(idx_0).Cooldowns)
                    Unit<CooldownUniqC>(idx_0).Sync(item.Key, (int)objects[_idx_cur++]);





                Build<BuildCellC>(idx_0).Sync((BuildTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++]);


                ref var envCell_0 = ref Environment<EnvCellC>(idx_0);
                ref var env_0 = ref Environment<EnvC>(idx_0);
                ref var envRes_0 = ref Environment<EnvResC>(idx_0);
                foreach (var item_0 in env_0.Envronments)
                    foreach (var item_1 in envRes_0.Resources)
                        envCell_0.Sync(item_1.Key, (bool)objects[_idx_cur++], (int)objects[_idx_cur++]);


                ref var river_0 = ref River<RiverC>(idx_0);
                river_0.Sync((RiverTypes)objects[_idx_cur++]);
                foreach (var item_0 in river_0.DirectsDict)
                    river_0.Sync(item_0.Key, (bool)objects[_idx_cur++]);



                ref var trail_0 = ref Trail<TrailC>(idx_0);
                foreach (var item_0 in trail_0.Health)
                    trail_0.SyncTrail(item_0.Key, (int)objects[_idx_cur++]);



                Cloud<CloudC>(idx_0).Sync((bool)objects[_idx_cur++]);
                Fire<FireC>(idx_0).Sync((bool)objects[_idx_cur++]);
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
            PlyerWinnerC.PlayerWinner = (PlayerTypes)objects[_idx_cur++];
            ReadyC.IsStartedGame = (bool)objects[_idx_cur++];
            ReadyC.SetIsReady(WhoseMoveC.CurPlayerI, (bool)objects[_idx_cur++]);


            MotionsC.AmountMotions = (int)objects[_idx_cur++];

            CloudCenterC.Sync((byte)objects[_idx_cur++]);
            foreach (var item in WindC.Directs) WindC.Sync(item.Key, (byte)objects[_idx_cur++]);
            WindC.Sync((DirectTypes)objects[_idx_cur++]);

            #endregion
        }


        [PunRPC]
        private void UpdateDataAndView()
        {
            DataSC.Run(DataSystTypes.RunAfterDoing);
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