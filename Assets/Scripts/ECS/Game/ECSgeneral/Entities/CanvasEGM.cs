using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Main;

internal sealed partial class EntitiesGeneralManager : EntitiesManager
{
    #region Center

    private EcsEntity _endGameEnt;
    internal ref EndGameComponent EndGameEntEndGameCom => ref _endGameEnt.Get<EndGameComponent>();
    internal ref ParentComponent EndGameEnt_ParentCom => ref _endGameEnt.Get<ParentComponent>();
    internal ref TextMeshProUGUIComponent EndGameEnt_TextMeshProGUICom => ref _endGameEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _readyEnt;
    internal ref ParentComponent ReadyEnt_ParentCom => ref _readyEnt.Get<ParentComponent>();
    internal ref ButtonComponent ReadyEnt_ButtonCom => ref _readyEnt.Get<ButtonComponent>();
    internal ref ActivatedDictionaryComponent ReadyEnt_ActivatedDictCom => ref _readyEnt.Get<ActivatedDictionaryComponent>();


    private EcsEntity _motionEnt;
    internal ref AmountComponent MotionEnt_AmountCom => ref _motionEnt.Get<AmountComponent>();
    internal ref IsActivatedComponent MotionEnt_IsActivatedCom => ref _motionEnt.Get<IsActivatedComponent>();
    internal ref ParentComponent MotionEnt_ParentCom => ref _motionEnt.Get<ParentComponent>();
    internal ref TextMeshProUGUIComponent MotionEnt_TextMeshProUGUICom => ref _motionEnt.Get<TextMeshProUGUIComponent>();

    #endregion


    #region Up

    private EcsEntity _economyEnt;
    internal ref EconomyComponent EconomyEnt_EconomyCom => ref _economyEnt.Get<EconomyComponent>();
    internal ref EconomyUIComponent EconomyUIEnt_EconomyUICom => ref _economyEnt.Get<EconomyUIComponent>();
    internal ref MistakeEconomyComponent EconomyEnt_MistakeEconomyCom => ref _economyEnt.Get<MistakeEconomyComponent>();


    private EcsEntity _leaveEnt;
    internal ref ButtonComponent LeaveEnt_ButtonCom => ref _leaveEnt.Get<ButtonComponent>();

    #endregion


    #region Down

    private EcsEntity _donerEntity;

    internal ref ButtonComponent DonerEnt_ButtonCom => ref _donerEntity.Get<ButtonComponent>();
    internal ref TextMeshProUGUIComponent DonerEnt_TextMeshProGUICom => ref _donerEntity.Get<TextMeshProUGUIComponent>();
    internal ref ActivatedDictionaryComponent DonerEnt_IsActivatedDictCom => ref _donerEntity.Get<ActivatedDictionaryComponent>();
    internal ref MistakeComponent DonerEnt_MistakeCom => ref _donerEntity.Get<MistakeComponent>();


    private EcsEntity _truceEntity;
    internal ref ButtonComponent TruceEnt_ButtonCom => ref _truceEntity.Get<ButtonComponent>();
    internal ref ActivatedDictionaryComponent TruceEnt_ActivatedDictCom => ref _truceEntity.Get<ActivatedDictionaryComponent>();
    internal ref MistakeComponent TruceEnt_MistakeCom => ref _truceEntity.Get<MistakeComponent>();


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

    #region StatsZone

    private EcsEntity _statsZoneEnt;
    internal ref ParentComponent StatsEnt_ParentCom => ref _statsZoneEnt.Get<ParentComponent>();

    #endregion


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


    #region Left

    private EcsEntity _leftZoneEnt;
    internal ref ImageComponent LeftZoneEnt_ImageCom => ref _leftZoneEnt.Get<ImageComponent>();
    internal ref ParentComponent LeftZoneEnt_ParentCom => ref _leftZoneEnt.Get<ParentComponent>();


    #region BuildingZone

    private EcsEntity _buildingZoneEnt;
    internal ref ParentComponent BuildingZoneEnt_ParentCom => ref _buildingZoneEnt.Get<ParentComponent>();


    private EcsEntity _meltOreUIEnt;
    internal ref ButtonComponent MeltOreEnt_ButtonCom => ref _meltOreUIEnt.Get<ButtonComponent>();

    #endregion


    #region EnvironmentZone

    private EcsEntity _environmentZoneEnt;
    internal ref ParentComponent EnvironmentZoneEnt_ParentCom => ref _environmentZoneEnt.Get<ParentComponent>();


    private EcsEntity _environmentInfoEnt;
    internal ref ButtonComponent EnvironmentInfoEnt_ButtonCom => ref _environmentInfoEnt.Get<ButtonComponent>();
    internal ref IsActivatedComponent EnvironmentInfoEnt_IsActivatedCom => ref _environmentZoneEnt.Get<IsActivatedComponent>();


    private EcsEntity _envFertilizerEnt;
    internal ref TextMeshProUGUIComponent EnvFerilizerEnt_TextMeshProUGUICom => ref _envFertilizerEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _envForestEnt;
    internal ref TextMeshProUGUIComponent EnvForestEnt_TextMeshProUGUICom => ref _envForestEnt.Get<TextMeshProUGUIComponent>();


    private EcsEntity _envOreEnt;
    internal ref TextMeshProUGUIComponent EnvOreEnt_TextMeshProUGUICom => ref _envOreEnt.Get<TextMeshProUGUIComponent>();

    #endregion

    #endregion


    internal void ConstructCanvast(EcsWorld gameWorld)
    {

        #region Center

        _endGameEnt = gameWorld.NewEntity()
            .Replace(new EndGameComponent())
            .Replace(new ParentComponent())
            .Replace(new TextMeshProUGUIComponent());

        _readyEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent())
            .Replace(new ButtonComponent())
            .Replace(new TextMeshProUGUIComponent());

        _motionEnt = gameWorld.NewEntity()
            .Replace(new IsActivatedComponent())
            .Replace(new AmountComponent())
            .Replace(new ParentComponent())
            .Replace(new TextMeshProUGUIComponent());

        #endregion


        #region Up

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


        _truceEntity = gameWorld.NewEntity()
            .Replace(new ButtonComponent())
            .Replace(new ActivatedDictionaryComponent())
            .Replace(new MistakeComponent());

        _donerEntity = gameWorld.NewEntity()
            .Replace(new ButtonComponent())
            .Replace(new ActivatedDictionaryComponent())
            .Replace(new MistakeComponent());

        #endregion


        #region LeftZone

        _leftZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent())
            .Replace(new ImageComponent());


        #region BuildingZone

        _buildingZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent());

        _meltOreUIEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent());

        #endregion


        #region Environment

        _environmentZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent());

        _environmentInfoEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent());

        _envFertilizerEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent());

        _envForestEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent());

        _envOreEnt = gameWorld.NewEntity()
            .Replace(new TextMeshProUGUIComponent());

        #endregion

        #endregion


        #region Right

        #region StatsZone

        _statsZoneEnt = gameWorld.NewEntity()
            .Replace(new ParentComponent());

        #endregion

        _uniqueFirstAbilityEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent())
            .Replace(new TextMeshProUGUIComponent());

        _uniqueSecondAbilityEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent())
            .Replace(new TextMeshProUGUIComponent());

        _uniqueThirdAbilityEnt = gameWorld.NewEntity()
            .Replace(new ButtonComponent())
            .Replace(new TextMeshProUGUIComponent());

        #endregion


    }

    internal void SpawnAndFillCanvasEntities()
    {
        #region Center

        var theEndGameZone = Instance.CanvasManager.InGameZoneGO.transform.Find("TheEndGameZone").gameObject;

        EndGameEnt_TextMeshProGUICom.SetTextMeshProUGUI(theEndGameZone.transform.Find("TheEndGameText").GetComponent<TextMeshProUGUI>());
        EndGameEnt_ParentCom.SetParent(theEndGameZone);


        var readyZone = Instance.CanvasManager.InGameZoneGO.transform.Find("ReadyZone").gameObject;

        ReadyEnt_ParentCom.SetParent(readyZone);
        ReadyEnt_ButtonCom.SetButton(readyZone.transform.Find("ReadyButton").GetComponent<Button>());
        ReadyEnt_ActivatedDictCom.CreateDict();


        var motionZone = Instance.CanvasManager.InGameZoneGO.transform.Find("MotionZone").gameObject;

        MotionEnt_ParentCom.SetParent(motionZone);
        MotionEnt_TextMeshProUGUICom.SetTextMeshProUGUI(motionZone.transform.Find("MotionText").GetComponent<TextMeshProUGUI>());

        #endregion


        #region Up

        var upZoneGO = Instance.CanvasManager.InGameZoneGO.transform.Find("UpZone").gameObject;

        LeaveEnt_ButtonCom.SetButton(upZoneGO.transform.Find("ButtonLeave").GetComponent<Button>());

        #endregion


        #region Down

        var downZone = Instance.CanvasManager.InGameZoneGO.transform.Find("DownZone");


        var takeUnitZone = downZone.transform.Find("TakeUnitZone");

        TakerKingEnt_UnitTypeCom.UnitType = UnitTypes.King;
        TakerKingEnt_ButtonCom.SetButton(takeUnitZone.transform.Find("TakeUnit0Button").GetComponent<Button>());

        TakerPawnEntityUnitTypeComponent.UnitType = UnitTypes.Pawn;
        TakerPawnEntityButtonComponent.SetButton(takeUnitZone.transform.Find("TakeUnit1Button").GetComponent<Button>());

        TakerRookEntityUnitTypeComponent.UnitType = UnitTypes.Rook;
        TakerRookEntityButtonComponent.SetButton(takeUnitZone.transform.Find("TakeUnit2Button").GetComponent<Button>());

        TakerBishopEntityUnitTypeComponent.UnitType = UnitTypes.Bishop;
        TakerBishopEntityButtonComponent.SetButton(takeUnitZone.transform.Find("TakeUnit3Button").GetComponent<Button>());


        DonerEnt_MistakeCom.CreateEvent();
        DonerEnt_IsActivatedDictCom.CreateDict();
        DonerEnt_ButtonCom.SetButton(downZone.transform.Find("DonerButton").GetComponent<Button>());

        TruceEnt_ActivatedDictCom.CreateDict();
        TruceEnt_ButtonCom.SetButton(downZone.transform.Find("TruceButton").GetComponent<Button>());
        TruceEnt_MistakeCom.CreateEvent();


        var uniqueAbilityButton1 = GameObject.Find("UniqueAbilityButton1").GetComponent<Button>();
        Unique1AbilityEnt_ButtonCom.SetButton(uniqueAbilityButton1);
        UniqueFirstAbilityEnt_TextMeshProGUICom.SetTextMeshProUGUI(uniqueAbilityButton1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

        var uniqueAbilityButton2 = GameObject.Find("UniqueAbilityButton2").GetComponent<Button>();
        Unique2AbilityEnt_ButtonCom.SetButton(uniqueAbilityButton2);
        Unique2AbilityEnt_TextMeshProGUICom.SetTextMeshProUGUI(uniqueAbilityButton2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

        var uniqueAbilityButton3 = GameObject.Find("UniqueAbilityButton3").GetComponent<Button>();
        Unique3AbilityEnt_ButtonCom.SetButton(uniqueAbilityButton3);
        Unique3AbilityEnt_TextMeshProGUICom.SetTextMeshProUGUI(uniqueAbilityButton3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());


        #endregion


        #region Left

        var leftZoneGO = Instance.CanvasManager.InGameZoneGO.transform.Find("LeftZone").gameObject;

        LeftZoneEnt_ParentCom.SetParent(leftZoneGO);
        LeftZoneEnt_ImageCom.SetImage(leftZoneGO.transform.Find("LeftZoneImage").GetComponent<Image>());


        #region BuildingZone

        var buildingZoneGO = leftZoneGO.transform.Find("BuildingZone").gameObject;

        BuildingZoneEnt_ParentCom.SetParent(buildingZoneGO);

        MeltOreEnt_ButtonCom.SetButton(buildingZoneGO.transform.Find("MeltOreButton").GetComponent<Button>());

        #endregion


        #region EnvironmentZone

        var environmentZoneGO = leftZoneGO.transform.Find("EnvironmentZone").gameObject;

        EnvironmentZoneEnt_ParentCom.SetParent(environmentZoneGO);

        EnvironmentInfoEnt_ButtonCom.SetButton(environmentZoneGO.transform.Find("EnvironmentInfoButton").GetComponent<Button>());

        EnvFerilizerEnt_TextMeshProUGUICom.SetTextMeshProUGUI(environmentZoneGO.transform.Find("FertilizerResourcesText").GetComponent<TextMeshProUGUI>());

        EnvForestEnt_TextMeshProUGUICom.SetTextMeshProUGUI(environmentZoneGO.transform.Find("ForestResourcesText").GetComponent<TextMeshProUGUI>());

        EnvOreEnt_TextMeshProUGUICom.SetTextMeshProUGUI(environmentZoneGO.transform.Find("OreResourcesText").GetComponent<TextMeshProUGUI>());

        #endregion

        #endregion


        #region RightZone

        var rightZoneGO = Instance.CanvasManager.InGameZoneGO.transform.Find("RightZone").gameObject;

        StatsEnt_ParentCom.SetParent(rightZoneGO.transform.Find("StatsZone").gameObject);

        #endregion
    }
}