using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using UnityEngine;
using static MainGame;

public partial class PhotonPunRPC : MonoBehaviour
{
    private PhotonView _photonView = default;
    private StartValuesGameConfig _startValues = default;
    private CellManager _cellManager = default;
    private SystemsGeneralManager _systemsGeneralManager = default;


    #region ComponetsRef

    #region Master

    private EcsComponentRef<SetterUnitMasterComponent> _setterUnitMasterComponentRef = default;
    private EcsComponentRef<ShiftUnitMasterComponent> _shiftUnitMasterComponentRef = default;
    private EcsComponentRef<DonerMasterComponent> _donerMasterComponentRef = default;
    private EcsComponentRef<AttackUnitMasterComponent> _attackUnitMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponentRef = default;
    private EcsComponentRef<BuilderCellMasterComponent> _builderCellMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> _economyUnitsMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> _economyBuildingsMasterComponentRef = default;
    private EcsComponentRef<GetterUnitMasterComponent> _getterUnitMasterComponentRef = default;
    private EcsComponentRef<ProtecterUnitMasterComponent> _protecterUnitMasterComponentRef = default;
    private EcsComponentRef<RefresherMasterComponent> _refresherMasterComponentRef = default;
    private EcsComponentRef<ReadyMasterComponent> _readyMasterComponentRef = default;
    private EcsComponentRef<TheEndGameMasterComponent> _theEndMasterComponentRef = default;

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
    private EcsComponentRef<UnitPathsComponent> _unitPathComponentRef = default;
    private EcsComponentRef<ReadyComponent> _readyComponentRef = default;
    private EcsComponentRef<TheEndGameComponent> _theEndComponentRef = default;
    private EcsComponentRef<StartGameComponent> _startGameComponentRef = default;
    private EcsComponentRef<AnimationAttackUnitComponent> _animationAttackUnitComponentRef = default;

    #endregion

    #endregion

    private ref CellComponent CellComponent(params int[] xy)
        => ref _cellComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();
    private ref CellComponent.UnitComponent CellUnitComponent(params int[] xy)
        => ref _cellUnitComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();
    private ref CellComponent.EnvironmentComponent CellEnvironmentComponent(params int[] xy)
        => ref _cellEnvironmentComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();
    private ref CellComponent.SupportVisionComponent CellSupportVisionComponent(params int[] xy)
        => ref _cellSupportVisionComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();
    private ref CellComponent.BuildingComponent CellBuildingComponent(params int[] xy)
        => ref _cellBuildingComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();



    internal void Constructor(PhotonGameManager photonManager)
    {
        _photonView = photonManager.PhotonView;
        _startValues = InstanceGame.StartValuesGameConfig;
        _cellManager = InstanceGame.CellManager;

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
    }

    internal void InitAfterECS(ECSmanager eCSmanager)
    {
        if (InstanceGame.IsMasterClient)
        {
            var entitiesMasterManager = eCSmanager.EntitiesMasterManager;

            _setterUnitMasterComponentRef = entitiesMasterManager.SetterUnitMasterComponentRef;
            _shiftUnitMasterComponentRef = entitiesMasterManager.ShiftUnitComponentRef;
            _donerMasterComponentRef = entitiesMasterManager.DonerMasterComponentRef;
            _attackUnitMasterComponentRef = entitiesMasterManager.AttackUnitMasterComponentRef;
            _economyMasterComponentRef = entitiesMasterManager.EconomyMasterComponentRef;
            _builderCellMasterComponentRef = entitiesMasterManager.BuilderCellMasterComponentRef;
            _economyUnitsMasterComponentRef = entitiesMasterManager.EconomyUnitsMasterComponentRef;
            _economyBuildingsMasterComponentRef = entitiesMasterManager.EconomyBuildingsMasterComponentRef;
            _getterUnitMasterComponentRef = entitiesMasterManager.GetterUnitMasterComponentRef;
            _protecterUnitMasterComponentRef = entitiesMasterManager.ProtecterUnitMasterComponentRef;
            _refresherMasterComponentRef = entitiesMasterManager.RefresherMasterComponentRef;
            _readyMasterComponentRef = entitiesMasterManager.ReadyMasterComponentRef;
            _theEndMasterComponentRef = entitiesMasterManager.TheEndGameMasterComponentRef;
        }


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
        _unitPathComponentRef = entitiesGeneralManager.UnitPathComponentRef;
        _readyComponentRef = entitiesGeneralManager.ReadyComponentRef;
        _theEndComponentRef = entitiesGeneralManager.TheEndGameComponentRef;
        _startGameComponentRef = eCSmanager.EntitiesGeneralManager.StartGameComponentRef;
        _animationAttackUnitComponentRef = eCSmanager.EntitiesGeneralManager.AnimationAttackUnitComponentRef;

        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;


        RefreshAll();
    }


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

    public void Done(in bool isDone) => _photonView.RPC("DoneToMaster", RpcTarget.MasterClient, isDone);

    [PunRPC]
    private void DoneToMaster(bool isDone, PhotonMessageInfo info)
    {
        if (info.Sender.IsMasterClient && _economyUnitsMasterComponentRef.Unref().IsSettedKingMaster 
            || !info.Sender.IsMasterClient && _economyUnitsMasterComponentRef.Unref().IsSettedKingOther)
        {
            _photonView.RPC("DoneToGeneral", info.Sender, isDone);

            if (_refresherMasterComponentRef.Unref().TryRefresh(isDone, info.Sender))
            {
                _photonView.RPC("DoneToGeneral", RpcTarget.All, false);
            }

            RefreshAll();
        }
        else
        {
            _photonView.RPC("DoneToGeneral", info.Sender);
        }
    }
    [PunRPC] private void DoneToGeneral() => _buttonComponentRef.Unref().IsMistaked = true;
    [PunRPC] private void DoneToGeneral(bool isDone) => _buttonComponentRef.Unref().IsDone = isDone;

    #endregion


    #region GetUnit

    internal void GetUnit(UnitTypes unitTypes) => _photonView.RPC("GetUnitToMaster", RpcTarget.MasterClient, unitTypes);

    [PunRPC]
    private void GetUnitToMaster(UnitTypes unitType, PhotonMessageInfo info)
    {
        var isGetted = _getterUnitMasterComponentRef.Unref().TryGetUnit(unitType, info.Sender);

        _photonView.RPC("GetUnitToGeneral", info.Sender, unitType, isGetted);

        RefreshAll();
    }

    [PunRPC]
    private void GetUnitToGeneral(UnitTypes unitType, bool isGetted)
    {
        if (isGetted)
        {
            _selectedUnitComponentRef.Unref().SetSelectedUnit(unitType);
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
            if (info.Sender.IsMasterClient) _economyUnitsMasterComponentRef.Unref().IsSettedKingMaster = isSetted;
            else _economyUnitsMasterComponentRef.Unref().IsSettedKingOther = isSetted;
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

    internal void AttackUnit(int[] xyPreviousCell, int[] xySelectedCell)
        => _photonView.RPC("AttackUnitMaster", RpcTarget.MasterClient, xyPreviousCell, xySelectedCell);

    [PunRPC]
    private void AttackUnitMaster(int[] xyPreviousCell, int[] xySelectedCell, PhotonMessageInfo info)
    {
        var isAttacked = _attackUnitMasterComponentRef.Unref()
            .TryAttackUnit(xyPreviousCell, xySelectedCell, info.Sender, out bool isKilledAttacker, out bool isKilledDefender);

        _photonView.RPC("AttackUnitGeneral", info.Sender, isAttacked);
        _photonView.RPC("AttackUnitGeneral", RpcTarget.All, xyPreviousCell, xySelectedCell, isKilledAttacker, isKilledDefender);

        RefreshAll();
    }

    [PunRPC]
    private void AttackUnitGeneral(bool isAttacked)
    {
        if (isAttacked)
        {
            _selectorComponentRef.Unref().AttackUnitDelegate();
        }
    }
    [PunRPC]
    private void AttackUnitGeneral(int[] xyPreviousCell, int[] xySelectedCell, bool isKilledAttacker, bool isKilledDefender)
    {
        //if (!isKilledAttacker && !isKilledDefender)
        //{
        //    _animationAttackUnitComponentRef.Unref().XYStartCell = xyPreviousCell;
        //    _animationAttackUnitComponentRef.Unref().XYEndCell = xySelectedCell;

        //    _systemsGeneralManager.ActiveRunSystem(true, SystemGeneralTypes.Update, nameof(AnimationAttackUnitSystem));
        //}
    }

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


    #region ProtectUnit

    public void ProtectUnit(in int[] xyCell) => _photonView.RPC("ProtectUnitMaster", RpcTarget.MasterClient, xyCell);

    [PunRPC]
    private void ProtectUnitMaster(int[] xyCell, PhotonMessageInfo info)
    {
        _protecterUnitMasterComponentRef.Unref().ProtectUnit(xyCell, info.Sender);
        RefreshAll();
    }

    #endregion


    #region RelaxUnit

    public void RelaxUnit(in int[] xyCell) => _photonView.RPC("RelaxUnitMaster", RpcTarget.MasterClient, xyCell);

    [PunRPC]
    private void RelaxUnitMaster(int[] xyCell, PhotonMessageInfo info)
    {
        if (CellUnitComponent(xyCell).MinAmountSteps)
        {
            CellUnitComponent(xyCell).IsRelaxed = true;
            CellUnitComponent(xyCell).IsProtected = false;
            CellUnitComponent(xyCell).AmountSteps -= _startValues.AMOUNT_FOR_TAKE_UNIT;
        }

        RefreshAll();
    }

    #endregion


    #region Build

    internal void Build(int[] xyCell, BuildingTypes buildingType) => _photonView.RPC("BuildMaster", RpcTarget.MasterClient, xyCell, buildingType);

    [PunRPC]
    private void BuildMaster(int[] xyCell, BuildingTypes buildingType, PhotonMessageInfo info)
    {
        _builderCellMasterComponentRef.Unref().Build(xyCell, buildingType, info.Sender);

        RefreshAll();
    }

    #endregion


    #region Destroy

    internal void Destroy(int[] xyCell) => _photonView.RPC("DestroyToMaster", RpcTarget.MasterClient, xyCell);

    [PunRPC]
    private void DestroyToMaster(int[] xyCell, PhotonMessageInfo info)
    {
        if (CellUnitComponent(xyCell).IsHim(info.Sender))
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

    internal void BuyUnit(in UnitTypes unitType) => _photonView.RPC("BuyUnitMaster", RpcTarget.MasterClient, unitType);

    [PunRPC]
    private void BuyUnitMaster(UnitTypes unitType, PhotonMessageInfo info)
    {
        if (info.Sender.IsMasterClient)
        {
            if (_economyMasterComponentRef.Unref().FoodMaster >= _startValues.FOOD_FOR_BUYING_PAWN)
            {
                _economyMasterComponentRef.Unref().FoodMaster -= _startValues.FOOD_FOR_BUYING_PAWN;
                _economyUnitsMasterComponentRef.Unref().AmountUnitPawnMaster += _startValues.AMOUNT_FOR_TAKE_UNIT;
            }
        }
        else
        {
            if (_economyMasterComponentRef.Unref().FoodOther >= _startValues.FOOD_FOR_BUYING_PAWN)
            {
                _economyMasterComponentRef.Unref().FoodOther -= _startValues.FOOD_FOR_BUYING_PAWN;
                _economyUnitsMasterComponentRef.Unref().AmountUnitPawnOther += _startValues.AMOUNT_FOR_TAKE_UNIT;
            }
        }

        RefreshAll();
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