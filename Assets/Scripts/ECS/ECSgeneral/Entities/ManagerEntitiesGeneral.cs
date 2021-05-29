﻿using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using System.Collections.Generic;
using static MainGame;

internal sealed class EntitiesGeneralManager : EntitiesManager
{
    private EcsEntity[,] _cellEnts;
    private EcsEntity[,] _cellBuildingEnts;
    private EcsEntity[,] _cellEnvironmentEnts;
    private EcsEntity[,] _cellEnttts;
    private EcsEntity _cellBaseOperationsEnt;

    internal ref CellBaseComponent CellEnt_CellBaseCom(params int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellBaseComponent>();
    internal ref CellComponent CellEnt_CellCom(params int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellComponent>();

    internal ref CellEnvironmentComponent CellEnvEnt_CellEnvCom(params int[] xy) => ref _cellEnvironmentEnts[xy[X], xy[Y]].Get<CellEnvironmentComponent>();
    internal ref CellComponent CellEnvEnt_CellCom(params int[] xy) => ref _cellEnvironmentEnts[xy[X], xy[Y]].Get<CellComponent>();  

    internal ref CellBuildingComponent CellBuildingEnt_CellBuildingCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellBuildingComponent>();
    internal ref BuildingTypeComponent CellBuildingEnt_BuildingTypeCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();
    internal ref CellComponent CellBuildingEnt_CellCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellComponent>();
    internal ref OwnerComponent CellBuildingEnt_OwnerCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();

    internal ref CellUnitComponent CellEnt_CellUnitCom(params int[] xy) => ref _cellEnttts[xy[X], xy[Y]].Get<CellUnitComponent>();
    
    internal ref CellSupportVisionComponent CellEnt_CellSupVisCom(params int[] xy) => ref _cellEnttts[xy[X], xy[Y]].Get<CellSupportVisionComponent>();
    internal ref CellEffectComponent CellEnt_CellEffectCom(params int[] xy) => ref _cellEnttts[xy[X], xy[Y]].Get<CellEffectComponent>();  
    internal ref CellBaseOperationsComponent CellBaseOperEnt_CellBaseOperCom => ref _cellBaseOperationsEnt.Get<CellBaseOperationsComponent>();

    internal int Xamount => _cellEnts.GetUpperBound(X) + 1;
    internal int Yamount => _cellEnts.GetUpperBound(Y) + 1;

    internal int XYForArray => Instance.StartValuesGameConfig.XY_FOR_ARRAY;

    internal int X => Instance.StartValuesGameConfig.X;
    internal int Y => Instance.StartValuesGameConfig.Y;





    #region Up

    private EcsEntity _foodEntity;
    private EcsEntity _woodEntity;
    private EcsEntity _oreEntity;
    private EcsEntity _ironEntity;
    private EcsEntity _goldEntity;


    internal ref AmountDictionaryComponent FoodEnt_AmountDictCom => ref _foodEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent FoodEntityImageComponent => ref _foodEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent FoodEntityTextMeshProGUIComponent => ref _foodEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent FoodEntityMistakeComponent => ref _foodEntity.Get<MistakeComponent>();

    internal ref AmountDictionaryComponent WoodEAmountDictC => ref _woodEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent WoodEntityImageComponent => ref _woodEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent WoodEntityTextMeshProGUIComponent => ref _woodEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent WoodEntityMistakeComponent => ref _woodEntity.Get<MistakeComponent>();

    internal ref AmountDictionaryComponent OreEAmountDictC => ref _oreEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent OreEntityImageComponent => ref _oreEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent OreEntityTextMeshProGUIComponent => ref _oreEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent OreEntityMistakeComponent => ref _oreEntity.Get<MistakeComponent>();

    internal ref AmountDictionaryComponent IronEAmountDictC => ref _ironEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent IronEntityImageComponent => ref _ironEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent IronEntityTextMeshProGUIComponent => ref _ironEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent IronEntityMistakeComponent => ref _ironEntity.Get<MistakeComponent>();

    internal ref AmountDictionaryComponent GoldEAmountDictC => ref _goldEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent GoldEntityImageComponent => ref _goldEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent GoldEntityTextMeshProGUIComponent => ref _goldEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent GoldEntityMistakeComponent => ref _goldEntity.Get<MistakeComponent>();

    #endregion


    #region Down

    private EcsEntity _donerEntity;
    private EcsEntity _truceEntity;

    private EcsEntity _takerKingEntity;
    private EcsEntity _takerPawnEntity;
    private EcsEntity _takerRookEntity;
    private EcsEntity _takerBishopEntity;


    internal ref ButtonComponent DonerEntityButtonComponent => ref _donerEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent DonerEntityTextMeshProGUIComponent => ref _donerEntity.Get<TextMeshProGUIComponent>();
    internal ref ActivatedDictionaryComponent DonerEntityIsActivatedDictionaryComponent => ref _donerEntity.Get<ActivatedDictionaryComponent>();
    internal ref MistakeComponent DonerEntityMistakeComponent => ref _donerEntity.Get<MistakeComponent>();

    internal ref ButtonComponent TruceEnt_ButtonCom => ref _truceEntity.Get<ButtonComponent>();
    internal ref ActivatedDictionaryComponent TruceEnt_ActivatedDictCom => ref _truceEntity.Get<ActivatedDictionaryComponent>();

    internal ref UnitTypeComponent TakerKingEntityUnitTypeComponent => ref _takerKingEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerKingEntityButtonComponent => ref _takerKingEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent TakerKingEntityTextMeshProGUIComponent => ref _takerKingEntity.Get<TextMeshProGUIComponent>();

    internal ref UnitTypeComponent TakerPawnEntityUnitTypeComponent => ref _takerPawnEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerPawnEntityButtonComponent => ref _takerPawnEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent TakerPawnEntityTextMeshProGUIComponent => ref _takerPawnEntity.Get<TextMeshProGUIComponent>();

    internal ref UnitTypeComponent TakerRookEntityUnitTypeComponent => ref _takerRookEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerRookEntityButtonComponent => ref _takerRookEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent TakerRookEntityTextMeshProGUIComponent => ref _takerRookEntity.Get<TextMeshProGUIComponent>();

    internal ref UnitTypeComponent TakerBishopEntityUnitTypeComponent => ref _takerBishopEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerBishopEntityButtonComponent => ref _takerBishopEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent TakerBishopEntityTextMeshProGUIComponent => ref _takerBishopEntity.Get<TextMeshProGUIComponent>();

    #endregion


    #region InputEntity

    private EcsEntity _inputEnt;

    internal ref MouseClickComponent InputEntityMouseClickComponent => ref _inputEnt.Get<MouseClickComponent>();

    #endregion


    #region Info

    private EcsEntity _infoEntity;

    internal ref UnitsInfoComponent InfoEnt_UnitsInfoCom => ref _infoEntity.Get<UnitsInfoComponent>();
    internal ref BuildingsInfoComponent InfoEnt_BuildingsInfoCom => ref _infoEntity.Get<BuildingsInfoComponent>();
    internal ref UpgradeInfoComponent InfoEnt_UpgradeInfoCom => ref _infoEntity.Get<UpgradeInfoComponent>();

    #endregion


    #region UpdatorEntity

    private EcsEntity _updatorEntity;

    internal ref AmountComponent UpdatorEntityAmountComponent => ref _updatorEntity.Get<AmountComponent>();
    internal ref ActiveComponent UpdatorEntityActiveComponent => ref _updatorEntity.Get<ActiveComponent>();

    #endregion


    #region RPCEntity

    private EcsEntity _rPCGeneralEntity;
    internal ref RpcComponent RpcGeneralEnt_FromInfoCom => ref _rPCGeneralEntity.Get<RpcComponent>();

    #endregion


    #region ReadyEntity

    private EcsEntity _readyEntity;

    internal ref ActivatedDictionaryComponent ReadyEntIsActivatedDictCom => ref _readyEntity.Get<ActivatedDictionaryComponent>();
    internal ref StartGameComponent ReadyEntStartGameCom => ref _readyEntity.Get<StartGameComponent>();

    #endregion


    #region SelectedUnitEntity

    private EcsEntity _selectedUnitEntity;

    internal ref UnitTypeComponent SelectedUnitEntUnitTypeCom => ref _selectedUnitEntity.Get<UnitTypeComponent>();

    #endregion


    #region SelectorEntity

    private EcsEntity _selectorEntity;

    internal ref SelectorComponent SelectorEntSelectorCom => ref _selectorEntity.Get<SelectorComponent>();
    internal ref RaycastHit2DComponent RayComponentSelectorEnt => ref _selectorEntity.Get<RaycastHit2DComponent>();

    #endregion


    #region SoundEntity

    private EcsEntity _soundEnt;
    internal ref SoundComponent SoundEntSoundCom => ref _soundEnt.Get<SoundComponent>();

    #endregion


    #region EndGameEntity

    private EcsEntity _endGameEnt;
    internal ref EndGameComponent EndGameEntEndGameCom => ref _endGameEnt.Get<EndGameComponent>();

    #endregion


    #region ZoneEntity

    private EcsEntity _zoneEnt;
    internal ref ZoneComponent ZoneComponentRef => ref _zoneEnt.Get<ZoneComponent>();

    #endregion


    #region AnimationEntity

    private EcsEntity _animationEnt;
    internal ref AnimationAttackUnitComponent AnimationAttackUnitComponent => ref _animationEnt.Get<AnimationAttackUnitComponent>();

    #endregion


    #region Ability

    #region UniqueAbilityOneEnt

    private EcsEntity _uniqueFirstAbilityEnt;
    internal ref ButtonComponent Unique1AbilityEnt_ButtonCom => ref _uniqueFirstAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent UniqueFirstAbilityEnt_TextMeshProGUICom => ref _uniqueFirstAbilityEnt.Get<TextMeshProGUIComponent>();

    #endregion


    #region UniqueAbilityTwoEnt

    private EcsEntity _uniqueSecondAbilityEnt;
    internal ref ButtonComponent Unique2AbilityEnt_ButtonCom => ref _uniqueSecondAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent Unique2AbilityEnt_TextMeshProGUICom => ref _uniqueSecondAbilityEnt.Get<TextMeshProGUIComponent>();

    #endregion


    #region UniqueAbilityThreeEnt

    private EcsEntity _uniqueThirdAbilityEnt;
    internal ref ButtonComponent Unique3AbilityEnt_ButtonCom => ref _uniqueThirdAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent Unique3AbilityEnt_TextMeshProGUICom => ref _uniqueThirdAbilityEnt.Get<TextMeshProGUIComponent>();

    #endregion

    #endregion



    public EntitiesGeneralManager(EcsWorld ecsWorld) : base(ecsWorld)
    {
        #region Economy

        _foodEntity = GameWorld.NewEntity();
        _woodEntity = GameWorld.NewEntity();
        _oreEntity = GameWorld.NewEntity();
        _ironEntity = GameWorld.NewEntity();
        _goldEntity = GameWorld.NewEntity();

        FoodEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.ObjectPool.FoodAmmountText;
        WoodEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.ObjectPool.WoodAmmountText;
        OreEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.ObjectPool.OreAmmountText;
        IronEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.ObjectPool.IronAmmountText;
        GoldEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.ObjectPool.GoldAmmountText;

        FoodEnt_AmountDictCom.AmountDict = new Dictionary<bool, int>();
        WoodEAmountDictC.AmountDict = new Dictionary<bool, int>();
        OreEAmountDictC.AmountDict = new Dictionary<bool, int>();
        IronEAmountDictC.AmountDict = new Dictionary<bool, int>();
        GoldEAmountDictC.AmountDict = new Dictionary<bool, int>();

        FoodEnt_AmountDictCom.AmountDict.Add(true, default);
        FoodEnt_AmountDictCom.AmountDict.Add(false, default);

        WoodEAmountDictC.AmountDict.Add(true, default);
        WoodEAmountDictC.AmountDict.Add(false, default);

        OreEAmountDictC.AmountDict.Add(true, default);
        OreEAmountDictC.AmountDict.Add(false, default);

        IronEAmountDictC.AmountDict.Add(true, default);
        IronEAmountDictC.AmountDict.Add(false, default);

        GoldEAmountDictC.AmountDict.Add(true, default);
        GoldEAmountDictC.AmountDict.Add(false, default);

        #endregion


        #region TakerUnits

        _takerKingEntity = GameWorld.NewEntity();

        TakerKingEntityUnitTypeComponent.UnitType = UnitTypes.King;
        TakerKingEntityButtonComponent.Button = Instance.ObjectPool.GameDownTakerKingButton;


        _takerPawnEntity = GameWorld.NewEntity();

        TakerPawnEntityUnitTypeComponent.UnitType = UnitTypes.Pawn;
        TakerPawnEntityButtonComponent.Button = Instance.ObjectPool.GameDownTakerPawnButton;


        _takerRookEntity = GameWorld.NewEntity();

        TakerRookEntityUnitTypeComponent.UnitType = UnitTypes.Rook;
        TakerRookEntityButtonComponent.Button = Instance.ObjectPool.GameDownTakerRookButton;


        _takerBishopEntity = GameWorld.NewEntity();

        TakerBishopEntityUnitTypeComponent.UnitType = UnitTypes.Bishop;
        TakerBishopEntityButtonComponent.Button = Instance.ObjectPool.GameDownTakerBishopButton;

        #endregion


        #region Doner

        _donerEntity = GameWorld.NewEntity();

        DonerEntityButtonComponent.Button = ObjectPool.DoneButton;

        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary = new Dictionary<bool, bool>();
        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary.Add(true, default);
        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary.Add(false, default);


        _truceEntity = GameWorld.NewEntity();

        TruceEnt_ButtonCom.Button = ObjectPool.TruceButton;

        TruceEnt_ActivatedDictCom.IsActivatedDictionary = new Dictionary<bool, bool>();
        TruceEnt_ActivatedDictCom.IsActivatedDictionary.Add(true, default);
        TruceEnt_ActivatedDictCom.IsActivatedDictionary.Add(false, default);

        #endregion


        #region Info

        _infoEntity = GameWorld.NewEntity();

        InfoEnt_UnitsInfoCom.IsSettedKingDict = new Dictionary<bool, bool>();
        InfoEnt_UnitsInfoCom.AmountKingDict = new Dictionary<bool, int>();
        InfoEnt_UnitsInfoCom.AmountPawnDict = new Dictionary<bool, int>();
        InfoEnt_UnitsInfoCom.AmountRookDict = new Dictionary<bool, int>();
        InfoEnt_UnitsInfoCom.AmountBishopDict = new Dictionary<bool, int>();

        InfoEnt_UnitsInfoCom.IsSettedKingDict.Add(true, default);
        InfoEnt_UnitsInfoCom.IsSettedKingDict.Add(false, default);

        InfoEnt_UnitsInfoCom.AmountKingDict.Add(true, default);
        InfoEnt_UnitsInfoCom.AmountKingDict.Add(false, default);

        InfoEnt_UnitsInfoCom.AmountPawnDict.Add(true, default);
        InfoEnt_UnitsInfoCom.AmountPawnDict.Add(false, default);

        InfoEnt_UnitsInfoCom.AmountRookDict.Add(true, default);
        InfoEnt_UnitsInfoCom.AmountRookDict.Add(false, default);

        InfoEnt_UnitsInfoCom.AmountBishopDict.Add(true, default);
        InfoEnt_UnitsInfoCom.AmountBishopDict.Add(false, default);



        InfoEnt_BuildingsInfoCom.IsSettedCityDict = new Dictionary<bool, bool>();
        InfoEnt_BuildingsInfoCom.XySettedCityDict = new Dictionary<bool, int[]>();
        InfoEnt_BuildingsInfoCom.AmountFarmDict = new Dictionary<bool, int>();
        InfoEnt_BuildingsInfoCom.AmountWoodcutterDict = new Dictionary<bool, int>();
        InfoEnt_BuildingsInfoCom.AmountMineDict = new Dictionary<bool, int>();

        InfoEnt_BuildingsInfoCom.IsSettedCityDict.Add(true, default);
        InfoEnt_BuildingsInfoCom.IsSettedCityDict.Add(false, default);

        InfoEnt_BuildingsInfoCom.XySettedCityDict.Add(true, default);
        InfoEnt_BuildingsInfoCom.XySettedCityDict.Add(false, default);

        InfoEnt_BuildingsInfoCom.AmountFarmDict.Add(true, default);
        InfoEnt_BuildingsInfoCom.AmountFarmDict.Add(false, default);

        InfoEnt_BuildingsInfoCom.AmountWoodcutterDict.Add(true, default);
        InfoEnt_BuildingsInfoCom.AmountWoodcutterDict.Add(false, default);

        InfoEnt_BuildingsInfoCom.AmountMineDict.Add(true, default);
        InfoEnt_BuildingsInfoCom.AmountMineDict.Add(false, default);



        InfoEnt_UpgradeInfoCom.AmountUpgradePawnDict = new Dictionary<bool, int>();
        InfoEnt_UpgradeInfoCom.AmountUpgradeRookDict = new Dictionary<bool, int>();
        InfoEnt_UpgradeInfoCom.AmountUpgradeBishopDict = new Dictionary<bool, int>();

        InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict = new Dictionary<bool, int>();
        InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict = new Dictionary<bool, int>();
        InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict = new Dictionary<bool, int>();


        InfoEnt_UpgradeInfoCom.AmountUpgradePawnDict.Add(true, default);
        InfoEnt_UpgradeInfoCom.AmountUpgradePawnDict.Add(false, default);

        InfoEnt_UpgradeInfoCom.AmountUpgradeRookDict.Add(true, default);
        InfoEnt_UpgradeInfoCom.AmountUpgradeRookDict.Add(false, default);

        InfoEnt_UpgradeInfoCom.AmountUpgradeBishopDict.Add(true, default);
        InfoEnt_UpgradeInfoCom.AmountUpgradeBishopDict.Add(false, default);


        InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict.Add(true, default);
        InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict.Add(false, default);

        InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict.Add(true, default);
        InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict.Add(false, default);

        InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict.Add(true, default);
        InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict.Add(false, default);

        #endregion


        #region Ready

        _readyEntity = GameWorld.NewEntity();

        ReadyEntIsActivatedDictCom.IsActivatedDictionary = new Dictionary<bool, bool>();
        ReadyEntIsActivatedDictCom.IsActivatedDictionary.Add(true, default);
        ReadyEntIsActivatedDictCom.IsActivatedDictionary.Add(false, default);

        #endregion


        #region Updator

        _updatorEntity = GameWorld.NewEntity();

        UpdatorEntityAmountComponent.Amount = default;
        UpdatorEntityActiveComponent.IsActived = default;

        #endregion


        #region SelectedUnitEntity

        _selectedUnitEntity = GameWorld.NewEntity();
        SelectedUnitEntUnitTypeCom.UnitType = UnitTypes.None;

        #endregion


        #region Selector

        _selectorEntity = GameWorld.NewEntity()
            .Replace(new SelectorComponent(StartValuesGameConfig))
            .Replace(new RaycastHit2DComponent());

        #endregion


        #region Ability

        _uniqueFirstAbilityEnt = GameWorld.NewEntity();

        Unique1AbilityEnt_ButtonCom.Button = Instance.ObjectPool.UniqueFirstAbilityButton;
        UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI = ObjectPool.UniqueFirstAbilityText;


        _uniqueSecondAbilityEnt = GameWorld.NewEntity();

        Unique2AbilityEnt_ButtonCom.Button = Instance.ObjectPool.UniqueSecondAbilityButton;
        Unique2AbilityEnt_TextMeshProGUICom.TextMeshProUGUI = Instance.ObjectPool.UniqueSecondAbilityText;


        _uniqueThirdAbilityEnt = GameWorld.NewEntity();

        Unique3AbilityEnt_ButtonCom.Button = Instance.ObjectPool.UniqueThirdAbilityButton;
        Unique3AbilityEnt_TextMeshProGUICom.TextMeshProUGUI = Instance.ObjectPool.UniqueThirdAbilityText;


        #endregion


        _rPCGeneralEntity = GameWorld.NewEntity();

        RpcGeneralEnt_FromInfoCom.FromInfo = default;






        _inputEnt = GameWorld.NewEntity()
            .Replace(new MouseClickComponent());



        _soundEnt = GameWorld.NewEntity()
            .Replace(new SoundComponent());

        _endGameEnt = GameWorld.NewEntity()
            .Replace(new EndGameComponent());


        _animationEnt = GameWorld.NewEntity()
            .Replace(new AnimationAttackUnitComponent());

        _zoneEnt = GameWorld.NewEntity()
            .Replace(new ZoneComponent());


        #region Cells

        _cellEnts = new EcsEntity[StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];
        _cellBuildingEnts = new EcsEntity[StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];
        _cellEnvironmentEnts = new EcsEntity[StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];
        _cellEnttts = new EcsEntity[StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];


        _cellBaseOperationsEnt = GameWorld.NewEntity();
        _cellBaseOperationsEnt.Replace(new CellBaseOperationsComponent(this));

        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                _cellEnts[x, y] = GameWorld.NewEntity();
                _cellBuildingEnts[x, y] = GameWorld.NewEntity();
                _cellEnvironmentEnts[x, y] = GameWorld.NewEntity();
                _cellEnttts[x, y] = GameWorld.NewEntity();

                _cellEnts[x, y]
                    .Replace(new CellBaseComponent(StartValuesGameConfig, ObjectPool, x, y))
                    .Replace(new CellComponent(x, y));

                _cellBuildingEnts[x, y]
                    .Replace(new CellBuildingComponent(ObjectPool, x, y))
                    .Replace(new BuildingTypeComponent())
                    .Replace(new CellComponent(x, y))
                    .Replace(new OwnerComponent());

                _cellEnvironmentEnts[x, y]
                        .Replace(new CellEnvironmentComponent(this, ObjectPool, x, y))
                        .Replace(new CellComponent(x, y));

                _cellEnttts[x, y]
                    .Replace(new CellUnitComponent(this, StartValuesGameConfig, ObjectPool, x, y))
                    .Replace(new CellSupportVisionComponent(ObjectPool, x, y))
                    .Replace(new CellEffectComponent(ObjectPool, x, y));
            }
        }

        #endregion
    }
}
