using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.Game.Other;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Assets.Scripts.ECS.Components.View.Else.Game.General;
using Assets.Scripts.ECS.Game.Components;
using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class RpcSys : MonoBehaviour, IEcsInitSystem
    {
        private EcsFilter<CellUnitDataComponent, OwnerOnlineComp> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerOnlineComp> _cellBuildFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvrFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

        private EcsFilter<InventorResourcesComponent> _inventorResFilter = default;
        private EcsFilter<InventorUnitsComponent> _invUnitsFilter = default;
        private EcsFilter<InventorToolsComp> _invToolsFilter = default;
        private EcsFilter<InventorWeaponsComp> _invWeaponsFilter = default;

        private EcsFilter<FromInfoComponent> _fromInfoFilter = default;
        private EcsFilter<SelectorComponent> _selectorFilter = default;
        private EcsFilter<UpgradesBuildingsComponent> _upgradesBuildFilter = default;
        private EcsFilter<EndGameDataUIComponent> _endGameFilter = default;
        private EcsFilter<ReadyDataUICom> _readyUIFilter = default;
        private EcsFilter<MotionsDataUIComponent> _motionsFilter = default;
        private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
        private EcsFilter<MistakeDataUICom> _mistakeUIFilter = default;
        private EcsFilter<SoundEffectsComp> _soundFilter = default;



        private EcsFilter<InfoMasCom> _infoMasterComFilter = default;
        private EcsFilter<ForReadyMasCom, NeedActiveSomethingMasCom> _readyFilter = default;
        private EcsFilter<ForDonerMasCom> _donerFilter = default;
        private EcsFilter<ForBuildingMasCom, XyCellForDoingMasCom> _buildFilter = default;
        private EcsFilter<ForDestroyMasCom> _destroyFilter = default;
        private EcsFilter<ForShiftMasCom> _shiftFilter = default;
        private EcsFilter<ForAttackMasCom> _attackFilter = default;
        private EcsFilter<ConditionMasCom, XyCellForDoingMasCom> _conditionFilter = default;
        private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;
        private EcsFilter<ForGettingUnitMasCom> _gettingUnitFilter = default;
        private EcsFilter<ForSettingUnitMasCom, XyCellForDoingMasCom> _settingUnitFilter = default;
        private EcsFilter<ForSeedingMasCom, XyCellForDoingMasCom> _seedingFilter = default;
        private EcsFilter<ForFireMasCom> _fireFilter = default;
        private EcsFilter<ForUpgradeMasCom> _upgradorFilter = default;
        private EcsFilter<ForCircularAttackMasCom, XyCellForDoingMasCom> _circularAttackFilter = default;
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGivePawnToolFilter = default;
        private EcsFilter<ForTakePawnExtraToolMastCom> _forTakePawnExtraToolFilter = default;
        private EcsFilter<ForSwapToolWeaponComp> _forSwapToolWeapFilter = default;

        private EcsFilter<InfoOtherCom> _infoOtherFilter = default;

        private static PhotonView PhotonView => PhotonViewComp.PhotonView;

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

        public static void ReadyToMaster(in bool isReady) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Ready, new object[] { isReady });

        public static void DoneToMaster(bool isDone) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Done, new object[] { isDone });
        public static void ActiveAmountMotionUIToGeneral(Player playerTo) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.ActiveAmountMotionUI, new object[default]);
        public static void ActiveAmountMotionUIToGeneral(RpcTarget rpcTarget) => PhotonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.ActiveAmountMotionUI, new object[default]);

        public static void UpgradeBuildingToMaster(BuildingTypes buildingType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.UpgradeBuild, new object[] { buildingType });

        public static void ShiftUnitToMaster(byte idxPreviousCell, byte idxSelectedCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Shift, new object[] { idxPreviousCell, idxSelectedCell });
        public static void AttackUnitToMaster(byte idxPreviousCell, byte idxSelectedCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Attack, new object[] { idxPreviousCell, idxSelectedCell });

        public static void BuildToMaster(byte idxCellForBuild, BuildingTypes buildingType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Build, new object[] { idxCellForBuild, buildingType });
        public static void DestroyBuildingToMaster(byte xyCellForDestroy) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.DestroyBuild, new object[] { xyCellForDestroy });

        public static void ConditionUnitToMaster(ConditionUnitTypes neededCondtionType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.ConditionUnit, new object[] { neededCondtionType, idxCell });

        public static void MistakeEconomyToGeneral(Player playerTo, params bool[] haves) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.Economy, haves });
        public static void SimpleMistakeToGeneral(MistakeTypes mistakeType, Player playerTo) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { mistakeType });

        public static void FireToMaster(byte fromIdx, byte toIdx) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Fire, new object[] { fromIdx, toIdx });
        public static void SeedEnvironmentToMaster(byte idxCell, EnvironmentTypes environmentType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SeedEnvironment, new object[] { idxCell, environmentType });

        public static void GiveTakeToolWeapon(ToolWeaponTypes toolAndWeaponType, byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.GiveTakeToolWeapon, new object[] { toolAndWeaponType, idxCell });

        public static void CircularAttackKingToMaster(byte idxCell) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.CircularAttackKing, new object[] { idxCell });

        public static void CreateUnitToMaster(UnitTypes unitType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.CreateUnit, new object[] { unitType });

        public static void MeltOreToMaster() => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.MeltOre, new object[] { });


        public static void SetUniToMaster(byte idxCell, UnitTypes unitType) => PhotonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SetUnit, new object[] { idxCell, unitType });
        public static void SetUnitToGeneral(Player playerTo, bool isSetted) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.SetUnit, new object[] { isSetted });

        public static void SoundToGeneral(RpcTarget rpcTarget, SoundEffectTypes soundEffectType) => PhotonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.Sound, new object[] { soundEffectType });
        public static void SoundToGeneral(Player playerTo, SoundEffectTypes soundEffectType) => PhotonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Sound, new object[] { soundEffectType });

        #endregion


        [PunRPC]
        private void MasterRPC(RpcMasterTypes rpcType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _curNumber = default;

            _infoMasterComFilter.Get1(0).FromInfo = infoFrom;

            switch (rpcType)
            {
                case RpcMasterTypes.None:
                    throw new Exception();

                case RpcMasterTypes.Ready:
                    _readyFilter.Get2(0).NeedActiveSomething = (bool)objects[0];
                    break;

                case RpcMasterTypes.Done:
                    _donerFilter.Get1(0).NeedActiveDoner = (bool)objects[0];
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
                    _conditionFilter.Get1(0).NeededCondUnitType = (ConditionUnitTypes)objects[0];
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
                    _seedingFilter.Get1(0).EnvTypeForSeeding = (EnvironmentTypes)objects[1];
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

                case RpcMasterTypes.GiveTakeToolWeapon:
                    _forGivePawnToolFilter.Get1(0).ToolWeapType = (ToolWeaponTypes)objects[_curNumber++];
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
            _fromInfoFilter.Get1(0).FromInfo = infoFrom;

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
                    _mistakeUIFilter.Get1(0).MistakeTypes = mistakeType;
                    _mistakeUIFilter.Get1(0).CurrentTime = default;
                    switch (mistakeType)
                    {
                        case MistakeTypes.None:
                            throw new Exception();

                        case MistakeTypes.Economy:
                            var haves = (bool[])objects[_curNumber++];
                            var haveFood = haves[0];
                            var haveWood = haves[1];
                            var haveOre = haves[2];
                            var haveIron = haves[3];
                            var haveGold = haves[4];

                            if (!haveFood) _mistakeUIFilter.Get1(0).AddNeedResources(ResourceTypes.Food);
                            if (!haveWood) _mistakeUIFilter.Get1(0).AddNeedResources(ResourceTypes.Wood);
                            if (!haveOre) _mistakeUIFilter.Get1(0).AddNeedResources(ResourceTypes.Ore);
                            if (!haveIron) _mistakeUIFilter.Get1(0).AddNeedResources(ResourceTypes.Iron);
                            if (!haveGold) _mistakeUIFilter.Get1(0).AddNeedResources(ResourceTypes.Gold);
                            break;

                        case MistakeTypes.NeedKing:
                            break;

                        case MistakeTypes.NeedMoreSteps:
                            break;

                        case MistakeTypes.NeedOtherPlace:
                            break;

                        case MistakeTypes.NeedMoreHealth:
                            break;

                        case MistakeTypes.PawnMustHaveTool:
                            break;

                        case MistakeTypes.PawnHaveTool:
                            break;

                        case MistakeTypes.NeedCity:
                            break;

                        case MistakeTypes.ThatIsForOtherUnit:
                            break;

                        case MistakeTypes.NearBorder:
                            break;

                        default:
                            throw new Exception();
                    }
                    break;

                case RpcGeneralTypes.SetUnit:
                    if ((bool)objects[_curNumber++])
                    {
                        selectorCom.DefSelectedCell();
                        selectorCom.SelUnitType = default;
                    }
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
            _infoOtherFilter.Get1(0).FromInfo = infoFrom;

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

            var isEndGame = _endGameFilter.Get1(0).IsEndGame;
            listObjects.Add(isEndGame);
            if (isEndGame)
            {
                var isOwnerWinner = _endGameFilter.Get1(0).IsOwnerWinner;
                listObjects.Add(isOwnerWinner);
                if (isOwnerWinner)
                {
                    listObjects.Add(_endGameFilter.Get1(0).PlayerWinner.ActorNumber);
                }
            }

            listObjects.Add(_readyUIFilter.Get1(0).IsStartedGame);
            listObjects.Add(_readyUIFilter.Get1(0).IsReady(false));

            listObjects.Add(_donerUIFilter.Get1(0).IsDoned(false));
            listObjects.Add(_motionsFilter.Get1(0).AmountMotions);

            foreach (var curIdxCell in _cellUnitFilter)
            {
                ref var curCellUnitDataComp = ref _cellUnitFilter.Get1(curIdxCell);
                listObjects.Add(curCellUnitDataComp.UnitType);
                listObjects.Add(curCellUnitDataComp.AmountHealth);
                listObjects.Add(curCellUnitDataComp.AmountSteps);
                listObjects.Add(curCellUnitDataComp.ConditionUnitType);
                listObjects.Add(curCellUnitDataComp.ArcherWeaponType);
                listObjects.Add(curCellUnitDataComp.ExtraTWPawnType);

                ref var curOwnerCellUnitComp = ref _cellUnitFilter.Get2(curIdxCell);
                var haveOwner = curOwnerCellUnitComp.HaveOwner;
                listObjects.Add(haveOwner);
                if (haveOwner) listObjects.Add(curOwnerCellUnitComp.ActorNumber);



                ref var curBuildDataComp = ref _cellBuildFilter.Get1(curIdxCell);
                listObjects.Add(curBuildDataComp.BuildingType);

                ref var curOwnerBuildComp = ref _cellBuildFilter.Get2(curIdxCell);
                var haveOwnerBuild = curOwnerBuildComp.HaveOwner;
                listObjects.Add(haveOwnerBuild);
                if (haveOwnerBuild) listObjects.Add(curOwnerBuildComp.ActorNumber);



                ref var curCellEnvDataComp = ref _cellEnvrFilter.Get1(curIdxCell);
                listObjects.Add(curCellEnvDataComp.HaveEnvironment(EnvironmentTypes.Fertilizer));
                listObjects.Add(curCellEnvDataComp.HaveEnvironment(EnvironmentTypes.YoungForest));
                listObjects.Add(curCellEnvDataComp.HaveEnvironment(EnvironmentTypes.AdultForest));
                listObjects.Add(curCellEnvDataComp.HaveEnvironment(EnvironmentTypes.Hill));
                listObjects.Add(curCellEnvDataComp.HaveEnvironment(EnvironmentTypes.Mountain));

                listObjects.Add(curCellEnvDataComp.GetAmountResources(EnvironmentTypes.Fertilizer));
                listObjects.Add(curCellEnvDataComp.GetAmountResources(EnvironmentTypes.YoungForest));
                listObjects.Add(curCellEnvDataComp.GetAmountResources(EnvironmentTypes.AdultForest));
                listObjects.Add(curCellEnvDataComp.GetAmountResources(EnvironmentTypes.Hill));
                listObjects.Add(curCellEnvDataComp.GetAmountResources(EnvironmentTypes.Mountain));



                ref var cellFireDataComp = ref _cellFireFilter.Get1(curIdxCell);
                listObjects.Add(cellFireDataComp.HaveFire);
            }



            ref var inventResComp = ref _inventorResFilter.Get1(0);
            listObjects.Add(inventResComp.AmountResources(ResourceTypes.Food, false));
            listObjects.Add(inventResComp.AmountResources(ResourceTypes.Wood, false));
            listObjects.Add(inventResComp.AmountResources(ResourceTypes.Ore, false));
            listObjects.Add(inventResComp.AmountResources(ResourceTypes.Iron, false));
            listObjects.Add(inventResComp.AmountResources(ResourceTypes.Gold, false));



            ref var invUnitsComp = ref _invUnitsFilter.Get1(0);
            listObjects.Add(invUnitsComp.AmountUnitsInInventor(UnitTypes.King, false));
            listObjects.Add(invUnitsComp.AmountUnitsInInventor(UnitTypes.Pawn, false));
            listObjects.Add(invUnitsComp.AmountUnitsInInventor(UnitTypes.Rook, false));
            listObjects.Add(invUnitsComp.AmountUnitsInInventor(UnitTypes.Bishop, false));



            ref var invToolsComp = ref _invToolsFilter.Get1(0);
            listObjects.Add(invToolsComp.GetAmountTools(false, ToolTypes.Pick));



            ref var invWeapsComp = ref _invWeaponsFilter.Get1(0);
            listObjects.Add(invWeapsComp.GetAmountWeapons(false, WeaponTypes.Sword));
            listObjects.Add(invWeapsComp.GetAmountWeapons(false, WeaponTypes.Crossbow));


            #region Else

            //for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            //    for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            //    {
            //        var xy = new int[] { x, y };

            //        listObjects.Add(CellUnitsDataSystem.UnitType(xy));
            //        listObjects.Add(CellUnitsDataSystem.IsVisibleUnit(false, xy));
            //        listObjects.Add(CellUnitsDataSystem.AmountSteps(xy));
            //        listObjects.Add(CellUnitsDataSystem.AmountHealth(xy));
            //        listObjects.Add(CellUnitsDataSystem.ConditionType(xy));
            //        if (CellUnitsDataSystem.HaveOwner(xy)) listObjects.Add(CellUnitsDataSystem.ActorNumber(xy));
            //        else listObjects.Add(-2);
            //        listObjects.Add(CellUnitsDataSystem.IsBot(xy));



            //        listObjects.Add(CellBuildDataSystem.BuildTypeCom(xy).BuildingType);
            //        if (CellBuildDataSystem.OwnerCom(xy).HaveOwner) listObjects.Add(CellBuildDataSystem.OwnerCom(xy).ActorNumber);
            //        else listObjects.Add(-2);
            //        listObjects.Add(CellBuildDataSystem.OwnerBotCom(xy).IsBot);



            //        listObjects.Add(CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Fertilizer, xy));
            //        listObjects.Add(CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.AdultForest, xy));
            //        listObjects.Add(CellEnvrDataSystem.GetAmountResources(EnvironmentTypes.Hill, xy));

            //        listObjects.Add(CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy));
            //        listObjects.Add(CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.YoungForest, xy));
            //        listObjects.Add(CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy));
            //        listObjects.Add(CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy));
            //        listObjects.Add(CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy));


            //        listObjects.Add(CellFireDataSystem.HaveFireCom(xy).HaveFire);
            //    }



            //listObjects.Add(SaverComponent.StepModeType);
            //listObjects.Add(_readyUIFilter.Get1(0).IsStartedGame);



            //listObjects.Add(_readyUIFilter.Get1(0).IsReady(false));
            //listObjects.Add(_donerUIFilter.Get1(0).IsDoned(false));


            //ref var invResCom = ref _inventorFilter.Get1(0);
            //listObjects.Add(invResCom.GetAmountResources(ResourceTypes.Food, false));
            //listObjects.Add(invResCom.GetAmountResources(ResourceTypes.Wood, false));
            //listObjects.Add(invResCom.GetAmountResources(ResourceTypes.Ore, false));
            //listObjects.Add(invResCom.GetAmountResources(ResourceTypes.Iron, false));
            //listObjects.Add(invResCom.GetAmountResources(ResourceTypes.Gold, false));



            //for (UnitTypes unitTypeType = (UnitTypes)1; (byte)unitTypeType < Enum.GetNames(typeof(UnitTypes)).Length; unitTypeType++)
            //{
            //    ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

            //    var amountUnitsInGame = xyUnitsCom.GetAmountUnitsInGame(unitTypeType, false);
            //    listObjects.Add(amountUnitsInGame);
            //    for (int indexXy = 0; indexXy < amountUnitsInGame; indexXy++)
            //    {
            //        listObjects.Add(xyUnitsCom.GetXyUnitInGame(unitTypeType, false, indexXy));
            //    }
            //}



            //listObjects.Add(_upgradesBuildFilter.Get1(0).GetAmountUpgrades(BuildingTypes.Farm, false));
            //listObjects.Add(_upgradesBuildFilter.Get1(0).GetAmountUpgrades(BuildingTypes.Woodcutter, false));
            //listObjects.Add(_upgradesBuildFilter.Get1(0).GetAmountUpgrades(BuildingTypes.Mine, false));

            //for (BuildingTypes buildingType = (BuildingTypes)1; (byte)buildingType < Enum.GetNames(typeof(BuildingTypes)).Length; buildingType++)
            //{
            //    var amountBuildingsInGame = MainGameSystem.XyBuildingsCom.GetAmountBuild(buildingType, false);
            //    listObjects.Add(amountBuildingsInGame);
            //    for (int indexXy = 0; indexXy < amountBuildingsInGame; indexXy++)
            //    {
            //        listObjects.Add(MainGameSystem.XyBuildingsCom.GetXyBuildByIndex(buildingType, false, indexXy));
            //    }
            //}

            #endregion


            var objects = new object[listObjects.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];


            PhotonView.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);
        }

        [PunRPC]
        private void SyncAllOther(object[] objects)
        {
            _curNumber = 0;

            ref var endGameDataUIComp = ref _endGameFilter.Get1(0);

            var isEndGame = (bool)objects[_curNumber++];
            endGameDataUIComp.IsEndGame = isEndGame;

            if (isEndGame)
            {
                var isOwnerWinner = (bool)objects[_curNumber++];
                endGameDataUIComp.IsOwnerWinner = isOwnerWinner;
                if (isOwnerWinner)
                {
                    var actorNumber = (int)objects[_curNumber++];

                    endGameDataUIComp.PlayerWinner = PhotonNetwork.PlayerList[actorNumber - 1];
                }
            }


            _readyUIFilter.Get1(0).IsStartedGame = (bool)objects[_curNumber++];
            _readyUIFilter.Get1(0).SetIsReady(PhotonNetwork.IsMasterClient, (bool)objects[_curNumber++]);

            _donerUIFilter.Get1(0).SetDoned(false, (bool)objects[_curNumber++]);
            _motionsFilter.Get1(0).AmountMotions = (int)objects[_curNumber++];

            foreach (var curIdxCell in _cellUnitFilter)
            {
                ref var curCellUnitDataComp = ref _cellUnitFilter.Get1(curIdxCell);
                curCellUnitDataComp.UnitType = (UnitTypes)objects[_curNumber++];
                curCellUnitDataComp.AmountHealth = (int)objects[_curNumber++];
                curCellUnitDataComp.AmountSteps = (int)objects[_curNumber++];
                curCellUnitDataComp.ConditionUnitType = (ConditionUnitTypes)objects[_curNumber++];
                curCellUnitDataComp.ArcherWeaponType = (ToolWeaponTypes)objects[_curNumber++];
                curCellUnitDataComp.ExtraTWPawnType = (ToolWeaponTypes)objects[_curNumber++];

                ref var curOwnerCellUnitComp = ref _cellUnitFilter.Get2(curIdxCell);
                var haveOwner = (bool)objects[_curNumber++];
                if (haveOwner) curOwnerCellUnitComp.SetOwner(PhotonNetwork.PlayerList[(int)objects[_curNumber++] - 1]);



                ref var curCellBuildDataComp = ref _cellBuildFilter.Get1(curIdxCell);
                curCellBuildDataComp.BuildingType = (BuildingTypes)objects[_curNumber++];

                ref var curOwnerCellBuildComp = ref _cellBuildFilter.Get2(curIdxCell);
                var haveOwnerBuild = (bool)objects[_curNumber++];
                if (haveOwnerBuild) curOwnerCellBuildComp.SetOwner(PhotonNetwork.PlayerList[(int)objects[_curNumber++] - 1]);



                ref var curCellEnvrDataComp = ref _cellEnvrFilter.Get1(curIdxCell);
                curCellEnvrDataComp.SetHaveEnvironment(EnvironmentTypes.Fertilizer, (bool)objects[_curNumber++]);
                curCellEnvrDataComp.SetHaveEnvironment(EnvironmentTypes.YoungForest, (bool)objects[_curNumber++]);
                curCellEnvrDataComp.SetHaveEnvironment(EnvironmentTypes.AdultForest, (bool)objects[_curNumber++]);
                curCellEnvrDataComp.SetHaveEnvironment(EnvironmentTypes.Hill, (bool)objects[_curNumber++]);
                curCellEnvrDataComp.SetHaveEnvironment(EnvironmentTypes.Mountain, (bool)objects[_curNumber++]);

                curCellEnvrDataComp.SetAmountResources(EnvironmentTypes.Fertilizer, (int)objects[_curNumber++]);
                curCellEnvrDataComp.SetAmountResources(EnvironmentTypes.YoungForest, (int)objects[_curNumber++]);
                curCellEnvrDataComp.SetAmountResources(EnvironmentTypes.AdultForest, (int)objects[_curNumber++]);
                curCellEnvrDataComp.SetAmountResources(EnvironmentTypes.Hill, (int)objects[_curNumber++]);
                curCellEnvrDataComp.SetAmountResources(EnvironmentTypes.Mountain, (int)objects[_curNumber++]);



                ref var curCellFireDataComp = ref _cellFireFilter.Get1(curIdxCell);
                curCellFireDataComp.HaveFire = (bool)objects[_curNumber++];
            }



            ref var inventResComp = ref _inventorResFilter.Get1(0);
            inventResComp.SetAmountResources(ResourceTypes.Food, PhotonNetwork.IsMasterClient, (int)objects[_curNumber++]);
            inventResComp.SetAmountResources(ResourceTypes.Wood, PhotonNetwork.IsMasterClient, (int)objects[_curNumber++]);
            inventResComp.SetAmountResources(ResourceTypes.Ore, PhotonNetwork.IsMasterClient, (int)objects[_curNumber++]);
            inventResComp.SetAmountResources(ResourceTypes.Iron, PhotonNetwork.IsMasterClient, (int)objects[_curNumber++]);
            inventResComp.SetAmountResources(ResourceTypes.Gold, PhotonNetwork.IsMasterClient, (int)objects[_curNumber++]);



            ref var invUnitsComp = ref _invUnitsFilter.Get1(0);
            invUnitsComp.SetAmountUnitsInInvent(UnitTypes.King, PhotonNetwork.IsMasterClient, (int)objects[_curNumber++]);
            invUnitsComp.SetAmountUnitsInInvent(UnitTypes.Pawn, PhotonNetwork.IsMasterClient, (int)objects[_curNumber++]);
            invUnitsComp.SetAmountUnitsInInvent(UnitTypes.Rook, PhotonNetwork.IsMasterClient, (int)objects[_curNumber++]);
            invUnitsComp.SetAmountUnitsInInvent(UnitTypes.Bishop, PhotonNetwork.IsMasterClient, (int)objects[_curNumber++]);



            ref var invToolsComp = ref _invToolsFilter.Get1(0);
            invToolsComp.SetAmountTools(PhotonNetwork.IsMasterClient, ToolTypes.Pick, (byte)objects[_curNumber++]);



            ref var invWeapsComp = ref _invWeaponsFilter.Get1(0);
            invWeapsComp.SetAmountWeapons(PhotonNetwork.IsMasterClient, WeaponTypes.Sword, (byte)objects[_curNumber++]);
            invWeapsComp.SetAmountWeapons(PhotonNetwork.IsMasterClient, WeaponTypes.Crossbow, (byte)objects[_curNumber++]);





            //for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            //    for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            //    {
            //        var xy = new int[] { x, y };

            //        Player owner;

            //        UnitTypes unitType = (UnitTypes)objects[_currentNumber++];
            //        bool isVisibleUnit = (bool)objects[_currentNumber++];
            //        int amountSteps = (int)objects[_currentNumber++];
            //        int amountHealth = (int)objects[_currentNumber++];
            //        ConditionUnitTypes conditionType = (ConditionUnitTypes)objects[_currentNumber++];
            //        int actorNumber = (int)objects[_currentNumber++];
            //        bool haveBot = (bool)objects[_currentNumber++];

            //        if (unitType != UnitTypes.None)
            //        {
            //            if (actorNumber != -2)
            //            {
            //                owner = PhotonNetwork.PlayerList[actorNumber - 1];
            //                CellUnitsDataSystem.SetPlayerUnit(unitType, amountHealth, amountSteps, conditionType, owner, xy);
            //            }
            //            else
            //            {
            //                CellUnitsDataSystem.SetBotUnit(unitType, haveBot, amountHealth, amountSteps, conditionType, xy);
            //            }

            //            CellUnitsDataSystem.SetIsVisibleUnit(PhotonNetwork.IsMasterClient, isVisibleUnit, xy);
            //        }

            //        else
            //        {
            //            CellUnitsDataSystem.ResetUnit(xy);
            //        }



            //        BuildingTypes buildingType = (BuildingTypes)objects[_currentNumber++];
            //        actorNumber = (int)objects[_currentNumber++];
            //        haveBot = (bool)objects[_currentNumber++];

            //        if (buildingType != BuildingTypes.None)
            //        {
            //            if (actorNumber != -2)
            //            {
            //                owner = PhotonNetwork.PlayerList[actorNumber - 1];
            //                CellBuildDataSystem.SetPlayerBuilding(buildingType, owner, xy);
            //            }
            //            else
            //            {
            //                CellBuildDataSystem.SetBotBuilding(buildingType, xy);
            //            }
            //        }

            //        else
            //        {
            //            CellBuildDataSystem.ResetBuild(xy);
            //        }



            //        int amountResourcesFertilizer = (int)objects[_currentNumber++];
            //        int amountResourcesAdultForest = (int)objects[_currentNumber++];
            //        int amountResourcesOre = (int)objects[_currentNumber++];

            //        bool haveFertilizer = (bool)objects[_currentNumber++];
            //        bool haveYoungTree = (bool)objects[_currentNumber++];
            //        bool haveAdultForest = (bool)objects[_currentNumber++];
            //        bool haveHill = (bool)objects[_currentNumber++];
            //        bool haveMountain = (bool)objects[_currentNumber++];

            //        CellEnvrDataSystem.SetEnvironment(EnvironmentTypes.Fertilizer, haveFertilizer, amountResourcesFertilizer, xy);
            //        CellEnvrDataSystem.SetEnvironment(EnvironmentTypes.YoungForest, haveYoungTree, default, xy);
            //        CellEnvrDataSystem.SetEnvironment(EnvironmentTypes.AdultForest, haveAdultForest, amountResourcesAdultForest, xy);
            //        CellEnvrDataSystem.SetEnvironment(EnvironmentTypes.Hill, haveHill, amountResourcesOre, xy);
            //        CellEnvrDataSystem.SetEnvironment(EnvironmentTypes.Mountain, haveMountain, default, xy);



            //        bool haveFire = (bool)objects[_currentNumber++];

            //        CellFireDataSystem.HaveFireCom(xy).HaveFire = haveFire;
            //    }



            //SaverComponent.StepModeType = (StepModeTypes)objects[_currentNumber++];



            //bool isStartedGame = (bool)objects[_currentNumber++];
            //_readyUIFilter.Get1(0).IsStartedGame = isStartedGame;



            //bool isActivatedReadyButton = (bool)objects[_currentNumber++];
            //_readyUIFilter.Get1(0).SetIsReady(PhotonNetwork.IsMasterClient, isActivatedReadyButton);



            //bool isActivatedDoner = (bool)objects[_currentNumber++];
            //_donerUIFilter.Get1(0).SetDoned(PhotonNetwork.IsMasterClient, isActivatedDoner);


            //ref var invResCom = ref _inventorFilter.Get1(0);
            //var food = (int)objects[_currentNumber++];
            //var wood = (int)objects[_currentNumber++];
            //var ore = (int)objects[_currentNumber++];
            //var iron = (int)objects[_currentNumber++];
            //var gold = (int)objects[_currentNumber++];
            //invResCom.SetAmountResources(ResourceTypes.Food, PhotonNetwork.IsMasterClient, food);
            //invResCom.SetAmountResources(ResourceTypes.Wood, PhotonNetwork.IsMasterClient, wood);
            //invResCom.SetAmountResources(ResourceTypes.Ore, PhotonNetwork.IsMasterClient, ore);
            //invResCom.SetAmountResources(ResourceTypes.Iron, PhotonNetwork.IsMasterClient, iron);
            //invResCom.SetAmountResources(ResourceTypes.Gold, PhotonNetwork.IsMasterClient, gold);



            //for (UnitTypes unitTypeType = (UnitTypes)1; (byte)unitTypeType < Enum.GetNames(typeof(UnitTypes)).Length; unitTypeType++)
            //{
            //    var amountUnits = (int)objects[_currentNumber++];

            //    ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

            //    List<int[]> xyUnits = new List<int[]>();
            //    for (int i = 0; i < amountUnits; i++)
            //    {
            //        var xyUnit = (int[])objects[_currentNumber++];
            //        xyUnits.Add(xyUnit);
            //    }
            //    xyUnitsCom.SetAmountUnitInGame(unitTypeType, PhotonNetwork.IsMasterClient, xyUnits);
            //}



            //var amountFarmUpgrades = (int)objects[_currentNumber++];
            //var amountWoodcutterUpgrades = (int)objects[_currentNumber++];
            //var amountMineUpgrades = (int)objects[_currentNumber++];
            //_upgradesBuildFilter.Get1(0).SetAmountUpgrades(BuildingTypes.Farm, PhotonNetwork.IsMasterClient, amountFarmUpgrades);
            //_upgradesBuildFilter.Get1(0).SetAmountUpgrades(BuildingTypes.Woodcutter, PhotonNetwork.IsMasterClient, amountWoodcutterUpgrades);
            //_upgradesBuildFilter.Get1(0).SetAmountUpgrades(BuildingTypes.Mine, PhotonNetwork.IsMasterClient, amountMineUpgrades);

            //for (BuildingTypes buildingType = (BuildingTypes)1; (byte)buildingType < Enum.GetNames(typeof(BuildingTypes)).Length; buildingType++)
            //{
            //    var amountBuildings = (int)objects[_currentNumber++];

            //    List<int[]> xyBuildings = new List<int[]>();
            //    for (int i = 0; i < amountBuildings; i++)
            //    {
            //        var xyBuilding = (int[])objects[_currentNumber++];
            //        xyBuildings.Add(xyBuilding);
            //    }
            //    MainGameSystem.XyBuildingsCom.SetXyBuildings(buildingType, PhotonNetwork.IsMasterClient, xyBuildings);
            //}



            //GameGeneralSystemManager.SyncCellVisionSystems.Run();
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