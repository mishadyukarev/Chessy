using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

public sealed class EntitiesGeneralManager : EntitiesManager
{
    private EcsEntity _soundEntity;
    private EcsEntity _readyEntity;
    private EcsEntity _theEndGameEntity;
    private EcsEntity _startGameEntity;
    private EcsEntity _animationAttackUnitEntity;
    private EcsEntity _zoneEntity;


    #region Economy

    #region FoodEntity

    internal EcsEntity FoodEntity;

    internal ref AmountDictionaryComponent FoodEntityAmountDictionaryComponent => ref FoodEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent FoodEntityImageComponent => ref FoodEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent FoodEntityTextMeshProGUIComponent => ref FoodEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent FoodEntityMistakeComponent => ref FoodEntity.Get<MistakeComponent>();

    #endregion


    #region WoodEntity

    internal EcsEntity WoodEntity;

    internal ref AmountDictionaryComponent WoodEntityAmountDictionaryComponent => ref WoodEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent WoodEntityImageComponent => ref WoodEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent WoodEntityTextMeshProGUIComponent => ref WoodEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent WoodEntityMistakeComponent => ref WoodEntity.Get<MistakeComponent>();

    #endregion


    #region OreEntity

    internal EcsEntity OreEntity;

    internal ref AmountDictionaryComponent OreEntityAmountDictionaryComponent => ref OreEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent OreEntityImageComponent => ref OreEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent OreEntityTextMeshProGUIComponent => ref OreEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent OreEntityMistakeComponent => ref OreEntity.Get<MistakeComponent>();

    #endregion


    #region IronEntity

    internal EcsEntity IronEntity;

    internal ref AmountDictionaryComponent IronEntityAmountDictionaryComponent => ref IronEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent IronEntityImageComponent => ref IronEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent IronEntityTextMeshProGUIComponent => ref IronEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent IronEntityMistakeComponent => ref IronEntity.Get<MistakeComponent>();

    #endregion


    #region GoldEntity

    internal EcsEntity GoldEntity;

    internal ref AmountDictionaryComponent GoldEntityAmountDictionaryComponent => ref GoldEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent GoldEntityImageComponent => ref GoldEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent GoldEntityTextMeshProGUIComponent => ref GoldEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent GoldEntityMistakeComponent => ref GoldEntity.Get<MistakeComponent>();

    #endregion

    #endregion


    #region InputEntity

    private EcsEntity _inputEntity;

    internal ref MouseClickComponent InputEntityMouseClickComponent => ref _inputEntity.Get<MouseClickComponent>();

    #endregion


    #region TakerUnits

    #region TakerKingEntity

    private EcsEntity _takerKingEntity;

    internal ref UnitTypeComponent TakerKingEntityUnitTypeComponent => ref _takerKingEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerKingEntityButtonComponent => ref _takerKingEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent TakerKingEntityTextMeshProGUIComponent => ref _takerKingEntity.Get<TextMeshProGUIComponent>();

    #endregion


    #region TakerPawnEntity

    private EcsEntity _takerPawnEntity;

    internal ref UnitTypeComponent TakerPawnEntityUnitTypeComponent => ref _takerPawnEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerPawnEntityButtonComponent => ref _takerPawnEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent TakerPawnEntityTextMeshProGUIComponent => ref _takerPawnEntity.Get<TextMeshProGUIComponent>();

    #endregion


    #region TakerRookEntity

    private EcsEntity _takerRookEntity;

    internal ref UnitTypeComponent TakerRookEntityUnitTypeComponent => ref _takerRookEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerRookEntityButtonComponent => ref _takerRookEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent TakerRookEntityTextMeshProGUIComponent => ref _takerRookEntity.Get<TextMeshProGUIComponent>();

    #endregion


    #region TakerBishopEntity

    private EcsEntity _takerBishopEntity;

    internal ref UnitTypeComponent TakerBishopEntityUnitTypeComponent => ref _takerBishopEntity.Get<UnitTypeComponent>();
    internal ref ButtonComponent TakerBishopEntityButtonComponent => ref _takerBishopEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent TakerBishopEntityTextMeshProGUIComponent => ref _takerBishopEntity.Get<TextMeshProGUIComponent>();

    #endregion

    #endregion


    #region DonerEntity

    private EcsEntity _donerEntity;

    internal ref ButtonComponent DonerEntityButtonComponent => ref _donerEntity.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent DonerEntityTextMeshProGUIComponent => ref _donerEntity.Get<TextMeshProGUIComponent>();
    internal ref IsActivatedDictionaryComponent DonerEntityIsActivatedDictionaryComponent => ref _donerEntity.Get<IsActivatedDictionaryComponent>();
    internal ref MistakeComponent DonerEntityMistakeComponent => ref _donerEntity.Get<MistakeComponent>();

    #endregion


    #region InfoEntity

    private EcsEntity _infoEntity;

    internal ref UnitsInfoComponent InfoEntityUnitsInfoComponent => ref _infoEntity.Get<UnitsInfoComponent>();
    internal ref BuildingsInfoComponent InfoEntityBuildingsInfoComponent => ref _infoEntity.Get<BuildingsInfoComponent>();

    #endregion


    #region CellEntity

    private EcsEntity[,] _cellsEntity;

    internal ref CellComponent CellComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent>();
    internal ref  CellComponent.EnvironmentComponent CellEnvironmentComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent.EnvironmentComponent>();
    internal ref CellComponent.SupportVisionComponent CellSupportVisionComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent.SupportVisionComponent>();
    internal ref CellComponent.UnitComponent CellUnitComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent.UnitComponent>();
    internal ref CellComponent.BuildingComponent CellBuildingComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent.BuildingComponent>();


    internal int Xcount => _cellsEntity.GetUpperBound(X) + 1;
    internal int Ycount => _cellsEntity.GetUpperBound(Y) + 1;

    internal int XYForArray = InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY;

    internal int X = InstanceGame.StartValuesGameConfig.X;
    internal int Y = InstanceGame.StartValuesGameConfig.Y;

    #endregion


    #region SelectorEntity

    private EcsEntity _selectorEntity;

    internal ref SelectorComponent SelectorComponentSelectorEnt => ref _selectorEntity.Get<SelectorComponent>();
    internal ref SelectedUnitComponent SelectedUnitComponentSelectorEnt => ref _selectorEntity.Get<SelectedUnitComponent>();
    internal ref RayComponent RayComponentSelectorEnt => ref _selectorEntity.Get<RayComponent>();

    #endregion


    #region RefreshEntity

    private EcsEntity _refreshEntity;

    internal ref RefreshComponent RefreshComponent => ref _refreshEntity.Get<RefreshComponent>();


    #endregion



    #region Else Entity

    private EcsEntity _elseEntity;

    internal EcsComponentRef<SoundComponent> SoundComponentRef => _soundEntity.Ref<SoundComponent>();
    internal EcsComponentRef<ReadyComponent> ReadyComponentRef => _readyEntity.Ref<ReadyComponent>();
    internal EcsComponentRef<TheEndGameComponent> TheEndGameComponentRef => _theEndGameEntity.Ref<TheEndGameComponent>();
    internal EcsComponentRef<StartGameComponent> StartGameComponentRef => _startGameEntity.Ref<StartGameComponent>();

    internal EcsComponentRef<AnimationAttackUnitComponent> AnimationAttackUnitComponentRef => _animationAttackUnitEntity.Ref<AnimationAttackUnitComponent>();
    internal EcsComponentRef<ZoneComponent> ZoneComponentRef => _zoneEntity.Ref<ZoneComponent>();


    #endregion



    public EntitiesGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities()
    {

        #region Economy

        FoodEntity = GameWorld.NewEntity();
        WoodEntity = GameWorld.NewEntity();
        OreEntity = GameWorld.NewEntity();
        IronEntity = GameWorld.NewEntity();
        GoldEntity = GameWorld.NewEntity();

        FoodEntityTextMeshProGUIComponent.TextMeshProUGUI = InstanceGame.GameObjectPool.FoodAmmountText;
        WoodEntityTextMeshProGUIComponent.TextMeshProUGUI = InstanceGame.GameObjectPool.WoodAmmountText;
        OreEntityTextMeshProGUIComponent.TextMeshProUGUI = InstanceGame.GameObjectPool.OreAmmountText;
        IronEntityTextMeshProGUIComponent.TextMeshProUGUI = InstanceGame.GameObjectPool.IronAmmountText;
        GoldEntityTextMeshProGUIComponent.TextMeshProUGUI = InstanceGame.GameObjectPool.GoldAmmountText;

        FoodEntityAmountDictionaryComponent.AmountDictionary = new Dictionary<bool, int>();
        WoodEntityAmountDictionaryComponent.AmountDictionary = new Dictionary<bool, int>();
        OreEntityAmountDictionaryComponent.AmountDictionary = new Dictionary<bool, int>();
        IronEntityAmountDictionaryComponent.AmountDictionary = new Dictionary<bool, int>();
        GoldEntityAmountDictionaryComponent.AmountDictionary = new Dictionary<bool, int>();

        FoodEntityAmountDictionaryComponent.AmountDictionary.Add(true, default);
        FoodEntityAmountDictionaryComponent.AmountDictionary.Add(false, default);

        WoodEntityAmountDictionaryComponent.AmountDictionary.Add(true, default);
        WoodEntityAmountDictionaryComponent.AmountDictionary.Add(false, default);

        OreEntityAmountDictionaryComponent.AmountDictionary.Add(true, default);
        OreEntityAmountDictionaryComponent.AmountDictionary.Add(false, default);

        IronEntityAmountDictionaryComponent.AmountDictionary.Add(true, default);
        IronEntityAmountDictionaryComponent.AmountDictionary.Add(false, default);

        GoldEntityAmountDictionaryComponent.AmountDictionary.Add(true, default);
        GoldEntityAmountDictionaryComponent.AmountDictionary.Add(false, default);

        #endregion


        #region TakerUnits

        _takerKingEntity = GameWorld.NewEntity();

        TakerKingEntityUnitTypeComponent.UnitType = UnitTypes.King;
        TakerKingEntityButtonComponent.Button = InstanceGame.GameObjectPool.GameDownTakerKingButton;


        _takerPawnEntity = GameWorld.NewEntity();

        TakerPawnEntityUnitTypeComponent.UnitType = UnitTypes.Pawn;
        TakerPawnEntityButtonComponent.Button = InstanceGame.GameObjectPool.GameDownTakerPawnButton;


        _takerRookEntity = GameWorld.NewEntity();

        TakerRookEntityUnitTypeComponent.UnitType = UnitTypes.Rook;
        TakerRookEntityButtonComponent.Button = InstanceGame.GameObjectPool.GameDownTakerRookButton;


        _takerBishopEntity = GameWorld.NewEntity();

        TakerBishopEntityUnitTypeComponent.UnitType = UnitTypes.Bishop;
        TakerBishopEntityButtonComponent.Button = InstanceGame.GameObjectPool.GameDownTakerBishopButton;

        #endregion


        #region Doner

        _donerEntity = GameWorld.NewEntity();

        DonerEntityButtonComponent.Button = InstanceGame.GameObjectPool.DoneButton;

        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary = new Dictionary<bool, bool>();
        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary.Add(true, default);
        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary.Add(false, default);

        #endregion


        #region Info

        _infoEntity = GameWorld.NewEntity();

        InfoEntityUnitsInfoComponent.IsSettedKingDictionary = new Dictionary<bool, bool>();
        InfoEntityUnitsInfoComponent.AmountKingDictionary = new Dictionary<bool, int>();
        InfoEntityUnitsInfoComponent.AmountPawnDictionary = new Dictionary<bool, int>();
        InfoEntityUnitsInfoComponent.AmountRookDictionary = new Dictionary<bool, int>();
        InfoEntityUnitsInfoComponent.AmountBishopDictionary = new Dictionary<bool, int>();

        InfoEntityUnitsInfoComponent.IsSettedKingDictionary.Add(true, default);
        InfoEntityUnitsInfoComponent.IsSettedKingDictionary.Add(false, default);

        InfoEntityUnitsInfoComponent.AmountKingDictionary.Add(true, default);
        InfoEntityUnitsInfoComponent.AmountKingDictionary.Add(false, default);

        InfoEntityUnitsInfoComponent.AmountPawnDictionary.Add(true, default);
        InfoEntityUnitsInfoComponent.AmountPawnDictionary.Add(false, default);

        InfoEntityUnitsInfoComponent.AmountRookDictionary.Add(true, default);
        InfoEntityUnitsInfoComponent.AmountRookDictionary.Add(false, default);

        InfoEntityUnitsInfoComponent.AmountBishopDictionary.Add(true, default);
        InfoEntityUnitsInfoComponent.AmountBishopDictionary.Add(false, default);



        InfoEntityBuildingsInfoComponent.IsBuildedCityDictionary = new Dictionary<bool, bool>();
        InfoEntityBuildingsInfoComponent.XYsettedCityDictionary = new Dictionary<bool, int[]>();
        InfoEntityBuildingsInfoComponent.AmountFarmDictionary = new Dictionary<bool, int>();
        InfoEntityBuildingsInfoComponent.AmountWoodcutterDictionary = new Dictionary<bool, int>();
        InfoEntityBuildingsInfoComponent.AmountMineDictionary = new Dictionary<bool, int>();

        InfoEntityBuildingsInfoComponent.IsBuildedCityDictionary.Add(true, default);
        InfoEntityBuildingsInfoComponent.IsBuildedCityDictionary.Add(false, default);

        InfoEntityBuildingsInfoComponent.XYsettedCityDictionary.Add(true, default);
        InfoEntityBuildingsInfoComponent.XYsettedCityDictionary.Add(false, default);

        InfoEntityBuildingsInfoComponent.AmountFarmDictionary.Add(true, default);
        InfoEntityBuildingsInfoComponent.AmountFarmDictionary.Add(false, default);

        InfoEntityBuildingsInfoComponent.AmountWoodcutterDictionary.Add(true, default);
        InfoEntityBuildingsInfoComponent.AmountWoodcutterDictionary.Add(false, default);

        InfoEntityBuildingsInfoComponent.AmountMineDictionary.Add(true, default);
        InfoEntityBuildingsInfoComponent.AmountMineDictionary.Add(false, default);

        InfoEntityBuildingsInfoComponent.XYsettedCityDictionary[true] = new int[StartValuesGameConfig.XY_FOR_ARRAY];
        InfoEntityBuildingsInfoComponent.XYsettedCityDictionary[false] = new int[StartValuesGameConfig.XY_FOR_ARRAY];

        #endregion


        _selectorEntity = GameWorld.NewEntity()
            .Replace(new SelectorComponent(StartValuesGameConfig))
            .Replace(new SelectedUnitComponent())
            .Replace(new RayComponent());

        _refreshEntity = GameWorld.NewEntity()
            .Replace(new RefreshComponent());






        _inputEntity = GameWorld.NewEntity()
            .Replace(new MouseClickComponent());



        _soundEntity = GameWorld.NewEntity()
            .Replace(new SoundComponent());

        _readyEntity = GameWorld.NewEntity()
            .Replace(new ReadyComponent());

        _theEndGameEntity = GameWorld.NewEntity()
            .Replace(new TheEndGameComponent());

        _startGameEntity = GameWorld.NewEntity()
            .Replace(new StartGameComponent());

        _animationAttackUnitEntity = GameWorld.NewEntity()
            .Replace(new AnimationAttackUnitComponent());

        _zoneEntity = GameWorld.NewEntity()
            .Replace(new ZoneComponent());


        #region Cells

        _cellsEntity = new EcsEntity[ StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];

        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                _cellsEntity[x, y] = GameWorld.NewEntity();

                _cellsEntity[x, y]
                    .Replace(new CellComponent(x, y))
                    .Replace(new CellComponent.EnvironmentComponent(x, y))
                    .Replace(new CellComponent.SupportVisionComponent(x, y))
                    .Replace(new CellComponent.UnitComponent(x, y))
                    .Replace(new CellComponent.BuildingComponent(x, y));
            }
        }

        #endregion

    }
}
