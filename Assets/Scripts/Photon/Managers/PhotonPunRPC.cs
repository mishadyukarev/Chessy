using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Common;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Data;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Game.UI;
using Assets.Scripts.Workers.Info;
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
        private static PhotonView _photonView;

        private EntGameGeneralElseDataManager EGM => Instance.ECSmanager.EntGameGeneralElseDataManager;
        private EntitiesGameGeneralUIViewManager EGGUIM => Instance.ECSmanager.EntGameGeneralUIViewManager;
        private EntitiesGameMasterManager EMM => Instance.ECSmanager.EntGameMasterManager;

        private SystemsGameGeneralManager SGM => Instance.ECSmanager.SysGameGeneralManager;
        private SystemsGameMasterManager SMM => Instance.ECSmanager.SysGameMasterManager;

        private EntitiesGameOtherManager EntOM => Instance.ECSmanager.EntGameOtherManager;
        private SystemsGameOtherManager SysOM => Instance.ECSmanager.SysGameOtherManager;

        private static string MasterRPCName => nameof(MasterRPC);
        private static string GeneralRPCName => nameof(GeneralRPC);
        private static string OtherRPCName => nameof(OtherRPC);
        private static string SyncMasterRPCName => nameof(SyncAllMaster);
        private static string SyncOtherRPCName => nameof(SyncAllOther);

        private int _currentNumber;

        internal void Constructor(PhotonView photonView)
        {
            _photonView = photonView;

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
                    SyncAllToMaster();
                    break;

                default:
                    throw new Exception();
            }
        }


        #region StandartPunRPC

        #region Methods

        public static void ReadyToMaster(in bool isReady) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.Ready, new object[] { isReady });

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

        public static void ProtectRelaxUnitToMaster(ConditionUnitTypes protectRelaxType, int[] xyCell) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.ProtectRelax, new object[] { protectRelaxType, xyCell });

        public static void EndGameToMaster(int actorNumberWinner) => _photonView.RPC(MasterRPCName, RpcTarget.MasterClient, RpcMasterTypes.EndGame, new object[] { actorNumberWinner });
        public static void EndGameToGeneral(RpcTarget rpcTarget, int actorNumberWinner) => _photonView.RPC(GeneralRPCName, rpcTarget, RpcGeneralTypes.EndGame, new object[] { actorNumberWinner });

        public static void MistakeEconomyToGeneral(Player playerTo, params bool[] haves) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.Economy, haves });
        public static void MistakeUnitToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.NeedKing });
        public static void MistakeStepsUnitToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.NeedSteps });
        public static void MistakeNeedOthePlaceToGeneral(Player playerTo) => _photonView.RPC(GeneralRPCName, playerTo, RpcGeneralTypes.Mistake, new object[] { MistakeTypes.NeedOtherPlace });

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

        #endregion


        [PunRPC]
        private void MasterRPC(RpcMasterTypes rpcType, object[] objects, PhotonMessageInfo infoFrom)
        {
            EMM.FromInfoEnt_FromInfoCom.FromInfo = infoFrom;

            switch (rpcType)
            {
                case RpcMasterTypes.None:
                    break;

                case RpcMasterTypes.Ready:
                    EMM.ReadyEnt_IsActivatedCom.IsActivated = (bool)objects[0];
                    SMM.TryInvokeRunSystem(nameof(ReadyMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.Done:
                    EMM.DonerEnt_IsActivatedCom.IsActivated = (bool)objects[0];
                    SMM.TryInvokeRunSystem(nameof(DonerMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.EndGame:
                    EndGameToGeneral(RpcTarget.All, (int)objects[0]);
                    break;

                case RpcMasterTypes.Build:
                    EMM.BuildEnt_XyCellCom.XyCell = (int[])objects[0];
                    EMM.BuildEnt_BuildingTypeCom.BuildingType = (BuildingTypes)objects[1];
                    SMM.TryInvokeRunSystem(nameof(BuilderMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.Destroy:
                    EMM.DestroyEnt_XyCellCom.XyCell = (int[])objects[0];
                    SMM.TryInvokeRunSystem(nameof(DestroyMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.Shift:
                    EMM.ShiftEnt_FromToXyCom.SetAllXy((int[])objects[0], (int[])objects[1]);
                    SMM.TryInvokeRunSystem(nameof(ShiftUnitMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.Attack:
                    EMM.AttackEnt_FromToXyCom.SetAllXy((int[])objects[0], (int[])objects[1]);
                    SMM.TryInvokeRunSystem(nameof(AttackUnitMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.ProtectRelax:
                    EMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType = (ConditionUnitTypes)objects[0];
                    EMM.ProtectRelaxEnt_XyCellCom.XyCell = (int[])objects[1];
                    SMM.TryInvokeRunSystem(nameof(ConditionMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.CreateUnit:
                    EMM.CreatorEnt_UnitTypeCom.UnitType = (UnitTypes)objects[0];
                    SMM.TryInvokeRunSystem(nameof(CreatorUnitMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.MeltOre:
                    SMM.TryInvokeRunSystem(nameof(MeltOreMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.GetUnit:
                    EMM.CreatorEnt_UnitTypeCom.UnitType = (UnitTypes)objects[0];
                    SMM.TryInvokeRunSystem(nameof(GetterUnitMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.SetUnit:
                    EMM.SettingUnitEnt_XyCellCom.XyCell = (int[])objects[0];
                    EMM.SettingUnitEnt_UnitTypeCom.UnitType = (UnitTypes)objects[1];
                    SMM.TryInvokeRunSystem(nameof(SetterUnitMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.SeedEnvironment:
                    EMM.SeedingEnt_XyCellCom.XyCell = (int[])objects[0];
                    EMM.SeedingEnt_EnvironmentTypesCom.EnvironmentType = (EnvironmentTypes)objects[1];
                    SMM.TryInvokeRunSystem(nameof(SeedingMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.Fire:
                    EMM.FireEnt_FromToXyCom.FromXy = (int[])objects[0];
                    EMM.FireEnt_FromToXyCom.ToXy = (int[])objects[1];
                    SMM.TryInvokeRunSystem(nameof(FireMasterSystem), SMM.RpcSystems);
                    break;

                case RpcMasterTypes.Upgrade:
                    var upgradeModType = (UpgradeModTypes)objects[0];
                    EMM.UpgradeEnt_UpgradeTypeCom.UpgradeModType = upgradeModType;
                    switch (upgradeModType)
                    {
                        case UpgradeModTypes.None:
                            throw new Exception();

                        case UpgradeModTypes.Unit:
                            EMM.UpgradeEnt_XyCellCom.XyCell = (int[])objects[1];
                            break;

                        case UpgradeModTypes.Building:
                            EMM.UpgradeEnt_BuildingTypeCom.BuildingType = (BuildingTypes)objects[1];
                            break;

                        default:
                            throw new Exception();
                    }
                    SMM.TryInvokeRunSystem(nameof(UpgradeMasterSystem), SMM.RpcSystems);
                    break;

                default:
                    break;
            }

            SMM.VisibilityUnitsSystems.Run();

            SyncAllToMaster();
        }

        [PunRPC]
        private void GeneralRPC(RpcGeneralTypes rpcGeneralType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _currentNumber = 0;
            EGM.FromInfoEnt_FromInfoCom.FromInfo = infoFrom;

            switch (rpcGeneralType)
            {
                case RpcGeneralTypes.None:
                    throw new Exception();

                case RpcGeneralTypes.SetDonerActiveUI:
                    DownDonerUIWorker.SetDoned(Instance.IsMasterClient, (bool)objects[_currentNumber++]);
                    break;

                case RpcGeneralTypes.ActiveAmountMotionUI:
                    EGGUIM.MotionEnt_ActivatedCom.IsActivated = true;
                    break;

                case RpcGeneralTypes.GetAvailableCellsForSetting:
                    AvailableCellsEntsWorker.SetAllCellsCopy(AvailableCellTypes.SettingUnit, CellUnitsDataWorker.GetStartCellsForSettingUnit(Instance.LocalPlayer));
                    break;

                case RpcGeneralTypes.EndGame:
                    EGGUIM.EndGameEnt_EndGameCom.IsEndGame = true;
                    EGGUIM.EndGameEnt_EndGameCom.PlayerWinner = PhotonNetwork.PlayerList[(int)objects[_currentNumber++] - 1];
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
                    var mistakeType = (MistakeTypes)objects[_currentNumber++];
                    switch (mistakeType)
                    {
                        case MistakeTypes.None:
                            throw new Exception();

                        case MistakeTypes.Economy:
                            var haves = (bool[])objects[_currentNumber++];
                            var haveFood = haves[0];
                            var haveWood = haves[1];
                            var haveOre = haves[2];
                            var haveIron = haves[3];
                            var haveGold = haves[4];

                            if (!haveFood) MistakeEconomyEventDataWorker.InvokeEconomyMistake(ResourceTypes.Food);
                            if (!haveWood) MistakeEconomyEventDataWorker.InvokeEconomyMistake(ResourceTypes.Wood);
                            if (!haveOre) MistakeEconomyEventDataWorker.InvokeEconomyMistake(ResourceTypes.Ore);
                            if (!haveIron) MistakeEconomyEventDataWorker.InvokeEconomyMistake(ResourceTypes.Iron);
                            if (!haveGold) MistakeEconomyEventDataWorker.InvokeEconomyMistake(ResourceTypes.Gold);
                            break;

                        case MistakeTypes.NeedKing:
                            EGGUIM.DonerUIEnt_MistakeCom.MistakeUnityEvent.Invoke();
                            break;

                        case MistakeTypes.NeedSteps:
                            MistakeEconomyEventDataWorker.InvokeStepsMistake();
                            break;

                        case MistakeTypes.NeedOtherPlace:
                            MistakeEconomyEventDataWorker.InvokeNeedOtherPlace();
                            break;

                        default:
                            break;
                    }
                    break;

                case RpcGeneralTypes.GetUnit:
                    if ((bool)objects[_currentNumber++])
                    {
                        SelectorWorker.SelectedUnitType = (UnitTypes)objects[_currentNumber++];
                    }
                    break;

                case RpcGeneralTypes.SetUnit:
                    if ((bool)objects[_currentNumber++])
                    {
                        EGM.SelectorEnt_SelectorCom.IsStartSelectedDirect = true;
                        EGM.SelectorEnt_UnitTypeCom.UnitType = default;
                    }
                    break;

                case RpcGeneralTypes.Sound:
                    var soundEffectType = (SoundEffectTypes)objects[_currentNumber++];
                    SoundGameGeneralViewWorker.PlaySoundEffect(soundEffectType);
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        private void OtherRPC(RpcOtherTypes rpcOtherType, object[] objects, PhotonMessageInfo infoFrom)
        {
            _currentNumber = 0;
            EntOM.FromInfoEnt_FromInfoCom.FromInfo = infoFrom;

            switch (rpcOtherType)
            {
                case RpcOtherTypes.None:
                    throw new Exception();

                case RpcOtherTypes.SetAmountMotion:
                    EGGUIM.MotionEnt_AmountCom.AmountMotions = (int)objects[_currentNumber++];
                    break;

                case RpcOtherTypes.SetStepModType:
                    Instance.EntComM.SaverEnt_StepModeTypeCom.StepModeType = (StepModeTypes)objects[_currentNumber++];
                    break;

                default:
                    throw new Exception();
            }
        }

        #endregion


        #region SyncData

        internal void SyncAllToMaster() => _photonView.RPC(SyncMasterRPCName, RpcTarget.MasterClient);

        [PunRPC]
        private void SyncAllMaster()
        {
            List<object> listObjects = new List<object>();



            for (int x = 0; x < CellViewWorker.Xamount; x++)
                for (int y = 0; y < CellViewWorker.Yamount; y++)
                {
                    var xy = new int[] { x, y };

                    listObjects.Add(CellUnitsDataWorker.UnitType(xy));
                    listObjects.Add(CellUnitsDataWorker.IsVisibleUnit(false, xy));
                    listObjects.Add(CellUnitsDataWorker.AmountSteps(xy));
                    listObjects.Add(CellUnitsDataWorker.AmountHealth(xy));
                    listObjects.Add(CellUnitsDataWorker.ConditionType(xy));
                    if (CellUnitsDataWorker.HaveOwner(xy)) listObjects.Add(CellUnitsDataWorker.ActorNumber(xy));
                    else listObjects.Add(-2);
                    listObjects.Add(CellUnitsDataWorker.IsBot(xy));



                    listObjects.Add(CellBuildingsDataWorker.GetBuildingType(xy));
                    if (CellBuildingsDataWorker.HaveOwner(xy)) listObjects.Add(CellBuildingsDataWorker.ActorNumber(xy));
                    else listObjects.Add(-2);
                    listObjects.Add(CellBuildingsDataWorker.IsBot(xy));



                    listObjects.Add(CellEnvirDataWorker.GetAmountResources(EnvironmentTypes.Fertilizer, xy));
                    listObjects.Add(CellEnvirDataWorker.GetAmountResources(EnvironmentTypes.AdultForest, xy));
                    listObjects.Add(CellEnvirDataWorker.GetAmountResources(EnvironmentTypes.Hill, xy));

                    listObjects.Add(CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy));
                    listObjects.Add(CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.YoungForest, xy));
                    listObjects.Add(CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy));
                    listObjects.Add(CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, xy));
                    listObjects.Add(CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy));


                    listObjects.Add(CellFireDataWorker.HaveFire(xy));
                    listObjects.Add(CellFireDataWorker.TimeSteps(xy));
                }



            listObjects.Add(SaverComWorker.StepModeType);
            listObjects.Add(MiddleViewUIWorker.IsStartedGame);



            listObjects.Add(MiddleViewUIWorker.IsReady(false));
            listObjects.Add(DownDonerUIWorker.IsDoned(false));



            listObjects.Add(ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Food, false));
            listObjects.Add(ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Wood, false));
            listObjects.Add(ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Ore, false));
            listObjects.Add(ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Iron, false));
            listObjects.Add(ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Gold, false));



            for (UnitTypes unitTypeType = (UnitTypes)1; (byte)unitTypeType < Enum.GetNames(typeof(UnitTypes)).Length; unitTypeType++)
            {
                var amountUnitsInGame = InfoAmountUnitsWorker.GetAmountUnitsInGame(unitTypeType, false);
                listObjects.Add(amountUnitsInGame);
                for (int indexXy = 0; indexXy < amountUnitsInGame; indexXy++)
                {
                    listObjects.Add(InfoAmountUnitsWorker.GetXyUnitInGame(unitTypeType, false, indexXy));
                }
            }



            listObjects.Add(InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Farm, false));
            listObjects.Add(InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Woodcutter, false));
            listObjects.Add(InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Mine, false));

            for (BuildingTypes buildingType = (BuildingTypes)1; (byte)buildingType < Enum.GetNames(typeof(BuildingTypes)).Length; buildingType++)
            {
                var amountBuildingsInGame = InfoBuidlingsWorker.GetAmountBuild(buildingType, false);
                listObjects.Add(amountBuildingsInGame);
                for (int indexXy = 0; indexXy < amountBuildingsInGame; indexXy++)
                {
                    listObjects.Add(InfoBuidlingsWorker.GetXyBuildByIndex(buildingType, false, indexXy));
                }
            }




            var objects = new object[listObjects.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];

            _photonView.RPC(SyncOtherRPCName, RpcTarget.Others, objects);


            SGM.SyncCellVisionSystems.Run();
        }

        [PunRPC]
        private void SyncAllOther(object[] objects)
        {
            _currentNumber = 0;

            for (int x = 0; x < CellViewWorker.Xamount; x++)
                for (int y = 0; y < CellViewWorker.Yamount; y++)
                {
                    var xy = new int[] { x, y };

                    Player owner;

                    UnitTypes unitType = (UnitTypes)objects[_currentNumber++];
                    bool isVisibleUnit = (bool)objects[_currentNumber++];
                    int amountSteps = (int)objects[_currentNumber++];
                    int amountHealth = (int)objects[_currentNumber++];
                    ConditionUnitTypes conditionType = (ConditionUnitTypes)objects[_currentNumber++];
                    int actorNumber = (int)objects[_currentNumber++];
                    bool haveBot = (bool)objects[_currentNumber++];

                    if (unitType != UnitTypes.None)
                    {
                        if (actorNumber != -2)
                        {
                            owner = PhotonNetwork.PlayerList[actorNumber - 1];
                            CellUnitsDataWorker.SetPlayerUnit(unitType, amountHealth, amountSteps, conditionType, owner, xy);
                        }
                        else
                        {
                            CellUnitsDataWorker.SetBotUnit(unitType, haveBot, amountHealth, amountSteps, conditionType, xy);
                        }

                        CellUnitsDataWorker.SetIsVisibleUnit(Instance.IsMasterClient, isVisibleUnit, xy);
                    }

                    else
                    {
                        CellUnitsDataWorker.ResetUnit(xy);
                    }



                    BuildingTypes buildingType = (BuildingTypes)objects[_currentNumber++];
                    actorNumber = (int)objects[_currentNumber++];
                    haveBot = (bool)objects[_currentNumber++];

                    if (buildingType != BuildingTypes.None)
                    {
                        if (actorNumber != -2)
                        {
                            owner = PhotonNetwork.PlayerList[actorNumber - 1];
                            CellBuildingsDataWorker.SetPlayerBuilding(buildingType, owner, xy);
                        }
                        else
                        {
                            CellBuildingsDataWorker.SetBotBuilding(buildingType, xy);
                        }
                    }

                    else
                    {
                        CellBuildingsDataWorker.ResetBuild(xy);
                    }



                    int amountResourcesFertilizer = (int)objects[_currentNumber++];
                    int amountResourcesAdultForest = (int)objects[_currentNumber++];
                    int amountResourcesOre = (int)objects[_currentNumber++];

                    bool haveFertilizer = (bool)objects[_currentNumber++];
                    bool haveYoungTree = (bool)objects[_currentNumber++];
                    bool haveAdultForest = (bool)objects[_currentNumber++];
                    bool haveHill = (bool)objects[_currentNumber++];
                    bool haveMountain = (bool)objects[_currentNumber++];

                    CellEnvirDataWorker.SetEnvironment(EnvironmentTypes.Fertilizer, haveFertilizer, amountResourcesFertilizer, xy);
                    CellEnvirDataWorker.SetEnvironment(EnvironmentTypes.YoungForest, haveYoungTree, default, xy);
                    CellEnvirDataWorker.SetEnvironment(EnvironmentTypes.AdultForest, haveAdultForest, amountResourcesAdultForest, xy);
                    CellEnvirDataWorker.SetEnvironment(EnvironmentTypes.Hill, haveHill, amountResourcesOre, xy);
                    CellEnvirDataWorker.SetEnvironment(EnvironmentTypes.Mountain, haveMountain, default, xy);



                    bool haveFire = (bool)objects[_currentNumber++];
                    int timeStepsFire = (int)objects[_currentNumber++];

                    CellFireDataWorker.SyncFireData(haveFire, timeStepsFire, xy);
                }



            SaverComWorker.StepModeType = (StepModeTypes)objects[_currentNumber++];



            bool isStartedGame = (bool)objects[_currentNumber++];
            MiddleViewUIWorker.IsStartedGame = isStartedGame;



            bool isActivatedReadyButton = (bool)objects[_currentNumber++];
            MiddleViewUIWorker.SetIsReady(Instance.IsMasterClient, isActivatedReadyButton);



            bool isActivatedDoner = (bool)objects[_currentNumber++];
            DownDonerUIWorker.SetDoned(Instance.IsMasterClient, isActivatedDoner);



            var food = (int)objects[_currentNumber++];
            var wood = (int)objects[_currentNumber++];
            var ore = (int)objects[_currentNumber++];
            var iron = (int)objects[_currentNumber++];
            var gold = (int)objects[_currentNumber++];
            ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Food, Instance.IsMasterClient, food);
            ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Wood, Instance.IsMasterClient, wood);
            ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Ore, Instance.IsMasterClient, ore);
            ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Iron, Instance.IsMasterClient, iron);
            ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Gold, Instance.IsMasterClient, gold);



            for (UnitTypes unitTypeType = (UnitTypes)1; (byte)unitTypeType < Enum.GetNames(typeof(UnitTypes)).Length; unitTypeType++)
            {
                var amountUnits = (int)objects[_currentNumber++];

                List<int[]> xyUnits = new List<int[]>();
                for (int i = 0; i < amountUnits; i++)
                {
                    var xyUnit = (int[])objects[_currentNumber++];
                    xyUnits.Add(xyUnit);
                }
                InfoAmountUnitsWorker.SetAmountUnitInGame(unitTypeType, PhotonNetwork.IsMasterClient, xyUnits);
            }



            var amountFarmUpgrades = (int)objects[_currentNumber++];
            var amountWoodcutterUpgrades = (int)objects[_currentNumber++];
            var amountMineUpgrades = (int)objects[_currentNumber++];
            InfoBuidlingsWorker.SetAmountUpgrades(BuildingTypes.Farm, Instance.IsMasterClient, amountFarmUpgrades);
            InfoBuidlingsWorker.SetAmountUpgrades(BuildingTypes.Woodcutter, Instance.IsMasterClient, amountWoodcutterUpgrades);
            InfoBuidlingsWorker.SetAmountUpgrades(BuildingTypes.Mine, Instance.IsMasterClient, amountMineUpgrades);

            for (BuildingTypes buildingType = (BuildingTypes)1; (byte)buildingType < Enum.GetNames(typeof(BuildingTypes)).Length; buildingType++)
            {
                var amountBuildings = (int)objects[_currentNumber++];

                List<int[]> xyBuildings = new List<int[]>();
                for (int i = 0; i < amountBuildings; i++)
                {
                    var xyBuilding = (int[])objects[_currentNumber++];
                    xyBuildings.Add(xyBuilding);
                }
                InfoBuidlingsWorker.SetXyBuildings(buildingType, PhotonNetwork.IsMasterClient, xyBuildings);
            }



            SGM.SyncCellVisionSystems.Run();
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