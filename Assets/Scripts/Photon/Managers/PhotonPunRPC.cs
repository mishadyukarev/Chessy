using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static MainGame;

internal partial class PhotonPunRPC : MonoBehaviour
{
    private EntitiesGeneralManager _eGM;
    private EntitiesMasterManager _eMM;

    private SystemsGeneralManager _sGM;
    private SystemsMasterManager _sMM;

    private PhotonView _photonView;

    private StartValuesGameConfig StartValuesGameConfig => Instance.StartValuesGameConfig;
    private string NameRPC => nameof(RPC);


    internal void Constructor(PhotonView photonView)
    {
        _photonView = photonView;

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
    }

    internal void InitAfterECS(ECSmanager eCSmanager)
    {
        if (Instance.IsMasterClient)
        {
            _sMM = eCSmanager.SystemsMasterManager;
            _eMM = eCSmanager.EntitiesMasterManager;
        }

        _eGM = eCSmanager.EntitiesGeneralManager;
        _sGM = eCSmanager.SystemsGeneralManager;

        RefreshAllToMaster();
    }


    #region PUN

    internal void ReadyToMaster(in bool isReady) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Ready, new object[] { isReady });
    internal void ReadyToGeneral(Player playerTo, bool isAttacked, bool isActivatedSound) => _photonView.RPC(NameRPC, playerTo, false, RpcTypes.Ready, new object[] { isAttacked, isActivatedSound });
    internal void ReadyToGeneral(RpcTarget rpcTarget, bool isReady, bool isStarted) => _photonView.RPC(NameRPC, rpcTarget, false, RpcTypes.Ready, new object[] { isReady, isStarted });

    internal void DoneToMaster(bool isDone) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Done, new object[] { isDone });
    internal void DoneToGeneral(Player playerTo, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(NameRPC, playerTo, false, RpcTypes.Done, new object[] { isRefreshed, isDone, numberMotion });
    internal void DoneToGeneral(RpcTarget rpcTarget, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(NameRPC, rpcTarget, false, RpcTypes.Done, new object[] { isRefreshed, isDone, numberMotion });

    internal void TruceToMaster(bool isTruce) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Truce, new object[] { isTruce });
    internal void TruceToGeneral(Player playerTo, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(NameRPC, playerTo, false, RpcTypes.Truce, new object[] { isRefreshed, isDone, numberMotion });
    internal void TruceToGeneral(RpcTarget rpcTarget, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(NameRPC, rpcTarget, false, RpcTypes.Truce, new object[] { isRefreshed, isDone, numberMotion });

    internal void ShiftUnitToMaster(in int[] xyPreviousCell, in int[] xySelectedCell) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Shift, new object[] { xyPreviousCell, xySelectedCell });
    internal void AttackUnitToMaster(int[] xyPreviousCell, int[] xySelectedCell) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Attack, new object[] { xyPreviousCell, xySelectedCell });
    internal void AttackUnitToGeneral(Player playerTo, bool isAttacked, bool isActivatedSound) => _photonView.RPC(NameRPC, playerTo, false, RpcTypes.Attack, new object[] { isAttacked, isActivatedSound });
    internal void AttackUnitToGeneral(RpcTarget rpcTarget, bool isAttacked, bool isActivatedSound) => _photonView.RPC(NameRPC, rpcTarget, false, RpcTypes.Attack, new object[] { isAttacked, isActivatedSound });

    internal void BuildToMaster(int[] xyCell, BuildingTypes buildingType) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Build, new object[] { xyCell, buildingType });
    internal void DestroyBuildingToMaster(int[] xyCell) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Destroy, new object[] { xyCell });

    internal void RelaxUnitToMaster(bool isActive, int[] xyCell) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Relax, new object[] { isActive, xyCell });
    public void ProtectUnitToMaster(bool isActive, in int[] xyCell) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.Protect, new object[] { isActive, xyCell });

    internal void EndGameToMaster(int actorNumberWinner) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.EndGame, new object[] { actorNumberWinner });
    internal void EndGameToGeneral(RpcTarget rpcTarget, int actorNumberWinner) => _photonView.RPC(NameRPC, rpcTarget, false, RpcTypes.EndGame, new object[] { actorNumberWinner });

    internal void MistakeEconomyToGeneral(Player playerTo, bool haveFood, bool haveWood, bool haveOre, bool haveIron, bool haveGold) => _photonView.RPC(NameRPC, playerTo, false, RpcTypes.Mistake, new object[] { MistakeTypes.EconomyType, haveFood, haveWood, haveOre, haveIron, haveGold });
    internal void MistakeUnitToGeneral(Player playerTo) => _photonView.RPC(NameRPC, playerTo, false, RpcTypes.Mistake, new object[] { MistakeTypes.UnitType });

    public void UniqueAbilityPawnToMaster(int[] xy, UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.UniquePawnAbility, new object[] { xy, uniqueAbilitiesPawnType });

    internal void CreateUnitToMaster(UnitTypes unitType) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.CreateUnit, new object[] { unitType });
    internal void UpgradeUnitToMaster(UnitTypes unitType) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.UpgradeUnit, new object[] { unitType });
    internal void UpgradeBuildingToMaster(BuildingTypes buildingType) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.UpgradeBuilding, new object[] { buildingType });

    internal void MeltOreToMaster() => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.MeltOre, new object[] { });

    internal void GetUnitToMaster(UnitTypes unitType) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.GetUnit, new object[] { unitType });
    internal void GetUnitToGeneral(Player playerTo, bool isGetted, UnitTypes unitType) => _photonView.RPC(NameRPC, playerTo, false, RpcTypes.GetUnit, new object[] { isGetted, unitType });


    internal void SetUniToMaster(in int[] xyCell, UnitTypes unitType) => _photonView.RPC(NameRPC, RpcTarget.MasterClient, true, RpcTypes.SetUnit, new object[] { xyCell, unitType });
    internal void SetUniToGeneral(Player playerTo, bool isSetted) => _photonView.RPC(NameRPC, playerTo, false, RpcTypes.SetUnit, new object[] { isSetted });


    [PunRPC]
    private void RPC(bool isToMaster, RpcTypes rPCType, object[] objects, PhotonMessageInfo info)
    {
        _eGM.RpcGeneralEnt_FromInfoCom.FromInfo = info;

        if (isToMaster)
        {
            switch (rPCType)
            {
                case RpcTypes.None:
                    break;

                case RpcTypes.Ready:
                    _eGM.RpcGeneralEnt_FromInfoCom.IsActived = (bool)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(ReadyMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Done:
                    _eGM.RpcGeneralEnt_FromInfoCom.IsActived = (bool)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(DonerMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Truce:

                    TruceToGeneral(info.Sender, false, (bool)objects[0], _eGM.UpdatorEntityAmountComponent.Amount);

                    _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[info.Sender.IsMasterClient] = (bool)objects[0];

                    bool isTruce = Instance.StartValuesGameConfig.IS_TEST ||
                    _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[true]
                        && _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[false];

                    if (isTruce)
                    {
                        _eGM.UpdatorEntityAmountComponent.Amount += UnityEngine.Random.Range(4500, 5500);
                        TruceToGeneral(RpcTarget.All, true, false, _eGM.UpdatorEntityAmountComponent.Amount);
                    }
                    break;

                case RpcTypes.EndGame:
                    EndGameToGeneral(RpcTarget.All, (int)objects[0]);
                    break;

                case RpcTypes.Build:
                    _eMM.RPCMasterEnt_RPCMasterCom.XyCell = (int[])objects[0];
                    _eMM.RPCMasterEnt_RPCMasterCom.BuildingType = (BuildingTypes)objects[1];
                    _sMM.TryInvokeRunSystem(nameof(BuilderMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Destroy:
                    _eMM.RPCMasterEnt_RPCMasterCom.XyCell = (int[])objects[0];
                    _sMM.TryInvokeRunSystem(nameof(DestroyMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Shift:
                    _eMM.RPCMasterEnt_RPCMasterCom.XyPrevious = (int[])objects[0];
                    _eMM.RPCMasterEnt_RPCMasterCom.XySelected = (int[])objects[1];
                    _sMM.TryInvokeRunSystem(nameof(ShiftUnitMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Attack:
                    _eMM.RPCMasterEnt_RPCMasterCom.XyPrevious = (int[])objects[0];
                    _eMM.RPCMasterEnt_RPCMasterCom.XySelected = (int[])objects[1];
                    _sMM.TryInvokeRunSystem(nameof(AttackUnitMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Protect:
                    _eGM.RpcGeneralEnt_FromInfoCom.IsActived = (bool)objects[0];
                    _eMM.RPCMasterEnt_RPCMasterCom.XyCell = (int[])objects[1];
                    _sMM.TryInvokeRunSystem(nameof(ProtectMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Relax:
                    _eGM.RpcGeneralEnt_FromInfoCom.IsActived = (bool)objects[0];
                    _eMM.RPCMasterEnt_RPCMasterCom.XyCell = (int[])objects[1];
                    _sMM.TryInvokeRunSystem(nameof(RelaxMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.CreateUnit:
                    _eMM.RPCMasterEnt_RPCMasterCom.UnitType = (UnitTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(CreatorUnitMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.UpgradeUnit:
                    _eMM.RPCMasterEnt_RPCMasterCom.UnitType = (UnitTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(UpgradeUnitMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.MeltOre:
                    _sMM.TryInvokeRunSystem(nameof(MeltOreMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.GetUnit:
                    _eMM.RPCMasterEnt_RPCMasterCom.UnitType = (UnitTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(GetterUnitMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.SetUnit:
                    _eMM.RPCMasterEnt_RPCMasterCom.XyCell = (int[])objects[0];
                    _eMM.RPCMasterEnt_RPCMasterCom.UnitType = (UnitTypes)objects[1];
                    _sMM.TryInvokeRunSystem(nameof(SetterUnitMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.UniquePawnAbility:
                    _eMM.RPCMasterEnt_RPCMasterCom.XyCell = (int[])objects[0];
                    _eMM.RPCMasterEnt_RPCMasterCom.UniqueAbilitiesPawnType = (UniqueAbilitiesPawnTypes)objects[1];
                    _sMM.TryInvokeRunSystem(nameof(UniquePawnAbilityMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.UpgradeBuilding:
                    _eMM.RPCMasterEnt_RPCMasterCom.BuildingType = (BuildingTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(UpgradeBuildingMasterSystem), _sMM.RPCSystems);
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
                    _eGM.ReadyEntIsActivatedDictCom.IsActivatedDictionary[Instance.IsMasterClient] = isReady;
                    _eGM.ReadyEntStartGameCom.IsStartedGame = isStarted;
                    break;

                case RpcTypes.Done:
                    _eGM.UpdatorEntityActiveComponent.IsActived = (bool)objects[0];
                    _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[Instance.IsMasterClient] = (bool)objects[1];
                    _eGM.UpdatorEntityAmountComponent.Amount = (int)objects[2];
                    break;

                case RpcTypes.Truce:
                    _eGM.UpdatorEntityActiveComponent.IsActived = (bool)objects[0];
                    _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[Instance.IsMasterClient] = (bool)objects[1];
                    _eGM.UpdatorEntityAmountComponent.Amount = (int)objects[2];
                    break;

                case RpcTypes.EndGame:
                    _eGM.EndGameEntEndGameCom.IsEndGame = true;
                    _eGM.EndGameEntEndGameCom.PlayerWinner = PhotonNetwork.PlayerList[(int)objects[0] - 1];
                    break;

                case RpcTypes.Attack:
                    if ((bool)objects[0]) _eGM.SelectorEntSelectorCom.AttackUnitAction();
                    if ((bool)objects[1]) _eGM.SoundEntSoundCom.AttackSoundAction();
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
                                _eGM.SoundEntSoundCom.MistakeSoundAction.Invoke();
                            break;

                        case MistakeTypes.UnitType:
                            _eGM.DonerEntityMistakeComponent.MistakeAction.Invoke();
                            break;

                        default:
                            break;
                    }

                    break;

                case RpcTypes.GetUnit:
                    if ((bool)objects[0]) _eGM.SelectedUnitEntUnitTypeCom.UnitType = (UnitTypes)objects[1];
                    break;

                case RpcTypes.SetUnit:
                    if ((bool)objects[0]) _eGM.SelectorEntSelectorCom.SetterUnitDelegate();
                    break;

                default:
                    break;
            }
        }
    }

    #endregion



    #region Refresh

    internal void RefreshAllToMaster() => _photonView.RPC(nameof(RefreshAllMaster), RpcTarget.MasterClient);

    [PunRPC]
    private void RefreshAllMaster()
    {
        _sMM.TryInvokeRunSystem(nameof(VisibilityUnitsMasterSystem), _sMM.RPCSystems);

        #region Sending

        List<object> listObjects = new List<object>();
        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).GetActiveUnit(false));
                listObjects.Add(_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType);
                listObjects.Add(_eGM.CellUnitEnt_OwnerCom(x, y).ActorNumber);
                listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).AmountSteps);
                listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth);
                listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).IsProtected);
                listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).IsRelaxed);

                listObjects.Add(_eGM.CellEnvEnt_CellEnvironmentCom(x, y).HaveFood);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvironmentCom(x, y).HaveTree);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvironmentCom(x, y).HaveHill);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvironmentCom(x, y).HaveMountain);

                listObjects.Add(_eGM.CellBuildingEnt_BuildingTypeCom(x, y).BuildingType);
                listObjects.Add(_eGM.CellBuildingEnt_OwnerCom(x, y).ActorNumber);

                listObjects.Add(_eGM.CellEffectEnt_CellEffectCom(x, y).HaveFire);
            }
        }
        object[] objects = new object[listObjects.Count];
        for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];

        _photonView.RPC(nameof(RefreshCellsGeneral), RpcTarget.Others, objects);


        objects = new object[]
        {
            _eGM.InfoEnt_UpgradeCom.AmountUpgradePawnDict[false],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeRookDict[false],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeBishopDict[false],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeFarmDict[false],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeWoodcutterDict[false],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeMineDict[false],

            _eGM.FoodEnt_AmountDictCom.AmountDict[false],
            _eGM.WoodEAmountDictC.AmountDict[false],
            _eGM.OreEAmountDictC.AmountDict[false],
            _eGM.IronEAmountDictC.AmountDict[false],
            _eGM.GoldEAmountDictC.AmountDict[false],

            _eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[false],
        };
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);

        objects = new object[]
        {
            _eGM.InfoEnt_UpgradeCom.AmountUpgradePawnDict[true],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeRookDict[true],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeBishopDict[true],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeFarmDict[true],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeWoodcutterDict[true],
            _eGM.InfoEnt_UpgradeCom.AmountUpgradeMineDict[true],

            _eGM.FoodEnt_AmountDictCom.AmountDict[true],
            _eGM.WoodEAmountDictC.AmountDict[true],
            _eGM.OreEAmountDictC.AmountDict[true],
            _eGM.IronEAmountDictC.AmountDict[true],
            _eGM.GoldEAmountDictC.AmountDict[true],

            _eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[true],
        };
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.MasterClient, objects);

        #endregion

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

                bool haveFood = (bool)objects[i++];
                bool haveTree = (bool)objects[i++];
                bool haveHill = (bool)objects[i++];
                bool haveMountain = (bool)objects[i++];

                BuildingTypes buildingType = (BuildingTypes)objects[i++];
                int actorNumberBuilding = (int)objects[i++];

                bool haveFire = (bool)objects[i++];




                Player player;
                if (actorNumber == -1) player = default;
                else player = PhotonNetwork.PlayerList[actorNumber - 1];

                _eGM.CellUnitEnt_CellUnitCom(x, y).SetActiveUnit(Instance.IsMasterClient, isActiveUnit);
                _eGM.CellUnitEnt_CellUnitCom(x, y).SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player);
                _eGM.CellUnitEnt_CellUnitCom(x, y).ActiveVisionCell(isActiveUnit, unitType);


                _eGM.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(haveFood, EnvironmentTypes.Food);
                _eGM.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(haveTree, EnvironmentTypes.Tree);
                _eGM.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(haveHill, EnvironmentTypes.Hill);
                _eGM.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(haveMountain, EnvironmentTypes.Mountain);

                if (actorNumberBuilding == -1) player = default;
                else player = PhotonNetwork.PlayerList[actorNumberBuilding - 1];
                _eGM.CellBuildingEnt_CellBuildingCom(x, y).SetBuilding(buildingType, player);

                _eGM.CellEffectEnt_CellEffectCom(x, y).SetEffect(haveFire, EffectTypes.Fire);
            }
        }
    }

    [PunRPC]
    private void RefreshEconomyGeneral(object[] objects)
    {
        int i = 0;

        _eGM.InfoEnt_UpgradeCom.AmountUpgradePawnDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeCom.AmountUpgradeRookDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeCom.AmountUpgradeBishopDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeCom.AmountUpgradeFarmDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeCom.AmountUpgradeWoodcutterDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeCom.AmountUpgradeMineDict[Instance.IsMasterClient] = (int)objects[i++];

        var food = (int)objects[i++];
        var wood = (int)objects[i++];
        var ore = (int)objects[i++];
        var iron = (int)objects[i++];
        var gold = (int)objects[i++];

        bool isSettedKing = (bool)objects[i++];


        _eGM.FoodEnt_AmountDictCom.AmountDict[Instance.IsMasterClient] = food;
        _eGM.WoodEAmountDictC.AmountDict[Instance.IsMasterClient] = wood;
        _eGM.OreEAmountDictC.AmountDict[Instance.IsMasterClient] = ore;
        _eGM.IronEAmountDictC.AmountDict[Instance.IsMasterClient] = iron;
        _eGM.GoldEAmountDictC.AmountDict[Instance.IsMasterClient] = gold;

        _eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[Instance.IsMasterClient] = isSettedKing;
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