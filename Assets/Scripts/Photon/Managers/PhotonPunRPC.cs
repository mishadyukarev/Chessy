using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Assets.Scripts.Static;
using Assets.Scripts.Static.Cell;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Main;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.CellUnitWorker;

namespace Assets.Scripts
{
    public sealed class PhotonPunRPC : MonoBehaviour
    {
        private PhotonView _photonView;

        private EntitiesGameGeneralManager _eGM;
        private EntitiesGameMasterManager _eMM;

        private SystemsGameGeneralManager _sGM;
        private SystemsGameMasterManager _sMM;

        private EntitiesGameOtherManager _entOM;
        private SystemsGameOtherManager _sysOM;

        private string MasterRPCName => nameof(MasterRPC);
        private string GeneralRPCName => nameof(GeneralRPC);
        private string OtherRPCName => nameof(OtherRPC);
        private string SyncMasterRPCName => nameof(SyncMaster);
        private string SyncOtherRPCName => nameof(SyncOther);

        private int _currentNumber;

        internal void Constructor(PhotonView photonView, ECSManager eCSmanager)
        {
            _photonView = photonView;

            _sMM = eCSmanager.SystemsGameMasterManager;
            _eMM = eCSmanager.EntitiesGameMasterManager;

            _eGM = eCSmanager.EntitiesGameGeneralManager;
            _sGM = eCSmanager.SystemsGameGeneralManager;

            _entOM = eCSmanager.EntitiesGameOtherManager;
            _sysOM = eCSmanager.SystemsGameOtherManager;


            PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
        }

        internal void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    _sMM.TryInvokeRunSystem(nameof(VisibilityUnitsMasterSystem), _sMM.RpcSystems);
                    if (!Instance.IsMasterClient)
                    {
                        SyncToMaster(SyncTypes.Cell);
                        SyncToMaster(SyncTypes.Economy);
                    }
                    break;

                default:
                    throw new Exception();
            }
        }


        #region PunRPCs

        public void ReadyToMaster(in bool isReady) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Ready, new object[] { isReady });
        public void ReadyToGeneral(Player playerTo, bool isCurrentReady, bool isStartedGame) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Ready, new object[] { isCurrentReady, isStartedGame });
        public void ReadyToGeneral(RpcTarget rpcTarget, bool isCurrentReady, bool isStartedGame) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.Ready, new object[] { isCurrentReady, isStartedGame });

        public void DoneToMaster(bool isDone) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Done, new object[] { isDone });
        public void ActiveAmountMotionUIToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.ActiveAmountMotionUI, new object[default]);
        public void ActiveAmountMotionUIToGeneral(RpcTarget rpcTarget) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.ActiveAmountMotionUI, new object[default]);
        public void SetAmountMotionToOther(Player playerTo, int numberMotion) => _photonView.RPC(OtherRPCName, playerTo, RpcOtherTypes.SetAmountMotion, new object[] { numberMotion });
        public void SetAmountMotionToOther(RpcTarget rpcTarget, int numberMotion) => _photonView.RPC(OtherRPCName, rpcTarget, RpcOtherTypes.SetAmountMotion, new object[] { numberMotion });

        public void UpgradeUnitToMaster(int[] xyCellForUpgrade, UpgradeModTypes upgradeModType = UpgradeModTypes.Unit) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Upgrade, new object[] { upgradeModType, xyCellForUpgrade });
        public void UpgradeBuildingToMaster(BuildingTypes buildingTypeForUpgrade, UpgradeModTypes upgradeModType = UpgradeModTypes.Building) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Upgrade, new object[] { upgradeModType, buildingTypeForUpgrade });

        public void ShiftUnitToMaster(in int[] xyPreviousCell, in int[] xySelectedCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Shift, new object[] { xyPreviousCell, xySelectedCell });
        public void AttackUnitToMaster(int[] xyPreviousCell, int[] xySelectedCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Attack, new object[] { xyPreviousCell, xySelectedCell });
        public void AttackUnitToGeneral(Player playerTo, bool isAttacked) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Attack, new object[] { isAttacked });
        public void AttackUnitToGeneral(RpcTarget rpcTarget, bool isAttacked, bool isActivatedSound, int[] xyStart, int[] xyEnd) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.Attack, new object[] { isAttacked, isActivatedSound, xyStart, xyEnd });

        public void BuildToMaster(int[] xyCell, BuildingTypes buildingType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Build, new object[] { xyCell, buildingType });
        public void DestroyBuildingToMaster(int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Destroy, new object[] { xyCell });

        public void ProtectRelaxUnitToMaster(ProtectRelaxTypes protectRelaxType, int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.ProtectRelax, new object[] { protectRelaxType, xyCell });

        public void EndGameToMaster(int actorNumberWinner) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.EndGame, new object[] { actorNumberWinner });
        public void EndGameToGeneral(RpcTarget rpcTarget, int actorNumberWinner) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.EndGame, new object[] { actorNumberWinner });

        public void MistakeEconomyToGeneral(Player playerTo, params bool[] haves) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.EconomyType, haves });
        public void MistakeUnitToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.UnitType });

        public void FireToMaster(int[] fromXy, int[] toXy) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Fire, new object[] { fromXy, toXy });
        public void SeedEnvironmentToMaster(int[] xy, EnvironmentTypes environmentType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SeedEnvironment, new object[] { xy, environmentType });

        public void CreateUnitToMaster(UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.CreateUnit, new object[] { unitType });

        public void MeltOreToMaster() => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.MeltOre, new object[] { });

        public void GetUnitToMaster(UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.GetUnit, new object[] { unitType });
        public void GetUnitToGeneral(Player playerTo, bool isGetted, UnitTypes unitType) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.GetUnit, new object[] { isGetted, unitType });


        public void SetUniToMaster(int[] xyCell, UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SetUnit, new object[] { xyCell, unitType });
        public void SetUnitToGeneral(Player playerTo, bool isSetted) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.SetUnit, new object[] { isSetted });

        public void SoundToGeneral(RpcTarget rpcTarget, SoundEffectTypes soundEffectType) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.Sound, new object[] { soundEffectType });
        public void SoundToGeneral(Player playerTo, SoundEffectTypes soundEffectType) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Sound, new object[] { soundEffectType });


        [PunRPC]
        private void MasterRPC(RpcMasterTypes rpcType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _eMM.FromInfoEnt_FromInfoCom.SetFromInfo(infoFrom);

            switch (rpcType)
            {
                case RpcMasterTypes.None:
                    break;

                case RpcMasterTypes.Ready:
                    _eMM.ReadyEnt_IsActivatedCom.SetActivated((bool)objects[0]);
                    _sMM.TryInvokeRunSystem(nameof(ReadyMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.Done:
                    _eMM.DonerEnt_IsActivatedCom.SetActivated((bool)objects[0]);
                    _sMM.TryInvokeRunSystem(nameof(DonerMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.EndGame:
                    EndGameToGeneral(RpcTarget.All, (int)objects[0]);
                    break;

                case RpcMasterTypes.Build:
                    _eMM.BuildEnt_XyCellCom.SetXyCell((int[])objects[0]);
                    _eMM.BuildEnt_BuildingTypeCom.SetBuildingType((BuildingTypes)objects[1]);
                    _sMM.TryInvokeRunSystem(nameof(BuilderMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.Destroy:
                    _eMM.DestroyEnt_XyCellCom.SetXyCell((int[])objects[0]);
                    _sMM.TryInvokeRunSystem(nameof(DestroyMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.Shift:
                    _eMM.ShiftEnt_FromToXyCom.SetAllXy((int[])objects[0], (int[])objects[1]);
                    _sMM.TryInvokeRunSystem(nameof(ShiftUnitMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.Attack:
                    _eMM.AttackEnt_FromToXyCom.SetAllXy((int[])objects[0], (int[])objects[1]);
                    _sMM.TryInvokeRunSystem(nameof(AttackUnitMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.ProtectRelax:
                    _eMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType = (ProtectRelaxTypes)objects[0];
                    _eMM.ProtectRelaxEnt_XyCellCom.SetXyCell((int[])objects[1]);
                    _sMM.TryInvokeRunSystem(nameof(ProtectRelaxMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.CreateUnit:
                    _eMM.CreatorEnt_UnitTypeCom.UnitType = (UnitTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(CreatorUnitMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.MeltOre:
                    _sMM.TryInvokeRunSystem(nameof(MeltOreMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.GetUnit:
                    _eMM.CreatorEnt_UnitTypeCom.UnitType = (UnitTypes)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(GetterUnitMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.SetUnit:
                    _eMM.SettingUnitEnt_XyCellCom.SetXyCell((int[])objects[0]);
                    _eMM.SettingUnitEnt_UnitTypeCom.UnitType = (UnitTypes)objects[1];
                    _sMM.TryInvokeRunSystem(nameof(SetterUnitMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.SeedEnvironment:
                    _eMM.SeedingEnt_XyCellCom.SetXyCell((int[])objects[0]);
                    _eMM.SeedingEnt_EnvironmentTypesCom.SetEnvironmentType((EnvironmentTypes)objects[1]);
                    _sMM.TryInvokeRunSystem(nameof(SeedingMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.Fire:
                    _eMM.FireEnt_FromToXyCom.FromXy = (int[])objects[0];
                    _eMM.FireEnt_FromToXyCom.ToXy = (int[])objects[1];
                    _sMM.TryInvokeRunSystem(nameof(FireMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.Upgrade:
                    var upgradeModType = (UpgradeModTypes)objects[0];
                    _eMM.UpgradeEnt_UpgradeTypeCom.SetUpgradeModType(upgradeModType);
                    switch (upgradeModType)
                    {
                        case UpgradeModTypes.None:
                            throw new Exception();

                        case UpgradeModTypes.Unit:
                            _eMM.UpgradeEnt_XyCellCom.SetXyCell((int[])objects[1]);
                            break;

                        case UpgradeModTypes.Building:
                            _eMM.UpgradeEnt_BuildingTypeCom.SetBuildingType((BuildingTypes)objects[1]);
                            break;

                        default:
                            throw new Exception();
                    }
                    _sMM.TryInvokeRunSystem(nameof(UpgradeMasterSystem), _sMM.RpcSystems);
                    break;

                default:
                    break;
            }

            _sMM.TryInvokeRunSystem(nameof(VisibilityUnitsMasterSystem), _sMM.RpcSystems);
            SyncToMaster(SyncTypes.Cell);
            SyncToMaster(SyncTypes.Economy);
        }

        public void GetAvailableCellsToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.GetAvailableCellsForSetting, new object[] { });

        [PunRPC]
        private void GeneralRPC(RpcGeneralTypes rpcGeneralType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _currentNumber = 0;
            _eGM.FromInfoEnt_FromInfoCom.SetFromInfo(infoFrom);

            switch (rpcGeneralType)
            {
                case RpcGeneralTypes.None:
                    throw new Exception();

                case RpcGeneralTypes.Ready:
                    bool isActivated = (bool)objects[_currentNumber++];
                    bool isStartedGame = (bool)objects[_currentNumber++];
                    _eGM.ReadyEnt_ActivatedDictCom.SetActivated(Instance.IsMasterClient, isActivated);
                    _eGM.ReadyEnt_StartedGameCom.IsStartedGame = isStartedGame;
                    break;

                case RpcGeneralTypes.SetDonerActiveUI:
                    _eGM.DonerUIEnt_IsActivatedDictCom.SetActivated(Instance.IsMasterClient, (bool)objects[_currentNumber++]);
                    break;

                case RpcGeneralTypes.ActiveAmountMotionUI:
                    _eGM.MotionEnt_ActivatedCom.SetActivated(true);
                    break;

                case RpcGeneralTypes.GetAvailableCellsForSetting:
                    _eGM.SelectorEnt_SelectorCom.GetCellsForSettingUnit(Instance.LocalPlayer);
                    break;

                case RpcGeneralTypes.EndGame:
                    _eGM.EndGameEntEndGameCom.IsEndGame = true;
                    _eGM.EndGameEntEndGameCom.PlayerWinner = PhotonNetwork.PlayerList[(int)objects[_currentNumber++] - 1];
                    break;

                case RpcGeneralTypes.Attack:
                    if ((bool)objects[_currentNumber++])
                    {
                        _eGM.SelectorEnt_SelectorCom.ClearAvailableCells(AvailableCellTypes.Shift);
                        _eGM.SelectorEnt_SelectorCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                        _eGM.SelectorEnt_SelectorCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
                    }
                    break;

                case RpcGeneralTypes.Mistake:
                    switch ((MistakeTypes)objects[_currentNumber++])
                    {
                        case MistakeTypes.EconomyType:
                            var haves = (bool[])objects[_currentNumber++];
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
                            break;

                        case MistakeTypes.UnitType:
                            _eGM.DonerUIEnt_MistakeCom.Invoke();
                            break;

                        default:
                            break;
                    }
                    break;

                case RpcGeneralTypes.GetUnit:
                    if ((bool)objects[_currentNumber++]) _eGM.SelectorEnt_UnitTypeCom.UnitType = (UnitTypes)objects[_currentNumber++];
                    break;

                case RpcGeneralTypes.SetUnit:
                    if ((bool)objects[_currentNumber++])
                    {
                        _eGM.SelectorEnt_SelectorCom.IsStartSelectedDirect = true;
                        _eGM.SelectorEnt_UnitTypeCom.ResetUnit();
                    }
                    break;

                case RpcGeneralTypes.Sound:
                    var soundEffectType = (SoundEffectTypes)objects[_currentNumber++];
                    SoundManager.PlaySoundEffect(soundEffectType);
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        private void OtherRPC(RpcOtherTypes rpcOtherType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _currentNumber = 0;
            _entOM.FromInfoEnt_FromInfoCom.SetFromInfo(infoFrom);

            switch (rpcOtherType)
            {
                case RpcOtherTypes.None:
                    throw new Exception();

                case RpcOtherTypes.SetAmountMotion:
                    _eGM.MotionEnt_AmountCom.Amount = (int)objects[_currentNumber++];
                    break;

                case RpcOtherTypes.SetStepModType:
                    Instance.EntComM.SaverEnt_StepModeTypeCom.SetStepModeType((StepModeTypes)objects[_currentNumber++]);
                    break;

                default:
                    throw new Exception();
            }
        }


        #region Sync

        internal void SyncToMaster(SyncTypes syncType) => _photonView.RPC(SyncMasterRPCName, RpcTarget.MasterClient, syncType);

        [PunRPC]
        private void SyncMaster(SyncTypes syncType)
        {
            var objects = new object[0];

            switch (syncType)
            {
                case SyncTypes.None:
                    throw new Exception();

                case SyncTypes.Cell:
                    List<object> listObjects = new List<object>();
                    for (int x = 0; x < _eGM.Xamount; x++)
                        for (int y = 0; y < _eGM.Yamount; y++)
                        {
                            var xy = new int[] { x, y };

                            listObjects.Add(Instance.EntComM.SaverEnt_StepModeTypeCom.StepModeType);
                            listObjects.Add(_eGM.DonerUIEnt_IsActivatedDictCom.IsActivated(false));


                            listObjects.Add(HaveAnyUnit(xy));
                            if (HaveAnyUnit(xy))
                            {
                                listObjects.Add(IsActivated(false, xy));
                                listObjects.Add(UnitType(xy));
                                listObjects.Add(AmountSteps(xy));
                                listObjects.Add(AmountHealth(xy));
                                listObjects.Add(ProtectRelaxType(xy));

                                listObjects.Add(HaveOwner(xy));
                                if (HaveOwner(xy))
                                {
                                    listObjects.Add(ActorNumber(xy));
                                }
                                else
                                {
                                    listObjects.Add(HaveBot(xy));
                                }
                            }


                            listObjects.Add(AmountResources(ResourceTypes.Food, xy));
                            listObjects.Add(AmountResources(ResourceTypes.Wood, xy));
                            listObjects.Add(AmountResources(ResourceTypes.Ore, xy));
                            listObjects.Add(HaveEnvironment(EnvironmentTypes.Fertilizer, xy));
                            listObjects.Add(HaveEnvironment(EnvironmentTypes.AdultForest, xy));
                            listObjects.Add(HaveEnvironment(EnvironmentTypes.YoungForest, xy));
                            listObjects.Add(HaveEnvironment(EnvironmentTypes.Hill, xy));
                            listObjects.Add(HaveEnvironment(EnvironmentTypes.Mountain, xy));



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


                            listObjects.Add(CellEffectsWorker.HaveEffect(EffectTypes.Fire, xy));
                        }

                    objects = new object[listObjects.Count];
                    for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];

                    _photonView.RPC(SyncOtherRPCName, RpcTarget.Others, SyncTypes.Cell, objects);
                    break;


                case SyncTypes.Economy:
                    objects = new object[]
                    {
                        _eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Farm, false),
                        _eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Woodcutter, false),
                        _eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Mine, false),

                        _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Farm, false),
                        _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, false),
                        _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Mine, false),

                        UnitInfoManager.AmountUnitsInGame(UnitTypes.Pawn, false),
                        UnitInfoManager.AmountUnitsInGame(UnitTypes.PawnSword, false),
                        UnitInfoManager.AmountUnitsInGame(UnitTypes.Rook, false),
                        UnitInfoManager.AmountUnitsInGame(UnitTypes.RookCrossbow, false),
                        UnitInfoManager.AmountUnitsInGame(UnitTypes.Bishop, false),
                        UnitInfoManager.AmountUnitsInGame(UnitTypes.BishopCrossbow, false),

                        _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, false),
                        _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, false),
                        _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, false),
                        _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, false),
                        _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, false),

                        _eGM.UnitInfoEnt_UnitInventorCom.IsSettedKing(false),
                        _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[false],
                        _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[false],
                    };
                    _photonView.RPC(SyncOtherRPCName, RpcTarget.Others, SyncTypes.Economy, objects);
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        private void SyncOther(SyncTypes syncType, object[] objects)
        {
            _currentNumber = 0;

            switch (syncType)
            {
                case SyncTypes.None:
                    throw new Exception();

                case SyncTypes.Cell:
                    for (int x = 0; x < _eGM.Xamount; x++)
                        for (int y = 0; y < _eGM.Yamount; y++)
                        {
                            var xy = new int[] { x, y };

                            Instance.EntComM.SaverEnt_StepModeTypeCom.SetStepModeType((StepModeTypes)objects[_currentNumber++]);
                            bool isActivatedDoner = (bool)objects[_currentNumber++];
                            _eGM.DonerUIEnt_IsActivatedDictCom.SetActivated(Instance.IsMasterClient, isActivatedDoner);


                            Player player;
                            bool haveOwner = false;

                            bool haveUnit = (bool)objects[_currentNumber++];
                            if (haveUnit)
                            {
                                bool isActiveUnit = (bool)objects[_currentNumber++];
                                UnitTypes unitType = (UnitTypes)objects[_currentNumber++];
                                int amountSteps = (int)objects[_currentNumber++];
                                int amountHealth = (int)objects[_currentNumber++];
                                ProtectRelaxTypes protectRelaxType = (ProtectRelaxTypes)objects[_currentNumber++];

                                haveOwner = (bool)objects[_currentNumber++];

                                if (haveOwner)
                                {
                                    int actorNumber = (int)objects[_currentNumber++];
                                    player = PhotonNetwork.PlayerList[actorNumber - 1];

                                    CellUnitWorker.SyncPlayerUnit(unitType, amountHealth, amountSteps, protectRelaxType, player, xy);
                                }
                                else
                                {
                                    bool haveBot = (bool)objects[_currentNumber++];
                                    CellUnitWorker.SetBotUnit(unitType, haveBot, amountHealth, amountSteps, protectRelaxType, xy);
                                }

                                SetIsActivated(Instance.IsMasterClient, isActiveUnit, xy);
                            }
                            else
                            {
                                //if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                                //{
                                //    CellUnitWorker.SyncPlayerUnit(false, x, y);
                                //}
                                //else
                                //{
                                //    CellUnitWorker.ResetBotUnit(x, y);
                                //}
                            }

                            int amountResourcesFertilizer = (int)objects[_currentNumber++];
                            int amountResourcesForest = (int)objects[_currentNumber++];
                            int oreResources = (int)objects[_currentNumber++];
                            bool haveFertilizer = (bool)objects[_currentNumber++];
                            bool haveAdultForest = (bool)objects[_currentNumber++];
                            bool haveYoungTree = (bool)objects[_currentNumber++];
                            bool haveHill = (bool)objects[_currentNumber++];
                            bool haveMountain = (bool)objects[_currentNumber++];

                            if (haveFertilizer) SetEnvironment(EnvironmentTypes.Fertilizer, amountResourcesFertilizer, xy);
                            else ResetEnvironment(EnvironmentTypes.Fertilizer, xy);

                            if (haveAdultForest) SetEnvironment(EnvironmentTypes.AdultForest, amountResourcesForest, xy);
                            else ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                            if (haveYoungTree) SetNewEnvironment(EnvironmentTypes.YoungForest, xy);
                            else ResetEnvironment(EnvironmentTypes.YoungForest, xy);

                            if (haveHill) SetEnvironment(EnvironmentTypes.Hill, oreResources, xy);
                            else ResetEnvironment(EnvironmentTypes.Hill, xy);

                            if (haveMountain) SetNewEnvironment(EnvironmentTypes.Mountain, xy);
                            else ResetEnvironment(EnvironmentTypes.Mountain, xy);



                            bool haveBuilding = (bool)objects[_currentNumber++];

                            if (haveBuilding)
                            {
                                BuildingTypes buildingType = (BuildingTypes)objects[_currentNumber++];

                                haveOwner = (bool)objects[_currentNumber++];

                                if (haveOwner)
                                {
                                    int actorNumberBuilding = (int)objects[_currentNumber++];
                                    player = PhotonNetwork.PlayerList[actorNumberBuilding - 1];
                                    CellBuildingWorker.SetPlayerBuilding(false, buildingType, player, x, y);
                                }
                                else
                                {
                                    bool haveBot = (bool)objects[_currentNumber++];
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


                            bool haveFire = (bool)objects[_currentNumber++];

                            CellEffectsWorker.SyncEffect(haveFire, EffectTypes.Fire, xy);
                        }
                    break;


                case SyncTypes.Economy:
                    var amountFarm = (int)objects[_currentNumber++];
                    var amountWoodcutter = (int)objects[_currentNumber++];
                    var amountMine = (int)objects[_currentNumber++];

                    _eGM.BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Farm, Instance.IsMasterClient, amountFarm);
                    _eGM.BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Woodcutter, Instance.IsMasterClient, amountWoodcutter);
                    _eGM.BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Mine, Instance.IsMasterClient, amountMine);

                    _eGM.BuildingsEnt_UpgradeBuildingsCom.SetAmountUpgrades(BuildingTypes.Farm, Instance.IsMasterClient, (int)objects[_currentNumber++]);
                    _eGM.BuildingsEnt_UpgradeBuildingsCom.SetAmountUpgrades(BuildingTypes.Woodcutter, Instance.IsMasterClient, (int)objects[_currentNumber++]);
                    _eGM.BuildingsEnt_UpgradeBuildingsCom.SetAmountUpgrades(BuildingTypes.Mine, Instance.IsMasterClient, (int)objects[_currentNumber++]);

                    var amountPawn = (int)objects[_currentNumber++];
                    var amountPawnSword = (int)objects[_currentNumber++];
                    var amountRook = (int)objects[_currentNumber++];
                    var amountRookCrossbow = (int)objects[_currentNumber++];
                    var amountBishop = (int)objects[_currentNumber++];
                    var amountBishopCrossbow = (int)objects[_currentNumber++];

                    var food = (int)objects[_currentNumber++];
                    var wood = (int)objects[_currentNumber++];
                    var ore = (int)objects[_currentNumber++];
                    var iron = (int)objects[_currentNumber++];
                    var gold = (int)objects[_currentNumber++];

                    bool isSettedKing = (bool)objects[_currentNumber++];
                    bool isSettedCity = (bool)objects[_currentNumber++];
                    int[] xySettedCity = (int[])objects[_currentNumber++];


                    UnitInfoManager.SetAmountUnitInGame(UnitTypes.Pawn, Instance.IsMasterClient, amountPawn);
                    UnitInfoManager.SetAmountUnitInGame(UnitTypes.PawnSword, Instance.IsMasterClient, amountPawnSword);
                    UnitInfoManager.SetAmountUnitInGame(UnitTypes.Rook, Instance.IsMasterClient, amountRook);
                    UnitInfoManager.SetAmountUnitInGame(UnitTypes.RookCrossbow, Instance.IsMasterClient, amountRookCrossbow);
                    UnitInfoManager.SetAmountUnitInGame(UnitTypes.Bishop, Instance.IsMasterClient, amountBishop);
                    UnitInfoManager.SetAmountUnitInGame(UnitTypes.BishopCrossbow, Instance.IsMasterClient, amountBishopCrossbow);


                    _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, Instance.IsMasterClient, food);
                    _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, Instance.IsMasterClient, wood);
                    _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, Instance.IsMasterClient, ore);
                    _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, Instance.IsMasterClient, iron);
                    _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, Instance.IsMasterClient, gold);

                    _eGM.UnitInfoEnt_UnitInventorCom.SetSettedKing(Instance.IsMasterClient, isSettedKing);
                    _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Instance.IsMasterClient] = isSettedCity;
                    _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[Instance.IsMasterClient] = xySettedCity;
                    break;

                default:
                    throw new Exception();
            }
        }

        #endregion

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