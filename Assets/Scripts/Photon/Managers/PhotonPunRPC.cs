using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Main;

internal sealed class PhotonPunRPC : MonoBehaviour
{
    private PhotonView _photonView;

    private EntitiesGeneralManager _eGM;
    private EntitiesMasterManager _eMM;

    private SystemsGeneralManager _sGM;
    private SystemsMasterManager _sMM;

    private CellManager _cM;
    private EconomyManager _eM;

    private string MasterRPCName => nameof(MasterRPC);
    private string GeneralRPCName => nameof(GeneralRPC);

    private int _i;

    internal void Constructor(PhotonView photonView)
    {
        _photonView = photonView;

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
    }

    internal void InitAfterECS(ECSmanagerGame eCSmanager, CellManager cellManager, EconomyManager economyManager)
    {
        _sMM = eCSmanager.SystemsMasterManager;
        _eMM = eCSmanager.EntitiesMasterManager;

        _eGM = eCSmanager.EntitiesGeneralManager;
        _sGM = eCSmanager.SystemsGeneralManager;

        _cM = cellManager;
        _eM = economyManager;

        RefreshAllToMaster();
    }


    #region PUN

    internal void ReadyToMaster(in bool isReady) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Ready, new object[] { isReady });
    internal void ReadyToGeneral(Player playerTo, bool isCurrentReady, bool isRefreshed) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Ready, new object[] { isCurrentReady, isRefreshed });
    internal void ReadyToGeneral(RpcTarget rpcTarget, bool isCurrentReady, bool isRefreshed) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.Ready, new object[] { isCurrentReady, isRefreshed });

    internal void DoneToMaster(bool isDone) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Done, new object[] { isDone });
    internal void DoneToGeneral(Player playerTo, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Done, new object[] { isRefreshed, isDone, numberMotion });
    internal void DoneToGeneral(RpcTarget rpcTarget, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.Done, new object[] { isRefreshed, isDone, numberMotion });

    internal void TruceToMaster(bool isTruce) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Truce, new object[] { isTruce });
    internal void TruceToGeneral(Player playerTo, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Truce, new object[] { isRefreshed, isDone, numberMotion });
    internal void TruceToGeneral(RpcTarget rpcTarget, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.Truce, new object[] { isRefreshed, isDone, numberMotion });

    internal void ShiftUnitToMaster(in int[] xyPreviousCell, in int[] xySelectedCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Shift, new object[] { xyPreviousCell, xySelectedCell });
    internal void AttackUnitToMaster(int[] xyPreviousCell, int[] xySelectedCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Attack, new object[] { xyPreviousCell, xySelectedCell });
    internal void AttackUnitToGeneral(Player playerTo, bool isAttacked, bool isActivatedSound) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Attack, new object[] { isAttacked, isActivatedSound });
    internal void AttackUnitToGeneral(RpcTarget rpcTarget, bool isAttacked, bool isActivatedSound, int[] xyStart, int[] xyEnd) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.Attack, new object[] { isAttacked, isActivatedSound, xyStart, xyEnd });

    internal void BuildToMaster(int[] xyCell, BuildingTypes buildingType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Build, new object[] { xyCell, buildingType });
    internal void DestroyBuildingToMaster(int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Destroy, new object[] { xyCell });

    internal void RelaxUnitToMaster(bool isActive, int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Relax, new object[] { isActive, xyCell });
    public void ProtectUnitToMaster(bool isActive, in int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Protect, new object[] { isActive, xyCell });

    internal void EndGameToMaster(int actorNumberWinner) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.EndGame, new object[] { actorNumberWinner });
    internal void EndGameToGeneral(RpcTarget rpcTarget, int actorNumberWinner) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.EndGame, new object[] { actorNumberWinner });

    internal void MistakeEconomyToGeneral(Player playerTo, params bool[] haves) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Mistake, new object[] { MistakeTypes.EconomyType, haves });
    internal void MistakeUnitToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Mistake, new object[] { MistakeTypes.UnitType });

    public void UniqueAbilityPawnToMaster(int[] xy, UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.UniquePawnAbility, new object[] { xy, uniqueAbilitiesPawnType });

    internal void CreateUnitToMaster(UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.CreateUnit, new object[] { unitType });
    internal void UpgradeUnitToMaster(UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.UpgradeUnit, new object[] { unitType });
    internal void UpgradeBuildingToMaster(BuildingTypes buildingType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.UpgradeBuilding, new object[] { buildingType });

    internal void MeltOreToMaster() => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.MeltOre, new object[] { });

    internal void GetUnitToMaster(UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.GetUnit, new object[] { unitType });
    internal void GetUnitToGeneral(Player playerTo, bool isGetted, UnitTypes unitType) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.GetUnit, new object[] { isGetted, unitType });


    internal void SetUniToMaster(int[] xyCell, UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.SetUnit, new object[] { xyCell, unitType });
    internal void SetUniToGeneral(Player playerTo, bool isSetted) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.SetUnit, new object[] { isSetted });


    [PunRPC]
    private void MasterRPC(RpcTypes rPCType, object[] objects, PhotonMessageInfo info)
    {
        _eGM.RpcGeneralEnt_FromInfoCom.FromInfo = info;

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
                _eGM.RpcGeneralEnt_FromInfoCom.IsActived = (bool)objects[0];
                _sMM.TryInvokeRunSystem(nameof(TruceMasterSystem), _sMM.RPCSystems);

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

    [PunRPC]
    private void GeneralRPC(RpcTypes rPCType, object[] objects, PhotonMessageInfo info)
    {
        _i = 0;
        _eGM.RpcGeneralEnt_FromInfoCom.FromInfo = info;

        switch (rPCType)
        {
            case RpcTypes.None:
                break;

            case RpcTypes.Ready:
                bool isActivated = (bool)objects[_i++];
                bool isSkipped = (bool)objects[_i++];
                _eGM.ReadyEnt_ReadyCom.IsActivatedDictionary[Instance.IsMasterClient] = isActivated;
                _eGM.ReadyEnt_ReadyCom.IsSkipped = isSkipped;
                break;

            case RpcTypes.Done:
                _eGM.InfoEnt_UpdatorCom.IsUpdated = (bool)objects[0];
                _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[Instance.IsMasterClient] = (bool)objects[1];
                _eGM.InfoEnt_UpdatorCom.AmountMotions = (int)objects[2];
                break;

            case RpcTypes.Truce:
                _eGM.InfoEnt_UpdatorCom.IsUpdated = (bool)objects[_i++];
                _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[Instance.IsMasterClient] = (bool)objects[_i++];
                _eGM.InfoEnt_UpdatorCom.AmountMotions = (int)objects[_i++];
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
                        var haves = (bool[])objects[1];
                        var haveFood = haves[0];
                        var haveWood = haves[1];
                        var haveOre = haves[2];
                        var haveIron = haves[3];
                        var haveGold = haves[4];

                        if (!haveFood) _eGM.MistakeEnt_MistakeEconomyCom.FoodMistake();
                        if (!haveWood) _eGM.MistakeEnt_MistakeEconomyCom.WoodMistake();
                        if (!haveOre) _eGM.MistakeEnt_MistakeEconomyCom.OreMistake();
                        if (!haveIron) _eGM.MistakeEnt_MistakeEconomyCom.IronMistake();
                        if (!haveGold) _eGM.MistakeEnt_MistakeEconomyCom.GoldMistake();

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
                if ((bool)objects[0]) _eGM.SelectorEnt_UnitTypeCom.UnitType = (UnitTypes)objects[1];
                break;

            case RpcTypes.SetUnit:
                if ((bool)objects[0]) _eGM.SelectorEntSelectorCom.SetterUnitDelegate();
                break;

            default:
                break;
        }

        RefreshAllToMaster();
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
        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                listObjects.Add(_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit);
                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                {
                    listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[false]);
                    listObjects.Add(_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType);
                    listObjects.Add(_eGM.CellUnitEnt_CellOwnerCom(x, y).ActorNumber);
                    listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).AmountSteps);
                    listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth);
                    listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).IsProtected);
                    listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).IsRelaxed);
                }


                listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountFertilizerResources);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountOreResources);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizer);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveYoungTree);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveHill);
                listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveMountain);

                listObjects.Add(_eGM.CellBuildingEnt_BuildingTypeCom(x, y).HaveBuilding);
                if (_eGM.CellBuildingEnt_BuildingTypeCom(x, y).HaveBuilding)
                {
                    listObjects.Add(_eGM.CellBuildingEnt_BuildingTypeCom(x, y).BuildingType);
                    listObjects.Add(_eGM.CellBuildingEnt_OwnerCom(x, y).ActorNumber);
                }


                listObjects.Add(_eGM.CellEffectEnt_CellEffectCom(x, y).HaveFire);
            }
        }
        object[] objects = new object[listObjects.Count];
        for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];

        _photonView.RPC(nameof(RefreshCellsGeneral), RpcTarget.Others, objects);


        objects = new object[]
        {
            _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradePawnDict[false],
            _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeRookDict[false],
            _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeBishopDict[false],
            _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict[false],
            _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict[false],
            _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict[false],

            _eGM.EconomyEnt_EconomyCom.Food(false),
            _eGM.EconomyEnt_EconomyCom.Wood(false),
            _eGM.EconomyEnt_EconomyCom.Ore(false),
            _eGM.EconomyEnt_EconomyCom.Iron(false),
            _eGM.EconomyEnt_EconomyCom.Gold(false),

            _eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[false],
            _eGM.InfoEnt_BuildingsInfoCom.IsSettedCityDict[false],
            _eGM.InfoEnt_BuildingsInfoCom.XySettedCityDict[false],
        };
        _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);

        #endregion

    }

    [PunRPC]
    private void RefreshCellsGeneral(object[] objects)
    {
        int i = 0;
        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                Player player;

                bool haveUnit = (bool)objects[i++];
                if (haveUnit)
                {
                    bool isActiveUnit = (bool)objects[i++];
                    UnitTypes unitType = (UnitTypes)objects[i++];
                    int actorNumber = (int)objects[i++];
                    int amountSteps = (int)objects[i++];
                    int amountHealth = (int)objects[i++];
                    bool isProtected = (bool)objects[i++];
                    bool isRelaxed = (bool)objects[i++];


                    player = PhotonNetwork.PlayerList[actorNumber - 1];

                    _eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[Instance.IsMasterClient] = isActiveUnit;
                    _cM.CellUnitWorker.SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player, x, y);
                    _eGM.CellUnitEnt_CellUnitCom(x, y).EnableSR(isActiveUnit, unitType);
                }
                else
                {
                    _cM.CellUnitWorker.ResetUnit(x, y);
                }

                int amountResourcesFertilizer = (int)objects[i++];
                int amountResourcesForest = (int)objects[i++];
                int oreResources = (int)objects[i++];
                bool haveFertilizer = (bool)objects[i++];
                bool haveAdultForest = (bool)objects[i++];
                bool haveYoungTree = (bool)objects[i++];
                bool haveHill = (bool)objects[i++];
                bool haveMountain = (bool)objects[i++];

                if (haveFertilizer) _eGM.CellEnvEnt_CellEnvCom(x, y).SetEnvironment(EnvironmentTypes.Fertilizer, amountResourcesFertilizer);
                else _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Fertilizer);

                if (haveAdultForest) _eGM.CellEnvEnt_CellEnvCom(x, y).SetEnvironment(EnvironmentTypes.AdultForest, amountResourcesForest);
                else _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);

                if (haveYoungTree) _eGM.CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.YoungForest);
                else _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.YoungForest);

                if (haveHill) _eGM.CellEnvEnt_CellEnvCom(x, y).SetEnvironment(EnvironmentTypes.Hill, oreResources);
                else _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Hill);

                if (haveMountain) _eGM.CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Mountain);
                else _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Mountain);



                bool haveBuilding = (bool)objects[i++];
                if (haveBuilding)
                {
                    BuildingTypes buildingType = (BuildingTypes)objects[i++];
                    int actorNumberBuilding = (int)objects[i++];

                    player = PhotonNetwork.PlayerList[actorNumberBuilding - 1];

                    _cM.CellBuildingWorker.SetBuilding(buildingType, player, x, y);
                }
                else
                {
                    _cM.CellBuildingWorker.ResetBuilding(x, y);
                }


                bool haveFire = (bool)objects[i++];

                _eGM.CellEffectEnt_CellEffectCom(x, y).SetResetEffect(haveFire, EffectTypes.Fire);
            }
        }
    }

    [PunRPC]
    private void RefreshEconomyGeneral(object[] objects)
    {
        int i = 0;

        _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradePawnDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeRookDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeBishopDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict[Instance.IsMasterClient] = (int)objects[i++];
        _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict[Instance.IsMasterClient] = (int)objects[i++];

        var food = (int)objects[i++];
        var wood = (int)objects[i++];
        var ore = (int)objects[i++];
        var iron = (int)objects[i++];
        var gold = (int)objects[i++];

        bool isSettedKing = (bool)objects[i++];
        bool isSettedCity = (bool)objects[i++];
        int[] xySettedCity = (int[])objects[i++];


        _eGM.EconomyEnt_EconomyCom.SetFood(Instance.IsMasterClient, food);
        _eGM.EconomyEnt_EconomyCom.SetWood(Instance.IsMasterClient, wood);
        _eGM.EconomyEnt_EconomyCom.SetOre(Instance.IsMasterClient, ore);
        _eGM.EconomyEnt_EconomyCom.SetIron(Instance.IsMasterClient, iron);
        _eGM.EconomyEnt_EconomyCom.SetGold(Instance.IsMasterClient, gold);

        _eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[Instance.IsMasterClient] = isSettedKing;
        _eGM.InfoEnt_BuildingsInfoCom.IsSettedCityDict[Instance.IsMasterClient] = isSettedCity;
        _eGM.InfoEnt_BuildingsInfoCom.XySettedCityDict[Instance.IsMasterClient] = xySettedCity;
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