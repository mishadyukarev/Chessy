using ExitGames.Client.Photon;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static MainGame;

internal partial class PhotonPunRPC : MonoBehaviour
{
    private PhotonView _photonView = default;
    private SystemsGeneralManager _systemGeneralManager = default;
    private SystemsMasterManager _systemsMasterManager = default;

    private EntitiesGeneralManager _entitiesGeneralManager = default;


    #region ComponetsRef

    #region Master

    private EcsComponentRef<MotionComponent> _refresherMasterComponentRef = default;
    private EcsComponentRef<ReadyMasterComponent> _readyMasterComponentRef = default;
    private EcsComponentRef<FromInfoComponent> _fromInfoComponentRef = default;

    #endregion


    #region General

    private EcsComponentRef<CellComponent>[,] _cellComponentRef = default;
    private EcsComponentRef<CellComponent.EnvironmentComponent>[,] _cellEnvironmentComponentRef = default;
    private EcsComponentRef<CellComponent.SupportVisionComponent>[,] _cellSupportVisionComponentRef = default;
    private EcsComponentRef<CellComponent.UnitComponent>[,] _cellUnitComponentRef = default;
    private EcsComponentRef<CellComponent.BuildingComponent>[,] _cellBuildingComponentRef = default;

    private ref EconomyComponent EconomyComponent => ref _entitiesGeneralManager.EconomyEntity.Get<EconomyComponent>();
    private ref UnitsInfoComponent UnitsInfoComponent => ref _entitiesGeneralManager.EconomyEntity.Get<UnitsInfoComponent>();
    private ref BuildingsInfoComponent BuildingsInfoComponent => ref _entitiesGeneralManager.EconomyEntity.Get<BuildingsInfoComponent>();

    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;
    private EcsComponentRef<DonerComponent> _donerComponentRef = default;
    private EcsComponentRef<SelectedUnitComponent> _selectedUnitComponentRef = default;
    private EcsComponentRef<ReadyComponent> _readyComponentRef = default;
    private EcsComponentRef<TheEndGameComponent> _theEndComponentRef = default;
    private EcsComponentRef<StartGameComponent> _startGameComponentRef = default;
    private EcsComponentRef<AnimationAttackUnitComponent> _animationAttackUnitComponentRef = default;
    private EcsComponentRef<InfoRefreshComponent> _infoRefreshComponentRef = default;
    private EcsComponentRef<InfoMotionComponent> _infoMotionComponentRef = default;
    private EcsComponentRef<SoundComponent> _soundComponentRef = default;
    private ref EconomyUIComponent _economyUIComponentRef => ref _entitiesGeneralManager.EconomyUIComponent;

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

            _refresherMasterComponentRef = entitiesMasterManager.RefresherMasterComponentRef;
            _readyMasterComponentRef = entitiesMasterManager.ReadyMasterComponentRef;
            _fromInfoComponentRef = entitiesMasterManager.FromInfoComponentRef;
        }

        _entitiesGeneralManager = eCSmanager.EntitiesGeneralManager;
        _systemGeneralManager = eCSmanager.SystemsGeneralManager;


        _cellComponentRef = _entitiesGeneralManager.CellComponentRef;
        _cellUnitComponentRef = _entitiesGeneralManager.CellUnitComponentRef;
        _cellEnvironmentComponentRef = _entitiesGeneralManager.CellEnvironmentComponentRef;
        _cellSupportVisionComponentRef = _entitiesGeneralManager.CellSupportVisionComponentRef;
        _cellBuildingComponentRef = _entitiesGeneralManager.CellBuildingComponentRef;

        _selectorComponentRef = _entitiesGeneralManager.SelectorComponentRef;
        _donerComponentRef = _entitiesGeneralManager.DonerComponentRef;
        _selectedUnitComponentRef = _entitiesGeneralManager.SelectedUnitComponentRef;
        _readyComponentRef = _entitiesGeneralManager.ReadyComponentRef;
        _theEndComponentRef = _entitiesGeneralManager.TheEndGameComponentRef;
        _startGameComponentRef = _entitiesGeneralManager.StartGameComponentRef;
        _animationAttackUnitComponentRef = _entitiesGeneralManager.AnimationAttackUnitComponentRef;
        _infoRefreshComponentRef = _entitiesGeneralManager.RefreshComponentRef;
        _infoMotionComponentRef = _entitiesGeneralManager.InfoMotionComponentRef;
        _soundComponentRef = _entitiesGeneralManager.SoundComponentRef;
        _economyUIComponentRef = _entitiesGeneralManager.EconomyUIComponent;



        RefreshAllToMaster();
    }


    #region Mistake

    private void MistakeEconomy(Player player, bool haveFood, bool haveWood, bool haveOre, bool haveIron, bool haveGold)
        => _photonView.RPC(nameof(MistakeEconomyGeneral), player, haveFood, haveWood, haveOre, haveIron, haveGold);

    [PunRPC]
    private void MistakeEconomyGeneral(bool haveFood, bool haveWood, bool haveOre, bool haveIron, bool haveGold)
    {
        _economyUIComponentRef.NeedFood = !haveFood;
        _economyUIComponentRef.NeedWood = !haveWood;
        _economyUIComponentRef.NeedOre = !haveOre;
        _economyUIComponentRef.NeedIron = !haveIron;
        _economyUIComponentRef.NeedGold = !haveGold;

        if (!haveFood || !haveWood || !haveOre || !haveIron || !haveGold)
            _soundComponentRef.Unref().MistakeSoundAction.Invoke();
    }


    private void MistakeUnitToGeneral(Player player) => _photonView.RPC(nameof(MistakeUnitGeneral), player);
    [PunRPC]
    private void MistakeUnitGeneral()
    {
        _donerComponentRef.Unref().NeedSetKing = true;
    }


    #endregion


    #region THE END OF GAME

    internal void EndGame(int actorNumberWinner) => _photonView.RPC("EndGameToMaster", RpcTarget.MasterClient, actorNumberWinner);

    [PunRPC]
    private void EndGameToMaster(int actorNumberWinner, PhotonMessageInfo info)
    {
        _photonView.RPC("EndGameToGeneral", RpcTarget.All, actorNumberWinner);

        RefreshAllToMaster();
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

        RefreshAllToMaster();
    }

    [PunRPC]
    private void ReadyToGeneral(bool isReady, bool isStarted)
    {
        _readyComponentRef.Unref().IsReady = isReady;
        _startGameComponentRef.Unref().IsStartedGame = isStarted;
    }


    #endregion


    #region Done

    internal void DoneToMaster(bool isDone) => _photonView.RPC(nameof(DoneMaster), RpcTarget.MasterClient, isDone);

    [PunRPC]
    private void DoneMaster(bool isDone, PhotonMessageInfo info)
    {
        if (info.Sender.IsMasterClient && UnitsInfoComponent.IsSettedKingMaster
            || !info.Sender.IsMasterClient && UnitsInfoComponent.IsSettedKingOther)
        {
            _photonView.RPC(nameof(DoneGeneralOne), info.Sender, isDone);


            if (info.Sender.IsMasterClient) _donerComponentRef.Unref().IsDoneMaster = isDone;
            else _donerComponentRef.Unref().IsDoneOther = isDone;

            bool isRefreshed = InstanceGame.StartValuesGameConfig.IS_TEST 
                || _donerComponentRef.Unref().IsDoneMaster && _donerComponentRef.Unref().IsDoneOther;

            if (isRefreshed)
            {
                _systemsMasterManager.TryInvokeRunSystem(nameof(RefreshMasterSystem), _systemsMasterManager.SoloSystems);

                _photonView.RPC(nameof(DoneGeneralOne), RpcTarget.All, false);
                _photonView.RPC(nameof(DoneGeneralTwo), RpcTarget.All, _refresherMasterComponentRef.Unref().NumberMotion);
            }
        }
        else
        {
            MistakeUnitToGeneral(info.Sender);
        }

        RefreshAllToMaster();
    }

    [PunRPC]
    private void DoneGeneralOne(bool isDone) =>_donerComponentRef.Unref().IsCurrentDone = isDone;

    [PunRPC]
    private void DoneGeneralTwo(int numberMotion)
    {
        _infoMotionComponentRef.Unref().NumberMotion = numberMotion;
        _infoRefreshComponentRef.Unref().IsRefreshed = true;
    }

    #endregion


    #region Refresh

    internal void RefreshAllToMaster()
    {
        _photonView.RPC(nameof(RefreshCellsMaster), RpcTarget.MasterClient);
        _photonView.RPC(nameof(RefreshEconomyMaster), RpcTarget.MasterClient);
    }

    [PunRPC]
    private void RefreshCellsMaster()
    {
        _systemsMasterManager.TryInvokeRunSystem(nameof(VisibilityUnitsMasterSystem), _systemsMasterManager.SoloSystems);

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
                listObjects.Add(CellUnitComponent(x, y).IsProtected);
                listObjects.Add(CellUnitComponent(x, y).IsRelaxed);

                listObjects.Add(CellUnitComponent(x, y).AmountUpgradePawnMaster);
                listObjects.Add(CellUnitComponent(x, y).AmountUpgradeRookMaster);
                listObjects.Add(CellUnitComponent(x, y).AmountUpgradeBishopMaster);
                listObjects.Add(CellUnitComponent(x, y).AmountUpgradePawnOther);
                listObjects.Add(CellUnitComponent(x, y).AmountUpgradeRookOther);
                listObjects.Add(CellUnitComponent(x, y).AmountUpgradeBishopOther);

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
            EconomyComponent.GoldOther,
            EconomyComponent.FoodOther,
            EconomyComponent.WoodOther,
            EconomyComponent.OreOther,
            EconomyComponent.IronOther,
            BuildingsInfoComponent.IsBuildedCityOther,
            BuildingsInfoComponent.XYsettedCityOther,
            UnitsInfoComponent.IsSettedKingOther,
        };
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);

        #endregion

    }

    [PunRPC]
    private void RefreshEconomyMaster()
    {
        var objects = new object[]
        {
            EconomyComponent.GoldOther,
            EconomyComponent.FoodOther,
            EconomyComponent.WoodOther,
            EconomyComponent.OreOther,
            EconomyComponent.IronOther,
            BuildingsInfoComponent.IsBuildedCityOther,
            BuildingsInfoComponent.XYsettedCityOther,
            UnitsInfoComponent.IsSettedKingOther,
        };
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);
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
                bool isProtected = (bool)objects[i++];
                bool isRelaxed = (bool)objects[i++];

                CellUnitComponent(x, y).AmountUpgradePawnMaster = (int)objects[i++];
                CellUnitComponent(x, y).AmountUpgradeRookMaster = (int)objects[i++];
                CellUnitComponent(x, y).AmountUpgradeBishopMaster = (int)objects[i++];
                CellUnitComponent(x, y).AmountUpgradePawnOther = (int)objects[i++];
                CellUnitComponent(x, y).AmountUpgradeRookOther = (int)objects[i++];
                CellUnitComponent(x, y).AmountUpgradeBishopOther = (int)objects[i++];

                CellEnvironmentComponent(x, y).SetResetEnvironment((bool)objects[i++], EnvironmentTypes.Food);
                CellEnvironmentComponent(x, y).SetResetEnvironment((bool)objects[i++], EnvironmentTypes.Tree);
                CellEnvironmentComponent(x, y).SetResetEnvironment((bool)objects[i++], EnvironmentTypes.Hill);
                CellEnvironmentComponent(x, y).SetResetEnvironment((bool)objects[i++], EnvironmentTypes.Mountain);



                BuildingTypes buildingType = (BuildingTypes)objects[i++];
                int actorNumberBuilding = (int)objects[i++];


                Player player;
                if (actorNumber == -1) player = default;
                else player = PhotonNetwork.PlayerList[actorNumber - 1];



                CellUnitComponent(x, y).SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player);
                CellUnitComponent(x, y).ActiveVisionCell(isActiveUnit, unitType);



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


        EconomyComponent.CurrentGold = gold;
        EconomyComponent.CurrentFood = food;
        EconomyComponent.CurrentWood = wood;
        EconomyComponent.CurrentOre = ore;
        EconomyComponent.CurrentIron = iron;

        BuildingsInfoComponent.IsSettedCurrentCity = isSettedCity;
        BuildingsInfoComponent.XYCurrentCity = xySettedCity;

        UnitsInfoComponent.IsSettedCurrentKing = isSettedKing;
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