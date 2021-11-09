﻿using Chessy.Common;
using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class RpcSys : MonoBehaviour, IEcsInitSystem
    {
        private EcsFilter<CellUnitDataC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataC, HpUnitC, StepComponent> _cellUnitStatFilt = default;
        private EcsFilter<CellUnitDataC, ConditionUnitC, UnitEffectsC, WaterUnitC> _cellUnitOthFilt = default;
        private EcsFilter<CellUnitDataC, ToolWeaponC> _cellUnitTWFilt = default;

        private EcsFilter<CellBuildDataC, OwnerC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC, CellEnvResC> _cellEnvrFilter = default;
        private EcsFilter<CellFireDataC> _cellFireFilter = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;
        private EcsFilter<CellTrailDataC> _cellTrailFilt = default;
        private EcsFilter<CellCloudDataC> _cellCloudFilt = default;


        private EcsFilter<ForBuildingMasCom, XyCellForDoingMasCom> _buildFilter = default;
        private EcsFilter<ForDestroyMasCom> _destroyFilter = default;
        private EcsFilter<ForAttackMasCom> _attackFilter = default;
        private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;
        private EcsFilter<ForSettingUnitMasCom, XyCellForDoingMasCom> _settingUnitFilter = default;
        private EcsFilter<ForSeedingMasCom, XyCellForDoingMasCom> _seedingFilter = default;
        private EcsFilter<ForFireMasCom> _fireFilter = default;
        private EcsFilter<ForCircularAttackMasCom, XyCellForDoingMasCom> _circularAttackFilter = default;
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGivePawnToolFilter = default;
        private EcsFilter<ForUpgradeUnitCom> _forUpgradeUnitFilt = default;
        private EcsFilter<ForOldNewUnitCom> _forOldToNewUnitFilt = default;

        private static PhotonView PhotonView => RpcViewC.PhotonView;

        private static string MasterRPCName => nameof(MasterRPC);
        private static string GeneralRPCName => nameof(GeneralRPC);
        private static string OtherRPCName => nameof(OtherRPC);
        private static string SyncMasterRPCName => nameof(SyncAllMaster);

        private int _curNumber;

        public void Init()
        {
            PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);

            if (!PhotonNetwork.IsMasterClient)
            {
                SyncAllToMast();
            }
        }


        #region StandartPunRPC

        #region Methods

        public static void ReadyToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Ready, new object[default]);

        public static void DoneToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Done, new object[default]);

        public static void RotateAllToMaster(Player sender) => PhotonView.RPC(GeneralRPCName, sender, RpcGeneralTypes.RotateAll, new object[default]);

        public static void BuyResToMaster(ResTypes res) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.BuyRes, new object[] { res });
        public static void PickUpgradeToMaster(PickUpgradeTypes upgBut) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.PickUpgrade, new object[] { upgBut });
        public static void GetHero(UnitTypes unit) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.GetHero, new object[] { unit });

        public static void ShiftUnitToMaster(byte idxPreviousCell, byte idxSelectedCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Shift, new object[] { idxPreviousCell, idxSelectedCell });
        public static void AttackUnitToMaster(byte idxPreviousCell, byte idxSelectedCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Attack, new object[] { idxPreviousCell, idxSelectedCell });

        public static void BuildToMaster(byte idxCellForBuild, BuildTypes buildingType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Build, new object[] { idxCellForBuild, buildingType });
        public static void DestroyBuildingToMaster(byte xyCellForDestroy) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.DestroyBuild, new object[] { xyCellForDestroy });

        public static void ConditionUnitToMaster(CondUnitTypes neededCondtionType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.ConditionUnit, new object[] { neededCondtionType, idxCell });

        public static void MistakeEconomyToGeneral(Player playerTo, Dictionary<ResTypes, int> needRes)
        {
            int[] needRes2 = new int[(int)Support.MaxResType];
            needRes2[0] = needRes[ResTypes.Food];
            needRes2[1] = needRes[ResTypes.Wood];
            needRes2[2] = needRes[ResTypes.Ore];
            needRes2[3] = needRes[ResTypes.Iron];
            needRes2[4] = needRes[ResTypes.Gold];

            PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.Economy, needRes2 });
        }
        public static void SimpleMistakeToGeneral(MistakeTypes mistakeType, Player playerTo) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { mistakeType });

        public static void FireToMaster(byte fromIdx, byte toIdx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Fire, new object[] { fromIdx, toIdx });
        public static void SeedEnvironmentToMaster(byte idxCell, EnvTypes environmentType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SeedEnvironment, new object[] { idxCell, environmentType });

        public static void BonusNearUnits(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.BonusNearUnitKing, new object[] { idxCell });

        public static void FromNewUnitToMas(UnitTypes unitType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.ToNewUnit, new object[] { unitType, idxCell });
        public static void FromToNewUnitToMas(UnitTypes unitType, byte idxFrom, byte idxTo) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.FromToNewUnit, new object[] { unitType, idxFrom, idxTo });
        public static void UpgradeUnitToMaster(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UpgradeUnit, new object[] { idxCell });
        public static void GiveTakeToolWeapon(ToolWeaponTypes toolAndWeaponType, LevelTWTypes levelTWType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.GiveTakeToolWeapon, new object[] { toolAndWeaponType, levelTWType, idxCell });

        public static void CircularAttackKingToMaster(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.CircularAttackKing, new object[] { idxCell });

        public static void CreateUnitToMaster(UnitTypes unitType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.CreateUnit, new object[] { unitType });

        public static void MeltOreToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.MeltOre, new object[] { });

        public static void GrowAdultForest(byte idx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.GrowAdultForest, new object[] { idx });

        public static void SetUniToMaster(byte idxCell, UnitTypes unitType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SetUnit, new object[] { idxCell, unitType });

        public static void SoundToGeneral(RpcTarget rpcTarget, ClipGameTypes soundEffectType) => PhotonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.Sound, new object[] { soundEffectType });
        public static void SoundToGeneral(Player playerTo, ClipGameTypes eff) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Sound, new object[] { eff });

        #endregion


        [PunRPC]
        private void MasterRPC(RpcMasterTypes rpcType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _curNumber = 0;

            InfoC.AddInfo(MGOTypes.Master, infoFrom);

            switch (rpcType)
            {
                case RpcMasterTypes.None:
                    throw new Exception();

                case RpcMasterTypes.Ready:
                    break;

                case RpcMasterTypes.Done:
                    break;

                case RpcMasterTypes.Build:
                    _buildFilter.Get1(0).BuildingTypeForBuidling = (BuildTypes)objects[1];
                    _buildFilter.Get1(0).IdxForBuild = (byte)objects[0];
                    break;

                case RpcMasterTypes.DestroyBuild:
                    _destroyFilter.Get1(0).IdxForDestroy = (byte)objects[0];
                    break;

                case RpcMasterTypes.Shift:
                    ForShiftMasCom.IdxFrom = (byte)objects[0];
                    ForShiftMasCom.IdxTo = (byte)objects[1];
                    break;

                case RpcMasterTypes.Attack:
                    _attackFilter.Get1(0).IdxFromCell = (byte)objects[0];
                    _attackFilter.Get1(0).IdxToCell = (byte)objects[1];
                    break;

                case RpcMasterTypes.ConditionUnit:
                    ForCondMasCom.NeededCondUnitType = (CondUnitTypes)objects[0];
                    ForCondMasCom.IdxForCondition = (byte)objects[1];
                    break;

                case RpcMasterTypes.CreateUnit:
                    _creatorUnitFilter.Get1(0).UnitTypeForCreating = (UnitTypes)objects[0];
                    break;

                case RpcMasterTypes.MeltOre:
                    break;

                case RpcMasterTypes.SetUnit:
                    _settingUnitFilter.Get1(0).IdxCellForSetting = (byte)objects[0];
                    _settingUnitFilter.Get1(0).UnitTypeForSetting = (UnitTypes)objects[1];
                    break;

                case RpcMasterTypes.SeedEnvironment:
                    _seedingFilter.Get1(0).IdxForSeeding = (byte)objects[0];
                    _seedingFilter.Get1(0).EnvTypeForSeeding = (EnvTypes)objects[1];
                    break;

                case RpcMasterTypes.Fire:
                    _fireFilter.Get1(0).FromIdx = (byte)objects[0];
                    _fireFilter.Get1(0).ToIdx = (byte)objects[1];
                    break;

                case RpcMasterTypes.BuyRes:
                    ForBuyResMasC.Res = (ResTypes)objects[_curNumber++];
                    break;

                case RpcMasterTypes.CircularAttackKing:
                    _circularAttackFilter.Get1(0).IdxUnitForCirculAttack = (byte)objects[0];
                    break;

                case RpcMasterTypes.BonusNearUnitKing:
                    ForBonusNearUnitC.IdxCell = (byte)objects[0];
                    break;

                case RpcMasterTypes.PickUpgrade:
                    ForPickUpgMasC.UpgButType = (PickUpgradeTypes)objects[0];
                    break;

                case RpcMasterTypes.ToNewUnit:
                    _forOldToNewUnitFilt.Get1(0).UnitType = (UnitTypes)objects[0];
                    _forOldToNewUnitFilt.Get1(0).IdxCell = (byte)objects[1];
                    break;

                case RpcMasterTypes.FromToNewUnit:
                    ForFromToNewUnitC.Set((UnitTypes)objects[_curNumber++],
                        (byte)objects[_curNumber++],
                        (byte)objects[_curNumber++]);
                    break;

                case RpcMasterTypes.UpgradeUnit:
                    _forUpgradeUnitFilt.Get1(0).idxCellForUpgrade = (byte)objects[0];
                    break;

                case RpcMasterTypes.GiveTakeToolWeapon:
                    _forGivePawnToolFilter.Get1(0).ToolWeapType = (ToolWeaponTypes)objects[_curNumber++];
                    _forGivePawnToolFilter.Get1(0).LevelTWType = (LevelTWTypes)objects[_curNumber++];
                    _forGivePawnToolFilter.Get1(0).IdxCell = (byte)objects[_curNumber++];
                    break;

                case RpcMasterTypes.GetHero:
                    ForGetHeroMasC.Unit = (UnitTypes)objects[_curNumber++];
                    break;

                case RpcMasterTypes.GrowAdultForest:
                    ForGrowAdultForestMC.Set((byte)objects[_curNumber++]);
                    break;

                default:
                    throw new Exception();
            }

            MastSysDataC.InvokeRun(rpcType);

            SyncAllToMast();
        }

        [PunRPC]
        private void GeneralRPC(RpcGeneralTypes rpcGeneralType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _curNumber = 0;
            InfoC.AddInfo(MGOTypes.General, infoFrom);

            switch (rpcGeneralType)
            {
                case RpcGeneralTypes.None:
                    throw new Exception();

                case RpcGeneralTypes.Mistake:
                    var mistakeType = (MistakeTypes)objects[_curNumber++];
                    MistakeDataUIC.MistakeType = mistakeType;
                    MistakeDataUIC.CurTime = default;

                    if (mistakeType == MistakeTypes.Economy)
                    {
                        MistakeDataUIC.ClearAllNeeds();

                        var needRes = (int[])objects[_curNumber++];

                        MistakeDataUIC.AddNeedRes(ResTypes.Food, needRes[0]);
                        MistakeDataUIC.AddNeedRes(ResTypes.Wood, needRes[1]);
                        MistakeDataUIC.AddNeedRes(ResTypes.Ore, needRes[2]);
                        MistakeDataUIC.AddNeedRes(ResTypes.Iron, needRes[3]);
                        MistakeDataUIC.AddNeedRes(ResTypes.Gold, needRes[4]);
                    }

                    SoundEffectC.Play(ClipGameTypes.Mistake);
                    break;

                case RpcGeneralTypes.Sound:
                    var soundEffectType = (ClipGameTypes)objects[_curNumber++];
                    SoundEffectC.Play(soundEffectType);
                    break;

                case RpcGeneralTypes.RotateAll:
                   
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        private void OtherRPC(RpcOtherTypes rpcOtherType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _curNumber = 0;
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

            objs.Add(WhoseMoveC.WhoseMove);

            objs.Add(EndGameDataUIC.PlayerWinner);

            objs.Add(ReadyDataUIC.IsStartedGame);
            objs.Add(ReadyDataUIC.IsReady(PlayerTypes.Second));

            foreach (var item_0 in MotionsDataUIC.IsActivatedUI) objs.Add(item_0.Value);
            objs.Add(MotionsDataUIC.AmountMotions);

            objs.Add(PickUpgZoneDataUIC.HaveUpgrade(PlayerTypes.Second));
            foreach (var item_0 in PickUpgZoneDataUIC.Activated_Buts)
            {
                foreach (var item_1 in item_0.Value)
                {
                    objs.Add(PickUpgZoneDataUIC.Activated_Buts[item_0.Key][item_1.Key]);
                }
            }

            objs.Add(WindC.DirectWind);

            foreach (var item_0 in UnitPercUpgC.PercUpgs)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        objs.Add(UnitPercUpgC.UpgPercent(item_0.Key, item_1.Key, item_2.Key));
                    }
                }
            }
            foreach (var item_0 in UnitStepUpgC.StepUpgs)
            {
                foreach (var item_1 in item_0.Value)
                {
                    objs.Add(UnitStepUpgC.UpgSteps(item_0.Key, item_1.Key));
                }
            }


            foreach (var idx_0 in _cellUnitOthFilt)
            {
                ref var unitC_0 = ref _cellUnitMainFilt.Get1(idx_0);
                objs.Add(unitC_0.Unit);
                objs.Add(_cellUnitMainFilt.Get3(idx_0).Owner);
                objs.Add(_cellUnitMainFilt.Get2(idx_0).Level);
                ref var hpUnit_0 = ref _cellUnitStatFilt.Get2(idx_0);
                objs.Add(hpUnit_0.Hp);
                objs.Add(_cellUnitStatFilt.Get3(idx_0).Steps);

                objs.Add(_cellUnitOthFilt.Get2(idx_0).Condition);
                foreach (var item in _cellUnitOthFilt.Get3(idx_0).Effects) objs.Add(item.Value);
                objs.Add(_cellUnitOthFilt.Get4(idx_0).Water);

                objs.Add(_cellUnitTWFilt.Get2(idx_0).ToolWeapType);
                objs.Add(_cellUnitTWFilt.Get2(idx_0).LevelTWType);
                objs.Add(_cellUnitTWFilt.Get2(idx_0).ShieldProt);


                objs.Add(_cellBuildFilter.Get1(idx_0).Build);
                objs.Add(_cellBuildFilter.Get2(idx_0).Owner);



                ref var env_0 = ref _cellEnvrFilter.Get1(idx_0);
                ref var envRes_0 = ref _cellEnvrFilter.Get2(idx_0);
                foreach (var item in env_0.Envronments) objs.Add(item.Value);
                foreach (var item in envRes_0.Resources) objs.Add(item.Value);



                objs.Add(_cellRiverFilt.Get1(idx_0).RiverType);
                foreach (var item_0 in _cellRiverFilt.Get1(idx_0).Directs)
                    objs.Add(item_0.Value);


                foreach (var item_0 in _cellTrailFilt.Get1(idx_0).Health)
                    objs.Add(item_0.Value);


                ref var cloud_0 = ref _cellCloudFilt.Get1(idx_0);
                objs.Add(cloud_0.HaveCloud);
                objs.Add(cloud_0.CloudWidthType);


                objs.Add(_cellFireFilter.Get1(idx_0).HaveFire);
            }

            #region Inventor

            foreach (var item_0 in InventResC.AmountResour)
            {
                foreach (var item_1 in item_0.Value)
                {
                    objs.Add(InventResC.AmountResour[item_0.Key][item_1.Key]);
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

            foreach (var item_0 in InvToolWeapC.ToolWeapons)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        objs.Add(InvToolWeapC.AmountToolWeap(item_0.Key, item_1.Key, item_2.Key));
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


            #region



            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            PhotonView.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);
            PhotonView.RPC(nameof(UpdateVision), RpcTarget.Others, new object { });
        }

        [PunRPC]
        private void SyncAllOther(object[] objects)
        {
            _curNumber = 0;

            WhoseMoveC.SetWhoseMove((PlayerTypes)objects[_curNumber++]);

            EndGameDataUIC.PlayerWinner = (PlayerTypes)objects[_curNumber++];

            ReadyDataUIC.IsStartedGame = (bool)objects[_curNumber++];
            ReadyDataUIC.SetIsReady(WhoseMoveC.CurPlayerI, (bool)objects[_curNumber++]);

            foreach (var item_0 in MotionsDataUIC.IsActivatedUI) MotionsDataUIC.Sync(item_0.Key, item_0.Value);
            MotionsDataUIC.AmountMotions = (int)objects[_curNumber++];

            PickUpgZoneDataUIC.SetHaveUpgrade(PlayerTypes.Second, (bool)objects[_curNumber++]);
            foreach (var item_0 in PickUpgZoneDataUIC.Activated_Buts)
            {
                foreach (var item_1 in item_0.Value)
                {
                    PickUpgZoneDataUIC.SetHave_But(item_0.Key, item_1.Key, (bool)objects[_curNumber++]);
                }
            }

            WindC.DirectWind = (DirectTypes)objects[_curNumber++];


            #region Upgrades

            foreach (var item_0 in UnitPercUpgC.PercUpgs)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        UnitPercUpgC.SetUpg(item_0.Key, item_1.Key, item_2.Key, (float)objects[_curNumber++]);
                    }
                }
            }
            foreach (var item_0 in UnitStepUpgC.StepUpgs)
            {
                foreach (var item_1 in item_0.Value)
                {
                    UnitStepUpgC.SetStepUpg(item_0.Key, item_1.Key, (int)objects[_curNumber++]);
                }
            }

            #endregion


            foreach (var idx_0 in _cellUnitOthFilt)
            {
                ref var unit_0 = ref _cellUnitOthFilt.Get1(idx_0);
                unit_0.Sync((UnitTypes)objects[_curNumber++]);
                _cellUnitMainFilt.Get3(idx_0).Sync((PlayerTypes)objects[_curNumber++]);
                _cellUnitMainFilt.Get2(idx_0).Sync((LevelUnitTypes)objects[_curNumber++]);
                _cellUnitStatFilt.Get2(idx_0).Sync((int)objects[_curNumber++]);
                _cellUnitStatFilt.Get3(idx_0).Sync((int)objects[_curNumber++]);

                _cellUnitOthFilt.Get2(idx_0).Sync((CondUnitTypes)objects[_curNumber++]);
                foreach (var item in _cellUnitOthFilt.Get3(idx_0).Effects) _cellUnitOthFilt.Get3(idx_0).Sync(item.Key, (bool)objects[_curNumber++]);
                _cellUnitOthFilt.Get4(idx_0).Sync((int)objects[_curNumber++]);

                _cellUnitTWFilt.Get2(idx_0).ToolWeapType = (ToolWeaponTypes)objects[_curNumber++];
                _cellUnitTWFilt.Get2(idx_0).LevelTWType = (LevelTWTypes)objects[_curNumber++];
                _cellUnitTWFilt.Get2(idx_0).SyncShield((int)objects[_curNumber++]);



                _cellBuildFilter.Get1(idx_0).Sync((BuildTypes)objects[_curNumber++]);
                _cellBuildFilter.Get2(idx_0).Sync((PlayerTypes)objects[_curNumber++]);



                ref var env_0 = ref _cellEnvrFilter.Get1(idx_0);
                ref var envRes_0 = ref _cellEnvrFilter.Get2(idx_0);
                foreach (var item in env_0.Envronments) env_0.Sync(item.Key, item.Value);
                foreach (var item in envRes_0.Resources) envRes_0.Sync(item.Key, item.Value);


                ref var river_0 = ref _cellRiverFilt.Get1(idx_0);
                river_0.RiverType = (RiverTypes)objects[_curNumber++];
                foreach (var item_0 in river_0.Directs)
                    river_0.Sync(item_0.Key, (bool)objects[_curNumber++]);



                ref var trail_0 = ref _cellTrailFilt.Get1(idx_0);
                foreach (var item_0 in trail_0.Health)
                    trail_0.SyncTrail(item_0.Key, (int)objects[_curNumber++]);



                ref var cloud_0 = ref _cellCloudFilt.Get1(idx_0);
                cloud_0.HaveCloud = (bool)objects[_curNumber++];
                cloud_0.CloudWidthType = (CloudWidthTypes)objects[_curNumber++];



                ref var fire_0 = ref _cellFireFilter.Get1(idx_0);
                fire_0.HaveFire = (bool)objects[_curNumber++];
            }


            #region Inventor

            foreach (var item_0 in InventResC.AmountResour)
            {
                foreach (var item_1 in item_0.Value)
                {
                    InventResC.Set(item_0.Key, item_1.Key, (int)objects[_curNumber++]);
                }
            }
            foreach (var item_0 in InvUnitsC.Units)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        InvUnitsC.Set(item_0.Key, item_1.Key, item_2.Key, (int)objects[_curNumber++]);
                    }
                }
            }
            foreach (var item_0 in InvToolWeapC.ToolWeapons)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        InvToolWeapC.Set(item_0.Key, item_1.Key, item_2.Key, (int)objects[_curNumber++]);
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
                            var obj = objects[_curNumber++];
                            needContinue = (bool)obj;
                            if (needContinue == true) break;

                            WhereUnitsC.Sync(item_0.Key, item_1.Key, item_2.Key, (byte)objects[_curNumber++]);
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
                        var obj = objects[_curNumber++];
                        needContinue = (bool)obj;
                        if (needContinue == true) break;

                        WhereBuildsC.Sync(item_0.Key, item_1.Key, (byte)objects[_curNumber++]);
                    }

                    if (needContinue) continue;
                }

            foreach (var item_0 in WhereEnvC.EnvInGame)
            {
                var needContinue = false;

                WhereEnvC.Clear(item_0.Key);
                for (int i = 0; i < Byte.MaxValue; i++)
                {
                    var obj = objects[_curNumber++];
                    needContinue = (bool)obj;
                    if (needContinue == true) break;

                    WhereEnvC.SyncAdd(item_0.Key, (byte)objects[_curNumber++]);
                }
                if (needContinue) continue;
            }


            #endregion
        }

        [PunRPC]
        private void UpdateVision()
        {
            GameGenSysDataViewC.RotateAll.Invoke();
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