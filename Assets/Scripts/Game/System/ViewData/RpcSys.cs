using Game.Common;
using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public sealed class RpcSys : MonoBehaviour, IEcsInitSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC, WaterC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, UnitEffectsC, StunC> _effUnitF = default;

        private EcsFilter<UniqAbilC, CooldownUniqC> _uniqUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;
        private EcsFilter<CornerArcherC> _archerF = default;


        private EcsFilter<BuildC, OwnerC> _buildF = default;
        private EcsFilter<EnvC, EnvResC> _envF = default;
        private EcsFilter<FireC> _fireF = default;
        private EcsFilter<RiverC> _riverF = default;
        private EcsFilter<TrailC> _trailF = default;
        private EcsFilter<CloudC> _cloudF = default;


        private static PhotonView PhotonView => RpcVC.PhotonView;

        private static string MasterRPCName => nameof(MasterRPC);
        private static string GeneralRPCName => nameof(GeneralRPC);
        private static string OtherRPCName => nameof(OtherRPC);
        private static string SyncMasterRPCName => nameof(SyncAllMaster);

        private int _idx_cur;

        public void Init()
        {
            PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);

            if (!PhotonNetwork.IsMasterClient)
            {
                SyncAllToMast();
            }
        }

        #region ToMaster

        #region Methods

        #region Uniq

        public static void FireArcherToMas(byte fromIdx, byte toIdx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.FireArcher, fromIdx, toIdx });
        public static void FirePawnToMas(byte idx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.FirePawn, idx });
        public static void PutOutFirePawnToMas(byte idx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.PutOutFirePawn, idx });
        public static void SeedEnvToMaster(byte idxCell, EnvTypes env) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.Seed, idxCell, env });
        public static void ChangeCornerArchToMas(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.ChangeCornerArcher, idxCell });

        public static void BonusNearUnits(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.BonusNear, idxCell });

        public static void StunElfemaleToMas(byte fromIdx, byte toIdx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.StunElfemale, fromIdx, toIdx });

        public static void GrowAdultForest(byte idx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.GrowAdultForest, idx });
        public static void PutOutFireElffToMas(byte fromIdx, byte toIdx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.ChangeDirWind, fromIdx, toIdx });

        public static void CircularAttackKingToMaster(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UniqAbil, new object[] { UniqAbilTypes.CircularAttack, idxCell });

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
                var uniqAbil = (UniqAbilTypes)objects[_idx_cur++];

                switch (uniqAbil)
                {
                    case UniqAbilTypes.None: throw new Exception();

                    case UniqAbilTypes.CircularAttack:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqAbilTypes.BonusNear:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqAbilTypes.FirePawn:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqAbilTypes.PutOutFirePawn:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqAbilTypes.Seed:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        EnvDoingMC.Set((EnvTypes)objects[_idx_cur++]);
                        break;

                    case UniqAbilTypes.FireArcher:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqAbilTypes.GrowAdultForest:
                        ForGrowAdultForestMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqAbilTypes.StunElfemale:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqAbilTypes.ChangeDirWind:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqAbilTypes.ChangeCornerArcher:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    default: throw new Exception();
                }

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

            SyncAllToMast();
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
        public static void SoundToGeneral(RpcTarget rpcTarget, UniqAbilTypes uniq) => PhotonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.SoundUniq, new object[] { uniq });
        public static void SoundToGeneral(Player playerTo, ClipTypes eff) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.SoundEff, new object[] { eff });
        public static void SoundToGeneral(Player playerTo, UniqAbilTypes uniq) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.SoundUniq, new object[] { uniq });

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

                    SoundEffectC.Play(ClipTypes.Mistake);
                    break;

                case RpcGeneralTypes.SoundEff:
                    var soundEffectType = (ClipTypes)objects[_idx_cur++];
                    SoundEffectC.Play(soundEffectType);
                    break;

                case RpcGeneralTypes.SoundUniq:
                    SoundEffectC.Play((UniqAbilTypes)objects[_idx_cur++]);
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

        public void SyncAllToMast() => PhotonView.RPC(SyncMasterRPCName, RpcTarget.MasterClient);

        [PunRPC]
        private void SyncAllMaster()
        {
            var objs = new List<object>();


            foreach (var idx_0 in _effUnitF)
            {
                ref var unitC_0 = ref _unitF.Get1(idx_0);
                objs.Add(unitC_0.Unit);
                objs.Add(_unitF.Get3(idx_0).Owner);
                objs.Add(_unitF.Get2(idx_0).Level);
                ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);
                objs.Add(hpUnit_0.Hp);
                objs.Add(_statUnitF.Get2(idx_0).Steps);

                objs.Add(_effUnitF.Get1(idx_0).Condition);
                foreach (var item in _effUnitF.Get2(idx_0).Effects) objs.Add(item.Value);
                objs.Add(_statUnitF.Get3(idx_0).Water);

                objs.Add(_twUnitF.Get1(idx_0).ToolWeapon);
                objs.Add(_twUnitF.Get1(idx_0).LevelTWType);
                objs.Add(_twUnitF.Get1(idx_0).ShieldProt);

                objs.Add(_effUnitF.Get3(idx_0).IsStunned);
                objs.Add(_effUnitF.Get3(idx_0).StepsInStun);

                objs.Add(_archerF.Get1(idx_0).IsCornered);

                foreach (var item in _uniqUnitF.Get2(idx_0).Cooldowns)
                    objs.Add(item.Value);





                objs.Add(_buildF.Get1(idx_0).Build);
                objs.Add(_buildF.Get2(idx_0).Owner);



                ref var env_0 = ref _envF.Get1(idx_0);
                ref var envRes_0 = ref _envF.Get2(idx_0);
                foreach (var item in env_0.Envronments) objs.Add(item.Value);
                foreach (var item in envRes_0.Resources) objs.Add(item.Value);



                objs.Add(_riverF.Get1(idx_0).River);
                foreach (var item_0 in _riverF.Get1(idx_0).DirectsDict)
                    objs.Add(item_0.Value);


                foreach (var item_0 in _trailF.Get1(idx_0).Health)
                    objs.Add(item_0.Value);


                ref var cloud_0 = ref _cloudF.Get1(idx_0);
                objs.Add(cloud_0.Have);
                //objs.Add(cloud_0.CloudWidth);


                objs.Add(_fireF.Get1(idx_0).Have);


                
            }


            #region Cooldowns

            foreach (var item_0 in ScoutHeroCooldownC.Cooldowns)
            {
                foreach (var item_1 in item_0.Value)
                {
                    objs.Add(ScoutHeroCooldownC.Cooldown(item_0.Key, item_1.Key));
                }
            }

            #endregion


            #region Upgrades

            foreach (var item_0 in UnitUpgC.Upgrades) objs.Add(item_0.Value);

            foreach (var item_0 in BuildsUpgC.HaveUpgrades)
            {
                foreach (var item_1 in item_0.Value)
                {
                    objs.Add(BuildsUpgC.HaveUpgrade(item_0.Key, item_1.Key));
                }
            }

            #endregion


            #region Inventor

            foreach (var item_0 in InvResC.AmountResour)
            {
                foreach (var item_1 in item_0.Value)
                {
                    objs.Add(InvResC.AmountResour[item_0.Key][item_1.Key]);
                }
            }

            foreach (var item_0 in InvUnitsC.Units)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        objs.Add(InvUnitsC.Units[item_0.Key][item_1.Key][item_2.Key]);
                    }
                }
            }

            foreach (var item_0 in InvTWC.ToolWeapons)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        objs.Add(InvTWC.AmountToolWeap(item_0.Key, item_1.Key, item_2.Key));
                    }
                }
            }

            #endregion


            #region Where

            foreach (var item_0 in WhereUnitsC.UnitsInGame)
                foreach (var item_1 in item_0.Value)
                    foreach (var item_2 in item_1.Value)
                    {
                        if (item_2.Value.Count == 0) objs.Add(true);
                        else
                        {
                            foreach (var item_3 in item_2.Value)
                            {
                                objs.Add(false);
                                objs.Add(item_3);
                            }

                            objs.Add(true);
                        }
                    }

            foreach (var item_0 in WhereBuildsC.BuildsInGame)
                foreach (var item_1 in item_0.Value)
                {
                    if (item_1.Value.Count == 0) objs.Add(true);
                    else
                    {
                        foreach (var item_3 in item_1.Value)
                        {
                            objs.Add(false);
                            objs.Add(item_3);
                        }

                        objs.Add(true);
                    }
                }

            foreach (var item_0 in WhereEnvC.EnvInGame)
                if (item_0.Value.Count == 0) objs.Add(true);
                else
                {
                    foreach (var item_3 in item_0.Value)
                    {
                        objs.Add(false);
                        objs.Add(item_3);
                    }

                    objs.Add(true);
                }

            #endregion


            #region PickUpgade

            foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);

            #endregion


            #region Other

            objs.Add(WhoseMoveC.WhoseMove);
            objs.Add(PlyerWinnerC.PlayerWinner);
            objs.Add(ReadyC.IsStartedGame);
            objs.Add(ReadyC.IsReady(PlayerTypes.Second));

            objs.Add(MotionsC.AmountMotions);

            objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            PhotonView.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);
            PhotonView.RPC(nameof(UpdateVision), RpcTarget.Others);
        }

        [PunRPC]
        private void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            foreach (var idx_0 in _effUnitF)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                unit_0.Sync((UnitTypes)objects[_idx_cur++]);
                _unitF.Get3(idx_0).Sync((PlayerTypes)objects[_idx_cur++]);
                _unitF.Get2(idx_0).Sync((LevelTypes)objects[_idx_cur++]);
                _statUnitF.Get1(idx_0).Sync((int)objects[_idx_cur++]);
                _statUnitF.Get2(idx_0).Sync((int)objects[_idx_cur++]);

                _effUnitF.Get1(idx_0).Sync((CondUnitTypes)objects[_idx_cur++]);
                foreach (var item in _effUnitF.Get2(idx_0).Effects) _effUnitF.Get2(idx_0).Sync(item.Key, (bool)objects[_idx_cur++]);
                _statUnitF.Get3(idx_0).Sync((int)objects[_idx_cur++]);

                _twUnitF.Get1(idx_0).ToolWeapon = (TWTypes)objects[_idx_cur++];
                _twUnitF.Get1(idx_0).LevelTWType = (LevelTypes)objects[_idx_cur++];
                _twUnitF.Get1(idx_0).SyncShield((int)objects[_idx_cur++]);

                
                _effUnitF.Get3(idx_0).Sync((bool)objects[_idx_cur++], (int)objects[_idx_cur++]);

                _archerF.Get1(idx_0).Sync((bool)objects[_idx_cur++]);

                foreach (var item in _uniqUnitF.Get2(idx_0).Cooldowns)
                    _uniqUnitF.Get2(idx_0).Sync(item.Key, (int)objects[_idx_cur++]);





                _buildF.Get1(idx_0).Sync((BuildTypes)objects[_idx_cur++]);
                _buildF.Get2(idx_0).Sync((PlayerTypes)objects[_idx_cur++]);



                ref var env_0 = ref _envF.Get1(idx_0);
                ref var envRes_0 = ref _envF.Get2(idx_0);
                foreach (var item in env_0.Envronments) env_0.Sync(item.Key, (bool)objects[_idx_cur++]);
                foreach (var item in envRes_0.Resources) envRes_0.Sync(item.Key, (int)objects[_idx_cur++]);


                ref var river_0 = ref _riverF.Get1(idx_0);
                river_0.Sync((RiverTypes)objects[_idx_cur++]);
                foreach (var item_0 in river_0.DirectsDict)
                    river_0.Sync(item_0.Key, (bool)objects[_idx_cur++]);



                ref var trail_0 = ref _trailF.Get1(idx_0);
                foreach (var item_0 in trail_0.Health)
                    trail_0.SyncTrail(item_0.Key, (int)objects[_idx_cur++]);



                ref var cloud_0 = ref _cloudF.Get1(idx_0);
                cloud_0.Sync((bool)objects[_idx_cur++]/*, (CloudWidthTypes)objects[_curIdx++]*/);



                ref var fire_0 = ref _fireF.Get1(idx_0);
                fire_0.Sync((bool)objects[_idx_cur++]);
            }


            #region Cooldowns

            foreach (var item_0 in ScoutHeroCooldownC.Cooldowns)
            {
                foreach (var item_1 in item_0.Value)
                {
                    ScoutHeroCooldownC.Sync(item_0.Key, item_1.Key, (int)objects[_idx_cur++]);
                }
            }

            #endregion


            #region Upgrades

            foreach (var item_0 in UnitUpgC.Upgrades) UnitUpgC.Sync(item_0.Key, (bool)objects[_idx_cur++]);

            foreach (var item_0 in BuildsUpgC.HaveUpgrades)
            {
                foreach (var item_1 in item_0.Value)
                {
                    BuildsUpgC.Sync(item_0.Key, item_1.Key, (bool)objects[_idx_cur++]);
                }
            }

            #endregion


            #region Inventor

            foreach (var item_0 in InvResC.AmountResour)
            {
                foreach (var item_1 in item_0.Value)
                {
                    InvResC.Set(item_0.Key, item_1.Key, (int)objects[_idx_cur++]);
                }
            }
            foreach (var item_0 in InvUnitsC.Units)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        InvUnitsC.Set(item_0.Key, item_1.Key, item_2.Key, (int)objects[_idx_cur++]);
                    }
                }
            }
            foreach (var item_0 in InvTWC.ToolWeapons)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        InvTWC.Set(item_0.Key, item_1.Key, item_2.Key, (int)objects[_idx_cur++]);
                    }
                }
            }

            #endregion


            #region Where

            foreach (var item_0 in WhereUnitsC.UnitsInGame)
                foreach (var item_1 in item_0.Value)
                    foreach (var item_2 in item_1.Value)
                    {
                        var needContinue = false;

                        WhereUnitsC.Clear(item_0.Key, item_1.Key, item_2.Key);
                        for (int i = 0; i < Byte.MaxValue; i++)
                        {
                            var obj = objects[_idx_cur++];
                            needContinue = (bool)obj;
                            if (needContinue == true) break;

                            WhereUnitsC.Sync(item_0.Key, item_1.Key, item_2.Key, (byte)objects[_idx_cur++]);
                        }

                        if (needContinue) continue;
                    }

            foreach (var item_0 in WhereBuildsC.BuildsInGame)
                foreach (var item_1 in item_0.Value)
                {
                    var needContinue = false;

                    WhereBuildsC.Clear(item_0.Key, item_1.Key);
                    for (int i = 0; i < Byte.MaxValue; i++)
                    {
                        var obj = objects[_idx_cur++];
                        needContinue = (bool)obj;
                        if (needContinue == true) break;

                        WhereBuildsC.Sync(item_0.Key, item_1.Key, (byte)objects[_idx_cur++]);
                    }

                    if (needContinue) continue;
                }

            foreach (var item_0 in WhereEnvC.EnvInGame)
            {
                var needContinue = false;

                WhereEnvC.Clear(item_0.Key);
                for (int i = 0; i < Byte.MaxValue; i++)
                {
                    var obj = objects[_idx_cur++];
                    needContinue = (bool)obj;
                    if (needContinue == true) break;

                    WhereEnvC.SyncAdd(item_0.Key, (byte)objects[_idx_cur++]);
                }
                if (needContinue) continue;
            }


            #endregion


            #region PickUpgade

            foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);

            #endregion


            #region Other

            WhoseMoveC.SetWhoseMove((PlayerTypes)objects[_idx_cur++]);
            PlyerWinnerC.PlayerWinner = (PlayerTypes)objects[_idx_cur++];
            ReadyC.IsStartedGame = (bool)objects[_idx_cur++];
            ReadyC.SetIsReady(WhoseMoveC.CurPlayerI, (bool)objects[_idx_cur++]);


            MotionsC.AmountMotions = (int)objects[_idx_cur++];

            WindC.Sync((DirectTypes)objects[_idx_cur++]);

            #endregion
        }

        [PunRPC]
        private void UpdateVision()
        {
            DataViewSC.RotateAll.Invoke();
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