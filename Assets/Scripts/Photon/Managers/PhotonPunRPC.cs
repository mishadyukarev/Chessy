using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using TMPro;
using UnityEngine;
using static Main;

public class PhotonPunRPC : MonoBehaviour
{
    private PhotonView _photonView = default;
    private StartValuesConfig _startValues;

    private EcsComponentRef<SetterUnitMasterComponent> _setterUnitMasterComponentRef = default;
    private EcsComponentRef<ShiftUnitMasterComponent> _shiftUnitMasterComponentRef = default;
    private EcsComponentRef<RefresherMasterComponent> _refresherMasterComponentRef = default;
    private EcsComponentRef<AttackUnitMasterComponent> _attackUnitMasterComponentRef = default;
    private EcsComponentRef<GetterUnitMasterComponent> _getterUnitMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponentRef = default;
    private EcsComponentRef<BuilderCellMasterComponent> _builderCellMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> _economyUnitsMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> _economyBuildingsMasterComponentRef = default;


    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;
    private EcsComponentRef<ButtonComponent> _buttonComponentRef = default;
    private EcsComponentRef<EconomyComponent> _economyComponentRef = default;
    private EcsComponentRef<EconomyComponent.BuildingsComponent> _economyBuildingsComponentRef = default;

    private EcsComponentRef<CellComponent>[,] _cellComponentRef = default;
    private EcsComponentRef<CellComponent.EnvironmentComponent>[,] _cellEnvironmentComponentRef = default;
    private EcsComponentRef<CellComponent.SupportVisionComponent>[,] _cellSupportVisionComponentRef = default;
    private EcsComponentRef<CellComponent.UnitComponent>[,] _cellUnitComponentRef = default;
    private EcsComponentRef<CellComponent.BuildingComponent>[,] _cellBuildingComponentRef = default;

    private ref CellComponent CellComponent(params int[] xy)
        => ref _cellComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();
    private ref CellComponent.UnitComponent CellUnitComponent(params int[] xy)
        => ref _cellUnitComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();
    private ref CellComponent.EnvironmentComponent CellEnvironmentComponent(params int[] xy)
        => ref _cellEnvironmentComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();
    private ref CellComponent.SupportVisionComponent CellSupportVisionComponent(params int[] xy)
        => ref _cellSupportVisionComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();
    private ref CellComponent.BuildingComponent  CellBuildingComponent(params int[] xy)
        => ref _cellBuildingComponentRef[xy[_startValues.X], xy[_startValues.Y]].Unref();



    public void Constructor(SupportManager supportManager, PhotonManager photonManager)
    {
        _startValues = supportManager.StartValuesConfig;
        _photonView = photonManager.PhotonView;
        _startValues = supportManager.StartValuesConfig;

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
    }

    public void InitAfterECS(ECSmanager eCSmanager)
    {
        if (Instance.IsMasterClient)
        {
            var entitiesMasterManager = eCSmanager.EntitiesMasterManager;

            _setterUnitMasterComponentRef = entitiesMasterManager.SetterUnitMasterComponentRef;
            _shiftUnitMasterComponentRef = entitiesMasterManager.ShiftUnitComponentRef;
            _refresherMasterComponentRef = entitiesMasterManager.RefresherMasterComponentRef;
            _attackUnitMasterComponentRef = entitiesMasterManager.AttackUnitMasterComponentRef;
            _getterUnitMasterComponentRef = entitiesMasterManager.GetterUnitMasterComponentRef;
            _economyMasterComponentRef = entitiesMasterManager.EconomyMasterComponentRef;
            _builderCellMasterComponentRef = entitiesMasterManager.BuilderCellMasterComponentRef;
            _economyUnitsMasterComponentRef = entitiesMasterManager.EconomyUnitsMasterComponentRef;
            _economyBuildingsMasterComponentRef = entitiesMasterManager.EconomyBuildingsMasterComponentRef;
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
        _buttonComponentRef = entitiesGeneralManager.ButtonComponentRef;




        RefreshAll();
    }


    #region Done

    public void Done(in bool isDone) => _photonView.RPC("DoneToMaster", RpcTarget.MasterClient, isDone);

    [PunRPC]
    private void DoneToMaster(bool isDone, PhotonMessageInfo info)
    {
        _photonView.RPC("DoneToGeneral", info.Sender, isDone);

        if (_refresherMasterComponentRef.Unref().TryRefreshed(info.Sender, isDone))
        {
            _photonView.RPC("DoneToGeneral", RpcTarget.All, false);
            _refresherMasterComponentRef.Unref().ResetValues();
        }

        RefreshAll();
    }

    [PunRPC] private void DoneToGeneral(bool isDone) => _buttonComponentRef.Unref().DonerDelegate(false, isDone);

    #endregion


    #region GetUnit

    internal void GetUnit(UnitTypes unitTypes) => _photonView.RPC("GetUnitToMaster", RpcTarget.MasterClient, unitTypes);

    [PunRPC]
    private void GetUnitToMaster(UnitTypes unitType, PhotonMessageInfo info)
    {
        var isGetted = _getterUnitMasterComponentRef.Unref().TryGetUnit(unitType, info.Sender);
        _photonView.RPC("GetUnitToGeneral", info.Sender, isGetted);

        RefreshAll();
    }

    [PunRPC] private void GetUnitToGeneral(bool isGetted) => _buttonComponentRef.Unref().Button1Delegate(false, isGetted);

    #endregion


    #region SetUnit

    internal void SetUnit(in int[] xyCell, UnitTypes unitType) => _photonView.RPC("SetUnitToMaster", RpcTarget.MasterClient, xyCell, unitType);

    [PunRPC]
    private void SetUnitToMaster(int[] xyCell, UnitTypes unitType, PhotonMessageInfo info)
    {
        bool isSetted = _setterUnitMasterComponentRef.Unref().TrySetUnit(xyCell, unitType, info.Sender);
        _photonView.RPC("SetUnitToGeneral", info.Sender, isSetted);

        RefreshAll();
    }

    [PunRPC] private void SetUnitToGeneral(bool isSetted) => _selectorComponentRef.Unref().SetterUnitDelegate(isSetted);

    #endregion


    #region AttackUnit

    internal void AttackUnit(int[] xyPreviousCell, int[] xySelectedCell)
        => _photonView.RPC("AttackUnitMaster", RpcTarget.MasterClient, xyPreviousCell, xySelectedCell);

    [PunRPC]
    private void AttackUnitMaster(int[] xyPreviousCell, int[] xySelectedCell, PhotonMessageInfo info)
    {
        _attackUnitMasterComponentRef.Unref().AttackUnit(xyPreviousCell, xySelectedCell, info.Sender);
        var attacked = true;
        _photonView.RPC("AttackUnitGeneral", info.Sender, attacked);

        RefreshAll();
    }

    [PunRPC] private void AttackUnitGeneral(bool isAttacked) => _selectorComponentRef.Unref().AttackUnitDelegate(isAttacked);

    #endregion


    #region ShiftUnit

    public void ShiftUnit(in int[] xyPreviousCell, in int[] xySelectedCell)
        => _photonView.RPC("ShiftUnitMaster", RpcTarget.MasterClient, xyPreviousCell, xySelectedCell);

    [PunRPC]
    private void ShiftUnitMaster(int[] xyPreviousCell, int[] xySelectedCell, PhotonMessageInfo info)
    {
        bool isShifted = _shiftUnitMasterComponentRef.Unref().CanShiftUnit(xyPreviousCell, xySelectedCell, info.Sender);
        _photonView.RPC("ShiftUnitGeneral", info.Sender, isShifted);

        RefreshAll();
    }

    [PunRPC] private void ShiftUnitGeneral(bool isShifted) => _selectorComponentRef.Unref().ShiftUnitDelegate(isShifted);

    #endregion


    #region ProtectUnit

    public void ProtectUnit(in int[] xyCell) => _photonView.RPC("ProtectUnitMaster", RpcTarget.MasterClient, xyCell);

    [PunRPC]
    private void ProtectUnitMaster(int[] xyCell, PhotonMessageInfo info)
    {
        if (CellUnitComponent(xyCell).HaveAmountSteps)
        {
            CellUnitComponent(xyCell).IsProtected = true;
            CellUnitComponent(xyCell).IsRelaxed = false;
            CellUnitComponent(xyCell).TakeAmountSteps(_startValues.TakeAmountSteps);
        }

        RefreshAll();
    }

    #endregion


    #region RelaxUnit

    public void RelaxUnit(in int[] xyCell) => _photonView.RPC("RelaxUnitMaster", RpcTarget.MasterClient, xyCell);

    [PunRPC]
    private void RelaxUnitMaster(int[] xyCell, PhotonMessageInfo info)
    {
        if (CellUnitComponent(xyCell).HaveAmountSteps)
        {
            CellUnitComponent(xyCell).IsRelaxed = true;
            CellUnitComponent(xyCell).IsProtected = false;
            CellUnitComponent(xyCell).TakeAmountSteps(_startValues.TakeAmountSteps);
        }

        RefreshAll();
    }

    #endregion


    #region Build

    public void Build(int[] xyCell, BuildingTypes buildingType) => _photonView.RPC("BuildMaster", RpcTarget.MasterClient, xyCell, buildingType);

    [PunRPC]
    private void BuildMaster(int[] xyCell, BuildingTypes buildingType, PhotonMessageInfo info)
    {
        var isSetted = _builderCellMasterComponentRef.Unref().Build(xyCell, buildingType, info.Sender);

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
            if(_economyMasterComponentRef.Unref().GoldMaster >= _startValues.GoldForBuyingPawn)
            {
                _economyMasterComponentRef.Unref().TakeGoldMaster(_startValues.GoldForBuyingPawn);
                _economyUnitsMasterComponentRef.Unref().AddAmountUnitPawnMaster(_startValues.AmountForTakeUnit);
            }
        }
        else
        {
            if(_economyMasterComponentRef.Unref().GoldOther >= _startValues.GoldForBuyingPawn)
            {
                _economyMasterComponentRef.Unref().TakeGoldOther(_startValues.GoldForBuyingPawn);
                _economyUnitsMasterComponentRef.Unref().AddAmountUnitPawnOther(_startValues.AmountForTakeUnit);
            }
        }

        RefreshAll();
    }

    #endregion


    #region RefreshAll

    internal void RefreshAll() => _photonView.RPC("RefreshAllMaster", RpcTarget.MasterClient);

    [PunRPC]
    private void RefreshAllMaster()
    {
        int countSync = 11;
        object[] objects = new object[_startValues.CellCountX * _startValues.CellCountY * countSync];
        int i = 0;
        for (int x = 0; x < _startValues.CellCountX; x++)
        {
            for (int y = 0; y < _startValues.CellCountY; y++)
            {
                objects[i++] = CellUnitComponent(x, y).UnitType;
                objects[i++] = CellUnitComponent(x, y).ActorNumber;
                objects[i++] = CellUnitComponent(x, y).AmountSteps;
                objects[i++] = CellUnitComponent(x, y).AmountHealth;
                objects[i++] = CellUnitComponent(x, y).PowerDamage;
                objects[i++] = CellUnitComponent(x, y).IsProtected;
                objects[i++] = CellUnitComponent(x, y).IsRelaxed;

                objects[i++] = CellEnvironmentComponent(x, y).HaveTree;
                objects[i++] = CellEnvironmentComponent(x, y).HaveHill;
                objects[i++] = CellEnvironmentComponent(x, y).HaveMountain;

                objects[i++] = CellBuildingComponent(x, y).BuildingType;
            }
        }

        _photonView.RPC("RefreshCellsGeneral", RpcTarget.Others, objects);

        objects = new object[]
        {
            _economyMasterComponentRef.Unref().GoldOther,
            _economyBuildingsMasterComponentRef.Unref().IsBuildedCityOther,
            _economyBuildingsMasterComponentRef.Unref().XYsettedCityOther,
        };
        _photonView.RPC("RefreshEconomyGeneral", RpcTarget.Others, objects);

        objects = new object[]
        {
            _economyMasterComponentRef.Unref().GoldMaster,
            _economyBuildingsMasterComponentRef.Unref().IsBuildedCityMaster,
            _economyBuildingsMasterComponentRef.Unref().XYsettedCityMaster,
        };
        _photonView.RPC("RefreshEconomyGeneral", RpcTarget.MasterClient, objects);
    }

    [PunRPC]
    private void RefreshCellsGeneral(object[] objects)
    {
        int i = 0;
        for (int x = 0; x < _startValues.CellCountX; x++)
        {
            for (int y = 0; y < _startValues.CellCountY; y++)
            {
                UnitTypes unitType = (UnitTypes)objects[i++];
                int actorNumber = (int)objects[i++];
                int amountSteps = (int)objects[i++];
                int amountHealth = (int)objects[i++];
                int powerDamage = (int)objects[i++];
                bool isProtected = (bool)objects[i++];
                bool isRelaxed = (bool)objects[i++];

                bool haveTree = (bool)objects[i++];
                bool haveHill = (bool)objects[i++];
                bool haveMountain = (bool)objects[i++];

                BuildingTypes buildingType = (BuildingTypes)objects[i++];



                var player = PhotonNetwork.PlayerList[actorNumber - 1];
                CellUnitComponent(x, y).SetResetUnit(unitType, amountHealth, powerDamage, amountSteps, isProtected, isRelaxed,  player);

                CellEnvironmentComponent(x, y).SetResetEnvironment(haveTree, EnvironmentTypes.Tree);
                CellEnvironmentComponent(x, y).SetResetEnvironment(haveHill, EnvironmentTypes.Hill);
                CellEnvironmentComponent(x, y).SetResetEnvironment(haveMountain, EnvironmentTypes.Mountain);

                CellBuildingComponent(x, y).SetResetBuilding(buildingType);
            }
        }
    }

    [PunRPC]
    private void RefreshEconomyGeneral(object[] objects)
    {
        var gold = (int)objects[0];
        var isSettedCity = (bool)objects[1];
        int[] xySettedCity = (int[])objects[2];

        _economyComponentRef.Unref().Gold = gold;
        _economyBuildingsComponentRef.Unref().IsSettedCity = isSettedCity;
        _economyBuildingsComponentRef.Unref().XYsettedCity = xySettedCity;
    }


    #endregion


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
}