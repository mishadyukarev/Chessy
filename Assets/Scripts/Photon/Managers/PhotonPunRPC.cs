using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static MainGame;

internal class PhotonPunRPC : MonoBehaviour
{
    private PhotonView _photonView = default;
    private SystemsGeneralManager _systemGeneralManager = default;
    private SystemsMasterManager _systemsMasterManager = default;


    #region ComponetsRef

    #region Master

    private EcsComponentRef<SetterUnitMasterComponent> _setterUnitMasterComponentRef = default;
    private EcsComponentRef<ShiftUnitMasterComponent> _shiftUnitMasterComponentRef = default;
    private EcsComponentRef<DonerMasterComponent> _donerMasterComponentRef = default;
    private EcsComponentRef<AttackUnitMasterComponent> _attackUnitMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponentRef = default;
    private EcsComponentRef<BuilderCellMasterComponent> _builderCellMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> _economyUnitMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> _economyBuildingsMasterComponentRef = default;
    private EcsComponentRef<ProtecterUnitMasterComponent> _protecterUnitMasterComponentRef = default;
    private EcsComponentRef<RefresherMasterComponent> _refresherMasterComponentRef = default;
    private EcsComponentRef<ReadyMasterComponent> _readyMasterComponentRef = default;
    private EcsComponentRef<TheEndGameMasterComponent> _theEndMasterComponentRef = default;
    private EcsComponentRef<FromInfoComponent> _fromInfoComponentRef = default;

    #endregion


    #region General

    private EcsComponentRef<EconomyComponent> _economyComponentRef = default;
    private EcsComponentRef<EconomyComponent.BuildingComponent> _economyBuildingsComponentRef = default;
    private EcsComponentRef<EconomyComponent.UnitComponent> _economyUnitsComponentRef = default;

    private EcsComponentRef<CellComponent>[,] _cellComponentRef = default;
    private EcsComponentRef<CellComponent.EnvironmentComponent>[,] _cellEnvironmentComponentRef = default;
    private EcsComponentRef<CellComponent.SupportVisionComponent>[,] _cellSupportVisionComponentRef = default;
    private EcsComponentRef<CellComponent.UnitComponent>[,] _cellUnitComponentRef = default;
    private EcsComponentRef<CellComponent.BuildingComponent>[,] _cellBuildingComponentRef = default;

    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;
    private EcsComponentRef<DonerComponent> _buttonComponentRef = default;
    private EcsComponentRef<SelectedUnitComponent> _selectedUnitComponentRef = default;
    private EcsComponentRef<ReadyComponent> _readyComponentRef = default;
    private EcsComponentRef<TheEndGameComponent> _theEndComponentRef = default;
    private EcsComponentRef<StartGameComponent> _startGameComponentRef = default;
    private EcsComponentRef<AnimationAttackUnitComponent> _animationAttackUnitComponentRef = default;
    private EcsComponentRef<InfoRefreshComponent> _infoRefreshComponentRef = default;
    private EcsComponentRef<InfoMotionComponent> _infoMotionComponentRef = default;
    private EcsComponentRef<SoundComponent> _soundComponentRef = default;
    private EcsComponentRef<EconomyUIComponent> _economyUIComponentRef;

    #endregion

    #endregion


    private ref CellComponent CellComponent(params int[] xy)
        => ref _cellComponentRef[xy[_startValuesGameConfig.X], xy[_startValuesGameConfig.Y]].Unref();
    private ref CellComponent.UnitComponent CellUnitComponent(params int[] xy)
        => ref _cellUnitComponentRef[xy[_startValuesGameConfig.X], xy[_startValuesGameConfig.Y]].Unref();
    private ref CellComponent.EnvironmentComponent CellEnvironmentComponent(params int[] xy)
        => ref _cellEnvironmentComponentRef[xy[_startValuesGameConfig.X], xy[_startValuesGameConfig.Y]].Unref();
    private ref CellComponent.SupportVisionComponent CellSupportVisionComponent(params int[] xy)
        => ref _cellSupportVisionComponentRef[xy[_startValuesGameConfig.X], xy[_startValuesGameConfig.Y]].Unref();
    private ref CellComponent.BuildingComponent CellBuildingComponent(params int[] xy)
        => ref _cellBuildingComponentRef[xy[_startValuesGameConfig.X], xy[_startValuesGameConfig.Y]].Unref();


    private StartValuesGameConfig _startValuesGameConfig => InstanceGame.StartValuesGameConfig;
    private CellBaseOperations _cellManager => InstanceGame.CellManager.CellBaseOperations;



    internal void Constructor(PhotonView photonView)
    {
        _photonView = photonView;

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
    }

    internal void InitAfterECS(ECSmanager eCSmanager)
    {
        if (InstanceGame.IsMasterClient)
        {
            _systemsMasterManager = eCSmanager.SystemsMasterManager;


            var entitiesMasterManager = eCSmanager.EntitiesMasterManager;

            _setterUnitMasterComponentRef = entitiesMasterManager.SetterUnitMasterComponentRef;
            _shiftUnitMasterComponentRef = entitiesMasterManager.ShiftUnitComponentRef;
            _donerMasterComponentRef = entitiesMasterManager.DonerMasterComponentRef;
            _attackUnitMasterComponentRef = entitiesMasterManager.AttackUnitMasterComponentRef;
            _economyMasterComponentRef = entitiesMasterManager.EconomyMasterComponentRef;
            _builderCellMasterComponentRef = entitiesMasterManager.BuilderCellMasterComponentRef;
            _economyUnitMasterComponentRef = entitiesMasterManager.EconomyUnitsMasterComponentRef;
            _economyBuildingsMasterComponentRef = entitiesMasterManager.EconomyBuildingsMasterComponentRef;
            _protecterUnitMasterComponentRef = entitiesMasterManager.ProtecterUnitMasterComponentRef;
            _refresherMasterComponentRef = entitiesMasterManager.RefresherMasterComponentRef;
            _readyMasterComponentRef = entitiesMasterManager.ReadyMasterComponentRef;
            _theEndMasterComponentRef = entitiesMasterManager.TheEndGameMasterComponentRef;
            _fromInfoComponentRef = entitiesMasterManager.FromInfoComponentRef;
        }


        _systemGeneralManager = eCSmanager.SystemsGeneralManager;


        var entitiesGeneralManager = eCSmanager.EntitiesGeneralManager;

        _cellComponentRef = entitiesGeneralManager.CellComponentRef;
        _cellUnitComponentRef = entitiesGeneralManager.CellUnitComponentRef;
        _cellEnvironmentComponentRef = entitiesGeneralManager.CellEnvironmentComponentRef;
        _cellSupportVisionComponentRef = entitiesGeneralManager.CellSupportVisionComponentRef;
        _cellBuildingComponentRef = entitiesGeneralManager.CellBuildingComponentRef;

        _economyComponentRef = entitiesGeneralManager.EconomyComponentRef;
        _economyBuildingsComponentRef = entitiesGeneralManager.EconomyBuildingsComponentRef;

        _selectorComponentRef = entitiesGeneralManager.SelectorComponentRef;
        _buttonComponentRef = entitiesGeneralManager.DonerComponentRef;
        _selectedUnitComponentRef = entitiesGeneralManager.SelectedUnitComponentRef;
        _economyUnitsComponentRef = entitiesGeneralManager.EconomyUnitsComponentRef;
        _readyComponentRef = entitiesGeneralManager.ReadyComponentRef;
        _theEndComponentRef = entitiesGeneralManager.TheEndGameComponentRef;
        _startGameComponentRef = entitiesGeneralManager.StartGameComponentRef;
        _animationAttackUnitComponentRef = entitiesGeneralManager.AnimationAttackUnitComponentRef;
        _infoRefreshComponentRef = entitiesGeneralManager.RefreshComponentRef;
        _infoMotionComponentRef = entitiesGeneralManager.InfoMotionComponentRef;
        _soundComponentRef = entitiesGeneralManager.SoundComponentRef;
        _economyUIComponentRef = entitiesGeneralManager.EconomyUIComponentRef;



        RefreshAll();
    }

    #region Mistake

    private void Mistake(Player player, bool haveFood, bool haveWood, bool haveOre, bool haveIron, bool haveGold)
    {
        _photonView.RPC(nameof(MistakeGeneral), player, !haveFood, !haveWood, !haveOre, !haveIron, !haveGold);
    }

    [PunRPC]
    private void MistakeGeneral(bool needFood, bool needWood, bool needOre, bool needIron, bool needGold)
    {
        _economyUIComponentRef.Unref().NeedFood = needFood;
        _economyUIComponentRef.Unref().NeedWood = needWood;
        _economyUIComponentRef.Unref().NeedOre = needOre;
        _economyUIComponentRef.Unref().NeedIron = needIron;
        _economyUIComponentRef.Unref().NeedGold = needGold;
    }

    #endregion


    #region THE END OF GAME

    internal void EndGame(int actorNumberWinner) => _photonView.RPC("EndGameToMaster", RpcTarget.MasterClient, actorNumberWinner);

    [PunRPC]
    private void EndGameToMaster(int actorNumberWinner, PhotonMessageInfo info)
    {
        _photonView.RPC("EndGameToGeneral", RpcTarget.All, actorNumberWinner);

        RefreshAll();
    }

    [PunRPC]
    private void EndGameToGeneral(int actorNumber)
    {
        _theEndComponentRef.Unref().IsEndGame = true;
        _theEndComponentRef.Unref().PlayerWinner = PhotonNetwork.PlayerList[actorNumber - 1];
    }

    #endregion


    #region Ready

    public void Ready(in bool isReady) => _photonView.RPC("ReadyToMaster", RpcTarget.MasterClient, isReady);

    [PunRPC]
    private void ReadyToMaster(bool isReady, PhotonMessageInfo info)
    {
        if (info.Sender.IsMasterClient) _readyMasterComponentRef.Unref().IsReadyMaster = isReady;
        else _readyMasterComponentRef.Unref().IsReadyOther = isReady;

        if (_readyMasterComponentRef.Unref().IsReadyMaster && _readyMasterComponentRef.Unref().IsReadyOther)
            _photonView.RPC("ReadyToGeneral", RpcTarget.All, true, true);
        else _photonView.RPC("ReadyToGeneral", info.Sender, isReady, false);

        RefreshAll();
    }

    [PunRPC]
    private void ReadyToGeneral(bool isReady, bool isStarted)
    {
        _readyComponentRef.Unref().IsReady = isReady;
        _startGameComponentRef.Unref().IsStartedGame = isStarted;
    }


    #endregion


    #region Done

    internal void Done(in bool isDone) => _photonView.RPC(nameof(DoneMaster), RpcTarget.MasterClient, isDone);

    [PunRPC]
    private void DoneMaster(bool isDone, PhotonMessageInfo info)
    {
        if (info.Sender.IsMasterClient && _economyUnitMasterComponentRef.Unref().IsSettedKingMaster
            || !info.Sender.IsMasterClient && _economyUnitMasterComponentRef.Unref().IsSettedKingOther)
        {
            _photonView.RPC(nameof(DoneGeneral), info.Sender, false, isDone, default);

            _fromInfoComponentRef.Unref().FromInfo = info;
            _refresherMasterComponentRef.Unref().IsDone = isDone;
            _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Solo, nameof(RefresherMasterSystem));

            if (_refresherMasterComponentRef.Unref().IsRefreshed)
            {
                
                _photonView.RPC(nameof(DoneGeneral), RpcTarget.All, false, false, _refresherMasterComponentRef.Unref().NumberMotion);
                RefreshAll();
            }
        }
        else
        {
            _photonView.RPC(nameof(DoneGeneral), info.Sender, true, false, default);
        }
    }

    [PunRPC]
    private void DoneGeneral(bool isMistaked, bool isDone, int numberMotion)
    {
        if (isMistaked) _buttonComponentRef.Unref().IsMistaked = isMistaked;
        else
        {
            _buttonComponentRef.Unref().IsDone = isDone;
        }

        if (!isMistaked && !isDone)
        {
            _infoMotionComponentRef.Unref().NumberMotion = numberMotion;
            _infoRefreshComponentRef.Unref().IsRefreshed = true;
        }
    }

    #endregion


    #region GetUnit

    internal void GetUnit(UnitTypes unitTypes) => _photonView.RPC(nameof(GetUnitMaster), RpcTarget.MasterClient, unitTypes);

    [PunRPC]
    private void GetUnitMaster(UnitTypes unitType, PhotonMessageInfo info)
    {
        bool isGetted = false;

        switch (unitType)
        {
            case UnitTypes.None:
                break;


            case UnitTypes.King:

                if (info.Sender.IsMasterClient)
                    isGetted = _economyUnitMasterComponentRef.Unref().AmountKingMaster >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = _economyUnitMasterComponentRef.Unref().AmountKingOther >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                break;


            case UnitTypes.Pawn:

                if (info.Sender.IsMasterClient)
                {
                    if (_economyUnitMasterComponentRef.Unref().AmountUnitPawnMaster >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (_economyUnitMasterComponentRef.Unref().AmountUnitPawnOther >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    if (_economyUnitMasterComponentRef.Unref().AmountRookMaster >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (_economyUnitMasterComponentRef.Unref().AmountRookOther >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                    isGetted = _economyUnitMasterComponentRef.Unref().AmountBishopMaster >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = _economyUnitMasterComponentRef.Unref().AmountBishopOther >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                break;


            default:
                break;

        }

        _photonView.RPC(nameof(GetUnitGeneral), info.Sender, unitType, isGetted);

        RefreshAll();
    }

    [PunRPC]
    private void GetUnitGeneral(UnitTypes unitType, bool isGetted)
    {
        if (isGetted)
        {
            _selectedUnitComponentRef.Unref().SelectedUnitType = unitType;
        }
    }

    #endregion


    #region SetUnit

    internal void SetUnit(in int[] xyCell, UnitTypes unitType)
        => _photonView.RPC("SetUnitToMaster", RpcTarget.MasterClient, xyCell, unitType);

    [PunRPC]
    private void SetUnitToMaster(int[] xyCell, UnitTypes unitType, PhotonMessageInfo info)
    {
        bool isSetted = _setterUnitMasterComponentRef.Unref().TrySetUnit(xyCell, unitType, info.Sender);

        if (unitType == UnitTypes.King)
        {
            if (info.Sender.IsMasterClient) _economyUnitMasterComponentRef.Unref().IsSettedKingMaster = isSetted;
            else _economyUnitMasterComponentRef.Unref().IsSettedKingOther = isSetted;
        }


        _photonView.RPC("SetUnitToGeneral", info.Sender, isSetted);

        RefreshAll();
    }

    [PunRPC]
    private void SetUnitToGeneral(bool isSetted)
    {
        if (isSetted) _selectorComponentRef.Unref().SetterUnitDelegate();
    }

    #endregion


    #region AttackUnit

    internal void AttackUnit(int[] xyPreviousCell, int[] xySelectedCell) => _photonView.RPC(nameof(AttackUnitMaster), RpcTarget.MasterClient, xyPreviousCell, xySelectedCell);

    [PunRPC]
    private void AttackUnitMaster(int[] xyPreviousCell, int[] xySelectedCell, PhotonMessageInfo info)
    {
        var isAttacked = _attackUnitMasterComponentRef.Unref()
            .TryAttackUnit(xyPreviousCell, xySelectedCell, info.Sender, out bool isKilledAttacker, out bool isKilledDefender);

        _photonView.RPC(nameof(AttackUnitGeneral), info.Sender, isAttacked);
        if(isAttacked) _photonView.RPC(nameof(AttackUnitGeneral), RpcTarget.All);

        RefreshAll();
    }

    [PunRPC]
    private void AttackUnitGeneral(bool isAttacked)
    {
        if (isAttacked)
        {
            _selectorComponentRef.Unref().AttackUnitAction();
        }
    }

    [PunRPC]
    private void AttackUnitGeneral() => _soundComponentRef.Unref().AttackSoundAction();

    #endregion


    #region ShiftUnit

    public void ShiftUnit(in int[] xyPreviousCell, in int[] xySelectedCell)
        => _photonView.RPC("ShiftUnitMaster", RpcTarget.MasterClient, xyPreviousCell, xySelectedCell);

    [PunRPC]
    private void ShiftUnitMaster(int[] xyPreviousCell, int[] xySelectedCell, PhotonMessageInfo info)
    {
        bool isShifted = _shiftUnitMasterComponentRef.Unref().ShiftUnit(xyPreviousCell, xySelectedCell, info.Sender);
        _photonView.RPC("ShiftUnitGeneral", info.Sender, isShifted);

        RefreshAll();
    }

    [PunRPC]
    private void ShiftUnitGeneral(bool isShifted)
    {
        if (isShifted) _selectorComponentRef.Unref().ShiftUnitDelegate();
    }

    #endregion


    #region Protect

    public void ProtectUnitToMaster(bool isActive, in int[] xyCell)
        => _photonView.RPC(nameof(ProtectUnitMaster), RpcTarget.MasterClient, isActive, xyCell);

    [PunRPC]
    private void ProtectUnitMaster(bool isActive, int[] xyCell, PhotonMessageInfo info)
    {
        if (isActive)
        {
            if (!CellUnitComponent(xyCell).IsProtected)
            {
                if (CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    CellUnitComponent(xyCell).IsProtected = true;
                    CellUnitComponent(xyCell).IsRelaxed = false;
                    CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (CellUnitComponent(xyCell).IsProtected)
            {
                if (CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    CellUnitComponent(xyCell).IsProtected = false;
                    CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        RefreshAll();
    }

    #endregion


    #region Relax

    public void RelaxUnitToMaster(bool isActive, in int[] xyCell) => _photonView.RPC(nameof(RelaxUnitMaster), RpcTarget.MasterClient, isActive, xyCell);

    [PunRPC]
    private void RelaxUnitMaster(bool isActive, int[] xyCell, PhotonMessageInfo info)
    {
        if (isActive)
        {
            if (!CellUnitComponent(xyCell).IsRelaxed)
            {
                if (CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    CellUnitComponent(xyCell).IsRelaxed = true;
                    CellUnitComponent(xyCell).IsProtected = false;
                    CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (CellUnitComponent(xyCell).IsRelaxed)
            {
                if (CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    CellUnitComponent(xyCell).IsRelaxed = false;
                    CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }
        RefreshAll();
    }

    #endregion


    #region Build

    internal void Build(int[] xyCell, BuildingTypes buildingType) => _photonView.RPC("BuildMaster", RpcTarget.MasterClient, xyCell, buildingType);

    [PunRPC]
    private void BuildMaster(int[] xyCell, BuildingTypes buildingType, PhotonMessageInfo info)
    {
        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        bool isBuilded;

        if (!CellEnvironmentComponent(xyCell).HaveMountain && CellUnitComponent(xyCell).HaveMaxSteps && !CellBuildingComponent(xyCell).HaveBuilding)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    isBuilded = false;
                    break;

                case BuildingTypes.City:

                    CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                    isBuilded = true;
                    CellUnitComponent(xyCell).AmountSteps = 0;

                    if (info.Sender.IsMasterClient)
                    {
                        _economyBuildingsMasterComponentRef.Unref().IsBuildedCityMaster = isBuilded;
                        InstanceGame.CellManager.CellBaseOperations.CopyXYinTo(xyCell, _economyBuildingsMasterComponentRef.Unref().XYsettedCityMaster);

                        //_zoneComponentRef.Unref().XYMasterZone = InstanceGame.CellManager.CellFinderWay.TryGetXYAround(xyCellIN);
                    }
                    else
                    {
                        _economyBuildingsMasterComponentRef.Unref().IsBuildedCityOther = isBuilded;
                        InstanceGame.CellManager.CellBaseOperations.CopyXYinTo(xyCell, _economyBuildingsMasterComponentRef.Unref().XYsettedCityOther);

                        //_zoneComponentRef.Unref().XYOtherZone = InstanceGame.CellManager.CellFinderWay.TryGetXYAround(xyCellIN);
                    }

                    break;

                case BuildingTypes.Farm:

                    if (CellEnvironmentComponent(xyCell).HaveFood)
                    {
                        if (info.Sender.IsMasterClient)
                        {
                            haveFood = _economyMasterComponentRef.Unref().FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                            haveWood = _economyMasterComponentRef.Unref().WoodMaster >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                            haveOre = _economyMasterComponentRef.Unref().OreMaster >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                            haveIron = _economyMasterComponentRef.Unref().IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                            haveGold = _economyMasterComponentRef.Unref().GoldMaster >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _economyMasterComponentRef.Unref().GoldMaster -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                                _economyMasterComponentRef.Unref().FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                _economyMasterComponentRef.Unref().WoodMaster -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                _economyMasterComponentRef.Unref().OreMaster -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                _economyMasterComponentRef.Unref().IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;


                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _economyBuildingsMasterComponentRef.Unref().AmountFarmMaster += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            haveFood = _economyMasterComponentRef.Unref().FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                            haveWood = _economyMasterComponentRef.Unref().WoodOther >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                            haveOre = _economyMasterComponentRef.Unref().OreOther >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                            haveIron = _economyMasterComponentRef.Unref().IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                            haveGold = _economyMasterComponentRef.Unref().GoldOther >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _economyMasterComponentRef.Unref().GoldOther -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                                _economyMasterComponentRef.Unref().FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                _economyMasterComponentRef.Unref().WoodOther -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                _economyMasterComponentRef.Unref().OreOther -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                _economyMasterComponentRef.Unref().IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;

                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _economyBuildingsMasterComponentRef.Unref().AmountFarmOther += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                    }
                    isBuilded = true;

                    break;

                case BuildingTypes.Woodcutter:

                    if (CellEnvironmentComponent(xyCell).HaveTree)
                    {
                        if (info.Sender.IsMasterClient)
                        {
                            haveFood = _economyMasterComponentRef.Unref().FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                            haveWood = _economyMasterComponentRef.Unref().WoodMaster >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                            haveOre = _economyMasterComponentRef.Unref().OreMaster >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                            haveIron = _economyMasterComponentRef.Unref().IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                            haveGold = _economyMasterComponentRef.Unref().GoldMaster >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _economyMasterComponentRef.Unref().GoldMaster -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                                _economyMasterComponentRef.Unref().FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                                _economyMasterComponentRef.Unref().WoodMaster -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                                _economyMasterComponentRef.Unref().OreMaster -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                                _economyMasterComponentRef.Unref().IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;

                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterMaster += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            haveFood = _economyMasterComponentRef.Unref().FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                            haveWood = _economyMasterComponentRef.Unref().WoodOther >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                            haveOre = _economyMasterComponentRef.Unref().OreOther >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                            haveIron = _economyMasterComponentRef.Unref().IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                            haveGold = _economyMasterComponentRef.Unref().GoldOther >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _economyMasterComponentRef.Unref().GoldOther -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                                _economyMasterComponentRef.Unref().FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                                _economyMasterComponentRef.Unref().WoodOther -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                                _economyMasterComponentRef.Unref().OreOther -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                                _economyMasterComponentRef.Unref().IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;


                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterOther += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }

                    }
                    isBuilded = true;

                    break;

                case BuildingTypes.Mine:

                    if (CellEnvironmentComponent(xyCell).HaveHill)
                    {
                        if (info.Sender.IsMasterClient)
                        {
                            haveFood = _economyMasterComponentRef.Unref().FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                            haveWood = _economyMasterComponentRef.Unref().WoodMaster >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                            haveOre = _economyMasterComponentRef.Unref().OreMaster >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                            haveIron = _economyMasterComponentRef.Unref().IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                            haveGold = _economyMasterComponentRef.Unref().GoldMaster >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;


                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _economyMasterComponentRef.Unref().GoldMaster -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                                _economyMasterComponentRef.Unref().FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                                _economyMasterComponentRef.Unref().WoodMaster -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                                _economyMasterComponentRef.Unref().OreMaster -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                                _economyMasterComponentRef.Unref().IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;


                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _economyBuildingsMasterComponentRef.Unref().AmountMineMaster += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            haveFood = _economyMasterComponentRef.Unref().FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                            haveWood = _economyMasterComponentRef.Unref().WoodOther >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                            haveOre = _economyMasterComponentRef.Unref().OreOther >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                            haveIron = _economyMasterComponentRef.Unref().IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                            haveGold = _economyMasterComponentRef.Unref().GoldOther >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _economyMasterComponentRef.Unref().GoldOther -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                                _economyMasterComponentRef.Unref().FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                                _economyMasterComponentRef.Unref().WoodOther -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                                _economyMasterComponentRef.Unref().OreOther -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                                _economyMasterComponentRef.Unref().IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;


                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _economyBuildingsMasterComponentRef.Unref().AmountMineOther += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }

                    }
                    isBuilded = true;

                    break;

                default:
                    isBuilded = false;
                    break;
            }
        }
        else
        {
            isBuilded = false;
        }

        Mistake(info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

        RefreshAll();
    }

    #endregion


    #region Destroy

    internal void DestroyBuilding(int[] xyCell) => _photonView.RPC(nameof(DestroyBuildingMaster), RpcTarget.MasterClient, xyCell);

    [PunRPC]
    private void DestroyBuildingMaster(int[] xyCell, PhotonMessageInfo info)
    {
        if (CellUnitComponent(xyCell).IsHisUnit(info.Sender))
        {
            if (CellUnitComponent(xyCell).HaveMaxSteps)
            {
                if (info.Sender.IsMasterClient)
                {
                    switch (CellBuildingComponent(xyCell).BuildingType)
                    {
                        case BuildingTypes.City:

                            EndGame(CellUnitComponent(xyCell).ActorNumber);

                            break;


                        case BuildingTypes.Farm:

                            _economyBuildingsMasterComponentRef.Unref().AmountFarmMaster -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;


                        case BuildingTypes.Woodcutter:

                            _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterMaster -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Mine:

                            _economyBuildingsMasterComponentRef.Unref().AmountMineMaster -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                    }
                }

                else
                {
                    switch (CellBuildingComponent(xyCell).BuildingType)
                    {
                        case BuildingTypes.City:

                            EndGame(CellUnitComponent(xyCell).ActorNumber);

                            break;


                        case BuildingTypes.Farm:

                            _economyBuildingsMasterComponentRef.Unref().AmountFarmOther -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Woodcutter:

                            _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterOther -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Mine:
                            break;
                    }
                }

            }
        }
        RefreshAll();
    }

    #endregion


    #region BuyUnit

    internal void CreateUnit(in UnitTypes unitType) => _photonView.RPC(nameof(CreateUnitMaster), RpcTarget.MasterClient, unitType);

    [PunRPC]
    private void CreateUnitMaster(UnitTypes unitType, PhotonMessageInfo info)
    {
        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        switch (unitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                break;

            case UnitTypes.Pawn:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _economyMasterComponentRef.Unref().FoodMaster >= _startValuesGameConfig.FOOD_FOR_BUYING_PAWN;

                    if (haveFood)
                    {
                        _economyMasterComponentRef.Unref().FoodMaster -= _startValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        _economyUnitMasterComponentRef.Unref().AmountUnitPawnMaster += _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }
                else
                {
                    haveFood = _economyMasterComponentRef.Unref().FoodOther >= _startValuesGameConfig.FOOD_FOR_BUYING_PAWN;

                    if (haveFood)
                    {
                        _economyMasterComponentRef.Unref().FoodOther -= _startValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        _economyUnitMasterComponentRef.Unref().AmountUnitPawnOther += _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }

                break;

            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _economyMasterComponentRef.Unref().FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveIron = _economyMasterComponentRef.Unref().IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUYING_ROOK;

                    if (haveFood && haveIron)
                    {
                        _economyMasterComponentRef.Unref().IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        _economyMasterComponentRef.Unref().FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;

                        _economyUnitMasterComponentRef.Unref().AmountRookMaster += 1;
                    }                 
                }
                else
                {
                    haveFood = _economyMasterComponentRef.Unref().FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveIron = _economyMasterComponentRef.Unref().IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUYING_ROOK;

                    if (haveFood && haveIron)
                    {
                        _economyMasterComponentRef.Unref().IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        _economyMasterComponentRef.Unref().FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;

                        _economyUnitMasterComponentRef.Unref().AmountRookOther += 1;
                    }
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _economyMasterComponentRef.Unref().FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveIron = _economyMasterComponentRef.Unref().IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;

                    if (haveFood && haveIron)
                    {
                        _economyMasterComponentRef.Unref().IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        _economyMasterComponentRef.Unref().FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;

                        _economyUnitMasterComponentRef.Unref().AmountBishopMaster += 1;
                    }
                }
                else
                {
                    haveFood = _economyMasterComponentRef.Unref().FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveIron = _economyMasterComponentRef.Unref().IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;

                    if (haveFood && haveIron)
                    {
                        _economyMasterComponentRef.Unref().IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        _economyMasterComponentRef.Unref().FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;

                        _economyUnitMasterComponentRef.Unref().AmountBishopOther += 1;
                    }
                }

                break;


            default:
                break;
        }

        Mistake(info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

        RefreshAll();
    }

    #endregion


    #region MeltOre

    internal void MeltOre() => _photonView.RPC(nameof(MeltOreMaster), RpcTarget.MasterClient);

    [PunRPC]
    private void MeltOreMaster(PhotonMessageInfo info)
    {
        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        if (info.Sender.IsMasterClient)
        {
            haveWood = _economyMasterComponentRef.Unref().WoodMaster >= InstanceGame.StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _economyMasterComponentRef.Unref().OreMaster >= InstanceGame.StartValuesGameConfig.ORE_FOR_MELTING_ORE;

            if (haveWood && haveOre)
            {
                _economyMasterComponentRef.Unref().OreMaster -= InstanceGame.StartValuesGameConfig.ORE_FOR_MELTING_ORE;
                _economyMasterComponentRef.Unref().WoodMaster -= InstanceGame.StartValuesGameConfig.WOOD_FOR_MELTING_ORE;

                _economyMasterComponentRef.Unref().IronMaster += 1;
            }
        }
        else
        {
            haveWood = _economyMasterComponentRef.Unref().WoodOther >= InstanceGame.StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _economyMasterComponentRef.Unref().OreOther >= InstanceGame.StartValuesGameConfig.ORE_FOR_MELTING_ORE;

            if (haveWood && haveOre)
            {
                _economyMasterComponentRef.Unref().OreOther -= InstanceGame.StartValuesGameConfig.ORE_FOR_MELTING_ORE;
                _economyMasterComponentRef.Unref().WoodOther -= InstanceGame.StartValuesGameConfig.WOOD_FOR_MELTING_ORE;

                _economyMasterComponentRef.Unref().IronOther += 1;
            }
        }

        _photonView.RPC(nameof(MistakeGeneral), info.Sender, !haveFood, !haveWood, !haveOre, !haveIron, !haveGold);

        RefreshAll();
    }

    #endregion


    #region Refresh

    internal void RefreshAll() => _photonView.RPC(nameof(RefreshAllMaster), RpcTarget.MasterClient);

    [PunRPC]
    private void RefreshAllMaster()
    {
        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Solo, nameof(VisibilityUnitsMasterSystem));


        #region Sending

        List<object> listObjects = new List<object>();
        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
            {
                listObjects.Add(CellUnitComponent(x, y).IsActiveUnitOther);
                listObjects.Add(CellUnitComponent(x, y).UnitType);
                listObjects.Add(CellUnitComponent(x, y).ActorNumber);
                listObjects.Add(CellUnitComponent(x, y).AmountSteps);
                listObjects.Add(CellUnitComponent(x, y).AmountHealth);
                listObjects.Add(CellUnitComponent(x, y).SimplePowerDamage);
                listObjects.Add(CellUnitComponent(x, y).IsProtected);
                listObjects.Add(CellUnitComponent(x, y).IsRelaxed);

                listObjects.Add(CellEnvironmentComponent(x, y).HaveFood);
                listObjects.Add(CellEnvironmentComponent(x, y).HaveTree);
                listObjects.Add(CellEnvironmentComponent(x, y).HaveHill);
                listObjects.Add(CellEnvironmentComponent(x, y).HaveMountain);

                listObjects.Add(CellBuildingComponent(x, y).BuildingType);
                listObjects.Add(CellBuildingComponent(x, y).ActorNumber);
            }
        }
        object[] objects = new object[listObjects.Count];
        for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];

        _photonView.RPC(nameof(RefreshCellsGeneral), RpcTarget.Others, objects);




        objects = new object[]
        {
            _economyMasterComponentRef.Unref().GoldOther,
            _economyMasterComponentRef.Unref().FoodOther,
            _economyMasterComponentRef.Unref().WoodOther,
            _economyMasterComponentRef.Unref().OreOther,
            _economyMasterComponentRef.Unref().IronOther,
            _economyBuildingsMasterComponentRef.Unref().IsBuildedCityOther,
            _economyBuildingsMasterComponentRef.Unref().XYsettedCityOther,
            _economyUnitMasterComponentRef.Unref().IsSettedKingOther,
        };
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);

        objects = new object[]
        {
            _economyMasterComponentRef.Unref().GoldMaster,
            _economyMasterComponentRef.Unref().FoodMaster,
            _economyMasterComponentRef.Unref().WoodMaster,
            _economyMasterComponentRef.Unref().OreMaster,
            _economyMasterComponentRef.Unref().IronMaster,
            _economyBuildingsMasterComponentRef.Unref().IsBuildedCityMaster,
            _economyBuildingsMasterComponentRef.Unref().XYsettedCityMaster,
            _economyUnitMasterComponentRef.Unref().IsSettedKingMaster,
        };
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.MasterClient, objects);

        #endregion

    }

    [PunRPC]
    private void RefreshCellsGeneral(object[] objects)
    {
        int i = 0;
        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
            {
                bool isActiveUnit = (bool)objects[i++];
                UnitTypes unitType = (UnitTypes)objects[i++];
                int actorNumber = (int)objects[i++];
                int amountSteps = (int)objects[i++];
                int amountHealth = (int)objects[i++];
                int powerDamage = (int)objects[i++];
                bool isProtected = (bool)objects[i++];
                bool isRelaxed = (bool)objects[i++];

                bool haveFood = (bool)objects[i++];
                bool haveTree = (bool)objects[i++];
                bool haveHill = (bool)objects[i++];
                bool haveMountain = (bool)objects[i++];

                BuildingTypes buildingType = (BuildingTypes)objects[i++];
                int actorNumberBuilding = (int)objects[i++];


                Player player;
                if (actorNumber == -1) player = default;
                else player = PhotonNetwork.PlayerList[actorNumber - 1];
                CellUnitComponent(x, y).SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player);
                CellUnitComponent(x, y).ActiveVisionCell(isActiveUnit, unitType);

                CellEnvironmentComponent(x, y).SetResetEnvironment(haveFood, EnvironmentTypes.Food);
                CellEnvironmentComponent(x, y).SetResetEnvironment(haveTree, EnvironmentTypes.Tree);
                CellEnvironmentComponent(x, y).SetResetEnvironment(haveHill, EnvironmentTypes.Hill);
                CellEnvironmentComponent(x, y).SetResetEnvironment(haveMountain, EnvironmentTypes.Mountain);

                if (actorNumberBuilding == -1) player = default;
                else player = PhotonNetwork.PlayerList[actorNumberBuilding - 1];
                CellBuildingComponent(x, y).SetBuilding(buildingType, player);
            }
        }
    }

    [PunRPC]
    private void RefreshEconomyGeneral(object[] objects)
    {
        int i = 0;

        var gold = (int)objects[i++];
        var food = (int)objects[i++];
        var wood = (int)objects[i++];
        var ore = (int)objects[i++];
        var iron = (int)objects[i++];

        var isSettedCity = (bool)objects[i++];
        int[] xySettedCity = (int[])objects[i++];
        bool isSettedKing = (bool)objects[i++];


        _economyComponentRef.Unref().Gold = gold;
        _economyComponentRef.Unref().Food = food;
        _economyComponentRef.Unref().Wood = wood;
        _economyComponentRef.Unref().Ore = ore;
        _economyComponentRef.Unref().Iron = iron;

        _economyBuildingsComponentRef.Unref().IsSettedCity = isSettedCity;
        _economyBuildingsComponentRef.Unref().XYsettedCity = xySettedCity;

        _economyUnitsComponentRef.Unref().IsSettedKing = isSettedKing;
    }

    #endregion Ref


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