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
    private EntitiesGeneralManager _eGM = default;
    private EntitiesMasterManager _eMM = default;

    private SystemsGeneralManager _sGM = default;
    private SystemsMasterManager _sMM = default;


    #region ComponetsRef

    #region General

    private EcsComponentRef<TheEndGameComponent> _theEndComponentRef = default;
    private EcsComponentRef<StartGameComponent> _startGameComponentRef = default;
    private EcsComponentRef<AnimationAttackUnitComponent> _animationAttackUnitComponentRef = default;
    private EcsComponentRef<SoundComponent> _soundComponentRef = default;

    #endregion

    #endregion


    private StartValuesGameConfig StartValuesGameConfig => InstanceGame.StartValuesGameConfig;
    private CellManager CellManager => InstanceGame.CellManager;

    internal PhotonView PhotonView = default;
    internal string NameRPC => nameof(RPC); 

    internal void Constructor(PhotonView photonView)
    {
        PhotonView = photonView;

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
    }

    internal void InitAfterECS(ECSmanager eCSmanager)
    {
        if (InstanceGame.IsMasterClient)
        {
            _sMM = eCSmanager.SystemsMasterManager;
            _eMM = eCSmanager.EntitiesMasterManager;


             var entitiesMasterManager = eCSmanager.EntitiesMasterManager;
        }

        _eGM = eCSmanager.EntitiesGeneralManager;
        _sGM = eCSmanager.SystemsGeneralManager;


        _theEndComponentRef = _eGM.TheEndGameComponentRef;
        _startGameComponentRef = _eGM.StartGameComponentRef;
        _animationAttackUnitComponentRef = _eGM.AnimationAttackUnitComponentRef;
        _soundComponentRef = _eGM.SoundComponentRef;



        RefreshAllToMaster();
    }


    #region Mistake

    internal void MistakeEconomy(Player player, bool haveFood, bool haveWood, bool haveOre, bool haveIron, bool haveGold)
        => PhotonView.RPC(nameof(MistakeEconomyGeneral), player, haveFood, haveWood, haveOre, haveIron, haveGold);

    [PunRPC]
    private void MistakeEconomyGeneral(bool haveFood, bool haveWood, bool haveOre, bool haveIron, bool haveGold)
    {
        if (!haveFood) _eGM.FoodEntityMistakeComponent.MistakeAction();
        if (!haveWood) _eGM.WoodEntityMistakeComponent.MistakeAction();
        if (!haveOre) _eGM.OreEntityMistakeComponent.MistakeAction();
        if (!haveIron) _eGM.IronEntityMistakeComponent.MistakeAction();
        if (!haveGold) _eGM.GoldEntityMistakeComponent.MistakeAction();

        if (!haveFood || !haveWood || !haveOre || !haveIron || !haveGold)
            _soundComponentRef.Unref().MistakeSoundAction.Invoke();
    }


    private void MistakeUnitToGeneral(Player player) => PhotonView.RPC(nameof(MistakeUnitGeneral), player);
    [PunRPC]
    private void MistakeUnitGeneral()
    {
        _eGM.DonerEntityMistakeComponent.MistakeAction.Invoke();
    }


    #endregion


    #region THE END OF GAME

    internal void EndGame(int actorNumberWinner) => PhotonView.RPC("EndGameToMaster", RpcTarget.MasterClient, actorNumberWinner);

    [PunRPC]
    private void EndGameToMaster(int actorNumberWinner, PhotonMessageInfo info)
    {
        PhotonView.RPC("EndGameToGeneral", RpcTarget.All, actorNumberWinner);

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

    public void ReadyToMaster(in bool isReady) => PhotonView.RPC(nameof(ReadyToMaster), RpcTarget.MasterClient, isReady);

    [PunRPC]
    private void ReadyMaster(bool isReady, PhotonMessageInfo info)
    {
       _eGM.ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary[info.Sender.IsMasterClient] = isReady;

        if (_eGM.ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true]
            && _eGM.ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false])
            PhotonView.RPC(nameof(ReadyGeneral), RpcTarget.All, true, true);

        else PhotonView.RPC(nameof(ReadyGeneral), info.Sender, isReady, false);

        RefreshAllToMaster();
    }

    [PunRPC]
    private void ReadyGeneral(bool isReady, bool isStarted)
    {
        _eGM.ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary[InstanceGame.IsMasterClient] = isReady;
        _startGameComponentRef.Unref().IsStartedGame = isStarted;
    }


    #endregion


    #region Done

    internal void DoneToMaster(bool isDone) => PhotonView.RPC(nameof(DoneMaster), RpcTarget.MasterClient, isDone);

    [PunRPC]
    private void DoneMaster(bool isDone, PhotonMessageInfo info)
    {
        if (_eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[info.Sender.IsMasterClient])
        {
            PhotonView.RPC(nameof(DoneGeneralOne), info.Sender, isDone);


            _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[info.Sender.IsMasterClient] = isDone;

            bool isRefreshed = InstanceGame.StartValuesGameConfig.IS_TEST 
                || _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true] 
                && _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false];

            if (isRefreshed)
            {
                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.SoloSystems);

                PhotonView.RPC(nameof(DoneGeneralOne), RpcTarget.All, false);
                PhotonView.RPC(nameof(DoneGeneralTwo), RpcTarget.All, _eGM.UpdatorEntityAmountComponent.Amount);
            }
        }
        else
        {
            MistakeUnitToGeneral(info.Sender);
        }

        RefreshAllToMaster();
    }

    [PunRPC]
    private void DoneGeneralOne(bool isDone) 
        => _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[InstanceGame.IsMasterClient] = isDone;

    [PunRPC]
    private void DoneGeneralTwo(int numberMotion)
    {
        _eGM.UpdatorEntityAmountComponent.Amount = numberMotion;
        _eGM.UpdatorEntityActiveComponent.IsActive = true;
    }

    #endregion


    #region Refresh

    internal void RefreshAllToMaster()
    {
        PhotonView.RPC(nameof(RefreshCellsMaster), RpcTarget.MasterClient);
        PhotonView.RPC(nameof(RefreshEconomyMaster), RpcTarget.MasterClient);
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

        PhotonView.RPC(nameof(RefreshCellsGeneral), RpcTarget.Others, objects);


        objects = new object[]
        {
            _eGM.GoldEAmountDictC.AmountDict[false],
            _eGM.FoodEAmountDictC.AmountDict[false],
            _eGM.WoodEAmountDictC.AmountDict[false],
            _eGM.OreEAmountDictC.AmountDict[false],
            _eGM.IronEAmountDictC.AmountDict[false],
            _eGM.InfoEntBuildingsInfoCom.IsBuildedCityDictionary[false],
            _eGM.InfoEntBuildingsInfoCom.XYsettedCityDictionary[false],
            _eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[false],
        };
        PhotonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);

        objects = new object[]
{
            _eGM.IronEAmountDictC.AmountDict[true],
            _eGM.FoodEAmountDictC.AmountDict[true],
            _eGM.WoodEAmountDictC.AmountDict[true],
            _eGM.OreEAmountDictC.AmountDict[true],
            _eGM.IronEAmountDictC.AmountDict[true],
            _eGM.InfoEntBuildingsInfoCom.IsBuildedCityDictionary[true],
            _eGM.InfoEntBuildingsInfoCom.XYsettedCityDictionary[true],
            _eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[true],
};
        PhotonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.MasterClient, objects);

        #endregion

    }

    [PunRPC]
    private void RefreshEconomyMaster()
    {
        var objects = new object[]
        {
            _eGM.GoldEAmountDictC.AmountDict[false],
            _eGM.FoodEAmountDictC.AmountDict[false],
            _eGM.WoodEAmountDictC.AmountDict[false],
            _eGM.OreEAmountDictC.AmountDict[false],
            _eGM.IronEAmountDictC.AmountDict[false],
            _eGM.InfoEntBuildingsInfoCom.IsBuildedCityDictionary[false],
            _eGM.InfoEntBuildingsInfoCom.XYsettedCityDictionary[false],
            _eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[false],
        };
        PhotonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);
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


        _eGM.FoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] = food;
        _eGM.WoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] = wood;
        _eGM.OreEAmountDictC.AmountDict[InstanceGame.IsMasterClient] = ore;
        _eGM.IronEAmountDictC.AmountDict[InstanceGame.IsMasterClient] = iron;
        _eGM.GoldEAmountDictC.AmountDict[InstanceGame.IsMasterClient] = gold;

        _eGM.InfoEntBuildingsInfoCom.IsBuildedCityDictionary[InstanceGame.IsMasterClient] = isSettedCity;
        _eGM.InfoEntBuildingsInfoCom.XYsettedCityDictionary[InstanceGame.IsMasterClient] = xySettedCity;

        _eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[InstanceGame.IsMasterClient] = isSettedKing;
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