using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using System.Collections.Generic;
using static MainGame;

public sealed class EntitiesGeneralManager : EntitiesManager
{
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

    private EcsEntity _inputEnt;

    internal ref MouseClickComponent InputEntityMouseClickComponent => ref _inputEnt.Get<MouseClickComponent>();

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

    internal ref UnitsInfoComponent InfoEnt_UnitsInfoCom => ref _infoEntity.Get<UnitsInfoComponent>();
    internal ref BuildingsInfoComponent InfoEnt_BuildingsInfoCom => ref _infoEntity.Get<BuildingsInfoComponent>();
    internal ref UpgradeComponent InfoEnt_UpgradeCom => ref _infoEntity.Get<UpgradeComponent>();

    #endregion


    #region UpdatorEntity

    private EcsEntity _updatorEntity;

    internal ref AmountComponent UpdatorEntityAmountComponent => ref _updatorEntity.Get<AmountComponent>();
    internal ref ActiveComponent UpdatorEntityActiveComponent => ref _updatorEntity.Get<ActiveComponent>();

    #endregion


    #region RPCEntity

    private EcsEntity _generalRPCEntity;
    internal ref FromInfoComponent GeneralRPCEntFromInfoCom => ref _generalRPCEntity.Get<FromInfoComponent>();
    internal ref ActiveComponent GeneralRPCEntActiveComponent => ref _generalRPCEntity.Get<ActiveComponent>();

    #endregion


    #region ReadyEntity

    private EcsEntity _readyEntity;

    internal ref ActivatedDictionaryComponent ReadyEntIsActivatedDictCom => ref _readyEntity.Get<ActivatedDictionaryComponent>();
    internal ref StartGameComponent ReadyEntStartGameCom => ref _readyEntity.Get<StartGameComponent>();

    #endregion


    #region CellEnts

    private EcsEntity[,] _cellEnts;
    internal ref CellComponent CellEnt_CellCom(params int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellComponent>();



    #region CellUnitEnts

    private EcsEntity[,] _cellUnitEnts;
    internal ref CellUnitComponent CellUnitEnt_CellUnitCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>();
    internal ref OwnerComponent CellUnitEnt_OwnerCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<OwnerComponent>();
    internal ref UnitTypeComponent CellUnitEnt_UnitTypeCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<UnitTypeComponent>();

    #endregion


    #region CellBuildingEnts

    private EcsEntity[,] _cellBuildingEnts;

    internal ref CellBuildingComponent CellBuildingEnt_CellBuildingCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellBuildingComponent>();
    internal ref OwnerComponent CellBuildingEnt_OwnerCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();
    internal ref BuildingTypeComponent CellBuildingEnt_BuildingTypeCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();

    #endregion


    #region CellEnvironmentEnts

    private EcsEntity[,] _cellEnvironmentEnts;
    internal ref CellEnvironmentComponent CellEnvEnt_CellEnvironmentCom(params int[] xy) => ref _cellEnvironmentEnts[xy[X], xy[Y]].Get<CellEnvironmentComponent>();

    #endregion


    #region CellSupportVisionEnts

    private EcsEntity[,] _cellSupportVisionEnts;
    internal ref CellSupportVisionComponent CellSupVisEntCellSupportVisionCom(params int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<CellSupportVisionComponent>();

    #endregion


    internal int Xamount => _cellEnts.GetUpperBound(X) + 1;
    internal int Yamount => _cellEnts.GetUpperBound(Y) + 1;

    internal int XYForArray = Instance.StartValuesGameConfig.XY_FOR_ARRAY;

    internal int X = Instance.StartValuesGameConfig.X;
    internal int Y = Instance.StartValuesGameConfig.Y;

    #endregion


    #region SelectedUnitEntity

    private EcsEntity _selectedUnitEntity;

    internal ref UnitTypeComponent SelectedUnitEntUnitTypeCom => ref _selectedUnitEntity.Get<UnitTypeComponent>();

    #endregion


    #region SelectorEntity

    private EcsEntity _selectorEntity;

    internal ref SelectorComponent SelectorESelectorC => ref _selectorEntity.Get<SelectorComponent>();
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
    internal ref AnimationAttackUnitComponent AnimationAttackUnitComponentRef => ref _animationEnt.Get<AnimationAttackUnitComponent>();

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

        FoodEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.GameObjectPool.FoodAmmountText;
        WoodEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.GameObjectPool.WoodAmmountText;
        OreEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.GameObjectPool.OreAmmountText;
        IronEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.GameObjectPool.IronAmmountText;
        GoldEntityTextMeshProGUIComponent.TextMeshProUGUI = Instance.GameObjectPool.GoldAmmountText;

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
        TakerKingEntityButtonComponent.Button = Instance.GameObjectPool.GameDownTakerKingButton;


        _takerPawnEntity = GameWorld.NewEntity();

        TakerPawnEntityUnitTypeComponent.UnitType = UnitTypes.Pawn;
        TakerPawnEntityButtonComponent.Button = Instance.GameObjectPool.GameDownTakerPawnButton;


        _takerRookEntity = GameWorld.NewEntity();

        TakerRookEntityUnitTypeComponent.UnitType = UnitTypes.Rook;
        TakerRookEntityButtonComponent.Button = Instance.GameObjectPool.GameDownTakerRookButton;


        _takerBishopEntity = GameWorld.NewEntity();

        TakerBishopEntityUnitTypeComponent.UnitType = UnitTypes.Bishop;
        TakerBishopEntityButtonComponent.Button = Instance.GameObjectPool.GameDownTakerBishopButton;

        #endregion


        #region Doner

        _donerEntity = GameWorld.NewEntity();

        DonerEntityButtonComponent.Button = Instance.GameObjectPool.DoneButton;

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



        InfoEnt_BuildingsInfoCom.AmountFarmDict = new Dictionary<bool, int>();
        InfoEnt_BuildingsInfoCom.AmountWoodcutterDict = new Dictionary<bool, int>();
        InfoEnt_BuildingsInfoCom.AmountMineDict = new Dictionary<bool, int>();

        InfoEnt_BuildingsInfoCom.AmountFarmDict.Add(true, default);
        InfoEnt_BuildingsInfoCom.AmountFarmDict.Add(false, default);

        InfoEnt_BuildingsInfoCom.AmountWoodcutterDict.Add(true, default);
        InfoEnt_BuildingsInfoCom.AmountWoodcutterDict.Add(false, default);

        InfoEnt_BuildingsInfoCom.AmountMineDict.Add(true, default);
        InfoEnt_BuildingsInfoCom.AmountMineDict.Add(false, default);



        InfoEnt_UpgradeCom.AmountUpgradePawnDict = new Dictionary<bool, int>();
        InfoEnt_UpgradeCom.AmountUpgradeRookDict = new Dictionary<bool, int>();
        InfoEnt_UpgradeCom.AmountUpgradeBishopDict = new Dictionary<bool, int>();

        InfoEnt_UpgradeCom.AmountUpgradePawnDict.Add(true, default);
        InfoEnt_UpgradeCom.AmountUpgradePawnDict.Add(false, default);

        InfoEnt_UpgradeCom.AmountUpgradeRookDict.Add(true, default);
        InfoEnt_UpgradeCom.AmountUpgradeRookDict.Add(false, default);

        InfoEnt_UpgradeCom.AmountUpgradeBishopDict.Add(true, default);
        InfoEnt_UpgradeCom.AmountUpgradeBishopDict.Add(false, default);

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


        _generalRPCEntity = GameWorld.NewEntity();

        GeneralRPCEntFromInfoCom.FromInfo = default;






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
        _cellSupportVisionEnts = new EcsEntity[StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];
        _cellEnvironmentEnts = new EcsEntity[StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];
        _cellBuildingEnts = new EcsEntity[StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];
        _cellUnitEnts = new EcsEntity[StartValuesGameConfig.CELL_COUNT_X, StartValuesGameConfig.CELL_COUNT_Y];

        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                _cellEnts[x, y] = GameWorld.NewEntity();
                _cellSupportVisionEnts[x, y] = GameWorld.NewEntity();
                _cellEnvironmentEnts[x, y] = GameWorld.NewEntity();
                _cellBuildingEnts[x, y] = GameWorld.NewEntity();
                _cellUnitEnts[x, y] = GameWorld.NewEntity();

                _cellEnts[x, y].Replace(new CellComponent(this, StartValuesGameConfig, x, y));
                _cellUnitEnts[x, y].Replace(new CellUnitComponent(this, x, y));
                _cellBuildingEnts[x, y].Replace(new CellBuildingComponent(this, Instance.GameObjectPool, x, y));
                _cellEnvironmentEnts[x, y].Replace(new CellEnvironmentComponent(this, Instance.GameObjectPool, x, y));
                _cellSupportVisionEnts[x, y].Replace(new CellSupportVisionComponent(this, Instance.GameObjectPool, x, y));
            }
        }

        #endregion

    }
}
