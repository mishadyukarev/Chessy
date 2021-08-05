using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities.Game.General.UI.Vis.Containers;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.Workers.Game.Else.Data;
using Assets.Scripts.Workers.Game.UI.Middle.MistakeInfo;
using Assets.Scripts.Workers.Game.UI.Vis.Up;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Game.General.Systems.StartFill
{
    internal sealed class MainGameSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;
        [EcsIgnoreInject] private EcsWorld _commonWorld;

        private EcsFilter<GeneralZoneViewComponent> _generalZoneViewFilter;


        #region Else


        #region Center

        private static EcsEntity _endGameEnt;
        internal static ref EndGameComponent EndGameEnt_EndGameCom => ref _endGameEnt.Get<EndGameComponent>();
        internal static ref ParentComponent EndGameEnt_ParentCom => ref _endGameEnt.Get<ParentComponent>();
        internal static ref TextMeshProUGUIComponent EndGameEnt_TextMeshProGUICom => ref _endGameEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _readyEnt;
        internal static ref ParentComponent ReadyEnt_ParentCom => ref _readyEnt.Get<ParentComponent>();
        internal static ref ButtonComponent ReadyEnt_ButtonCom => ref _readyEnt.Get<ButtonComponent>();
        internal static ref ActivatedButtonDictComponent ReadyEnt_ActivatedDictCom => ref _readyEnt.Get<ActivatedButtonDictComponent>();
        internal static ref StartedGameComponent ReadyEnt_StartedGameCom => ref _readyEnt.Get<StartedGameComponent>();


        private static EcsEntity _joinDiscordEnt;
        internal static ref ButtonComponent JoinDiscordEnt_ButtonCom => ref _joinDiscordEnt.Get<ButtonComponent>();


        private static EcsEntity _motionEnt;
        internal static ref AmountMotionsComponent MotionEnt_AmountCom => ref _motionEnt.Get<AmountMotionsComponent>();
        internal static ref ActivatedComponent MotionEnt_ActivatedCom => ref _motionEnt.Get<ActivatedComponent>();
        internal static ref ParentComponent MotionEnt_ParentCom => ref _motionEnt.Get<ParentComponent>();
        internal static ref TextMeshProUGUIComponent MotionEnt_TextMeshProUGUICom => ref _motionEnt.Get<TextMeshProUGUIComponent>();


        private CenterSupTextUIViewContainer _mistakeVisUICont;



        #endregion


        #region Up


        private static EcsEntity _leaveEnt;
        internal static ref ButtonComponent LeaveEnt_ButtonCom => ref _leaveEnt.Get<ButtonComponent>();

        #endregion


        #region Down

        private  static  EcsEntity _donerUIEnt;

        internal  static  ref ButtonComponent DonerUIEnt_ButtonCom => ref _donerUIEnt.Get<ButtonComponent>();
        internal  static  ref ActivatedButtonDictComponent DonerUIEnt_IsActivatedDictCom => ref _donerUIEnt.Get<ActivatedButtonDictComponent>();
        internal  static  ref MistakeComponent DonerUIEnt_MistakeCom => ref _donerUIEnt.Get<MistakeComponent>();


        private  static  EcsEntity _finderIdleEntity;
        internal static ref ButtonComponent FinderIdleEnt_ButtonCom => ref _finderIdleEntity.Get<ButtonComponent>();


        #region Takers

        private static  EcsEntity _takerKingEntity;
        internal static  ref UnitTypeComponent TakerKingEnt_UnitTypeCom => ref _takerKingEntity.Get<UnitTypeComponent>();
        internal static  ref ButtonComponent TakerKingEnt_ButtonCom => ref _takerKingEntity.Get<ButtonComponent>();
        internal static  ref TextMeshProUGUIComponent TakerKingEntityTextMeshProGUIComponent => ref _takerKingEntity.Get<TextMeshProUGUIComponent>();


        private static  EcsEntity _takerPawnEntity;
        internal static  ref UnitTypeComponent TakerPawnEntityUnitTypeComponent => ref _takerPawnEntity.Get<UnitTypeComponent>();
        internal static  ref ButtonComponent TakerPawnEntityButtonComponent => ref _takerPawnEntity.Get<ButtonComponent>();
        internal static ref TextMeshProUGUIComponent TakerPawnEntityTextMeshProGUIComponent => ref _takerPawnEntity.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _takerRookEntity;
        internal static ref UnitTypeComponent TakerRookEntityUnitTypeComponent => ref _takerRookEntity.Get<UnitTypeComponent>();
        internal static ref ButtonComponent TakerRookEntityButtonComponent => ref _takerRookEntity.Get<ButtonComponent>();
        internal static ref TextMeshProUGUIComponent TakerRookEntityTextMeshProGUIComponent => ref _takerRookEntity.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _takerBishopEntity;
        internal static ref UnitTypeComponent TakerBishopEntityUnitTypeComponent => ref _takerBishopEntity.Get<UnitTypeComponent>();
        internal static ref ButtonComponent TakerBishopEntityButtonComponent => ref _takerBishopEntity.Get<ButtonComponent>();
        internal static ref TextMeshProUGUIComponent TakerBishopEntityTextMeshProGUIComponent => ref _takerBishopEntity.Get<TextMeshProUGUIComponent>();

        #endregion

        #endregion


        #region Right

        private static EcsEntity _rightZoneEnt;
        internal static ref ParentComponent RightZoneEnt_ParentCom => ref _rightZoneEnt.Get<ParentComponent>();


        #region StatsZone

        private static EcsEntity _statsZoneEnt;
        internal static ref ParentComponent StatsEnt_ParentCom => ref _statsZoneEnt.Get<ParentComponent>();


        private static EcsEntity _healthUIEnt;
        internal static ref TextMeshProUGUIComponent HealthUIEnt_TextMeshProUGUICom => ref _healthUIEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _powerAttackUIEnt;
        internal static ref TextMeshProUGUIComponent DamageUIEnt_TextMeshProUGUICom => ref _powerAttackUIEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _powerProtectionUIEnt;
        internal static ref TextMeshProUGUIComponent PowerProtectionUIEnt_TextMeshProUGUICom => ref _powerProtectionUIEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _amountStepsUIEnt;
        internal static ref TextMeshProUGUIComponent AmountStepsUIEnt_TextMeshProUGUICom => ref _amountStepsUIEnt.Get<TextMeshProUGUIComponent>();

        #endregion


        #region StandartAbilities

        private static EcsEntity _conditionZoneEnt;
        internal static ref ParentComponent ConditionZoneEnt_ParentGOCom => ref _conditionZoneEnt.Get<ParentComponent>();
        internal static ref TextMeshProUGUIComponent ConditionZoneEnt_TextMeshProUGUICom => ref _conditionZoneEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _protectConditionEnt;
        internal static ref ButtonComponent ProtectConditionEnt_ButtonCom => ref _protectConditionEnt.Get<ButtonComponent>();


        private static EcsEntity _relaxConditionEnt;
        internal static ref ButtonComponent RelaxConditionEnt_ButtonCom => ref _relaxConditionEnt.Get<ButtonComponent>();

        #endregion


        #region UniqueAbilities

        private static EcsEntity _uniquePareZoneEnt;
        internal static ref TextMeshProUGUIComponent UniquePareZoneEnt_TextMeshProUGUICom => ref _uniquePareZoneEnt.Get<TextMeshProUGUIComponent>();
        internal static ref ParentComponent UniquePareZoneEnt_ParentCom => ref _uniquePareZoneEnt.Get<ParentComponent>();


        private static EcsEntity _uniqueFirstAbilityEnt;
        internal static ref ButtonComponent Unique1AbilityEnt_ButtonCom => ref _uniqueFirstAbilityEnt.Get<ButtonComponent>();
        internal static ref TextMeshProUGUIComponent Unique1AbilityEnt_TextMeshProGUICom => ref _uniqueFirstAbilityEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _uniqueSecondAbilityEnt;
        internal static ref ButtonComponent Unique2AbilityEnt_ButtonCom => ref _uniqueSecondAbilityEnt.Get<ButtonComponent>();
        internal static ref TextMeshProUGUIComponent Unique2AbilityEnt_TextMeshProGUICom => ref _uniqueSecondAbilityEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _uniqueThirdAbilityEnt;
        internal static ref ButtonComponent Unique3AbilityEnt_ButtonCom => ref _uniqueThirdAbilityEnt.Get<ButtonComponent>();
        internal static ref TextMeshProUGUIComponent Unique3AbilityEnt_TextMeshProGUICom => ref _uniqueThirdAbilityEnt.Get<TextMeshProUGUIComponent>();

        #endregion


        #region BuildingAbilities

        private static EcsEntity _buildingAbilitiesZoneEnt;
        internal static ref ParentComponent BuildingAbilitiesZoneEnt_ParentCom => ref _buildingAbilitiesZoneEnt.Get<ParentComponent>();
        internal static ref TextMeshProUGUIComponent BuildingAbilitiesZoneEnt_TextMeshProUGUICom => ref _buildingAbilitiesZoneEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _buildingFirstAbilityEnt;
        internal static ref ButtonComponent BuildingFirstAbilityEnt_ButtonCom => ref _buildingFirstAbilityEnt.Get<ButtonComponent>();
        internal static ref TextMeshProUGUIComponent BuildingFirstAbilityEnt_TextMeshProGUICom => ref _buildingFirstAbilityEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _buildingSecondAbilityEnt;
        internal static ref ButtonComponent BuildingSecondAbilityEnt_ButtonCom => ref _buildingSecondAbilityEnt.Get<ButtonComponent>();
        internal static ref TextMeshProUGUIComponent BuildingSecondAbilityEnt_TextMeshProGUICom => ref _buildingSecondAbilityEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _buildingThirdAbilityEnt;
        internal static ref ButtonComponent BuildingThirdAbilityEnt_ButtonCom => ref _buildingThirdAbilityEnt.Get<ButtonComponent>();
        internal static ref TextMeshProUGUIComponent BuildingThirdAbilityEnt_TextMeshProGUICom => ref _buildingThirdAbilityEnt.Get<TextMeshProUGUIComponent>();

        #endregion

        #endregion


        #region Left

        private static EcsEntity _leftZoneEnt;
        internal static ref ParentComponent LeftZoneEnt_ParentCom => ref _leftZoneEnt.Get<ParentComponent>();


        #region BuildingZone

        private static EcsEntity _buildingZoneEnt;
        internal static ref ParentComponent BuildingZoneEnt_ParentCom => ref _buildingZoneEnt.Get<ParentComponent>();



        private static EcsEntity _meltOreUIEnt;
        internal static ref ButtonComponent MeltOreEnt_ButtonCom => ref _meltOreUIEnt.Get<ButtonComponent>();



        private static EcsEntity _buyPawnUIEnt;
        internal static ref ButtonComponent BuyPawnUIEnt_ButtonCom => ref _buyPawnUIEnt.Get<ButtonComponent>();


        private static EcsEntity _buyRookUIEnt;
        internal static ref ButtonComponent BuyRookUIEnt_ButtonCom => ref _buyRookUIEnt.Get<ButtonComponent>();


        private static EcsEntity _buyBishopUIEnt;
        internal static ref ButtonComponent BuyBishopUIEnt_ButtonCom => ref _buyBishopUIEnt.Get<ButtonComponent>();



        private static EcsEntity _upgradeUnitUIEnt;
        internal static ref ButtonComponent UpgradeUnitUIEnt_ButtonCom => ref _upgradeUnitUIEnt.Get<ButtonComponent>();



        private static EcsEntity _upgradeFarmUIEnt;
        internal static ref ButtonComponent UpgradeFarmUIEnt_ButtonCom => ref _upgradeFarmUIEnt.Get<ButtonComponent>();


        private static EcsEntity _upgradeWoodcutterUIEnt;
        internal static ref ButtonComponent UpgradeWoodcutterUIEnt_ButtonCom => ref _upgradeWoodcutterUIEnt.Get<ButtonComponent>();


        private static EcsEntity _upgradeMineUIEnt;
        internal static ref ButtonComponent UpgradeMineUIEnt_ButtonCom => ref _upgradeMineUIEnt.Get<ButtonComponent>();

        #endregion


        #region EnvironmentZone

        private static EcsEntity _environmentZoneEnt;
        internal static ref ParentComponent EnvironmentZoneEnt_ParentCom => ref _environmentZoneEnt.Get<ParentComponent>();


        private static EcsEntity _environmentInfoEnt;
        internal static ref ButtonComponent EnvironmentInfoEnt_ButtonCom => ref _environmentInfoEnt.Get<ButtonComponent>();
        internal static ref ActivatedComponent EnvironmentInfoEnt_IsActivatedCom => ref _environmentZoneEnt.Get<ActivatedComponent>();


        private static EcsEntity _envFertilizerEnt;
        internal static ref TextMeshProUGUIComponent EnvFerilizerEnt_TextMeshProUGUICom => ref _envFertilizerEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _envForestEnt;
        internal static ref TextMeshProUGUIComponent EnvForestEnt_TextMeshProUGUICom => ref _envForestEnt.Get<TextMeshProUGUIComponent>();


        private static EcsEntity _envOreEnt;
        internal static ref TextMeshProUGUIComponent EnvOreEnt_TextMeshProUGUICom => ref _envOreEnt.Get<TextMeshProUGUIComponent>();

        #endregion

        #endregion



        private static EcsEntity _infoUnitsEnt;
        internal static ref XyUnitsComponent XyUnitsCom => ref _infoUnitsEnt.Get<XyUnitsComponent>();
        internal static ref XyUnitsContitionComponent XyUnitsContitionCom => ref _infoUnitsEnt.Get<XyUnitsContitionComponent>();


        private static EcsEntity _infoBuildingsEnt;
        internal static ref XyBuildingsComponent XyBuildingsCom => ref _infoBuildingsEnt.Get<XyBuildingsComponent>();
        internal static ref UpgradesBuildingsComponent UpgradesBuildingsCom => ref _infoBuildingsEnt.Get<UpgradesBuildingsComponent>();


        private static EcsEntity _inventor;
        internal static ref UnitInventorComponent UnitInventorCom => ref _inventor.Get<UnitInventorComponent>();


        private static EcsEntity _infoCellsEnt;
        internal static ref XyStartCellsComponent XyStartCellsCom => ref _infoCellsEnt.Get<XyStartCellsComponent>();


        private static EcsEntity _fromInfoEnt;
        internal static ref FromInfoComponent FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();


        private static EcsEntity _mistakeEconomyEventEnt;
        internal static ref MistakeEconomyComponent MistakeEconomyCom => ref _mistakeEconomyEventEnt.Get<MistakeEconomyComponent>();

        #endregion

        internal MainGameSystem(EcsWorld commonWorld)
        {
            _commonWorld = commonWorld;
        }

        public void Init()
        {
            ref var toggleZoneCom = ref _commonWorld.GetPool<ToggleZoneComponent>().GetItem(0);
            ref var canvasCom = ref _commonWorld.GetPool<CanvasComponent>().GetItem(0);
            ref var resourcesCom = ref _commonWorld.GetPool<ResourcesComponent>().GetItem(0);

            toggleZoneCom.ReplaceZone(Main.SceneType);
            canvasCom.ReplaceZone(Main.SceneType, resourcesCom);


            var generalZoneGO = new GameObject("GeneralZone");
            toggleZoneCom.Attach(generalZoneGO.transform);

            var backGroundGO = GameObject.Instantiate(resourcesCom.PrefabConfig.BackGroundCollider2D,
                Main.Instance.transform.position + new Vector3(7, 5.5f, 2), Main.Instance.transform.rotation);

            _gameWorld.NewEntity()
                .Replace(new GeneralZoneViewComponent(generalZoneGO))
                .Replace(new BackgroundComponent(backGroundGO));

            ref var generalZoneCom = ref _generalZoneViewFilter.Get1(0);
            generalZoneCom.Attach(backGroundGO.transform);


            _infoUnitsEnt = _gameWorld.NewEntity()
                .Replace(new XyUnitsComponent(new Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>()))
                .Replace(new XyUnitsContitionComponent(new Dictionary<ConditionUnitTypes, Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>>()));

            _infoBuildingsEnt = _gameWorld.NewEntity()
                .Replace(new XyBuildingsComponent(new Dictionary<BuildingTypes, Dictionary<bool, List<int[]>>>()))
                .Replace(new UpgradesBuildingsComponent(new Dictionary<BuildingTypes, Dictionary<bool, int>>()));

            _inventor = _gameWorld.NewEntity()
                .Replace(new UnitInventorComponent(new Dictionary<UnitTypes, Dictionary<bool, int>>()));

            _fromInfoEnt = _gameWorld.NewEntity()
                .Replace(new FromInfoComponent());

            _mistakeEconomyEventEnt = _gameWorld.NewEntity()
                .Replace(new MistakeEconomyComponent(new Dictionary<ResourceTypes, UnityEvent>()));

            new MistakeEconomyEventDataWorker(_gameWorld);

            var listMaster = new List<int[]>();
            var listOther = new List<int[]>();

            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    if (y < 3 && x > 2 && x < 12)
                    {
                        listMaster.Add(new int[] { x, y });
                    }
                    else if (y > 7 && x > 2 && x < 12)
                    {
                        listOther.Add(new int[] { x, y });
                    }
                }
            var dict = new Dictionary<bool, List<int[]>>();
            dict.Add(true, listMaster);
            dict.Add(false, listOther);

            _infoCellsEnt = _gameWorld.NewEntity()
                .Replace(new XyStartCellsComponent(dict));








            if (PhotonNetwork.IsMasterClient)
            {
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, true, EconomyValues.AMOUNT_KING_MASTER);
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, false, EconomyValues.AMOUNT_KING_OTHER);

                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, true, EconomyValues.AMOUNT_PAWN_MASTER);
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, false, EconomyValues.AMOUNT_PAWN_OTHER);

                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, true, EconomyValues.AMOUNT_ROOK_MASTER);
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, false, EconomyValues.AMOUNT_ROOK_OTHER);

                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, true, EconomyValues.AMOUNT_BISHOP_MASTER);
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, false, EconomyValues.AMOUNT_BISHOP_OTHER);


                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Food, true, EconomyValues.AMOUNT_FOOD_MASTER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Wood, true, EconomyValues.AMOUNT_WOOD_MASTER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Ore, true, EconomyValues.AMOUNT_ORE_MASTER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Iron, true, EconomyValues.AMOUNT_IRON_MASTER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Gold, true, EconomyValues.AMOUNT_GOLD_MASTER);

                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Food, false, EconomyValues.AMOUNT_FOOD_OTHER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Wood, false, EconomyValues.AMOUNT_WOOD_OTHER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Ore, false, EconomyValues.AMOUNT_ORE_OTHER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Iron, false, EconomyValues.AMOUNT_IRON_OTHER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Gold, false, EconomyValues.AMOUNT_GOLD_OTHER);
            }




            _leaveEnt = _gameWorld.NewEntity()
        .Replace(new ButtonComponent(MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent<Button>("ButtonLeave")));


            #region Center

            var middleZone = MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent("MiddleZone");

            var theEndGameZone = MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent("TheEndGameZone");
            _endGameEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(theEndGameZone))
                .Replace(new TextMeshProUGUIComponent(theEndGameZone.transform.Find("TheEndGameText").GetComponent<TextMeshProUGUI>()))
                .Replace(new EndGameComponent());


            var readyZone = MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent("ReadyZone");

            _readyEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(readyZone))
                .Replace(new ButtonComponent(readyZone.transform.Find("ReadyButton").GetComponent<Button>()))
                .Replace(new ActivatedButtonDictComponent(new Dictionary<bool, bool>()))
                .Replace(new TextMeshProUGUIComponent())
                .Replace(new StartedGameComponent());


            _joinDiscordEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(readyZone.transform.Find("JoinDiscordButton").GetComponent<Button>()));
            JoinDiscordEnt_ButtonCom.Button.onClick.AddListener(delegate { Application.OpenURL("https://discord.gg/yxfZnrkBPU"); });


            var motionZone = MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent("MotionZone");
            _motionEnt = _gameWorld.NewEntity()
                .Replace(new ActivatedComponent())
                .Replace(new AmountMotionsComponent())
                .Replace(new ParentComponent(motionZone))
                .Replace(new TextMeshProUGUIComponent(motionZone.transform.Find("MotionText").GetComponent<TextMeshProUGUI>()));



            _mistakeVisUICont = new CenterSupTextUIViewContainer(middleZone, _gameWorld);
            new CenterSupTextUIViewWorker(_mistakeVisUICont);


            #endregion


            #region Up

            var upZoneGO = MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent("UpZone");

            new ResourcesViewUIWorker(_gameWorld, upZoneGO);


            #endregion


            #region Down


            var downZone = MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent("DownZone");


            var takeUnitZone = downZone.transform.Find("TakeUnitZone");


            _takerKingEntity = _gameWorld.NewEntity()
                .Replace(new UnitTypeComponent(UnitTypes.King))
                .Replace(new ButtonComponent(takeUnitZone.transform.Find("TakeUnit0Button").GetComponent<Button>()))
                .Replace(new TextMeshProUGUIComponent());

            _takerPawnEntity = _gameWorld.NewEntity()
                .Replace(new UnitTypeComponent(UnitTypes.Pawn))
                .Replace(new ButtonComponent(takeUnitZone.transform.Find("TakeUnit1Button").GetComponent<Button>()))
                .Replace(new TextMeshProUGUIComponent());

            _takerRookEntity = _gameWorld.NewEntity()
                .Replace(new UnitTypeComponent(UnitTypes.Rook))
                .Replace(new ButtonComponent(takeUnitZone.transform.Find("TakeUnit2Button").GetComponent<Button>()))
                .Replace(new TextMeshProUGUIComponent());

            _takerBishopEntity = _gameWorld.NewEntity()
                .Replace(new UnitTypeComponent(UnitTypes.Bishop))
                .Replace(new ButtonComponent(takeUnitZone.transform.Find("TakeUnit3Button").GetComponent<Button>()))
                .Replace(new TextMeshProUGUIComponent());


            _donerUIEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(downZone.transform.Find("DonerButton").GetComponent<Button>()))
                .Replace(new ActivatedButtonDictComponent(new Dictionary<bool, bool>()))
                .Replace(new MistakeComponent(new UnityEvent(), default));



            _finderIdleEntity = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(downZone.transform.Find("FinderIdleButton").GetComponent<Button>()));


            #endregion


            #region Left



            var leftZoneGO = MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent("LeftZone");
            _leftZoneEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(leftZoneGO));


            #region BuildingZone

            var buildingZoneGO = leftZoneGO.transform.Find("BuildingZone").gameObject;

            _buildingZoneEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(buildingZoneGO));

            _meltOreUIEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingZoneGO.transform.Find("MeltOreButton").GetComponent<Button>()));

            _buyPawnUIEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingZoneGO.transform.Find("BuyPawnButton").GetComponent<Button>()));

            _buyRookUIEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingZoneGO.transform.Find("BuyRookButton").GetComponent<Button>()));

            _buyBishopUIEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingZoneGO.transform.Find("BuyBishopButton").GetComponent<Button>()));

            _upgradeUnitUIEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingZoneGO.transform.Find("UpgradeUnitButton").GetComponent<Button>()));

            _upgradeFarmUIEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingZoneGO.transform.Find("UpgradeFarmButton").GetComponent<Button>()));

            _upgradeWoodcutterUIEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingZoneGO.transform.Find("UpgradeWoodcutterButton").GetComponent<Button>()));

            _upgradeMineUIEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingZoneGO.transform.Find("UpgradeMineButton").GetComponent<Button>()));

            #endregion


            #region EnvironmentZone

            var environmentZoneGO = leftZoneGO.transform.Find("EnvironmentZone").gameObject;


            _environmentZoneEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(environmentZoneGO));

            _environmentInfoEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(environmentZoneGO.transform.Find("EnvironmentInfoButton").GetComponent<Button>()));

            _envFertilizerEnt = _gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(environmentZoneGO.transform.Find("FertilizerResourcesText").GetComponent<TextMeshProUGUI>()));

            _envForestEnt = _gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(environmentZoneGO.transform.Find("ForestResourcesText").GetComponent<TextMeshProUGUI>()));

            _envOreEnt = _gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(environmentZoneGO.transform.Find("OreResourcesText").GetComponent<TextMeshProUGUI>()));


            #endregion

            #endregion


            #region RightZone

            var rightZoneGO = MainCommonSystem.CommonEnt_CanvasCom.FindUnderParent("RightZone");

            _rightZoneEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(rightZoneGO));


            var statsZoneGO = rightZoneGO.transform.Find("StatsZone").gameObject;

            _statsZoneEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(statsZoneGO));

            _healthUIEnt = _gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(statsZoneGO.transform.Find("HpCurrentUnitText").GetComponent<TextMeshProUGUI>()));

            _powerAttackUIEnt = _gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(statsZoneGO.transform.Find("DamageCurrentUnitText").GetComponent<TextMeshProUGUI>()));

            _powerProtectionUIEnt = _gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(statsZoneGO.transform.Find("ProtectionCurrentUnitText").GetComponent<TextMeshProUGUI>()));

            _amountStepsUIEnt = _gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(statsZoneGO.transform.Find("StepsCurrentUnitText").GetComponent<TextMeshProUGUI>()));









            var conditionZoneGO = rightZoneGO.transform.Find("ConditionZone").gameObject;
            _conditionZoneEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(conditionZoneGO))
                .Replace(new TextMeshProUGUIComponent(conditionZoneGO.transform.Find("StandartAbilityText").GetComponent<TextMeshProUGUI>()));

            _protectConditionEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(conditionZoneGO.transform.Find("StandartAbilityButton1").GetComponent<Button>()));

            _relaxConditionEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(conditionZoneGO.transform.Find("StandartAbilityButton2").GetComponent<Button>()));



            var uniqueAbilitiesZoneGO = rightZoneGO.transform.Find("UniqueAbilitiesZone").gameObject;
            _uniquePareZoneEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(uniqueAbilitiesZoneGO))
                .Replace(new TextMeshProUGUIComponent(uniqueAbilitiesZoneGO.transform.Find("UniqueAbilitiesText").GetComponent<TextMeshProUGUI>()));


            var uniqueAbilityButton1 = uniqueAbilitiesZoneGO.transform.Find("UniqueAbilityButton1").GetComponent<Button>();
            _uniqueFirstAbilityEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(uniqueAbilityButton1))
                .Replace(new TextMeshProUGUIComponent(uniqueAbilityButton1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));

            var uniqueAbilityButton2 = uniqueAbilitiesZoneGO.transform.Find("UniqueAbilityButton2").GetComponent<Button>();
            _uniqueSecondAbilityEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(uniqueAbilityButton2))
                .Replace(new TextMeshProUGUIComponent(uniqueAbilityButton2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));

            var uniqueAbilityButton3 = uniqueAbilitiesZoneGO.transform.Find("UniqueAbilityButton3").GetComponent<Button>();
            _uniqueThirdAbilityEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(uniqueAbilityButton3))
                .Replace(new TextMeshProUGUIComponent(uniqueAbilityButton3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));



            var buildingZoneG = rightZoneGO.transform.Find("BuildingZone").gameObject;

            _buildingAbilitiesZoneEnt = _gameWorld.NewEntity()
                .Replace(new ParentComponent(buildingZoneG))
                .Replace(new TextMeshProUGUIComponent(buildingZoneG.transform.Find("BuildingAbilitiesText").GetComponent<TextMeshProUGUI>()));




            var buildingFirstAbilityButtom = buildingZoneG.transform.Find("BuildingAbilityButton1").GetComponent<Button>();
            _buildingFirstAbilityEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingFirstAbilityButtom))
                .Replace(new TextMeshProUGUIComponent(buildingFirstAbilityButtom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));


            _buildingSecondAbilityEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingZoneG.transform.Find("BuildingAbilityButton2").GetComponent<Button>()));


            var buildingThirdAbilityButtom = buildingZoneG.transform.Find("BuildingAbilityButton3").GetComponent<Button>();
            _buildingThirdAbilityEnt = _gameWorld.NewEntity()
                .Replace(new ButtonComponent(buildingThirdAbilityButtom))
                .Replace(new TextMeshProUGUIComponent(buildingThirdAbilityButtom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));

            #endregion

        }
    }
}
