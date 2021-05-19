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


    private EcsComponentRef<TheEndGameComponent> _theEndComponentRef = default;
    private EcsComponentRef<StartGameComponent> _startGameComponentRef = default;
    private EcsComponentRef<AnimationAttackUnitComponent> _animationAttackUnitComponentRef = default;
    private EcsComponentRef<SoundComponent> _soundComponentRef = default;



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
        }

        _eGM = eCSmanager.EntitiesGeneralManager;
        _sGM = eCSmanager.SystemsGeneralManager;


        _theEndComponentRef = _eGM.TheEndGameComponentRef;
        _startGameComponentRef = _eGM.StartGameComponentRef;
        _animationAttackUnitComponentRef = _eGM.AnimationAttackUnitComponentRef;
        _soundComponentRef = _eGM.SoundComponentRef;



        RefreshAllToMaster();
    }


    #region PUN

    internal void ReadyToMaster(in bool isReady) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Ready, new object[] { isReady });
    internal void ReadyToGeneral(Player playerTo, bool isAttacked, bool isActivatedSound) => PhotonView.RPC(NameRPC, playerTo, false, RpcTypes.Ready, new object[] { isAttacked, isActivatedSound });
    internal void ReadyToGeneral(RpcTarget rpcTarget, bool isReady, bool isStarted) => PhotonView.RPC(NameRPC, rpcTarget, false, RpcTypes.Ready, new object[] { isReady, isStarted });

    internal void DoneToMaster(bool isDone) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Done, new object[] { isDone });
    internal void DoneToGeneral(Player playerTo, bool isRefreshed, bool isDone, int numberMotion) => PhotonView.RPC(NameRPC, playerTo, false, RpcTypes.Done, new object[] { isRefreshed, isDone, numberMotion });
    internal void DoneToGeneral(RpcTarget rpcTarget, bool isRefreshed, bool isDone, int numberMotion) => PhotonView.RPC(NameRPC, rpcTarget, false, RpcTypes.Done, new object[] { isRefreshed, isDone, numberMotion });

    internal void ShiftUnitToMaster(in int[] xyPreviousCell, in int[] xySelectedCell) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Shift, new object[] { xyPreviousCell, xySelectedCell });
    internal void AttackUnitToMaster(int[] xyPreviousCell, int[] xySelectedCell) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Attack, new object[] { xyPreviousCell, xySelectedCell });
    internal void AttackUnitToGeneral(Player playerTo, bool isAttacked, bool isActivatedSound) => PhotonView.RPC(NameRPC, playerTo, false, RpcTypes.Attack, new object[] { isAttacked, isActivatedSound });
    internal void AttackUnitToGeneral(RpcTarget rpcTarget, bool isAttacked, bool isActivatedSound) => PhotonView.RPC(NameRPC, rpcTarget, false, RpcTypes.Attack, new object[] { isAttacked, isActivatedSound });

    internal void BuildToMaster(int[] xyCell, BuildingTypes buildingType) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Build, new object[] { xyCell, buildingType });
    internal void DestroyBuildingToMaster(int[] xyCell) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Destroy, new object[] { xyCell });

    internal void RelaxUnitToMaster(bool isActive, int[] xyCell) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Relax, new object[] { isActive, xyCell });
    public void ProtectUnitToMaster(bool isActive, in int[] xyCell) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Protect, new object[] { isActive, xyCell });

    internal void EndGameToMaster(int actorNumberWinner) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.EndGame, new object[] { actorNumberWinner });
    internal void EndGameToGeneral(RpcTarget rpcTarget, int actorNumberWinner) => PhotonView.RPC(NameRPC, rpcTarget, false, RpcTypes.EndGame, new object[] { actorNumberWinner });

    internal void MistakeEconomyToGeneral(Player playerTo, bool haveFood, bool haveWood, bool haveOre, bool haveIron, bool haveGold) => PhotonView.RPC(NameRPC, playerTo, false, RpcTypes.Mistake, new object[] { MistakeTypes.EconomyType, haveFood, haveWood, haveOre, haveIron, haveGold });
    internal void MistakeUnitToGeneral(Player playerTo) => PhotonView.RPC(NameRPC, playerTo, false, RpcTypes.Mistake, new object[] { MistakeTypes.UnitType });

    ///public void UniqueAbilityPawnToMaster(int[] xy, UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, xy, uniqueAbilitiesPawnType);

    internal void CreateUnitToMaster(UnitTypes unitType) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.CreateUnit, new object[] { unitType });
    internal void UpgradeUnitToMaster(UnitTypes unitType) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.UpgradeUnit, new object[] { unitType });

    internal void MeltOreToMaster() => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.MeltOre, new object[] { });

    internal void GetUnitToMaster(UnitTypes unitType) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.GetUnit, new object[] { unitType });
    internal void GetUnitToGeneral(Player playerTo, bool isGetted, UnitTypes unitType) => PhotonView.RPC(NameRPC, playerTo, false, RpcTypes.GetUnit, new object[] { isGetted, unitType });


    internal void SetUniToMaster(in int[] xyCell, UnitTypes unitType) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.SetUnit, new object[] { xyCell, unitType });
    internal void SetUniToGeneral(Player playerTo, bool isSetted) => PhotonView.RPC(NameRPC, playerTo, false, RpcTypes.SetUnit, new object[] { isSetted });


    [PunRPC]
    private void RPC(bool isToMaster, RpcTypes rPCType, object[] objects, PhotonMessageInfo info)
    {
        _eGM.GeneralRPCEntFromInfoCom.FromInfo = info;

        if (isToMaster)
        {
            switch (rPCType)
            {
                case RpcTypes.None:
                    break;

                case RpcTypes.Ready:
                    _eGM.GeneralRPCEntActiveComponent.IsActived = (bool)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(ReadyMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.Done:
                    _eGM.GeneralRPCEntActiveComponent.IsActived = (bool)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(DonerMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.EndGame:
                    EndGameToGeneral(RpcTarget.All, (int)objects[0]);
                    break;

                case RpcTypes.Build:
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[0], _eMM.MasterRPCEntXyCellCom.XyCell);
                    _eMM.MasterRPCEntBuildingTypeCom.BuildingType = (BuildingTypes)objects[1];
                    _sMM.TryInvokeRunSystem(nameof(BuilderMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.Destroy:
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[0], _eMM.MasterRPCEntXyCellCom.XyCell);
                    _sMM.TryInvokeRunSystem(nameof(DestroyMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.Shift:
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[0], _eMM.MasterRPCEntXySelPreCom.XyPrevious);
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[1], _eMM.MasterRPCEntXySelPreCom.XySelected);
                    _sMM.TryInvokeRunSystem(nameof(ShiftUnitMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.Attack:
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[0], _eMM.MasterRPCEntXySelPreCom.XyPrevious);
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[1], _eMM.MasterRPCEntXySelPreCom.XySelected);
                    _sMM.TryInvokeRunSystem(nameof(AttackUnitMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.Protect:
                    _eGM.GeneralRPCEntActiveComponent.IsActived = (bool)objects[0];
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[1], _eMM.MasterRPCEntXyCellCom.XyCell);
                    _sMM.TryInvokeRunSystem(nameof(ProtectMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.Relax:
                    _eGM.GeneralRPCEntActiveComponent.IsActived = (bool)objects[0];
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[1], _eMM.MasterRPCEntXyCellCom.XyCell);
                    _sMM.TryInvokeRunSystem(nameof(RelaxMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.CreateUnit:
                    _eMM.MasterRPCEntUnitTypeCom.UnitType = (UnitTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(CreatorUnitMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.UpgradeUnit:
                    _eMM.MasterRPCEntUnitTypeCom.UnitType = (UnitTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(UpgradeUnitMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.MeltOre:
                    _sMM.TryInvokeRunSystem(nameof(MeltOreMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.GetUnit:
                    _eMM.MasterRPCEntUnitTypeCom.UnitType = (UnitTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(GetterUnitMasterSystem), _sMM.SoloSystems);
                    break;

                case RpcTypes.SetUnit:
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[0], _eMM.MasterRPCEntXyCellCom.XyCell);
                    _eMM.MasterRPCEntUnitTypeCom.UnitType = (UnitTypes)objects[1];
                    _sMM.TryInvokeRunSystem(nameof(SetterUnitMasterSystem), _sMM.SoloSystems);
                    break;

                default:
                    break;
            }
            RefreshAllToMaster();
        }
        else
        {
            switch (rPCType)
            {
                case RpcTypes.None:
                    break;

                case RpcTypes.Ready:
                    bool isReady = (bool)objects[0];
                    bool isStarted = (bool)objects[1];
                    _eGM.ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary[InstanceGame.IsMasterClient] = isReady;
                    _startGameComponentRef.Unref().IsStartedGame = isStarted;
                    break;

                case RpcTypes.Done:
                    _eGM.UpdatorEntityActiveComponent.IsActived = (bool)objects[0];
                    _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[InstanceGame.IsMasterClient] = (bool)objects[1];
                    _eGM.UpdatorEntityAmountComponent.Amount = (int)objects[2];
                    break;

                case RpcTypes.EndGame:
                    _theEndComponentRef.Unref().IsEndGame = true;
                    _theEndComponentRef.Unref().PlayerWinner = PhotonNetwork.PlayerList[(int)objects[0] - 1];
                    break;

                case RpcTypes.Attack:
                    if ((bool)objects[0])
                        _eGM.SelectorESelectorC.AttackUnitAction();
                    if ((bool)objects[1])
                        _soundComponentRef.Unref().AttackSoundAction();

                    break;

                case RpcTypes.Mistake:

                    switch ((MistakeTypes)objects[0])
                    {
                        case MistakeTypes.EconomyType:
                            var haveFood = (bool)objects[1];
                            var haveWood = (bool)objects[2];
                            var haveOre = (bool)objects[3];
                            var haveIron = (bool)objects[4];
                            var haveGold = (bool)objects[5];

                            if (!haveFood) _eGM.FoodEntityMistakeComponent.MistakeAction();
                            if (!haveWood) _eGM.WoodEntityMistakeComponent.MistakeAction();
                            if (!haveOre) _eGM.OreEntityMistakeComponent.MistakeAction();
                            if (!haveIron) _eGM.IronEntityMistakeComponent.MistakeAction();
                            if (!haveGold) _eGM.GoldEntityMistakeComponent.MistakeAction();

                            if (!haveFood || !haveWood || !haveOre || !haveIron || !haveGold)
                                _soundComponentRef.Unref().MistakeSoundAction.Invoke();
                            break;

                        case MistakeTypes.UnitType:
                            _eGM.DonerEntityMistakeComponent.MistakeAction.Invoke();
                            break;

                        default:
                            break;
                    }

                    break;

                case RpcTypes.GetUnit:
                    if ((bool)objects[0]) _eGM.SelectedUnitEUnitTypeC.UnitType = (UnitTypes)objects[1];
                    break;

                case RpcTypes.SetUnit:
                    if ((bool)objects[0]) _eGM.SelectorESelectorC.SetterUnitDelegate();
                    break;

                default:
                    break;
            }
        }
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