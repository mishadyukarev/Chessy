using Assets.Scripts.ECS.Entities.Game.General.UI.Vis.Containers;
using Assets.Scripts.Workers.Common;
using Assets.Scripts.Workers.Game.UI.Middle.MistakeInfo;
using Assets.Scripts.Workers.Game.UI.Vis.Up;
using Leopotam.Ecs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class EntitiesGameGeneralUIViewManager
{

    #region Center

    private EcsEntity _endGameEnt;
    internal ref EndGameComponent EndGameEnt_EndGameCom => ref _endGameEnt.Get<EndGameComponent>();
    internal ref ParentComponent EndGameEnt_ParentCom => ref _endGameEnt.Get<ParentComponent>();
    internal ref TextMeshProUGUIComponent EndGameEnt_TextMeshProGUICom => ref _endGameEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _readyEnt;
    internal ref ParentComponent ReadyEnt_ParentCom => ref _readyEnt.Get<ParentComponent>();
    internal ref ButtonComponent ReadyEnt_ButtonCom => ref _readyEnt.Get<ButtonComponent>();
    internal ref ActivatedButtonDictComponent ReadyEnt_ActivatedDictCom => ref _readyEnt.Get<ActivatedButtonDictComponent>();
    internal ref StartedGameComponent ReadyEnt_StartedGameCom => ref _readyEnt.Get<StartedGameComponent>();


    private EcsEntity _joinDiscordEnt;
    internal ref ButtonComponent JoinDiscordEnt_ButtonCom => ref _joinDiscordEnt.Get<ButtonComponent>();


    private EcsEntity _motionEnt;
    internal ref AmountMotionsComponent MotionEnt_AmountCom => ref _motionEnt.Get<AmountMotionsComponent>();
    internal ref ActivatedComponent MotionEnt_ActivatedCom => ref _motionEnt.Get<ActivatedComponent>();
    internal ref ParentComponent MotionEnt_ParentCom => ref _motionEnt.Get<ParentComponent>();
    internal ref TextMeshProUGUIComponent MotionEnt_TextMeshProUGUICom => ref _motionEnt.Get<TextMeshProUGUIComponent>();


    private CenterSupTextUIViewContainer _mistakeVisUICont;



    #endregion


    #region Up

    #region Resources

    private ResourcesVisUIContainer _resourcesVisUIContainer;

    #endregion


    private EcsEntity _leaveEnt;
    internal ref ButtonComponent LeaveEnt_ButtonCom => ref _leaveEnt.Get<ButtonComponent>();

    #endregion


    #region Down

    private EcsEntity _donerUIEnt;

    internal ref ButtonComponent DonerUIEnt_ButtonCom => ref _donerUIEnt.Get<ButtonComponent>();
    internal ref ActivatedButtonDictComponent DonerUIEnt_IsActivatedDictCom => ref _donerUIEnt.Get<ActivatedButtonDictComponent>();
    internal ref MistakeComponent DonerUIEnt_MistakeCom => ref _donerUIEnt.Get<MistakeComponent>();


    private EcsEntity _finderIdleEntity;
    internal ref ButtonComponent FinderIdleEnt_ButtonCom => ref _finderIdleEntity.Get<ButtonComponent>();


    #region Takers

    private EcsEntity _takerKingEntity;
    internal ref UnitTypeComponent TakerKingEnt_UnitTypeCom => ref _takerKingEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerKingEnt_ButtonCom => ref _takerKingEntity.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent TakerKingEntityTextMeshProGUIComponent => ref _takerKingEntity.Get<TextMeshProUGUIComponent>();


    private EcsEntity _takerPawnEntity;
    internal ref UnitTypeComponent TakerPawnEntityUnitTypeComponent => ref _takerPawnEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerPawnEntityButtonComponent => ref _takerPawnEntity.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent TakerPawnEntityTextMeshProGUIComponent => ref _takerPawnEntity.Get<TextMeshProUGUIComponent>();


    private EcsEntity _takerRookEntity;
    internal ref UnitTypeComponent TakerRookEntityUnitTypeComponent => ref _takerRookEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerRookEntityButtonComponent => ref _takerRookEntity.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent TakerRookEntityTextMeshProGUIComponent => ref _takerRookEntity.Get<TextMeshProUGUIComponent>();


    private EcsEntity _takerBishopEntity;
    internal ref UnitTypeComponent TakerBishopEntityUnitTypeComponent => ref _takerBishopEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerBishopEntityButtonComponent => ref _takerBishopEntity.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent TakerBishopEntityTextMeshProGUIComponent => ref _takerBishopEntity.Get<TextMeshProUGUIComponent>();

    #endregion

    #endregion


    #region Right

    private EcsEntity _rightZoneEnt;
    internal ref ParentComponent RightZoneEnt_ParentCom => ref _rightZoneEnt.Get<ParentComponent>();


    #region StatsZone

    private EcsEntity _statsZoneEnt;
    internal ref ParentComponent StatsEnt_ParentCom => ref _statsZoneEnt.Get<ParentComponent>();


    private EcsEntity _healthUIEnt;
    internal ref TextMeshProUGUIComponent HealthUIEnt_TextMeshProUGUICom => ref _healthUIEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _powerAttackUIEnt;
    internal ref TextMeshProUGUIComponent DamageUIEnt_TextMeshProUGUICom => ref _powerAttackUIEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _powerProtectionUIEnt;
    internal ref TextMeshProUGUIComponent PowerProtectionUIEnt_TextMeshProUGUICom => ref _powerProtectionUIEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _amountStepsUIEnt;
    internal ref TextMeshProUGUIComponent AmountStepsUIEnt_TextMeshProUGUICom => ref _amountStepsUIEnt.Get<TextMeshProUGUIComponent>();

    #endregion


    #region StandartAbilities

    private EcsEntity _conditionZoneEnt;
    internal ref ParentComponent ConditionZoneEnt_ParentGOCom => ref _conditionZoneEnt.Get<ParentComponent>();
    internal ref TextMeshProUGUIComponent ConditionZoneEnt_TextMeshProUGUICom => ref _conditionZoneEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _protectConditionEnt;
    internal ref ButtonComponent ProtectConditionEnt_ButtonCom => ref _protectConditionEnt.Get<ButtonComponent>();


    private EcsEntity _relaxConditionEnt;
    internal ref ButtonComponent RelaxConditionEnt_ButtonCom => ref _relaxConditionEnt.Get<ButtonComponent>();

    #endregion


    #region UniqueAbilities

    private EcsEntity _uniquePareZoneEnt;
    internal ref TextMeshProUGUIComponent UniquePareZoneEnt_TextMeshProUGUICom => ref _uniquePareZoneEnt.Get<TextMeshProUGUIComponent>();
    internal ref ParentComponent UniquePareZoneEnt_ParentCom => ref _uniquePareZoneEnt.Get<ParentComponent>();


    private EcsEntity _uniqueFirstAbilityEnt;
    internal ref ButtonComponent Unique1AbilityEnt_ButtonCom => ref _uniqueFirstAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent Unique1AbilityEnt_TextMeshProGUICom => ref _uniqueFirstAbilityEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _uniqueSecondAbilityEnt;
    internal ref ButtonComponent Unique2AbilityEnt_ButtonCom => ref _uniqueSecondAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent Unique2AbilityEnt_TextMeshProGUICom => ref _uniqueSecondAbilityEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _uniqueThirdAbilityEnt;
    internal ref ButtonComponent Unique3AbilityEnt_ButtonCom => ref _uniqueThirdAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent Unique3AbilityEnt_TextMeshProGUICom => ref _uniqueThirdAbilityEnt.Get<TextMeshProUGUIComponent>();

    #endregion


    #region BuildingAbilities

    private EcsEntity _buildingAbilitiesZoneEnt;
    internal ref ParentComponent BuildingAbilitiesZoneEnt_ParentCom => ref _buildingAbilitiesZoneEnt.Get<ParentComponent>();
    internal ref TextMeshProUGUIComponent BuildingAbilitiesZoneEnt_TextMeshProUGUICom => ref _buildingAbilitiesZoneEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _buildingFirstAbilityEnt;
    internal ref ButtonComponent BuildingFirstAbilityEnt_ButtonCom => ref _buildingFirstAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent BuildingFirstAbilityEnt_TextMeshProGUICom => ref _buildingFirstAbilityEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _buildingSecondAbilityEnt;
    internal ref ButtonComponent BuildingSecondAbilityEnt_ButtonCom => ref _buildingSecondAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent BuildingSecondAbilityEnt_TextMeshProGUICom => ref _buildingSecondAbilityEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _buildingThirdAbilityEnt;
    internal ref ButtonComponent BuildingThirdAbilityEnt_ButtonCom => ref _buildingThirdAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent BuildingThirdAbilityEnt_TextMeshProGUICom => ref _buildingThirdAbilityEnt.Get<TextMeshProUGUIComponent>();

    #endregion

    #endregion


    #region Left

    private EcsEntity _leftZoneEnt;
    internal ref ParentComponent LeftZoneEnt_ParentCom => ref _leftZoneEnt.Get<ParentComponent>();


    #region BuildingZone

    private EcsEntity _buildingZoneEnt;
    internal ref ParentComponent BuildingZoneEnt_ParentCom => ref _buildingZoneEnt.Get<ParentComponent>();



    private EcsEntity _meltOreUIEnt;
    internal ref ButtonComponent MeltOreEnt_ButtonCom => ref _meltOreUIEnt.Get<ButtonComponent>();



    private EcsEntity _buyPawnUIEnt;
    internal ref ButtonComponent BuyPawnUIEnt_ButtonCom => ref _buyPawnUIEnt.Get<ButtonComponent>();


    private EcsEntity _buyRookUIEnt;
    internal ref ButtonComponent BuyRookUIEnt_ButtonCom => ref _buyRookUIEnt.Get<ButtonComponent>();


    private EcsEntity _buyBishopUIEnt;
    internal ref ButtonComponent BuyBishopUIEnt_ButtonCom => ref _buyBishopUIEnt.Get<ButtonComponent>();



    private EcsEntity _upgradeUnitUIEnt;
    internal ref ButtonComponent UpgradeUnitUIEnt_ButtonCom => ref _upgradeUnitUIEnt.Get<ButtonComponent>();



    private EcsEntity _upgradeFarmUIEnt;
    internal ref ButtonComponent UpgradeFarmUIEnt_ButtonCom => ref _upgradeFarmUIEnt.Get<ButtonComponent>();


    private EcsEntity _upgradeWoodcutterUIEnt;
    internal ref ButtonComponent UpgradeWoodcutterUIEnt_ButtonCom => ref _upgradeWoodcutterUIEnt.Get<ButtonComponent>();


    private EcsEntity _upgradeMineUIEnt;
    internal ref ButtonComponent UpgradeMineUIEnt_ButtonCom => ref _upgradeMineUIEnt.Get<ButtonComponent>();

    #endregion


    #region EnvironmentZone

    private EcsEntity _environmentZoneEnt;
    internal ref ParentComponent EnvironmentZoneEnt_ParentCom => ref _environmentZoneEnt.Get<ParentComponent>();


    private EcsEntity _environmentInfoEnt;
    internal ref ButtonComponent EnvironmentInfoEnt_ButtonCom => ref _environmentInfoEnt.Get<ButtonComponent>();
    internal ref ActivatedComponent EnvironmentInfoEnt_IsActivatedCom => ref _environmentZoneEnt.Get<ActivatedComponent>();


    private EcsEntity _envFertilizerEnt;
    internal ref TextMeshProUGUIComponent EnvFerilizerEnt_TextMeshProUGUICom => ref _envFertilizerEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _envForestEnt;
    internal ref TextMeshProUGUIComponent EnvForestEnt_TextMeshProUGUICom => ref _envForestEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _envOreEnt;
    internal ref TextMeshProUGUIComponent EnvOreEnt_TextMeshProUGUICom => ref _envOreEnt.Get<TextMeshProUGUIComponent>();

    #endregion

    #endregion


    internal EntitiesGameGeneralUIViewManager(EcsWorld gameWorld)
    {

        _leaveEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(CanvasUIWorker.FindUnderParent<Button>(SceneTypes.Game, "ButtonLeave")));


        #region Center

        var middleZone = CanvasUIWorker.FindUnderParent(SceneTypes.Game, "MiddleZone");

        var theEndGameZone = CanvasUIWorker.FindUnderParent(SceneTypes.Game, "TheEndGameZone");
        _endGameEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(theEndGameZone))
            .Replace(new TextMeshProUGUIComponent(theEndGameZone.transform.Find("TheEndGameText").GetComponent<TextMeshProUGUI>()))
            .Replace(new EndGameComponent());


        var readyZone = CanvasUIWorker.FindUnderParent(SceneTypes.Game, "ReadyZone");

        _readyEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(readyZone))
            .Replace(new ButtonComponent(readyZone.transform.Find("ReadyButton").GetComponent<Button>()))
            .Replace(new ActivatedButtonDictComponent(new Dictionary<bool, bool>()))
            .Replace(new TextMeshProUGUIComponent())
            .Replace(new StartedGameComponent());


        _joinDiscordEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(readyZone.transform.Find("JoinDiscordButton").GetComponent<Button>()));
        JoinDiscordEnt_ButtonCom.Button.onClick.AddListener(delegate { Application.OpenURL("https://discord.gg/yxfZnrkBPU"); });


        var motionZone = CanvasUIWorker.FindUnderParent(SceneTypes.Game, "MotionZone");
        _motionEnt = gameWorld.NewEntity()
            .Replace(new ActivatedComponent())
            .Replace(new AmountMotionsComponent())
            .Replace(new ParentComponent(motionZone))
            .Replace(new TextMeshProUGUIComponent(motionZone.transform.Find("MotionText").GetComponent<TextMeshProUGUI>()));



        _mistakeVisUICont = new CenterSupTextUIViewContainer(middleZone, gameWorld);
        new CenterSupTextUIViewWorker(_mistakeVisUICont);


        #endregion


        #region Up

        var upZoneGO = CanvasUIWorker.FindUnderParent(SceneTypes.Game, "UpZone");

        _resourcesVisUIContainer = new ResourcesVisUIContainer(gameWorld, upZoneGO);
        new ResourcesViewUIWorker(_resourcesVisUIContainer);


        #endregion


        #region Down


        var downZone = CanvasUIWorker.FindUnderParent(SceneTypes.Game, "DownZone");


        var takeUnitZone = downZone.transform.Find("TakeUnitZone");


        _takerKingEntity = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent(UnitTypes.King))
            .Replace(new ButtonComponent(takeUnitZone.transform.Find("TakeUnit0Button").GetComponent<Button>()))
            .Replace(new TextMeshProUGUIComponent());

        _takerPawnEntity = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent(UnitTypes.Pawn))
            .Replace(new ButtonComponent(takeUnitZone.transform.Find("TakeUnit1Button").GetComponent<Button>()))
            .Replace(new TextMeshProUGUIComponent());

        _takerRookEntity = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent(UnitTypes.Rook))
            .Replace(new ButtonComponent(takeUnitZone.transform.Find("TakeUnit2Button").GetComponent<Button>()))
            .Replace(new TextMeshProUGUIComponent());

        _takerBishopEntity = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent(UnitTypes.Bishop))
            .Replace(new ButtonComponent(takeUnitZone.transform.Find("TakeUnit3Button").GetComponent<Button>()))
            .Replace(new TextMeshProUGUIComponent());


        _donerUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(downZone.transform.Find("DonerButton").GetComponent<Button>()))
            .Replace(new ActivatedButtonDictComponent(new Dictionary<bool, bool>()))
            .Replace(new MistakeComponent(new UnityEvent(), default));



        _finderIdleEntity = gameWorld.NewEntity()
            .Replace(new ButtonComponent(downZone.transform.Find("FinderIdleButton").GetComponent<Button>()));


        #endregion


        #region Left



        var leftZoneGO = CanvasUIWorker.FindUnderParent(SceneTypes.Game, "LeftZone");
        _leftZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(leftZoneGO));


        #region BuildingZone

        var buildingZoneGO = leftZoneGO.transform.Find("BuildingZone").gameObject;

        _buildingZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(buildingZoneGO));

        _meltOreUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingZoneGO.transform.Find("MeltOreButton").GetComponent<Button>()));

        _buyPawnUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingZoneGO.transform.Find("BuyPawnButton").GetComponent<Button>()));

        _buyRookUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingZoneGO.transform.Find("BuyRookButton").GetComponent<Button>()));

        _buyBishopUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingZoneGO.transform.Find("BuyBishopButton").GetComponent<Button>()));

        _upgradeUnitUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingZoneGO.transform.Find("UpgradeUnitButton").GetComponent<Button>()));

        _upgradeFarmUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingZoneGO.transform.Find("UpgradeFarmButton").GetComponent<Button>()));

        _upgradeWoodcutterUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingZoneGO.transform.Find("UpgradeWoodcutterButton").GetComponent<Button>()));

        _upgradeMineUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingZoneGO.transform.Find("UpgradeMineButton").GetComponent<Button>()));

        #endregion


        #region EnvironmentZone

        var environmentZoneGO = leftZoneGO.transform.Find("EnvironmentZone").gameObject;


        _environmentZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(environmentZoneGO));

        _environmentInfoEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(environmentZoneGO.transform.Find("EnvironmentInfoButton").GetComponent<Button>()));

        _envFertilizerEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent(environmentZoneGO.transform.Find("FertilizerResourcesText").GetComponent<TextMeshProUGUI>()));

        _envForestEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent(environmentZoneGO.transform.Find("ForestResourcesText").GetComponent<TextMeshProUGUI>()));

        _envOreEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent(environmentZoneGO.transform.Find("OreResourcesText").GetComponent<TextMeshProUGUI>()));


        #endregion

        #endregion


        #region RightZone

        var rightZoneGO = CanvasUIWorker.FindUnderParent(SceneTypes.Game, "RightZone");

        _rightZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(rightZoneGO));


        var statsZoneGO = rightZoneGO.transform.Find("StatsZone").gameObject;

        _statsZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(statsZoneGO));

        _healthUIEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent(statsZoneGO.transform.Find("HpCurrentUnitText").GetComponent<TextMeshProUGUI>()));

        _powerAttackUIEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent(statsZoneGO.transform.Find("DamageCurrentUnitText").GetComponent<TextMeshProUGUI>()));

        _powerProtectionUIEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent(statsZoneGO.transform.Find("ProtectionCurrentUnitText").GetComponent<TextMeshProUGUI>()));

        _amountStepsUIEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent(statsZoneGO.transform.Find("StepsCurrentUnitText").GetComponent<TextMeshProUGUI>()));









        var conditionZoneGO = rightZoneGO.transform.Find("ConditionZone").gameObject;
        _conditionZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(conditionZoneGO))
            .Replace(new TextMeshProUGUIComponent(conditionZoneGO.transform.Find("StandartAbilityText").GetComponent<TextMeshProUGUI>()));

        _protectConditionEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(conditionZoneGO.transform.Find("StandartAbilityButton1").GetComponent<Button>()));

        _relaxConditionEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(conditionZoneGO.transform.Find("StandartAbilityButton2").GetComponent<Button>()));



        var uniqueAbilitiesZoneGO = rightZoneGO.transform.Find("UniqueAbilitiesZone").gameObject;
        _uniquePareZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(uniqueAbilitiesZoneGO))
            .Replace(new TextMeshProUGUIComponent(uniqueAbilitiesZoneGO.transform.Find("UniqueAbilitiesText").GetComponent<TextMeshProUGUI>()));


        var uniqueAbilityButton1 = uniqueAbilitiesZoneGO.transform.Find("UniqueAbilityButton1").GetComponent<Button>();
        _uniqueFirstAbilityEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(uniqueAbilityButton1))
            .Replace(new TextMeshProUGUIComponent(uniqueAbilityButton1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));

        var uniqueAbilityButton2 = uniqueAbilitiesZoneGO.transform.Find("UniqueAbilityButton2").GetComponent<Button>();
        _uniqueSecondAbilityEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(uniqueAbilityButton2))
            .Replace(new TextMeshProUGUIComponent(uniqueAbilityButton2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));

        var uniqueAbilityButton3 = uniqueAbilitiesZoneGO.transform.Find("UniqueAbilityButton3").GetComponent<Button>();
        _uniqueThirdAbilityEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(uniqueAbilityButton3))
            .Replace(new TextMeshProUGUIComponent(uniqueAbilityButton3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));



        var buildingZoneG = rightZoneGO.transform.Find("BuildingZone").gameObject;

        _buildingAbilitiesZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent(buildingZoneG))
            .Replace(new TextMeshProUGUIComponent(buildingZoneG.transform.Find("BuildingAbilitiesText").GetComponent<TextMeshProUGUI>()));




        var buildingFirstAbilityButtom = buildingZoneG.transform.Find("BuildingAbilityButton1").GetComponent<Button>();
        _buildingFirstAbilityEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingFirstAbilityButtom))
            .Replace(new TextMeshProUGUIComponent(buildingFirstAbilityButtom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));


        _buildingSecondAbilityEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingZoneG.transform.Find("BuildingAbilityButton2").GetComponent<Button>()));


        var buildingThirdAbilityButtom = buildingZoneG.transform.Find("BuildingAbilityButton3").GetComponent<Button>();
        _buildingThirdAbilityEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent(buildingThirdAbilityButtom))
            .Replace(new TextMeshProUGUIComponent(buildingThirdAbilityButtom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));

        #endregion
    }

}