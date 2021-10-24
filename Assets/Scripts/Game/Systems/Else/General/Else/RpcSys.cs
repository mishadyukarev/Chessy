using Assets.Scripts.ECS.Components.Data.Else.Game.Master;
using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public sealed class RpcSys : MonoBehaviour, IEcsInitSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvrFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

        private EcsFilter<InventResourCom> _inventorResFilter = default;
        private EcsFilter<InventorUnitsCom> _invUnitsFilter = default;
        private EcsFilter<InventorTWCom> _invToolsFilter = default;

        private EcsFilter<SelectorCom> _selectorFilter = default;
        private EcsFilter<UpgradesBuildsCom> _upgradesBuildFilter = default;
        private EcsFilter<EndGameDataUIComponent> _endGameFilter = default;
        private EcsFilter<ReadyDataUICom> _readyUIFilter = default;
        private EcsFilter<MotionsDataUIComponent> _motionsFilter = default;
        private EcsFilter<MistakeDataUICom> _mistakeUIFilter = default;
        private EcsFilter<SoundEffectsComp> _soundFilter = default;



        private EcsFilter<InfoCom> _fromInfoFilt = default;
        private EcsFilter<ForBuildingMasCom, XyCellForDoingMasCom> _buildFilter = default;
        private EcsFilter<ForDestroyMasCom> _destroyFilter = default;
        private EcsFilter<ForShiftMasCom> _shiftFilter = default;
        private EcsFilter<ForAttackMasCom> _attackFilter = default;
        private EcsFilter<ConditionMasCom, XyCellForDoingMasCom> _conditionFilter = default;
        private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;
        private EcsFilter<ForSettingUnitMasCom, XyCellForDoingMasCom> _settingUnitFilter = default;
        private EcsFilter<ForSeedingMasCom, XyCellForDoingMasCom> _seedingFilter = default;
        private EcsFilter<ForFireMasCom> _fireFilter = default;
        private EcsFilter<ForUpgradeMasCom> _upgradorFilter = default;
        private EcsFilter<ForCircularAttackMasCom, XyCellForDoingMasCom> _circularAttackFilter = default;
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGivePawnToolFilter = default;
        private EcsFilter<ForUpgradeUnitCom> _forUpgradeUnitFilt = default;
        private EcsFilter<UpdatedMasCom> _updatedMotMasFilt = default;
        private EcsFilter<ForOldNewUnitCom> _forOldToNewUnitFilt = default;

        private static PhotonView PhotonView => PhotonRpcViewGameCom.PhotonView;

        private static string MasterRPCName => nameof(MasterRPC);
        private static string GeneralRPCName => nameof(GeneralRPC);
        private static string OtherRPCName => nameof(OtherRPC);
        private static string SyncMasterRPCName => nameof(SyncAllMaster);

        private int _curNumber;

        public void Init()
        {
            PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);

            if (!PhotonNetwork.IsMasterClient) SyncAllToMaster();
        }


        #region StandartPunRPC

        #region Methods

        public static void ReadyToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Ready, new object[default]);

        public static void DoneToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Done, new object[default]);
        public static void ActiveAmountMotionUIToGeneral(RpcTarget rpcTarget) => PhotonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.ActiveAmountMotionUI, new object[default]);

        public static void UpgradeBuildingToMaster(BuildingTypes buildingType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UpgradeBuild, new object[] { buildingType });

        public static void ShiftUnitToMaster(byte idxPreviousCell, byte idxSelectedCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Shift, new object[] { idxPreviousCell, idxSelectedCell });
        public static void AttackUnitToMaster(byte idxPreviousCell, byte idxSelectedCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Attack, new object[] { idxPreviousCell, idxSelectedCell });

        public static void BuildToMaster(byte idxCellForBuild, BuildingTypes buildingType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Build, new object[] { idxCellForBuild, buildingType });
        public static void DestroyBuildingToMaster(byte xyCellForDestroy) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.DestroyBuild, new object[] { xyCellForDestroy });

        public static void ConditionUnitToMaster(CondUnitTypes neededCondtionType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.ConditionUnit, new object[] { neededCondtionType, idxCell });

        public static void MistakeEconomyToGeneral(Player playerTo, Dictionary<ResourceTypes, int> needRes)
        {
            int[] needRes2 = new int[(int)Support.MaxResType];
            needRes2[0] = needRes[ResourceTypes.Food];
            needRes2[1] = needRes[ResourceTypes.Wood];
            needRes2[2] = needRes[ResourceTypes.Ore];
            needRes2[3] = needRes[ResourceTypes.Iron];
            needRes2[4] = needRes[ResourceTypes.Gold];

            PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.Economy, needRes2 });
        }
        public static void SimpleMistakeToGeneral(MistakeTypes mistakeType, Player playerTo) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { mistakeType });

        public static void FireToMaster(byte fromIdx, byte toIdx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Fire, new object[] { fromIdx, toIdx });
        public static void SeedEnvironmentToMaster(byte idxCell, EnvirTypes environmentType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SeedEnvironment, new object[] { idxCell, environmentType });

        internal static void OldToNewToMaster(UnitTypes unitType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.OldToNewUnit, new object[] { unitType, idxCell });
        internal static void UpgradeUnitToMaster(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UpgradeUnit, new object[] { idxCell });
        internal static void GiveTakeToolWeapon(ToolWeaponTypes toolAndWeaponType, LevelTWTypes levelTWType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.GiveTakeToolWeapon, new object[] { toolAndWeaponType, levelTWType, idxCell });

        public static void CircularAttackKingToMaster(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.CircularAttackKing, new object[] { idxCell });

        public static void CreateUnitToMaster(UnitTypes unitType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.CreateUnit, new object[] { unitType });

        public static void MeltOreToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.MeltOre, new object[] { });


        public static void SetUniToMaster(byte idxCell, UnitTypes unitType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SetUnit, new object[] { idxCell, unitType });

        public static void SoundToGeneral(RpcTarget rpcTarget, SoundEffectTypes soundEffectType) => PhotonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.Sound, new object[] { soundEffectType });
        public static void SoundToGeneral(Player playerTo, SoundEffectTypes soundEffectType) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Sound, new object[] { soundEffectType });

        #endregion


        [PunRPC]
        private void MasterRPC(RpcMasterTypes rpcType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _curNumber = default;

            _fromInfoFilt.Get1(0).FromInfo = infoFrom;

            switch (rpcType)
            {
                case RpcMasterTypes.None:
                    throw new Exception();

                case RpcMasterTypes.Ready:
                    break;

                case RpcMasterTypes.Done:
                    break;

                case RpcMasterTypes.Build:
                    _buildFilter.Get1(0).BuildingTypeForBuidling = (BuildingTypes)objects[1];
                    _buildFilter.Get1(0).IdxForBuild = (byte)objects[0];
                    break;

                case RpcMasterTypes.DestroyBuild:
                    _destroyFilter.Get1(0).IdxForDestroy = (byte)objects[0];
                    break;

                case RpcMasterTypes.Shift:
                    _shiftFilter.Get1(0).IdxFrom = (byte)objects[0];
                    _shiftFilter.Get1(0).IdxTo = (byte)objects[1];
                    break;

                case RpcMasterTypes.Attack:
                    _attackFilter.Get1(0).IdxFromCell = (byte)objects[0];
                    _attackFilter.Get1(0).IdxToCell = (byte)objects[1];
                    break;

                case RpcMasterTypes.ConditionUnit:
                    _conditionFilter.Get1(0).NeededCondUnitType = (CondUnitTypes)objects[0];
                    _conditionFilter.Get1(0).IdxForCondition = (byte)objects[1];
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
                    _seedingFilter.Get1(0).EnvTypeForSeeding = (EnvirTypes)objects[1];
                    break;

                case RpcMasterTypes.Fire:
                    _fireFilter.Get1(0).FromIdx = (byte)objects[0];
                    _fireFilter.Get1(0).ToIdx = (byte)objects[1];
                    break;

                case RpcMasterTypes.UpgradeBuild:
                    _upgradorFilter.Get1(0).BuildingType = (BuildingTypes)objects[_curNumber++];
                    break;

                case RpcMasterTypes.CircularAttackKing:
                    _circularAttackFilter.Get1(0).IdxUnitForCirculAttack = (byte)objects[0];
                    break;

                case RpcMasterTypes.OldToNewUnit:
                    _forOldToNewUnitFilt.Get1(0).UnitType = (UnitTypes)objects[0];
                    _forOldToNewUnitFilt.Get1(0).IdxCell = (byte)objects[1];
                    break;

                case RpcMasterTypes.UpgradeUnit:
                    _forUpgradeUnitFilt.Get1(0).idxCellForUpgrade = (byte)objects[0];
                    break;

                case RpcMasterTypes.GiveTakeToolWeapon:
                    _forGivePawnToolFilter.Get1(0).ToolWeapType = (ToolWeaponTypes)objects[_curNumber++];
                    _forGivePawnToolFilter.Get1(0).LevelTWType = (LevelTWTypes)objects[_curNumber++];
                    _forGivePawnToolFilter.Get1(0).IdxCell = (byte)objects[_curNumber++];
                    break;

                default:
                    throw new Exception();
            }

            GameMasterSystemManager.RunRpcSystem(rpcType);

            SyncAllToMaster();
        }

        [PunRPC]
        private void GeneralRPC(RpcGeneralTypes rpcGeneralType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _curNumber = 0;
            _fromInfoFilt.Get1(0).FromInfo = infoFrom;

            ref var selectorCom = ref _selectorFilter.Get1(0);

            switch (rpcGeneralType)
            {
                case RpcGeneralTypes.None:
                    throw new Exception();

                case RpcGeneralTypes.ActiveAmountMotionUI:
                    _motionsFilter.Get1(0).IsActivatedUI = true;
                    break;

                case RpcGeneralTypes.Mistake:
                    var mistakeType = (MistakeTypes)objects[_curNumber++];
                    _mistakeUIFilter.Get1(0).MistakeType = mistakeType;
                    _mistakeUIFilter.Get1(0).CurTime = default;

                    if(mistakeType == MistakeTypes.Economy)
                    {
                        _mistakeUIFilter.Get1(0).ClearAllNeeds();

                        var needRes = (int[])objects[_curNumber++];

                        _mistakeUIFilter.Get1(0).AddNeedRes(ResourceTypes.Food, needRes[0]);
                        _mistakeUIFilter.Get1(0).AddNeedRes(ResourceTypes.Wood, needRes[1]);
                        _mistakeUIFilter.Get1(0).AddNeedRes(ResourceTypes.Ore, needRes[2]);
                        _mistakeUIFilter.Get1(0).AddNeedRes(ResourceTypes.Iron, needRes[3]);
                        _mistakeUIFilter.Get1(0).AddNeedRes(ResourceTypes.Gold, needRes[4]);
                    }

                    _soundFilter.Get1(0).Play(SoundEffectTypes.Mistake);
                    break;

                case RpcGeneralTypes.Sound:
                    var soundEffectType = (SoundEffectTypes)objects[_curNumber++];
                    _soundFilter.Get1(0).Play(soundEffectType);
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        private void OtherRPC(RpcOtherTypes rpcOtherType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _curNumber = 0;
            _fromInfoFilt.Get1(0).FromInfo = infoFrom;

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

        internal void SyncAllToMaster() => PhotonView.RPC(SyncMasterRPCName, RpcTarget.MasterClient);

        [PunRPC]
        private void SyncAllMaster()
        {
            var listObjects = new List<object>();

            listObjects.Add(WhoseMoveCom.WhoseMoveOnline);

            listObjects.Add(_endGameFilter.Get1(0).PlayerWinner);

            listObjects.Add(_readyUIFilter.Get1(0).IsStartedGame);
            listObjects.Add(_readyUIFilter.Get1(0).IsReady(false));

            listObjects.Add(_motionsFilter.Get1(0).AmountMotions);

            foreach (var curIdxCell in _cellUnitFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                listObjects.Add(curUnitDatCom.UnitType);
                listObjects.Add(curUnitDatCom.AmountHealth);
                listObjects.Add(curUnitDatCom.AmountSteps);
                listObjects.Add(curUnitDatCom.CondUnitType);
                listObjects.Add(curUnitDatCom.LevelUnitType);
                listObjects.Add(curUnitDatCom.TWExtraType);

                listObjects.Add(_cellUnitFilter.Get2(curIdxCell).PlayerType);


                listObjects.Add(_cellBuildFilter.Get1(curIdxCell).BuildType);
                listObjects.Add(_cellBuildFilter.Get2(curIdxCell).PlayerType);


                ref var curEnvDatCom = ref _cellEnvrFilter.Get1(curIdxCell);
                listObjects.Add(curEnvDatCom.Have(EnvirTypes.Fertilizer));
                listObjects.Add(curEnvDatCom.Have(EnvirTypes.YoungForest));
                listObjects.Add(curEnvDatCom.Have(EnvirTypes.AdultForest));
                listObjects.Add(curEnvDatCom.Have(EnvirTypes.Hill));
                listObjects.Add(curEnvDatCom.Have(EnvirTypes.Mountain));

                listObjects.Add(curEnvDatCom.AmountResources(EnvirTypes.Fertilizer));
                listObjects.Add(curEnvDatCom.AmountResources(EnvirTypes.YoungForest));
                listObjects.Add(curEnvDatCom.AmountResources(EnvirTypes.AdultForest));
                listObjects.Add(curEnvDatCom.AmountResources(EnvirTypes.Hill));
                listObjects.Add(curEnvDatCom.AmountResources(EnvirTypes.Mountain));


                listObjects.Add(_cellFireFilter.Get1(curIdxCell).HaveFire);
            }



            ref var inventResComp = ref _inventorResFilter.Get1(0);
            listObjects.Add(inventResComp.AmountRes(PlayerTypes.Second, ResourceTypes.Food));
            listObjects.Add(inventResComp.AmountRes(PlayerTypes.Second, ResourceTypes.Wood));
            listObjects.Add(inventResComp.AmountRes(PlayerTypes.Second, ResourceTypes.Ore));
            listObjects.Add(inventResComp.AmountRes(PlayerTypes.Second, ResourceTypes.Iron));
            listObjects.Add(inventResComp.AmountRes(PlayerTypes.Second, ResourceTypes.Gold));



            ref var invUnitsCom = ref _invUnitsFilter.Get1(0);
            listObjects.Add(invUnitsCom.AmountUnitsInInv(PlayerTypes.Second, UnitTypes.King, LevelUnitTypes.Wood));
            listObjects.Add(invUnitsCom.AmountUnitsInInv(PlayerTypes.Second, UnitTypes.Pawn, LevelUnitTypes.Wood));
            listObjects.Add(invUnitsCom.AmountUnitsInInv(PlayerTypes.Second, UnitTypes.Rook, LevelUnitTypes.Wood));
            listObjects.Add(invUnitsCom.AmountUnitsInInv(PlayerTypes.Second, UnitTypes.Bishop, LevelUnitTypes.Wood));

            listObjects.Add(invUnitsCom.AmountUnitsInInv(PlayerTypes.Second, UnitTypes.King, LevelUnitTypes.Iron));
            listObjects.Add(invUnitsCom.AmountUnitsInInv(PlayerTypes.Second, UnitTypes.Pawn, LevelUnitTypes.Iron));
            listObjects.Add(invUnitsCom.AmountUnitsInInv(PlayerTypes.Second, UnitTypes.Rook, LevelUnitTypes.Iron));
            listObjects.Add(invUnitsCom.AmountUnitsInInv(PlayerTypes.Second, UnitTypes.Bishop, LevelUnitTypes.Iron));



            ref var invToolsComp = ref _invToolsFilter.Get1(0);
            listObjects.Add(invToolsComp.GetAmountTools(PlayerTypes.Second, ToolWeaponTypes.Pick, LevelTWTypes.Wood));
            listObjects.Add(invToolsComp.GetAmountTools(PlayerTypes.Second, ToolWeaponTypes.Sword, LevelTWTypes.Wood));
            listObjects.Add(invToolsComp.GetAmountTools(PlayerTypes.Second, ToolWeaponTypes.Shield, LevelTWTypes.Wood));

            listObjects.Add(invToolsComp.GetAmountTools(PlayerTypes.Second, ToolWeaponTypes.Pick, LevelTWTypes.Iron));
            listObjects.Add(invToolsComp.GetAmountTools(PlayerTypes.Second, ToolWeaponTypes.Sword, LevelTWTypes.Iron));
            listObjects.Add(invToolsComp.GetAmountTools(PlayerTypes.Second, ToolWeaponTypes.Shield, LevelTWTypes.Iron));


            var objects = new object[listObjects.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];


            PhotonView.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);
        }

        [PunRPC]
        private void SyncAllOther(object[] objects)
        {
            _curNumber = 0;

            WhoseMoveCom.WhoseMoveOnline = (PlayerTypes)objects[_curNumber++];

            _endGameFilter.Get1(0).PlayerWinner = (PlayerTypes)objects[_curNumber++];

            _readyUIFilter.Get1(0).IsStartedGame = (bool)objects[_curNumber++];
            _readyUIFilter.Get1(0).SetIsReady(PhotonNetwork.IsMasterClient, (bool)objects[_curNumber++]);

            _motionsFilter.Get1(0).AmountMotions = (int)objects[_curNumber++];

            foreach (var curIdxCell in _cellUnitFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                curUnitDatCom.UnitType = (UnitTypes)objects[_curNumber++];
                curUnitDatCom.AmountHealth = (int)objects[_curNumber++];
                curUnitDatCom.AmountSteps = (int)objects[_curNumber++];
                curUnitDatCom.CondUnitType = (CondUnitTypes)objects[_curNumber++];
                curUnitDatCom.LevelUnitType = (LevelUnitTypes)objects[_curNumber++];
                curUnitDatCom.TWExtraType = (ToolWeaponTypes)objects[_curNumber++];
                _cellUnitFilter.Get2(curIdxCell).PlayerType = (PlayerTypes)objects[_curNumber++];


                _cellBuildFilter.Get1(curIdxCell).BuildType = (BuildingTypes)objects[_curNumber++];
                _cellBuildFilter.Get2(curIdxCell).PlayerType = (PlayerTypes)objects[_curNumber++];



                ref var curEnvrDatCom = ref _cellEnvrFilter.Get1(curIdxCell);
                curEnvrDatCom.SetHaveEnvironment(EnvirTypes.Fertilizer, (bool)objects[_curNumber++]);
                curEnvrDatCom.SetHaveEnvironment(EnvirTypes.YoungForest, (bool)objects[_curNumber++]);
                curEnvrDatCom.SetHaveEnvironment(EnvirTypes.AdultForest, (bool)objects[_curNumber++]);
                curEnvrDatCom.SetHaveEnvironment(EnvirTypes.Hill, (bool)objects[_curNumber++]);
                curEnvrDatCom.SetHaveEnvironment(EnvirTypes.Mountain, (bool)objects[_curNumber++]);

                curEnvrDatCom.SetAmountResources(EnvirTypes.Fertilizer, (int)objects[_curNumber++]);
                curEnvrDatCom.SetAmountResources(EnvirTypes.YoungForest, (int)objects[_curNumber++]);
                curEnvrDatCom.SetAmountResources(EnvirTypes.AdultForest, (int)objects[_curNumber++]);
                curEnvrDatCom.SetAmountResources(EnvirTypes.Hill, (int)objects[_curNumber++]);
                curEnvrDatCom.SetAmountResources(EnvirTypes.Mountain, (int)objects[_curNumber++]);



                ref var curFireDatCom = ref _cellFireFilter.Get1(curIdxCell);
                curFireDatCom.HaveFire = (bool)objects[_curNumber++];
            }



            ref var inventResComp = ref _inventorResFilter.Get1(0);
            inventResComp.SetAmountRes(WhoseMoveCom.CurOnlinePlayer, ResourceTypes.Food, (int)objects[_curNumber++]);
            inventResComp.SetAmountRes(WhoseMoveCom.CurOnlinePlayer, ResourceTypes.Wood, (int)objects[_curNumber++]);
            inventResComp.SetAmountRes(WhoseMoveCom.CurOnlinePlayer, ResourceTypes.Ore, (int)objects[_curNumber++]);
            inventResComp.SetAmountRes(WhoseMoveCom.CurOnlinePlayer, ResourceTypes.Iron, (int)objects[_curNumber++]);
            inventResComp.SetAmountRes(WhoseMoveCom.CurOnlinePlayer, ResourceTypes.Gold, (int)objects[_curNumber++]);



            ref var invUnitsCom = ref _invUnitsFilter.Get1(0);
            invUnitsCom.SetAmountUnitsInInvent(WhoseMoveCom.CurOnlinePlayer, UnitTypes.King, LevelUnitTypes.Wood, (int)objects[_curNumber++]);
            invUnitsCom.SetAmountUnitsInInvent(WhoseMoveCom.CurOnlinePlayer, UnitTypes.Pawn, LevelUnitTypes.Wood, (int)objects[_curNumber++]);
            invUnitsCom.SetAmountUnitsInInvent(WhoseMoveCom.CurOnlinePlayer, UnitTypes.Rook, LevelUnitTypes.Wood, (int)objects[_curNumber++]);
            invUnitsCom.SetAmountUnitsInInvent(WhoseMoveCom.CurOnlinePlayer, UnitTypes.Bishop, LevelUnitTypes.Wood, (int)objects[_curNumber++]);

            invUnitsCom.SetAmountUnitsInInvent(WhoseMoveCom.CurOnlinePlayer, UnitTypes.King, LevelUnitTypes.Iron, (int)objects[_curNumber++]);
            invUnitsCom.SetAmountUnitsInInvent(WhoseMoveCom.CurOnlinePlayer, UnitTypes.Pawn, LevelUnitTypes.Iron, (int)objects[_curNumber++]);
            invUnitsCom.SetAmountUnitsInInvent(WhoseMoveCom.CurOnlinePlayer, UnitTypes.Rook, LevelUnitTypes.Iron, (int)objects[_curNumber++]);
            invUnitsCom.SetAmountUnitsInInvent(WhoseMoveCom.CurOnlinePlayer, UnitTypes.Bishop, LevelUnitTypes.Iron, (int)objects[_curNumber++]);


            ref var invToolsComp = ref _invToolsFilter.Get1(0);
            invToolsComp.SetAmountTW(WhoseMoveCom.CurOnlinePlayer, ToolWeaponTypes.Pick, LevelTWTypes.Wood, (byte)objects[_curNumber++]);
            invToolsComp.SetAmountTW(WhoseMoveCom.CurOnlinePlayer, ToolWeaponTypes.Sword, LevelTWTypes.Wood, (byte)objects[_curNumber++]);
            invToolsComp.SetAmountTW(WhoseMoveCom.CurOnlinePlayer, ToolWeaponTypes.Shield, LevelTWTypes.Wood, (byte)objects[_curNumber++]);

            invToolsComp.SetAmountTW(WhoseMoveCom.CurOnlinePlayer, ToolWeaponTypes.Pick, LevelTWTypes.Iron, (byte)objects[_curNumber++]);
            invToolsComp.SetAmountTW(WhoseMoveCom.CurOnlinePlayer, ToolWeaponTypes.Sword, LevelTWTypes.Iron, (byte)objects[_curNumber++]);
            invToolsComp.SetAmountTW(WhoseMoveCom.CurOnlinePlayer, ToolWeaponTypes.Shield, LevelTWTypes.Iron, (byte)objects[_curNumber++]);
        }

        #endregion


        #region Serialize

        internal static object DeserializeVector2Int(byte[] data)
        {
            Vector2Int result = new Vector2Int();

            result.x = BitConverter.ToInt32(data, 0);
            result.y = BitConverter.ToInt32(data, 4);

            return result;

        }
        internal static byte[] SerializeVector2Int(object obj)
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