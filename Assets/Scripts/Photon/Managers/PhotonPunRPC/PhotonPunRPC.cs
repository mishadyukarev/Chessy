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

    private EntitiesGeneralManager _eGM = default;

    private SystemsGeneralManager _sGM = default;
    private SystemsMasterManager _sMM = default;


    #region ComponetsRef

    #region Master

    private EcsComponentRef<ReadyMasterComponent> _readyMasterComponentRef = default;
    private EcsComponentRef<FromInfoComponent> _fromInfoComponentRef = default;

    #endregion


    #region General

    private EcsComponentRef<ReadyComponent> _readyComponentRef = default;
    private EcsComponentRef<TheEndGameComponent> _theEndComponentRef = default;
    private EcsComponentRef<StartGameComponent> _startGameComponentRef = default;
    private EcsComponentRef<AnimationAttackUnitComponent> _animationAttackUnitComponentRef = default;
    private EcsComponentRef<SoundComponent> _soundComponentRef = default;
    private ref EconomyUIComponent _economyUIComponentRef => ref _eGM.EconomyUIComponent;

    #endregion

    #endregion


    private StartValuesGameConfig StartValuesGameConfig => InstanceGame.StartValuesGameConfig;
    private CellBaseOperations CellManager => InstanceGame.CellManager.CellBaseOperations;



    internal void Constructor(PhotonView photonView)
    {
        _photonView = photonView;

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
    }

    internal void InitAfterECS(ECSmanager eCSmanager)
    {
        if (InstanceGame.IsMasterClient)
        {
            _sMM = eCSmanager.SystemsMasterManager;


            var entitiesMasterManager = eCSmanager.EntitiesMasterManager;

            _readyMasterComponentRef = entitiesMasterManager.ReadyMasterComponentRef;
            _fromInfoComponentRef = entitiesMasterManager.FromInfoComponentRef;
        }

        _eGM = eCSmanager.EntitiesGeneralManager;
        _sGM = eCSmanager.SystemsGeneralManager;


        _readyComponentRef = _eGM.ReadyComponentRef;
        _theEndComponentRef = _eGM.TheEndGameComponentRef;
        _startGameComponentRef = _eGM.StartGameComponentRef;
        _animationAttackUnitComponentRef = _eGM.AnimationAttackUnitComponentRef;
        _soundComponentRef = _eGM.SoundComponentRef;
        _economyUIComponentRef = _eGM.EconomyUIComponent;



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
        _eGM.DonerComponent.NeedSetKing = true;
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
        if (info.Sender.IsMasterClient && _eGM.UnitsInfoComponent.IsSettedKingMaster
            || !info.Sender.IsMasterClient && _eGM.UnitsInfoComponent.IsSettedKingOther)
        {
            _photonView.RPC(nameof(DoneGeneralOne), info.Sender, isDone);


            if (info.Sender.IsMasterClient) _eGM.DonerComponent.IsDoneMaster = isDone;
            else _eGM.DonerComponent.IsDoneOther = isDone;

            bool isRefreshed = InstanceGame.StartValuesGameConfig.IS_TEST 
                || _eGM.DonerComponent.IsDoneMaster && _eGM.DonerComponent.IsDoneOther;

            if (isRefreshed)
            {
                _sMM.TryInvokeRunSystem(nameof(RefreshMasterSystem), _sMM.SoloSystems);

                _photonView.RPC(nameof(DoneGeneralOne), RpcTarget.All, false);
                _photonView.RPC(nameof(DoneGeneralTwo), RpcTarget.All, _eGM.RefreshComponent.NumberMotion);
            }
        }
        else
        {
            MistakeUnitToGeneral(info.Sender);
        }

        RefreshAllToMaster();
    }

    [PunRPC]
    private void DoneGeneralOne(bool isDone) => _eGM.DonerComponent.IsCurrentDone = isDone;

    [PunRPC]
    private void DoneGeneralTwo(int numberMotion)
    {
        _eGM.RefreshComponent.NumberMotion = numberMotion;
        _eGM.RefreshComponent.IsRefreshed = true;
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
        _sMM.TryInvokeRunSystem(nameof(VisibilityUnitsMasterSystem), _sMM.SoloSystems);

        #region Sending

        List<object> listObjects = new List<object>();
        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                listObjects.Add(_eGM.CellUnitComponent(x, y).IsActiveUnitOther);
                listObjects.Add(_eGM.CellUnitComponent(x, y).UnitType);
                listObjects.Add(_eGM.CellUnitComponent(x, y).ActorNumber);
                listObjects.Add(_eGM.CellUnitComponent(x, y).AmountSteps);
                listObjects.Add(_eGM.CellUnitComponent(x, y).AmountHealth);
                listObjects.Add(_eGM.CellUnitComponent(x, y).IsProtected);
                listObjects.Add(_eGM.CellUnitComponent(x, y).IsRelaxed);

                listObjects.Add(_eGM.CellUnitComponent(x, y).AmountUpgradePawnMaster);
                listObjects.Add(_eGM.CellUnitComponent(x, y).AmountUpgradeRookMaster);
                listObjects.Add(_eGM.CellUnitComponent(x, y).AmountUpgradeBishopMaster);
                listObjects.Add(_eGM.CellUnitComponent(x, y).AmountUpgradePawnOther);
                listObjects.Add(_eGM.CellUnitComponent(x, y).AmountUpgradeRookOther);
                listObjects.Add(_eGM.CellUnitComponent(x, y).AmountUpgradeBishopOther);

                listObjects.Add(_eGM.CellEnvironmentComponent(x, y).HaveFood);
                listObjects.Add(_eGM.CellEnvironmentComponent(x, y).HaveTree);
                listObjects.Add(_eGM.CellEnvironmentComponent(x, y).HaveHill);
                listObjects.Add(_eGM.CellEnvironmentComponent(x, y).HaveMountain);

                listObjects.Add(_eGM.CellBuildingComponent(x, y).BuildingType);
                listObjects.Add(_eGM.CellBuildingComponent(x, y).ActorNumber);
            }
        }
        object[] objects = new object[listObjects.Count];
        for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];

        _photonView.RPC(nameof(RefreshCellsGeneral), RpcTarget.Others, objects);


        objects = new object[]
        {
            _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.BuildingsInfoComponent.IsBuildedCityOther,
            _eGM.BuildingsInfoComponent.XYsettedCityOther,
            _eGM.UnitsInfoComponent.IsSettedKingOther,
        };
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);

        objects = new object[]
{
            _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[true],
            _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[true],
            _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[true],
            _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[true],
            _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[true],
            _eGM.BuildingsInfoComponent.IsBuildedCityMaster,
            _eGM.BuildingsInfoComponent.XYsettedCityMaster,
            _eGM.UnitsInfoComponent.IsSettedKingMaster,
};
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.MasterClient, objects);

        #endregion

    }

    [PunRPC]
    private void RefreshEconomyMaster()
    {
        var objects = new object[]
        {
            _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[false],
            _eGM.BuildingsInfoComponent.IsBuildedCityOther,
            _eGM.BuildingsInfoComponent.XYsettedCityOther,
            _eGM.UnitsInfoComponent.IsSettedKingOther,
        };
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);
    }

    [PunRPC]
    private void RefreshCellsGeneral(object[] objects)
    {
        int i = 0;
        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                bool isActiveUnit = (bool)objects[i++];
                UnitTypes unitType = (UnitTypes)objects[i++];
                int actorNumber = (int)objects[i++];
                int amountSteps = (int)objects[i++];
                int amountHealth = (int)objects[i++];
                bool isProtected = (bool)objects[i++];
                bool isRelaxed = (bool)objects[i++];

                _eGM.CellUnitComponent(x, y).AmountUpgradePawnMaster = (int)objects[i++];
                _eGM.CellUnitComponent(x, y).AmountUpgradeRookMaster = (int)objects[i++];
                _eGM.CellUnitComponent(x, y).AmountUpgradeBishopMaster = (int)objects[i++];
                _eGM.CellUnitComponent(x, y).AmountUpgradePawnOther = (int)objects[i++];
                _eGM.CellUnitComponent(x, y).AmountUpgradeRookOther = (int)objects[i++];
                _eGM.CellUnitComponent(x, y).AmountUpgradeBishopOther = (int)objects[i++];

                _eGM.CellEnvironmentComponent(x, y).SetResetEnvironment((bool)objects[i++], EnvironmentTypes.Food);
                _eGM.CellEnvironmentComponent(x, y).SetResetEnvironment((bool)objects[i++], EnvironmentTypes.Tree);
                _eGM.CellEnvironmentComponent(x, y).SetResetEnvironment((bool)objects[i++], EnvironmentTypes.Hill);
                _eGM.CellEnvironmentComponent(x, y).SetResetEnvironment((bool)objects[i++], EnvironmentTypes.Mountain);



                BuildingTypes buildingType = (BuildingTypes)objects[i++];
                int actorNumberBuilding = (int)objects[i++];


                Player player;
                if (actorNumber == -1) player = default;
                else player = PhotonNetwork.PlayerList[actorNumber - 1];



                _eGM.CellUnitComponent(x, y).SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player);
                _eGM.CellUnitComponent(x, y).ActiveVisionCell(isActiveUnit, unitType);



                if (actorNumberBuilding == -1) player = default;
                else player = PhotonNetwork.PlayerList[actorNumberBuilding - 1];
                _eGM.CellBuildingComponent(x, y).SetBuilding(buildingType, player);
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


        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] = food;
        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] = wood;
        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] = ore;
        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] = iron;
        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] = gold;

        _eGM.BuildingsInfoComponent.IsSettedCurrentCity = isSettedCity;
        _eGM.BuildingsInfoComponent.XYCurrentCity = xySettedCity;

        _eGM.UnitsInfoComponent.IsSettedCurrentKing = isSettedKing;
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