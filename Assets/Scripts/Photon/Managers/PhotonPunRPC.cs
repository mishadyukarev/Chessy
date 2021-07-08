using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Assets.Scripts.Static;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class PhotonPunRPC : MonoBehaviour
    {
        private PhotonView _photonView;

        private EntitiesGameGeneralManager _eGM;
        private EntitiesGameMasterManager _eMM;

        private SystemsGameGeneralManager _sGM;
        private SystemsGameMasterManager _sMM;

        private string MasterRPCName => nameof(MasterRPC);
        private string GeneralRPCName => nameof(GeneralRPC);

        private int _i;

        internal void Constructor(PhotonView photonView, ECSManager eCSmanager)
        {
            _photonView = photonView;

            _sMM = eCSmanager.SystemsGameMasterManager;
            _eMM = eCSmanager.EntitiesGameMasterManager;

            _eGM = eCSmanager.EntitiesGameGeneralManager;
            _sGM = eCSmanager.SystemsGameGeneralManager;


            PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
        }


        #region PUN

        public void ReadyToMaster(in bool isReady) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Ready, new object[] { isReady });
        public void ReadyToGeneral(Player playerTo, bool isCurrentReady, bool isStartedGame) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Ready, new object[] { isCurrentReady, isStartedGame });
        public void ReadyToGeneral(RpcTarget rpcTarget, bool isCurrentReady, bool isStartedGame) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.Ready, new object[] { isCurrentReady, isStartedGame });

        public void DoneToMaster(bool isDone) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Done, new object[] { isDone });
        public void DoneToGeneral(Player playerTo, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Done, new object[] { isRefreshed, isDone, numberMotion });
        public void DoneToGeneral(RpcTarget rpcTarget, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.Done, new object[] { isRefreshed, isDone, numberMotion });

        public void TruceToMaster(bool isTruce) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Truce, new object[] { isTruce });
        public void TruceToGeneral(Player playerTo, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Truce, new object[] { isRefreshed, isDone, numberMotion });
        public void TruceToGeneral(RpcTarget rpcTarget, bool isRefreshed, bool isDone, int numberMotion) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.Truce, new object[] { isRefreshed, isDone, numberMotion });

        public void UpgradeUnitToMaster(int[] xyCell, UpgradeModTypes upgradeModType = UpgradeModTypes.Unit) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Upgrade, new object[] { upgradeModType, xyCell });
        public void UpgradeBuildingToMaster(BuildingTypes buildingType, UpgradeModTypes upgradeModType = UpgradeModTypes.Building) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Upgrade, new object[] { upgradeModType, buildingType });

        public void ShiftUnitToMaster(in int[] xyPreviousCell, in int[] xySelectedCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Shift, new object[] { xyPreviousCell, xySelectedCell });
        public void AttackUnitToMaster(int[] xyPreviousCell, int[] xySelectedCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Attack, new object[] { xyPreviousCell, xySelectedCell });
        public void AttackUnitToGeneral(Player playerTo, bool isAttacked, bool isActivatedSound) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Attack, new object[] { isAttacked, isActivatedSound });
        public void AttackUnitToGeneral(RpcTarget rpcTarget, bool isAttacked, bool isActivatedSound, int[] xyStart, int[] xyEnd) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.Attack, new object[] { isAttacked, isActivatedSound, xyStart, xyEnd });

        public void BuildToMaster(int[] xyCell, BuildingTypes buildingType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Build, new object[] { xyCell, buildingType });
        public void DestroyBuildingToMaster(int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Destroy, new object[] { xyCell });

        public void ProtectRelaxUnitToMaster(ProtectRelaxTypes protectRelaxType, int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.ProtectRelax, new object[] { protectRelaxType, xyCell });

        public void EndGameToMaster(int actorNumberWinner) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.EndGame, new object[] { actorNumberWinner });
        public void EndGameToGeneral(RpcTarget rpcTarget, int actorNumberWinner) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcTypes.EndGame, new object[] { actorNumberWinner });

        public void MistakeEconomyToGeneral(Player playerTo, params bool[] haves) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Mistake, new object[] { MistakeTypes.EconomyType, haves });
        public void MistakeUnitToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.Mistake, new object[] { MistakeTypes.UnitType });

        public void FireToMaster(int[] fromXy, int[] toXy) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.Fire, new object[] { fromXy, toXy });
        public void UniqueAbilityPawnToMaster(int[] xy, UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.UniquePawnAbility, new object[] { xy, uniqueAbilitiesPawnType });

        public void CreateUnitToMaster(UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.CreateUnit, new object[] { unitType });

        public void MeltOreToMaster() => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.MeltOre, new object[] { });

        public void GetUnitToMaster(UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.GetUnit, new object[] { unitType });
        public void GetUnitToGeneral(Player playerTo, bool isGetted, UnitTypes unitType) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.GetUnit, new object[] { isGetted, unitType });


        public void SetUniToMaster(int[] xyCell, UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcTypes.SetUnit, new object[] { xyCell, unitType });
        public void SetUniToGeneral(Player playerTo, bool isSetted) => _photonView.RPC(GeneralRPCName, playerTo, RpcTypes.SetUnit, new object[] { isSetted });


        [PunRPC]
        private void MasterRPC(RpcTypes rPCType, object[] objects, PhotonMessageInfo info)
        {
            _eGM.RpcGeneralEnt_RPCCom.FromInfo = info;

            switch (rPCType)
            {
                case RpcTypes.None:
                    break;

                case RpcTypes.Ready:
                    _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething = (bool)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(ReadyMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Done:
                    _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething = (bool)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(DonerMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Truce:
                    _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething = (bool)objects[0];
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

                case RpcTypes.ProtectRelax:
                    _eMM.ProtectRelaxEnt_ProtectRelaxCom.SetProtectedRelaxedType((ProtectRelaxTypes)objects[0]);
                    _eMM.RPCMasterEnt_RPCMasterCom.XyCell = (int[])objects[1];
                    _sMM.TryInvokeRunSystem(nameof(ProtectRelaxMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.CreateUnit:
                    _eMM.RPCMasterEnt_RPCMasterCom.UnitType = (UnitTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(CreatorUnitMasterSystem), _sMM.RPCSystems);
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

                case RpcTypes.Fire:
                    _eMM.FromInfoEnt_FromInfoCom.Info = info;

                    _eMM.FireEnt_FromToXyCom.FromXyCopy = (int[])objects[0];
                    _eMM.FireEnt_FromToXyCom.ToXyCopy = (int[])objects[1];
                    _sMM.TryInvokeRunSystem(nameof(FireMasterSystem), _sMM.RPCSystems);
                    break;

                case RpcTypes.Upgrade:
                    var upgradeModType = (UpgradeModTypes)objects[0];
                    _eMM.UpgradeEnt_UpgradeTypeCom.UpgradeModType = upgradeModType;

                    switch (upgradeModType)
                    {
                        case UpgradeModTypes.None:
                            throw new Exception();

                        case UpgradeModTypes.Unit:
                            _eMM.UpgradeEnt_XyCellCom.XyCell = (int[])objects[1];
                            break;

                        case UpgradeModTypes.Building:
                            _eMM.UpgradeEnt_BuildingTypeCom.SetBuildingType((BuildingTypes)objects[1]);
                            break;

                        default:
                            throw new Exception();
                    }

                    _sMM.TryInvokeRunSystem(nameof(UpgradeMasterSystem), _sMM.RPCSystems);

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
            _eGM.RpcGeneralEnt_RPCCom.FromInfo = info;

            switch (rPCType)
            {
                case RpcTypes.None:
                    break;

                case RpcTypes.Ready:
                    bool isActivated = (bool)objects[_i++];
                    bool isStartedGame = (bool)objects[_i++];
                    _eGM.ReadyEnt_ActivatedDictCom.SetIsActivated(Instance.IsMasterClient, isActivated);
                    _eGM.ReadyEnt_StartedGameCom.IsStartedGame = isStartedGame;
                    break;

                case RpcTypes.Done:
                    _eGM.MotionEnt_IsActivatedCom.IsActivated = (bool)objects[0];
                    _eGM.DonerEnt_IsActivatedDictCom.SetIsActivated(Instance.IsMasterClient, (bool)objects[1]);
                    _eGM.MotionEnt_AmountCom.SetAmount((int)objects[2]);
                    break;

                case RpcTypes.Truce:
                    _eGM.MotionEnt_IsActivatedCom.IsActivated = (bool)objects[_i++];
                    _eGM.TruceEnt_ActivatedDictCom.SetIsActivated(Instance.IsMasterClient, (bool)objects[_i++]);
                    _eGM.MotionEnt_AmountCom.SetAmount((int)objects[_i++]);
                    break;

                case RpcTypes.EndGame:
                    _eGM.EndGameEntEndGameCom.IsEndGame = true;
                    _eGM.EndGameEntEndGameCom.PlayerWinner = PhotonNetwork.PlayerList[(int)objects[0] - 1];
                    break;

                case RpcTypes.Attack:
                    if ((bool)objects[0]) _eGM.SelectorEnt_SelectorCom.AttackUnitAction();
                    if ((bool)objects[1]) _eGM.SoundEnt_SoundCom.AttackSoundAction();
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

                            if (!haveFood) _eGM.EconomyEnt_MistakeEconomyCom.FoodMistake();
                            if (!haveWood) _eGM.EconomyEnt_MistakeEconomyCom.WoodMistake();
                            if (!haveOre) _eGM.EconomyEnt_MistakeEconomyCom.OreMistake();
                            if (!haveIron) _eGM.EconomyEnt_MistakeEconomyCom.IronMistake();
                            if (!haveGold) _eGM.EconomyEnt_MistakeEconomyCom.GoldMistake();

                            if (!haveFood || !haveWood || !haveOre || !haveIron || !haveGold)
                                _eGM.SoundEnt_SoundCom.MistakeSoundAction.Invoke();
                            break;

                        case MistakeTypes.UnitType:
                            _eGM.DonerEnt_MistakeCom.Invoke();
                            break;

                        default:
                            break;
                    }

                    break;

                case RpcTypes.GetUnit:
                    if ((bool)objects[0]) _eGM.SelectorEnt_UnitTypeCom.SetUnitType((UnitTypes)objects[1]);
                    break;

                case RpcTypes.SetUnit:
                    if ((bool)objects[0]) _eGM.SelectorEnt_SelectorCom.SetterUnitDelegate();
                    break;

                default:
                    break;
            }

            //RefreshAllToMaster();
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
                        listObjects.Add(_eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).IsActivated(false));
                        listObjects.Add(_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType);
                        listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).AmountSteps);
                        listObjects.Add(_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth);
                        listObjects.Add(_eGM.CellUnitEnt_ProtectRelaxCom(x, y).ProtectRelaxType);

                        listObjects.Add(_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner);
                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                        {
                            listObjects.Add(_eGM.CellUnitEnt_CellOwnerCom(x, y).ActorNumber);
                        }
                        else
                        {
                            listObjects.Add(_eGM.CellUnitEnt_CellOwnerBotCom(x, y).HaveBot);
                        }         
                    }


                    listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountResources(ResourceTypes.Food));
                    listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountResources(ResourceTypes.Wood));
                    listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountResources(ResourceTypes.Ore));
                    listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Fertilizer));
                    listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest));
                    listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.YoungForest));
                    listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Hill));
                    listObjects.Add(_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Mountain));



                    var haveBuilding = _eGM.CellBuildEnt_BuilTypeCom(x, y).HaveBuilding;
                    listObjects.Add(haveBuilding);

                    if (haveBuilding)
                    {
                        listObjects.Add(_eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType);

                        var haveOwner = _eGM.CellBuildEnt_OwnerCom(x, y).HaveOwner;
                        listObjects.Add(haveOwner);
                        if (haveOwner)
                        {
                            listObjects.Add(_eGM.CellBuildEnt_OwnerCom(x, y).ActorNumber);
                        }
                        else
                        {
                            listObjects.Add(_eGM.CellBuildEnt_CellOwnerBotCom(x, y).HaveBot);
                        }
                    }


                    listObjects.Add(_eGM.CellEffectEnt_CellEffectCom(x, y).HaveEffect(EffectTypes.Fire));
                }
            }
            object[] objects = new object[listObjects.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];

            _photonView.RPC(nameof(RefreshCellsGeneral), RpcTarget.Others, objects);


            objects = new object[]
            {
            _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Farm, false),
            _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, false),
            _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Mine, false),

            _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, false),
            _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, false),
            _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, false),
            _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, false),
            _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, false),

            _eGM.UnitInfoEnt_UnitInventorCom.IsSettedKing(false),
            _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[false],
            _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[false],
            };
            _photonView.RPC(nameof(RefreshEconomyGeneral), RpcTarget.Others, objects);

            #endregion

        }

        [PunRPC]
        private void RefreshCellsGeneral(object[] objects)
        {
            int i = 0;
            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    Player player;
                    bool haveOwner = false;

                    bool haveUnit = (bool)objects[i++];
                    if (haveUnit)
                    {
                        bool isActiveUnit = (bool)objects[i++];
                        UnitTypes unitType = (UnitTypes)objects[i++];
                        int amountSteps = (int)objects[i++];
                        int amountHealth = (int)objects[i++];
                        ProtectRelaxTypes protectRelaxType = (ProtectRelaxTypes)objects[i++];

                        haveOwner = (bool)objects[i++];
                        if (haveOwner)
                        {
                            int actorNumber = (int)objects[i++];
                            player = PhotonNetwork.PlayerList[actorNumber - 1];

                            CellUnitWorker.SetPlayerUnit(unitType, amountHealth, amountSteps, protectRelaxType, player, x, y);
                        }
                        else
                        {
                            bool haveBot = (bool)objects[i++];
                            CellUnitWorker.SetBotUnit(unitType, haveBot, amountHealth, amountSteps, protectRelaxType, x, y);
                        }

                        _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetIsActivated(Instance.IsMasterClient, isActiveUnit);
                        _eGM.CellUnitEnt_CellUnitCom(x, y).EnableSR(isActiveUnit, unitType);
                    }
                    else
                    {
                        CellUnitWorker.ResetUnit(x, y);
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

                        haveOwner = (bool)objects[i++];

                        if (haveOwner)
                        {
                            int actorNumberBuilding = (int)objects[i++];
                            player = PhotonNetwork.PlayerList[actorNumberBuilding - 1];
                            CellBuildingWorker.SetPlayerBuilding(false, buildingType, player, x, y);
                        }
                        else
                        {
                            bool haveBot = (bool)objects[i++];
                            if (haveBot)
                            {
                                CellBuildingWorker.SetBotBuilding(BuildingTypes.City, x, y);
                            }
                        }
                    }
                    else
                    {
                        CellBuildingWorker.ResetBuilding(false, x, y);
                    }


                    bool haveFire = (bool)objects[i++];

                    _eGM.CellEffectEnt_CellEffectCom(x, y).SyncEffect(haveFire, EffectTypes.Fire);
                }

        }

        [PunRPC]
        private void RefreshEconomyGeneral(object[] objects)
        {
            int i = 0;

            _eGM.BuildingsEnt_UpgradeBuildingsCom.SetAmountUpgrades(BuildingTypes.Farm, Instance.IsMasterClient, (int)objects[i++]);
            _eGM.BuildingsEnt_UpgradeBuildingsCom.SetAmountUpgrades(BuildingTypes.Woodcutter, Instance.IsMasterClient, (int)objects[i++]);
            _eGM.BuildingsEnt_UpgradeBuildingsCom.SetAmountUpgrades(BuildingTypes.Mine, Instance.IsMasterClient, (int)objects[i++]);

            var food = (int)objects[i++];
            var wood = (int)objects[i++];
            var ore = (int)objects[i++];
            var iron = (int)objects[i++];
            var gold = (int)objects[i++];

            bool isSettedKing = (bool)objects[i++];
            bool isSettedCity = (bool)objects[i++];
            int[] xySettedCity = (int[])objects[i++];


            _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, Instance.IsMasterClient, food);
            _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, Instance.IsMasterClient, wood);
            _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, Instance.IsMasterClient, ore);
            _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, Instance.IsMasterClient, iron);
            _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, Instance.IsMasterClient, gold);

            _eGM.UnitInfoEnt_UnitInventorCom.SetSettedKing(Instance.IsMasterClient, isSettedKing);
            _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Instance.IsMasterClient] = isSettedCity;
            _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[Instance.IsMasterClient] = xySettedCity;
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