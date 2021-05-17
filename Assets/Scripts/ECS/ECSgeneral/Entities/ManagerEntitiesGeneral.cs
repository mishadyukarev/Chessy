using Leopotam.Ecs;
using System.Collections.Generic;
using static MainGame;

public sealed class EntitiesGeneralManager : EntitiesManager
{
    private StartValuesGameConfig StartValuesGameConfig => InstanceGame.StartValuesGameConfig;



    private EcsEntity _soundEntity;
    private EcsEntity _readyEntity;
    private EcsEntity _selectorUnitEntity;
    private EcsEntity _theEndGameEntity;
    private EcsEntity _startGameEntity;
    private EcsEntity _animationAttackUnitEntity;
    private EcsEntity _zoneEntity;





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
    internal ref DonerComponent DonerComponent => ref  _refreshEntity.Get<DonerComponent>();

    #endregion


    #region EconomyEntity

    private EcsEntity _economyEntity;

    internal ref UnitsInfoComponent UnitsInfoComponent => ref _economyEntity.Get<UnitsInfoComponent>();
    internal ref BuildingsInfoComponent BuildingsInfoComponent => ref _economyEntity.Get<BuildingsInfoComponent>();
    internal ref EconomyUIComponent EconomyUIComponent => ref _economyEntity.Get<EconomyUIComponent>();


    #endregion


    #region Economy

    #region FoodEntity

    internal EcsEntity FoodEntity;

    internal ref AmountDictionaryComponent FoodEntityAmountDictionaryComponent => ref FoodEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent FoodEntityImageComponent => ref FoodEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent FoodEntityTextMeshProGUIComponent => ref FoodEntity.Get<TextMeshProGUIComponent>();

    #endregion


    #region WoodEntity

    internal EcsEntity WoodEntity;

    internal ref AmountDictionaryComponent WoodEntityAmountDictionaryComponent => ref WoodEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent WoodEntityImageComponent => ref WoodEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent WoodEntityTextMeshProGUIComponent => ref WoodEntity.Get<TextMeshProGUIComponent>();

    #endregion


    #region OreEntity

    internal EcsEntity OreEntity;

    internal ref AmountDictionaryComponent OreEntityAmountDictionaryComponent => ref OreEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent OreEntityImageComponent => ref OreEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent OreEntityTextMeshProGUIComponent => ref OreEntity.Get<TextMeshProGUIComponent>();

    #endregion


    #region IronEntity

    internal EcsEntity IronEntity;

    internal ref AmountDictionaryComponent IronEntityAmountDictionaryComponent => ref IronEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent IronEntityImageComponent => ref IronEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent IronEntityTextMeshProGUIComponent => ref IronEntity.Get<TextMeshProGUIComponent>();

    #endregion


    #region GoldEntity

    internal EcsEntity GoldEntity;

    internal ref AmountDictionaryComponent GoldEntityAmountDictionaryComponent => ref GoldEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent GoldEntityImageComponent => ref GoldEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent GoldEntityTextMeshProGUIComponent => ref GoldEntity.Get<TextMeshProGUIComponent>();

    #endregion

    #endregion


    #region InputEntity

    private EcsEntity _inputEntity;

    internal ref MouseClickComponent InputEntityMouseClickComponent => ref _inputEntity.Get<MouseClickComponent>();

    #endregion


    #region InfoEntity

    private EcsEntity _infoEntity;

    //internal ref UnitsInfoComponent UnitsInfoComponent => ref _infoEntity.Get<UnitsInfoComponent>();

    #endregion


    #region Else Entity

    private EcsEntity _elseEntity;

    internal EcsComponentRef<SoundComponent> SoundComponentRef => _soundEntity.Ref<SoundComponent>();
    internal EcsComponentRef<ReadyComponent> ReadyComponentRef => _readyEntity.Ref<ReadyComponent>();
    internal EcsComponentRef<TakerUnitUnitComponent> SelectorUnitComponent => _selectorUnitEntity.Ref<TakerUnitUnitComponent>();
    internal EcsComponentRef<TheEndGameComponent> TheEndGameComponentRef => _theEndGameEntity.Ref<TheEndGameComponent>();
    internal EcsComponentRef<StartGameComponent> StartGameComponentRef => _startGameEntity.Ref<StartGameComponent>();

    internal EcsComponentRef<AnimationAttackUnitComponent> AnimationAttackUnitComponentRef => _animationAttackUnitEntity.Ref<AnimationAttackUnitComponent>();
    internal EcsComponentRef<ZoneComponent> ZoneComponentRef => _zoneEntity.Ref<ZoneComponent>();


    #endregion



    public EntitiesGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities()
    {

        #region EconomyEntities

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


        _infoEntity = GameWorld.NewEntity()
            .Replace(new UnitsInfoComponent(StartValuesGameConfig))
            .Replace(new BuildingsInfoComponent(StartValuesGameConfig));



        _economyEntity = GameWorld.NewEntity()
            .Replace(new UnitsInfoComponent(StartValuesGameConfig))
            .Replace(new BuildingsInfoComponent(StartValuesGameConfig))
            .Replace(new EconomyUIComponent(InstanceGame.GameObjectPool));

        _selectorEntity = GameWorld.NewEntity()
            .Replace(new SelectorComponent(StartValuesGameConfig))
            .Replace(new SelectedUnitComponent())
            .Replace(new RayComponent());

        _refreshEntity = GameWorld.NewEntity()
            .Replace(new RefreshComponent())
            .Replace(new DonerComponent());






        _inputEntity = GameWorld.NewEntity()
            .Replace(new MouseClickComponent());



        _soundEntity = GameWorld.NewEntity()
            .Replace(new SoundComponent());

        _readyEntity = GameWorld.NewEntity()
            .Replace(new ReadyComponent());

        _selectorUnitEntity = GameWorld.NewEntity()
            .Replace(new TakerUnitUnitComponent());

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
