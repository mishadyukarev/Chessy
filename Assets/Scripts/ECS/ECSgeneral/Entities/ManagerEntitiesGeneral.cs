using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using System.Collections.Generic;
using static MainGame;

public sealed class EntitiesGeneralManager : EntitiesManager
{
    private EcsEntity _soundEntity;

    private EcsEntity _theEndGameEntity;
    private EcsEntity _startGameEntity;
    private EcsEntity _animationAttackUnitEntity;
    private EcsEntity _zoneEntity;


    #region Economy

    #region FoodEntity

    internal EcsEntity FoodEntity;

    internal ref AmountDictionaryComponent FoodEAmountDictC => ref FoodEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent FoodEntityImageComponent => ref FoodEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent FoodEntityTextMeshProGUIComponent => ref FoodEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent FoodEntityMistakeComponent => ref FoodEntity.Get<MistakeComponent>();

    #endregion


    #region WoodEntity

    internal EcsEntity WoodEntity;

    internal ref AmountDictionaryComponent WoodEAmountDictC => ref WoodEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent WoodEntityImageComponent => ref WoodEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent WoodEntityTextMeshProGUIComponent => ref WoodEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent WoodEntityMistakeComponent => ref WoodEntity.Get<MistakeComponent>();

    #endregion


    #region OreEntity

    internal EcsEntity OreEntity;

    internal ref AmountDictionaryComponent OreEAmountDictC => ref OreEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent OreEntityImageComponent => ref OreEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent OreEntityTextMeshProGUIComponent => ref OreEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent OreEntityMistakeComponent => ref OreEntity.Get<MistakeComponent>();

    #endregion


    #region IronEntity

    internal EcsEntity IronEntity;

    internal ref AmountDictionaryComponent IronEAmountDictC => ref IronEntity.Get<AmountDictionaryComponent>();
    internal ref ImageComponent IronEntityImageComponent => ref IronEntity.Get<ImageComponent>();
    internal ref TextMeshProGUIComponent IronEntityTextMeshProGUIComponent => ref IronEntity.Get<TextMeshProGUIComponent>();
    internal ref MistakeComponent IronEntityMistakeComponent => ref IronEntity.Get<MistakeComponent>();

    #endregion


    #region GoldEntity

    internal EcsEntity GoldEntity;

    internal ref AmountDictionaryComponent GoldEAmountDictC => ref GoldEntity.Get<AmountDictionaryComponent>();
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
    internal ref ActivatedDictionaryComponent DonerEntityIsActivatedDictionaryComponent => ref _donerEntity.Get<ActivatedDictionaryComponent>();
    internal ref MistakeComponent DonerEntityMistakeComponent => ref _donerEntity.Get<MistakeComponent>();

    #endregion


    #region Info

    #region InfoKingEntity

    //private EcsEntity _kingInfoEntity;

    //internal ref AmountDictionaryComponent KingInfoEntityAmountDictionaryComponent => ref _kingInfoEntity.Get<AmountDictionaryComponent>();
    //internal ref ActivatedDictionaryComponent KingInfoEntityActivatedDictionaryComponent => ref _kingInfoEntity.Get<ActivatedDictionaryComponent>();

    #endregion


    private EcsEntity _infoEntity;

    internal ref UnitsInfoComponent InfoEntityUnitsInfoComponent => ref _infoEntity.Get<UnitsInfoComponent>();
    internal ref BuildingsInfoComponent InfoEntBuildingsInfoCom => ref _infoEntity.Get<BuildingsInfoComponent>();

    #endregion


    #region UpdatorEntity

    private EcsEntity _updatorEntity;

    internal ref AmountComponent UpdatorEntityAmountComponent => ref _updatorEntity.Get<AmountComponent>();
    internal ref ActiveComponent UpdatorEntityActiveComponent => ref _updatorEntity.Get<ActiveComponent>();

    #endregion


    #region ReadyEntity

    private EcsEntity _readyEntity;

    internal ref ActivatedDictionaryComponent ReadyEntityIsActivatedDictionaryComponent => ref _readyEntity.Get<ActivatedDictionaryComponent>();

    #endregion


    #region CellEntity

    private EcsEntity[,] _cellsEntity;

    internal ref CellComponent CellComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent>();
    internal ref CellComponent.EnvironmentComponent CellEnvironmentComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent.EnvironmentComponent>();
    internal ref CellComponent.SupportVisionComponent CellSupportVisionComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent.SupportVisionComponent>();
    internal ref CellComponent.UnitComponent CellUnitComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent.UnitComponent>();
    internal ref CellComponent.BuildingComponent CellBuildingComponent(params int[] xy) => ref _cellsEntity[xy[X], xy[Y]].Get<CellComponent.BuildingComponent>();


    internal int Xcount => _cellsEntity.GetUpperBound(X) + 1;
    internal int Ycount => _cellsEntity.GetUpperBound(Y) + 1;

    internal int XYForArray = InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY;

    internal int X = InstanceGame.StartValuesGameConfig.X;
    internal int Y = InstanceGame.StartValuesGameConfig.Y;

    #endregion


    #region SelectedUnitEntity

    private EcsEntity _selectedUnitEntity;

    internal ref UnitTypeComponent SelectedUnitEUnitTypeC => ref _selectedUnitEntity.Get<UnitTypeComponent>();

    #endregion


    #region SelectorEntity

    private EcsEntity _selectorEntity;

    internal ref SelectorComponent SelectorESelectorC => ref _selectorEntity.Get<SelectorComponent>();
    internal ref RaycastHit2DComponent RayComponentSelectorEnt => ref _selectorEntity.Get<RaycastHit2DComponent>();

    #endregion



    #region Else Entity

    private EcsEntity _elseEntity;

    internal EcsComponentRef<SoundComponent> SoundComponentRef => _soundEntity.Ref<SoundComponent>();

    internal EcsComponentRef<TheEndGameComponent> TheEndGameComponentRef => _theEndGameEntity.Ref<TheEndGameComponent>();
    internal EcsComponentRef<StartGameComponent> StartGameComponentRef => _startGameEntity.Ref<StartGameComponent>();

    internal EcsComponentRef<AnimationAttackUnitComponent> AnimationAttackUnitComponentRef => _animationAttackUnitEntity.Ref<AnimationAttackUnitComponent>();
    internal EcsComponentRef<ZoneComponent> ZoneComponentRef => _zoneEntity.Ref<ZoneComponent>();


    #endregion


    private EcsEntity _generalRPCEntity;
    internal ref FromInfoComponent GeneralRPCEntFromInfoCom => ref _generalRPCEntity.Get<FromInfoComponent>();
    internal ref ActiveComponent GeneralRPCEntActiveComponent => ref _generalRPCEntity.Get<ActiveComponent>();


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

        FoodEAmountDictC.AmountDict = new Dictionary<bool, int>();
        WoodEAmountDictC.AmountDict = new Dictionary<bool, int>();
        OreEAmountDictC.AmountDict = new Dictionary<bool, int>();
        IronEAmountDictC.AmountDict = new Dictionary<bool, int>();
        GoldEAmountDictC.AmountDict = new Dictionary<bool, int>();

        FoodEAmountDictC.AmountDict.Add(true, default);
        FoodEAmountDictC.AmountDict.Add(false, default);

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

        #region King

        //_kingInfoEntity = GameWorld.NewEntity();

        //KingInfoEntityAmountDictionaryComponent.AmountDictionary = default;

        //KingInfoEntityActivatedDictionaryComponent.IsActivatedDictionary = new Dictionary<bool, bool>();
        //KingInfoEntityActivatedDictionaryComponent.IsActivatedDictionary.Add(true, default);
        //KingInfoEntityActivatedDictionaryComponent.IsActivatedDictionary.Add(false, default);

        #endregion


        _infoEntity = GameWorld.NewEntity();

        InfoEntityUnitsInfoComponent.IsSettedKingDict = new Dictionary<bool, bool>();
        InfoEntityUnitsInfoComponent.AmountKingDict = new Dictionary<bool, int>();
        InfoEntityUnitsInfoComponent.AmountPawnDict = new Dictionary<bool, int>();
        InfoEntityUnitsInfoComponent.AmountRookDict = new Dictionary<bool, int>();
        InfoEntityUnitsInfoComponent.AmountBishopDict = new Dictionary<bool, int>();

        InfoEntityUnitsInfoComponent.IsSettedKingDict.Add(true, default);
        InfoEntityUnitsInfoComponent.IsSettedKingDict.Add(false, default);

        InfoEntityUnitsInfoComponent.AmountKingDict.Add(true, default);
        InfoEntityUnitsInfoComponent.AmountKingDict.Add(false, default);

        InfoEntityUnitsInfoComponent.AmountPawnDict.Add(true, default);
        InfoEntityUnitsInfoComponent.AmountPawnDict.Add(false, default);

        InfoEntityUnitsInfoComponent.AmountRookDict.Add(true, default);
        InfoEntityUnitsInfoComponent.AmountRookDict.Add(false, default);

        InfoEntityUnitsInfoComponent.AmountBishopDict.Add(true, default);
        InfoEntityUnitsInfoComponent.AmountBishopDict.Add(false, default);



        InfoEntBuildingsInfoCom.IsBuildedCityDictionary = new Dictionary<bool, bool>();
        InfoEntBuildingsInfoCom.XYsettedCityDictionary = new Dictionary<bool, int[]>();
        InfoEntBuildingsInfoCom.AmountFarmDict = new Dictionary<bool, int>();
        InfoEntBuildingsInfoCom.AmountWoodcutterDict = new Dictionary<bool, int>();
        InfoEntBuildingsInfoCom.AmountMineDict = new Dictionary<bool, int>();

        InfoEntBuildingsInfoCom.IsBuildedCityDictionary.Add(true, default);
        InfoEntBuildingsInfoCom.IsBuildedCityDictionary.Add(false, default);

        InfoEntBuildingsInfoCom.XYsettedCityDictionary.Add(true, default);
        InfoEntBuildingsInfoCom.XYsettedCityDictionary.Add(false, default);

        InfoEntBuildingsInfoCom.AmountFarmDict.Add(true, default);
        InfoEntBuildingsInfoCom.AmountFarmDict.Add(false, default);

        InfoEntBuildingsInfoCom.AmountWoodcutterDict.Add(true, default);
        InfoEntBuildingsInfoCom.AmountWoodcutterDict.Add(false, default);

        InfoEntBuildingsInfoCom.AmountMineDict.Add(true, default);
        InfoEntBuildingsInfoCom.AmountMineDict.Add(false, default);

        InfoEntBuildingsInfoCom.XYsettedCityDictionary[true] = new int[StartValuesGameConfig.XY_FOR_ARRAY];
        InfoEntBuildingsInfoCom.XYsettedCityDictionary[false] = new int[StartValuesGameConfig.XY_FOR_ARRAY];

        #endregion


        #region Ready

        _readyEntity = GameWorld.NewEntity();

        ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary = new Dictionary<bool, bool>();
        ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary.Add(true, default);
        ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary.Add(false, default);

        #endregion


        #region Updator

        _updatorEntity = GameWorld.NewEntity();

        UpdatorEntityAmountComponent.Amount = default;
        UpdatorEntityActiveComponent.IsActived = default;

        #endregion


        #region SelectedUnitEntity

        _selectedUnitEntity = GameWorld.NewEntity();
        SelectedUnitEUnitTypeC.UnitType = UnitTypes.None;

        #endregion


        #region Selector

        _selectorEntity = GameWorld.NewEntity()
            .Replace(new SelectorComponent(StartValuesGameConfig))
            .Replace(new RaycastHit2DComponent());

        #endregion


        _generalRPCEntity = GameWorld.NewEntity();

        GeneralRPCEntFromInfoCom.FromInfo = default;






        _inputEntity = GameWorld.NewEntity()
            .Replace(new MouseClickComponent());



        _soundEntity = GameWorld.NewEntity()
            .Replace(new SoundComponent());

        _theEndGameEntity = GameWorld.NewEntity()
            .Replace(new TheEndGameComponent());

        _startGameEntity = GameWorld.NewEntity()
            .Replace(new StartGameComponent());

        _animationAttackUnitEntity = GameWorld.NewEntity()
            .Replace(new AnimationAttackUnitComponent());

        _zoneEntity = GameWorld.NewEntity()
            .Replace(new ZoneComponent());


        #region Cells

        _cellsEntity = new EcsEntity[StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];

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
