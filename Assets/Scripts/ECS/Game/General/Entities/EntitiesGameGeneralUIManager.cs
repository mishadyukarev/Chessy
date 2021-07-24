using Assets.Scripts;
using Assets.Scripts.ECS.Components.UI;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Main;

public sealed class EntitiesGameGeneralUIManager : EntitiesManager
{

    #region Center

    private EcsEntity _endGameEnt;
    internal ref EndGameComponent EndGameEntEndGameCom => ref _endGameEnt.Get<EndGameComponent>();
    internal ref ParentComponent EndGameEnt_ParentCom => ref _endGameEnt.Get<ParentComponent>();
    internal ref TextMeshProUGUIComponent EndGameEnt_TextMeshProGUICom => ref _endGameEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _readyEnt;
    internal ref ParentComponent ReadyEnt_ParentCom => ref _readyEnt.Get<ParentComponent>();
    internal ref ButtonComponent ReadyEnt_ButtonCom => ref _readyEnt.Get<ButtonComponent>();
    internal ref ActivatedDictComponent ReadyEnt_ActivatedDictCom => ref _readyEnt.Get<ActivatedDictComponent>();
    internal ref StartedGameComponent ReadyEnt_StartedGameCom => ref _readyEnt.Get<StartedGameComponent>();


    private EcsEntity _joinDiscordEnt;
    internal ref ButtonComponent JoinDiscordEnt_ButtonCom => ref _joinDiscordEnt.Get<ButtonComponent>();


    private EcsEntity _motionEnt;
    internal ref AmountComponent MotionEnt_AmountCom => ref _motionEnt.Get<AmountComponent>();
    internal ref ActivatedComponent MotionEnt_ActivatedCom => ref _motionEnt.Get<ActivatedComponent>();
    internal ref ParentComponent MotionEnt_ParentCom => ref _motionEnt.Get<ParentComponent>();
    internal ref TextMeshProUGUIComponent MotionEnt_TextMeshProUGUICom => ref _motionEnt.Get<TextMeshProUGUIComponent>();

    #endregion


    #region Up

    #region Resources

    private EcsEntity _foodInfoUIEnt;
    internal ref TextMeshProUGUIComponent FoodInfoUIEnt_TextMeshProUGUICom => ref _foodInfoUIEnt.Get<TextMeshProUGUIComponent>();
    internal ref MistakeResourcesUIComponent FoodInfoUIEnt_MistakeResourcesUICom => ref _foodInfoUIEnt.Get<MistakeResourcesUIComponent>();
    internal ref AddingTMPUIComponent FoodInfoUIEnt_AddingTMPUICom => ref _foodInfoUIEnt.Get<AddingTMPUIComponent>();


    private EcsEntity _woodInfoUIEnt;
    internal ref TextMeshProUGUIComponent WoodInfoUIEnt_TextMeshProUGUICom => ref _woodInfoUIEnt.Get<TextMeshProUGUIComponent>();
    internal ref MistakeResourcesUIComponent WoodInfoUIEnt_MistakeResourcesUICom => ref _woodInfoUIEnt.Get<MistakeResourcesUIComponent>();
    internal ref AddingTMPUIComponent WoodInfoUIEnt_AddingTMPUICom => ref _woodInfoUIEnt.Get<AddingTMPUIComponent>();


    private EcsEntity _oreInfoUIEnt;
    internal ref TextMeshProUGUIComponent OreInfoUIEnt_TextMeshProUGUICom => ref _oreInfoUIEnt.Get<TextMeshProUGUIComponent>();
    internal ref MistakeResourcesUIComponent OreInfoUIEnt_MistakeResourcesUICom => ref _oreInfoUIEnt.Get<MistakeResourcesUIComponent>();
    internal ref AddingTMPUIComponent OreInfoUIEnt_AddingTMPUICom => ref _oreInfoUIEnt.Get<AddingTMPUIComponent>();


    private EcsEntity _ironInfoUIEnt;
    internal ref TextMeshProUGUIComponent IronInfoUIEnt_TextMeshProUGUICom => ref _ironInfoUIEnt.Get<TextMeshProUGUIComponent>();
    internal ref MistakeResourcesUIComponent IronInfoUIEnt_MistakeResourcesUICom => ref _ironInfoUIEnt.Get<MistakeResourcesUIComponent>();


    private EcsEntity _goldInfoUIEnt;
    internal ref TextMeshProUGUIComponent GoldInfoUIEnt_TextMeshProUGUICom => ref _goldInfoUIEnt.Get<TextMeshProUGUIComponent>();
    internal ref MistakeResourcesUIComponent GoldInfoUIEnt_MistakeResourcesUICom => ref _goldInfoUIEnt.Get<MistakeResourcesUIComponent>();

    #endregion


    private EcsEntity _leaveEnt;
    internal ref ButtonComponent LeaveEnt_ButtonCom => ref _leaveEnt.Get<ButtonComponent>();

    #endregion


    #region Down

    private EcsEntity _donerUIEnt;

    internal ref ButtonComponent DonerUIEnt_ButtonCom => ref _donerUIEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent DonerUIEnt_TextMeshProGUICom => ref _donerUIEnt.Get<TextMeshProUGUIComponent>();
    internal ref ActivatedDictComponent DonerUIEnt_IsActivatedDictCom => ref _donerUIEnt.Get<ActivatedDictComponent>();
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
    internal ref TextMeshProUGUIComponent PowerAttackUIEnt_TextMeshProUGUICom => ref _powerAttackUIEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _powerProtectionUIEnt;
    internal ref TextMeshProUGUIComponent PowerProtectionUIEnt_TextMeshProUGUICom => ref _powerProtectionUIEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _amountStepsUIEnt;
    internal ref TextMeshProUGUIComponent AmountStepsUIEnt_TextMeshProUGUICom => ref _amountStepsUIEnt.Get<TextMeshProUGUIComponent>();

    #endregion


    #region StandartAbilities

    private EcsEntity _standartAbilitiesZoneEnt;
    internal ref TextMeshProUGUIComponent StandartAbilitiesZoneEnt_TextMeshProUGUICom => ref _standartAbilitiesZoneEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _standartFirstAbilityEnt;
    internal ref ButtonComponent StandartFirstAbilityEnt_ButtonCom => ref _standartFirstAbilityEnt.Get<ButtonComponent>();


    private EcsEntity _standartSecondAbilityEnt;
    internal ref ButtonComponent StandartSecondAbilityEnt_ButtonCom => ref _standartSecondAbilityEnt.Get<ButtonComponent>();

    #endregion


    #region UniqueAbilities

    private EcsEntity _uniqueAbilitiesZoneEnt;
    internal ref TextMeshProUGUIComponent UniqueAbilitiesZoneEnt_TextMeshProUGUICom => ref _uniqueAbilitiesZoneEnt.Get<TextMeshProUGUIComponent>();
    internal ref ParentComponent UniqueAbilitiesZoneEnt_ParentCom => ref _uniqueAbilitiesZoneEnt.Get<ParentComponent>();


    private EcsEntity _uniqueFirstAbilityEnt;
    internal ref ButtonComponent Unique1AbilityEnt_ButtonCom => ref _uniqueFirstAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent UniqueFirstAbilityEnt_TextMeshProGUICom => ref _uniqueFirstAbilityEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _uniqueSecondAbilityEnt;
    internal ref ButtonComponent Unique2AbilityEnt_ButtonCom => ref _uniqueSecondAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent Unique2AbilityEnt_TextMeshProGUICom => ref _uniqueSecondAbilityEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _uniqueThirdAbilityEnt;
    internal ref ButtonComponent Unique3AbilityEnt_ButtonCom => ref _uniqueThirdAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent Unique3AbilityEnt_TextMeshProGUICom => ref _uniqueThirdAbilityEnt.Get<TextMeshProUGUIComponent>();

    #endregion


    #region BuildingAbilities

    private EcsEntity _buildingAbilitiesZoneEnt;
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


    private EcsEntity _buildingFourthAbilityEnt;
    internal ref ButtonComponent BuildingFourthAbilityEnt_ButtonCom => ref _buildingFourthAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent BuildingFourthAbilityEnt_TextMeshProGUICom => ref _buildingFourthAbilityEnt.Get<TextMeshProUGUIComponent>();

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


    internal EntitiesGameGeneralUIManager(EcsWorld gameWorld)
    {
    }

    internal override void FillEntities(EcsWorld gameWorld)
    {
        base.FillEntities(gameWorld);






        #region Center

        _endGameEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent())
            .Replace(new TextMeshProUGUIComponent());

        _readyEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent())
            .Replace(new ButtonComponent())
            .Replace(new TextMeshProUGUIComponent());

        _motionEnt = gameWorld.NewEntity()
            .Replace(new ActivatedComponent())
            .Replace(new AmountComponent())
            .Replace(new ParentComponent())
            .Replace(new TextMeshProUGUIComponent());

        _joinDiscordEnt = gameWorld.NewEntity();

        #endregion


        #region Up

        _foodInfoUIEnt = gameWorld.NewEntity();
        _woodInfoUIEnt = gameWorld.NewEntity();
        _oreInfoUIEnt = gameWorld.NewEntity();
        _ironInfoUIEnt = gameWorld.NewEntity();
        _goldInfoUIEnt = gameWorld.NewEntity();

        _leaveEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent());

        #endregion


        #region Down

        _takerKingEntity = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent())
            .Replace(new ButtonComponent())
            .Replace(new TextMeshProUGUIComponent());

        _takerPawnEntity = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent())
            .Replace(new ButtonComponent())
            .Replace(new TextMeshProUGUIComponent());

        _takerRookEntity = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent())
            .Replace(new ButtonComponent())
            .Replace(new TextMeshProUGUIComponent());

        _takerBishopEntity = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent())
            .Replace(new ButtonComponent())
            .Replace(new TextMeshProUGUIComponent());

        _donerUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent())
            .Replace(new ActivatedDictComponent())
            .Replace(new MistakeComponent());

        _finderIdleEntity = gameWorld.NewEntity();

        #endregion


        #region LeftZone

        _leftZoneEnt = gameWorld.NewEntity();

        _buildingZoneEnt = gameWorld.NewEntity();
        _meltOreUIEnt = gameWorld.NewEntity();
        _buyPawnUIEnt = gameWorld.NewEntity();
        _buyRookUIEnt = gameWorld.NewEntity();
        _buyBishopUIEnt = gameWorld.NewEntity();
        _upgradeUnitUIEnt = gameWorld.NewEntity();
        _upgradeFarmUIEnt = gameWorld.NewEntity();
        _upgradeWoodcutterUIEnt = gameWorld.NewEntity();
        _upgradeMineUIEnt = gameWorld.NewEntity();

        _environmentZoneEnt = gameWorld.NewEntity();
        _environmentInfoEnt = gameWorld.NewEntity();
        _envFertilizerEnt = gameWorld.NewEntity();
        _envForestEnt = gameWorld.NewEntity();
        _envOreEnt = gameWorld.NewEntity();

        #endregion


        #region Right

        _rightZoneEnt = gameWorld.NewEntity();

        _statsZoneEnt = gameWorld.NewEntity();
        _healthUIEnt = gameWorld.NewEntity();
        _powerAttackUIEnt = gameWorld.NewEntity();
        _powerProtectionUIEnt = gameWorld.NewEntity();
        _amountStepsUIEnt = gameWorld.NewEntity();

        _standartAbilitiesZoneEnt = gameWorld.NewEntity();
        _standartFirstAbilityEnt = gameWorld.NewEntity();
        _standartSecondAbilityEnt = gameWorld.NewEntity();

        _uniqueAbilitiesZoneEnt = gameWorld.NewEntity();
        _uniqueFirstAbilityEnt = gameWorld.NewEntity();
        _uniqueSecondAbilityEnt = gameWorld.NewEntity();
        _uniqueThirdAbilityEnt = gameWorld.NewEntity();

        _buildingAbilitiesZoneEnt = gameWorld.NewEntity();
        _buildingFirstAbilityEnt = gameWorld.NewEntity();
        _buildingSecondAbilityEnt = gameWorld.NewEntity();
        _buildingThirdAbilityEnt = gameWorld.NewEntity();
        _buildingFourthAbilityEnt = gameWorld.NewEntity();

        #endregion






















        LeaveEnt_ButtonCom.StartFill(Instance.CanvasManager.FindUnderParent<Button>(SceneTypes.Game, "ButtonLeave"));

        EndGameEntEndGameCom.StartFill();

        #region Center


        var theEndGameZone = Instance.CanvasManager.FindUnderParent(SceneTypes.Game, "TheEndGameZone");

        EndGameEnt_TextMeshProGUICom.StartFill(theEndGameZone.transform.Find("TheEndGameText").GetComponent<TextMeshProUGUI>());
        EndGameEnt_ParentCom.SetParent(theEndGameZone);


        var readyZone = Instance.CanvasManager.FindUnderParent(SceneTypes.Game, "ReadyZone");

        ReadyEnt_ParentCom.SetParent(readyZone);
        ReadyEnt_ButtonCom.StartFill(readyZone.transform.Find("ReadyButton").GetComponent<Button>());
        ReadyEnt_ActivatedDictCom.StartFill();
        ReadyEnt_StartedGameCom.StartFill();


        JoinDiscordEnt_ButtonCom.StartFill(readyZone.transform.Find("JoinDiscordButton").GetComponent<Button>());
        JoinDiscordEnt_ButtonCom.AddListener(delegate { Application.OpenURL("https://discord.gg/yxfZnrkBPU"); });


        var motionZone = Instance.CanvasManager.FindUnderParent(SceneTypes.Game, "MotionZone");

        MotionEnt_ParentCom.SetParent(motionZone);
        MotionEnt_TextMeshProUGUICom.StartFill(motionZone.transform.Find("MotionText").GetComponent<TextMeshProUGUI>());
        MotionEnt_AmountCom.StartFill();


        #endregion


        #region Up

        var upZoneGO = Instance.CanvasManager.FindUnderParent(SceneTypes.Game, "UpZone");

        FoodInfoUIEnt_TextMeshProUGUICom.StartFill(upZoneGO.transform.Find("FoodAmount").GetComponent<TextMeshProUGUI>());
        FoodInfoUIEnt_MistakeResourcesUICom.StartFill();
        FoodInfoUIEnt_AddingTMPUICom.StartFill(upZoneGO.transform.Find("FoodAdding_TMP").GetComponent<TextMeshProUGUI>());

        WoodInfoUIEnt_TextMeshProUGUICom.StartFill(upZoneGO.transform.Find("WoodAmount").GetComponent<TextMeshProUGUI>());
        WoodInfoUIEnt_MistakeResourcesUICom.StartFill();
        WoodInfoUIEnt_AddingTMPUICom.StartFill(upZoneGO.transform.Find("WoodAdding_TMP").GetComponent<TextMeshProUGUI>());

        OreInfoUIEnt_TextMeshProUGUICom.StartFill(upZoneGO.transform.Find("OreAmount").GetComponent<TextMeshProUGUI>());
        OreInfoUIEnt_MistakeResourcesUICom.StartFill();
        OreInfoUIEnt_AddingTMPUICom.StartFill(upZoneGO.transform.Find("OreAdding_TMP").GetComponent<TextMeshProUGUI>());

        IronInfoUIEnt_TextMeshProUGUICom.StartFill(upZoneGO.transform.Find("IronAmount").GetComponent<TextMeshProUGUI>());
        IronInfoUIEnt_MistakeResourcesUICom.StartFill();

        GoldInfoUIEnt_TextMeshProUGUICom.StartFill(upZoneGO.transform.Find("GoldAmount").GetComponent<TextMeshProUGUI>());
        GoldInfoUIEnt_MistakeResourcesUICom.StartFill();

        #endregion


        #region Down


        var downZone = Instance.CanvasManager.FindUnderParent(SceneTypes.Game, "DownZone");


        var takeUnitZone = downZone.transform.Find("TakeUnitZone");

        TakerKingEnt_UnitTypeCom.UnitType = UnitTypes.King;
        TakerKingEnt_ButtonCom.StartFill(takeUnitZone.transform.Find("TakeUnit0Button").GetComponent<Button>());

        TakerPawnEntityUnitTypeComponent.UnitType = UnitTypes.Pawn;
        TakerPawnEntityButtonComponent.StartFill(takeUnitZone.transform.Find("TakeUnit1Button").GetComponent<Button>());

        TakerRookEntityUnitTypeComponent.UnitType = UnitTypes.Rook;
        TakerRookEntityButtonComponent.StartFill(takeUnitZone.transform.Find("TakeUnit2Button").GetComponent<Button>());

        TakerBishopEntityUnitTypeComponent.UnitType = UnitTypes.Bishop;
        TakerBishopEntityButtonComponent.StartFill(takeUnitZone.transform.Find("TakeUnit3Button").GetComponent<Button>());


        DonerUIEnt_MistakeCom.CreateEvent();
        DonerUIEnt_IsActivatedDictCom.StartFill();

        if (Instance.IsMasterClient)
        {
            if (Instance.EntComM.SaverEnt_StepModeTypeCom.IsByQueueMode)
            {
                DonerUIEnt_IsActivatedDictCom.SetActivated(false, true);
            }
        }

        DonerUIEnt_ButtonCom.StartFill(downZone.transform.Find("DonerButton").GetComponent<Button>());

        FinderIdleEnt_ButtonCom.StartFill(downZone.transform.Find("FinderIdleButton").GetComponent<Button>());


        #endregion


        #region Left



        var leftZoneGO = Instance.CanvasManager.FindUnderParent(SceneTypes.Game, "LeftZone");

        LeftZoneEnt_ParentCom.SetParent(leftZoneGO);


        #region BuildingZone

        var buildingZoneGO = leftZoneGO.transform.Find("BuildingZone").gameObject;

        BuildingZoneEnt_ParentCom.SetParent(buildingZoneGO);

        MeltOreEnt_ButtonCom.StartFill(buildingZoneGO.transform.Find("MeltOreButton").GetComponent<Button>());
        BuyPawnUIEnt_ButtonCom.StartFill(buildingZoneGO.transform.Find("BuyPawnButton").GetComponent<Button>());
        BuyRookUIEnt_ButtonCom.StartFill(buildingZoneGO.transform.Find("BuyRookButton").GetComponent<Button>());
        BuyBishopUIEnt_ButtonCom.StartFill(buildingZoneGO.transform.Find("BuyBishopButton").GetComponent<Button>());
        UpgradeUnitUIEnt_ButtonCom.StartFill(buildingZoneGO.transform.Find("UpgradeUnitButton").GetComponent<Button>());
        UpgradeFarmUIEnt_ButtonCom.StartFill(buildingZoneGO.transform.Find("UpgradeFarmButton").GetComponent<Button>());
        UpgradeWoodcutterUIEnt_ButtonCom.StartFill(buildingZoneGO.transform.Find("UpgradeWoodcutterButton").GetComponent<Button>());
        UpgradeMineUIEnt_ButtonCom.StartFill(buildingZoneGO.transform.Find("UpgradeMineButton").GetComponent<Button>());

        #endregion


        #region EnvironmentZone

        var environmentZoneGO = leftZoneGO.transform.Find("EnvironmentZone").gameObject;

        EnvironmentZoneEnt_ParentCom.SetParent(environmentZoneGO);

        EnvironmentInfoEnt_ButtonCom.StartFill(environmentZoneGO.transform.Find("EnvironmentInfoButton").GetComponent<Button>());

        EnvFerilizerEnt_TextMeshProUGUICom.StartFill(environmentZoneGO.transform.Find("FertilizerResourcesText").GetComponent<TextMeshProUGUI>());

        EnvForestEnt_TextMeshProUGUICom.StartFill(environmentZoneGO.transform.Find("ForestResourcesText").GetComponent<TextMeshProUGUI>());

        EnvOreEnt_TextMeshProUGUICom.StartFill(environmentZoneGO.transform.Find("OreResourcesText").GetComponent<TextMeshProUGUI>());

        #endregion

        #endregion


        #region RightZone

        var rightZoneGO = Instance.CanvasManager.FindUnderParent(SceneTypes.Game, "RightZone");

        RightZoneEnt_ParentCom.SetParent(rightZoneGO);

        var statsZoneGO = rightZoneGO.transform.Find("StatsZone").gameObject;
        StatsEnt_ParentCom.SetParent(statsZoneGO);
        HealthUIEnt_TextMeshProUGUICom.StartFill(statsZoneGO.transform.Find("HpCurrentUnitText").GetComponent<TextMeshProUGUI>());
        PowerAttackUIEnt_TextMeshProUGUICom.StartFill(statsZoneGO.transform.Find("DamageCurrentUnitText").GetComponent<TextMeshProUGUI>());
        PowerProtectionUIEnt_TextMeshProUGUICom.StartFill(statsZoneGO.transform.Find("ProtectionCurrentUnitText").GetComponent<TextMeshProUGUI>());
        AmountStepsUIEnt_TextMeshProUGUICom.StartFill(statsZoneGO.transform.Find("StepsCurrentUnitText").GetComponent<TextMeshProUGUI>());

        StandartAbilitiesZoneEnt_TextMeshProUGUICom.StartFill(rightZoneGO.transform.Find("StandartAbilityText").GetComponent<TextMeshProUGUI>());
        StandartFirstAbilityEnt_ButtonCom.StartFill(rightZoneGO.transform.Find("StandartAbilityButton1").GetComponent<Button>());
        StandartSecondAbilityEnt_ButtonCom.StartFill(rightZoneGO.transform.Find("StandartAbilityButton2").GetComponent<Button>());


        var uniqueAbilitiesZoneGO = rightZoneGO.transform.Find("UniqueAbilitiesZone").gameObject;
        UniqueAbilitiesZoneEnt_ParentCom.SetParent(uniqueAbilitiesZoneGO);
        UniqueAbilitiesZoneEnt_TextMeshProUGUICom.StartFill(uniqueAbilitiesZoneGO.transform.Find("UniqueAbilitiesText").GetComponent<TextMeshProUGUI>());

        var uniqueAbilityButton1 = uniqueAbilitiesZoneGO.transform.Find("UniqueAbilityButton1").GetComponent<Button>();
        Unique1AbilityEnt_ButtonCom.StartFill(uniqueAbilityButton1);
        UniqueFirstAbilityEnt_TextMeshProGUICom.StartFill(uniqueAbilityButton1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

        var uniqueAbilityButton2 = uniqueAbilitiesZoneGO.transform.Find("UniqueAbilityButton2").GetComponent<Button>();
        Unique2AbilityEnt_ButtonCom.StartFill(uniqueAbilityButton2);
        Unique2AbilityEnt_TextMeshProGUICom.StartFill(uniqueAbilityButton2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

        var uniqueAbilityButton3 = uniqueAbilitiesZoneGO.transform.Find("UniqueAbilityButton3").GetComponent<Button>();
        Unique3AbilityEnt_ButtonCom.StartFill(uniqueAbilityButton3);
        Unique3AbilityEnt_TextMeshProGUICom.StartFill(uniqueAbilityButton3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());


        BuildingAbilitiesZoneEnt_TextMeshProUGUICom.StartFill(rightZoneGO.transform.Find("BuildingAbilitiesText").GetComponent<TextMeshProUGUI>());
        var buildingFirstAbilityButtom = rightZoneGO.transform.Find("BuildingAbilityButton1").GetComponent<Button>();
        BuildingFirstAbilityEnt_ButtonCom.StartFill(buildingFirstAbilityButtom);
        BuildingFirstAbilityEnt_TextMeshProGUICom.StartFill(buildingFirstAbilityButtom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
        BuildingSecondAbilityEnt_ButtonCom.StartFill(rightZoneGO.transform.Find("BuildingAbilityButton2").GetComponent<Button>());
        BuildingThirdAbilityEnt_ButtonCom.StartFill(rightZoneGO.transform.Find("BuildingAbilityButton3").GetComponent<Button>());
        var buildingFourthAbilityButtom = rightZoneGO.transform.Find("BuildingAbilityButton4").GetComponent<Button>();
        BuildingFourthAbilityEnt_ButtonCom.StartFill(buildingFourthAbilityButtom);
        BuildingFourthAbilityEnt_TextMeshProGUICom.StartFill(buildingFourthAbilityButtom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

        #endregion
    }
}