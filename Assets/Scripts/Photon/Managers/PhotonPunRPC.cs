using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Info;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.CellUnitWorker;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class PhotonPunRPC : MonoBehaviour
    {
        private static PhotonView _photonView;

        private EntitiesGameGeneralManager _eGM;
        private EntitiesGameGeneralUIManager _eGGUIM;
        private EntitiesGameMasterManager _eMM;

        private SystemsGameGeneralManager _sGM;
        private SystemsGameMasterManager _sMM;

        private EntitiesGameOtherManager _entOM;
        private SystemsGameOtherManager _sysOM;

        private static string MasterRPCName => nameof(MasterRPC);
        private static string GeneralRPCName => nameof(GeneralRPC);
        private static string OtherRPCName => nameof(OtherRPC);
        private static string SyncMasterRPCName => nameof(SyncMaster);
        private static string SyncOtherRPCName => nameof(SyncOther);

        private int _currentNumber;

        internal void Constructor(PhotonView photonView, ECSManager eCSmanager)
        {
            _photonView = photonView;

            _sMM = eCSmanager.SystemsGameMasterManager;
            _eMM = eCSmanager.EntitiesGameMasterManager;

            _eGM = eCSmanager.EntitiesGameGeneralManager;
            _eGGUIM = eCSmanager.EntitiesGameGeneralUIManager;
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

        public static void ReadyToMaster(in bool isReady) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Ready, new object[] { isReady });
        public static void ReadyToGeneral(Player playerTo, bool isCurrentReady, bool isStartedGame) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Ready, new object[] { isCurrentReady, isStartedGame });
        public static void ReadyToGeneral(RpcTarget rpcTarget, bool isCurrentReady, bool isStartedGame) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.Ready, new object[] { isCurrentReady, isStartedGame });

        public static void DoneToMaster(bool isDone) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Done, new object[] { isDone });
        public static void ActiveAmountMotionUIToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.ActiveAmountMotionUI, new object[default]);
        public static void ActiveAmountMotionUIToGeneral(RpcTarget rpcTarget) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.ActiveAmountMotionUI, new object[default]);
        public static void SetAmountMotionToOther(Player playerTo, int numberMotion) => _photonView.RPC(OtherRPCName, playerTo, RpcOtherTypes.SetAmountMotion, new object[] { numberMotion });
        public static void SetAmountMotionToOther(RpcTarget rpcTarget, int numberMotion) => _photonView.RPC(OtherRPCName, rpcTarget, RpcOtherTypes.SetAmountMotion, new object[] { numberMotion });

        public static void UpgradeUnitToMaster(int[] xyCellForUpgrade, UpgradeModTypes upgradeModType = UpgradeModTypes.Unit) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Upgrade, new object[] { upgradeModType, xyCellForUpgrade });
        public static void UpgradeBuildingToMaster(BuildingTypes buildingTypeForUpgrade, UpgradeModTypes upgradeModType = UpgradeModTypes.Building) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Upgrade, new object[] { upgradeModType, buildingTypeForUpgrade });

        public static void ShiftUnitToMaster(in int[] xyPreviousCell, in int[] xySelectedCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Shift, new object[] { xyPreviousCell, xySelectedCell });
        public static void AttackUnitToMaster(int[] xyPreviousCell, int[] xySelectedCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Attack, new object[] { xyPreviousCell, xySelectedCell });
        public static void AttackUnitToGeneral(Player playerTo, bool isAttacked) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Attack, new object[] { isAttacked });
        public static void AttackUnitToGeneral(RpcTarget rpcTarget, bool isAttacked, bool isActivatedSound, int[] xyStart, int[] xyEnd) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.Attack, new object[] { isAttacked, isActivatedSound, xyStart, xyEnd });

        public static void BuildToMaster(int[] xyCell, BuildingTypes buildingType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Build, new object[] { xyCell, buildingType });
        public static void DestroyBuildingToMaster(int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Destroy, new object[] { xyCell });

        public static void ProtectRelaxUnitToMaster(ConditionTypes protectRelaxType, int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.ProtectRelax, new object[] { protectRelaxType, xyCell });

        public static void EndGameToMaster(int actorNumberWinner) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.EndGame, new object[] { actorNumberWinner });
        public static void EndGameToGeneral(RpcTarget rpcTarget, int actorNumberWinner) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.EndGame, new object[] { actorNumberWinner });

        public static void MistakeEconomyToGeneral(Player playerTo, params bool[] haves) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.EconomyType, haves });
        public static void MistakeUnitToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.UnitType });

        public static void FireToMaster(int[] fromXy, int[] toXy) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Fire, new object[] { fromXy, toXy });
        public static void SeedEnvironmentToMaster(int[] xy, EnvironmentTypes environmentType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SeedEnvironment, new object[] { xy, environmentType });

        public static void CreateUnitToMaster(UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.CreateUnit, new object[] { unitType });

        public static void MeltOreToMaster() => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.MeltOre, new object[] { });

        public static void GetUnitToMaster(UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.GetUnit, new object[] { unitType });
        public static void GetUnitToGeneral(Player playerTo, bool isGetted, UnitTypes unitType) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.GetUnit, new object[] { isGetted, unitType });


        public static void SetUniToMaster(int[] xyCell, UnitTypes unitType) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.SetUnit, new object[] { xyCell, unitType });
        public static void SetUnitToGeneral(Player playerTo, bool isSetted) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.SetUnit, new object[] { isSetted });

        public static void SoundToGeneral(RpcTarget rpcTarget, SoundEffectTypes soundEffectType) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.Sound, new object[] { soundEffectType });
        public static void SoundToGeneral(Player playerTo, SoundEffectTypes soundEffectType) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Sound, new object[] { soundEffectType });


        [PunRPC]
        private void MasterRPC(RpcMasterTypes rpcType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _eMM.FromInfoEnt_FromInfoCom.SetFromInfo(infoFrom);

            switch (rpcType)
            {
                case RpcMasterTypes.None:
                    break;

                case RpcMasterTypes.Ready:
                    _eMM.ReadyEnt_IsActivatedCom.IsActivated = (bool)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(ReadyMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.Done:
                    _eMM.DonerEnt_IsActivatedCom.IsActivated = (bool)objects[0];
                    _sMM.TryInvokeRunSystem(nameof(DonerMasterSystem), _sMM.RpcSystems);
                    break;

                case RpcMasterTypes.EndGame:
                    EndGameToGeneral(RpcTarget.All, (int)objects[0]);
                    break;

                case RpcMasterTypes.Build:
                    _eMM.BuildEnt_XyCellCom.SetXyCell((int[])objects[0]);
                    _eMM.BuildEnt_BuildingTypeCom.BuildingType = (BuildingTypes)objects[1];
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
                    _eMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType = (ConditionTypes)objects[0];
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
                    _eMM.UpgradeEnt_UpgradeTypeCom.UpgradeModType = upgradeModType;
                    switch (upgradeModType)
                    {
                        case UpgradeModTypes.None:
                            throw new Exception();

                        case UpgradeModTypes.Unit:
                            _eMM.UpgradeEnt_XyCellCom.SetXyCell((int[])objects[1]);
                            break;

                        case UpgradeModTypes.Building:
                            _eMM.UpgradeEnt_BuildingTypeCom.BuildingType = (BuildingTypes)objects[1];
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
                    _eGGUIM.ReadyEnt_ActivatedDictCom.SetActivated(Instance.IsMasterClient, isActivated);
                    _eGGUIM.ReadyEnt_StartedGameCom.IsStartedGame = isStartedGame;
                    break;

                case RpcGeneralTypes.SetDonerActiveUI:
                    _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(Instance.IsMasterClient, (bool)objects[_currentNumber++]);
                    break;

                case RpcGeneralTypes.ActiveAmountMotionUI:
                    _eGGUIM.MotionEnt_ActivatedCom.IsActivated = true;
                    break;

                case RpcGeneralTypes.GetAvailableCellsForSetting:
                    AvailableCellsEntsWorker.SetAllCells(AvailableCellTypes.SettingUnit, GetStartCellsForSettingUnit(Instance.LocalPlayer));
                    break;

                case RpcGeneralTypes.EndGame:
                    _eGGUIM.EndGameEntEndGameCom.IsEndGame = true;
                    _eGGUIM.EndGameEntEndGameCom.PlayerWinner = PhotonNetwork.PlayerList[(int)objects[_currentNumber++] - 1];
                    break;

                case RpcGeneralTypes.Attack:
                    if ((bool)objects[_currentNumber++])
                    {
                        AvailableCellsEntsWorker.ClearAvailableCells(AvailableCellTypes.Shift);
                        AvailableCellsEntsWorker.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                        AvailableCellsEntsWorker.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
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

                            if (!haveFood) _eGGUIM.FoodInfoUIEnt_MistakeResourcesUICom.Invoke();
                            if (!haveWood) _eGGUIM.WoodInfoUIEnt_MistakeResourcesUICom.Invoke();
                            if (!haveOre) _eGGUIM.OreInfoUIEnt_MistakeResourcesUICom.Invoke();
                            if (!haveIron) _eGGUIM.IronInfoUIEnt_MistakeResourcesUICom.Invoke();
                            if (!haveGold) _eGGUIM.GoldInfoUIEnt_MistakeResourcesUICom.Invoke();
                            break;

                        case MistakeTypes.UnitType:
                            _eGGUIM.DonerUIEnt_MistakeCom.Invoke();
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
                        _eGM.SelectorEnt_UnitTypeCom.UnitType = default;
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
                    _eGGUIM.MotionEnt_AmountCom.Amount = (int)objects[_currentNumber++];
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
                            listObjects.Add(_eGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivated(false));


                            listObjects.Add(HaveAnyUnit(xy));
                            if (HaveAnyUnit(xy))
                            {
                                listObjects.Add(IsVisibleUnit(false, xy));
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
                                    listObjects.Add(IsBot(xy));
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



                            var haveBuilding = CellBuildingWorker.HaveBuilding(xy);
                            listObjects.Add(haveBuilding);

                            if (haveBuilding)
                            {
                                listObjects.Add(CellBuildingWorker.BuildingType(xy));

                                var haveOwner = CellUnitWorker.HaveOwner(xy);
                                listObjects.Add(haveOwner);
                                if (haveOwner)
                                {
                                    listObjects.Add(CellUnitWorker.ActorNumber(xy));
                                }
                                else
                                {
                                    listObjects.Add(CellBuildingWorker.IsBot(xy));
                                }
                            }


                            listObjects.Add(CellFireWorker.HaveEffect(EffectTypes.Fire, xy));
                        }

                    objects = new object[listObjects.Count];
                    for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];

                    _photonView.RPC(SyncOtherRPCName, RpcTarget.Others, SyncTypes.Cell, objects);
                    break;


                case SyncTypes.Economy:
                    objects = new object[]
                    {
                        InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Farm, false),
                        InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Woodcutter, false),
                        InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Mine, false),

                        InfoUnitsWorker.AmountUnitsInGame(UnitTypes.Pawn, false),
                        InfoUnitsWorker.AmountUnitsInGame(UnitTypes.PawnSword, false),
                        InfoUnitsWorker.AmountUnitsInGame(UnitTypes.Rook, false),
                        InfoUnitsWorker.AmountUnitsInGame(UnitTypes.RookCrossbow, false),
                        InfoUnitsWorker.AmountUnitsInGame(UnitTypes.Bishop, false),
                        InfoUnitsWorker.AmountUnitsInGame(UnitTypes.BishopCrossbow, false),

                        InfoResourcesWorker.AmountResources(ResourceTypes.Food, false),
                        InfoResourcesWorker.AmountResources(ResourceTypes.Wood, false),
                        InfoResourcesWorker.AmountResources(ResourceTypes.Ore, false),
                        InfoResourcesWorker.AmountResources(ResourceTypes.Iron, false),
                        InfoResourcesWorker.AmountResources(ResourceTypes.Gold, false),
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
                    #region Cell

                    for (int x = 0; x < _eGM.Xamount; x++)
                        for (int y = 0; y < _eGM.Yamount; y++)
                        {
                            var xy = new int[] { x, y };

                            Instance.EntComM.SaverEnt_StepModeTypeCom.SetStepModeType((StepModeTypes)objects[_currentNumber++]);
                            bool isActivatedDoner = (bool)objects[_currentNumber++];
                            _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(Instance.IsMasterClient, isActivatedDoner);


                            Player player;
                            bool haveOwner = false;

                            bool haveUnit = (bool)objects[_currentNumber++];
                            if (haveUnit)
                            {
                                bool isActiveUnit = (bool)objects[_currentNumber++];
                                UnitTypes unitType = (UnitTypes)objects[_currentNumber++];
                                int amountSteps = (int)objects[_currentNumber++];
                                int amountHealth = (int)objects[_currentNumber++];
                                ConditionTypes protectRelaxType = (ConditionTypes)objects[_currentNumber++];

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

                                SetIsVisibleUnit(Instance.IsMasterClient, isActiveUnit, xy);
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
                                    CellBuildingWorker.CreatePlayerBuilding(buildingType, player, xy);
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
                                //Sync
                                //CellBuildingWorker.ResetBuilding(false, x, y);
                            }


                            bool haveFire = (bool)objects[_currentNumber++];

                            CellFireWorker.SyncEffect(haveFire, EffectTypes.Fire, xy);
                        }

                    #endregion
                    break;


                case SyncTypes.Economy:
                    var amountFarmUpgrades = (int)objects[_currentNumber++];
                    var amountWoodcutterUpgrades = (int)objects[_currentNumber++];
                    var amountMineUpgrades = (int)objects[_currentNumber++];

                    //var amountPawn = (int)objects[_currentNumber++];
                    //var amountPawnSword = (int)objects[_currentNumber++];
                    //var amountRook = (int)objects[_currentNumber++];
                    //var amountRookCrossbow = (int)objects[_currentNumber++];
                    //var amountBishop = (int)objects[_currentNumber++];
                    //var amountBishopCrossbow = (int)objects[_currentNumber++];

                    var food = (int)objects[_currentNumber++];
                    var wood = (int)objects[_currentNumber++];
                    var ore = (int)objects[_currentNumber++];
                    var iron = (int)objects[_currentNumber++];
                    var gold = (int)objects[_currentNumber++];


                    InfoBuidlingsWorker.SetAmountUpgrades(BuildingTypes.Farm, Instance.IsMasterClient, amountFarmUpgrades);
                    InfoBuidlingsWorker.SetAmountUpgrades(BuildingTypes.Woodcutter, Instance.IsMasterClient, amountWoodcutterUpgrades);
                    InfoBuidlingsWorker.SetAmountUpgrades(BuildingTypes.Mine, Instance.IsMasterClient, amountMineUpgrades);

                    //InfoUnitsWorker.SetAmountUnitInGame(UnitTypes.Pawn, Instance.IsMasterClient, amountPawn);
                    //InfoUnitsWorker.SetAmountUnitInGame(UnitTypes.PawnSword, Instance.IsMasterClient, amountPawnSword);
                    //InfoUnitsWorker.SetAmountUnitInGame(UnitTypes.Rook, Instance.IsMasterClient, amountRook);
                    //InfoUnitsWorker.SetAmountUnitInGame(UnitTypes.RookCrossbow, Instance.IsMasterClient, amountRookCrossbow);
                    //InfoUnitsWorker.SetAmountUnitInGame(UnitTypes.Bishop, Instance.IsMasterClient, amountBishop);
                    //InfoUnitsWorker.SetAmountUnitInGame(UnitTypes.BishopCrossbow, Instance.IsMasterClient, amountBishopCrossbow);

                    InfoResourcesWorker.SetAmountResources(ResourceTypes.Food, Instance.IsMasterClient, food);
                    InfoResourcesWorker.SetAmountResources(ResourceTypes.Wood, Instance.IsMasterClient, wood);
                    InfoResourcesWorker.SetAmountResources(ResourceTypes.Ore, Instance.IsMasterClient, ore);
                    InfoResourcesWorker.SetAmountResources(ResourceTypes.Iron, Instance.IsMasterClient, iron);
                    InfoResourcesWorker.SetAmountResources(ResourceTypes.Gold, Instance.IsMasterClient, gold);
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