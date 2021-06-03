using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

internal sealed class EntitiesGeneralManager : EntitiesManager
{
    private EcsEntity[,] _cellEnts;
    private EcsEntity[,] _cellUnitEnts;
    private EcsEntity[,] _cellBuildingEnts;
    private EcsEntity[,] _cellEnvironmentEnts;
    private EcsEntity[,] _cellSupportVisionEnts;
    private EcsEntity[,] _cellEffectEnts;

    internal ref CellBaseComponent CellEnt_CellBaseCom(params int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellBaseComponent>();
    internal ref CellComponent CellEnt_CellCom(params int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellComponent>();

    internal ref CellUnitComponent CellUnitEnt_CellUnitCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>();
    internal ref OwnerComponent CellUnitEnt_CellOwnerCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<OwnerComponent>();
    internal ref UnitTypeComponent CellUnitEnt_UnitTypeCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<UnitTypeComponent>();

    internal ref CellEnvironmentComponent CellEnvEnt_CellEnvCom(params int[] xy) => ref _cellEnvironmentEnts[xy[X], xy[Y]].Get<CellEnvironmentComponent>();
    internal ref CellComponent CellEnvEnt_CellCom(params int[] xy) => ref _cellEnvironmentEnts[xy[X], xy[Y]].Get<CellComponent>();

    internal ref CellBuildingComponent CellBuildingEnt_CellBuildingCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellBuildingComponent>();
    internal ref BuildingTypeComponent CellBuildingEnt_BuildingTypeCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();
    internal ref CellComponent CellBuildingEnt_CellCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellComponent>();
    internal ref OwnerComponent CellBuildingEnt_OwnerCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();

    internal ref CellSupportVisionComponent CellSupVisEnt_CellSupVisCom(params int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<CellSupportVisionComponent>();

    internal ref CellEffectComponent CellEffectEnt_CellEffectCom(params int[] xy) => ref _cellEffectEnts[xy[X], xy[Y]].Get<CellEffectComponent>();

    internal int Xamount => _cellEnts.GetUpperBound(X) + 1;
    internal int Yamount => _cellEnts.GetUpperBound(Y) + 1;

    internal int XyForArray => 2;
    internal int X => 0;
    internal int Y => 1;


    private EcsEntity _economyEntity;
    internal ref EconomyComponent EconomyEnt_EconomyCom => ref _economyEntity.Get<EconomyComponent>();


    private EcsEntity _mistakeEntity;
    internal ref MistakeEconomyComponent MistakeEnt_MistakeEconomyCom => ref _mistakeEntity.Get<MistakeEconomyComponent>();


    #region Up

    private EcsEntity _foodEntity;
    private EcsEntity _woodEntity;
    private EcsEntity _oreEntity;
    private EcsEntity _ironEntity;
    private EcsEntity _goldEntity;


    internal ref TextMeshProGUIComponent FoodEntityTextMeshProGUIComponent => ref _foodEntity.Get<TextMeshProGUIComponent>();
    internal ref TextMeshProGUIComponent WoodEntityTextMeshProGUIComponent => ref _woodEntity.Get<TextMeshProGUIComponent>();
    internal ref TextMeshProGUIComponent OreEntityTextMeshProGUIComponent => ref _oreEntity.Get<TextMeshProGUIComponent>();
    internal ref TextMeshProGUIComponent IronEntityTextMeshProGUIComponent => ref _ironEntity.Get<TextMeshProGUIComponent>();
    internal ref TextMeshProGUIComponent GoldEntityTextMeshProGUIComponent => ref _goldEntity.Get<TextMeshProGUIComponent>();

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
    internal ref MistakeeeComponent DonerEntityMistakeComponent => ref _donerEntity.Get<MistakeeeComponent>();

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



    private EcsEntity _inputEnt;
    internal ref InputComponent InputEnt_InputCom => ref _inputEnt.Get<InputComponent>();


    private EcsEntity _infoEntity;
    internal ref UnitsInfoComponent InfoEnt_UnitsInfoCom => ref _infoEntity.Get<UnitsInfoComponent>();
    internal ref BuildingsInfoComponent InfoEnt_BuildingsInfoCom => ref _infoEntity.Get<BuildingsInfoComponent>();
    internal ref UpgradeInfoComponent InfoEnt_UpgradeInfoCom => ref _infoEntity.Get<UpgradeInfoComponent>();
    internal ref UpdatorComponent InfoEnt_UpdatorCom => ref _infoEntity.Get<UpdatorComponent>();



    private EcsEntity _rPCGeneralEntity;
    internal ref RpcComponent RpcGeneralEnt_FromInfoCom => ref _rPCGeneralEntity.Get<RpcComponent>();


    private EcsEntity _readyEntity;
    internal ref ReadyComponent ReadyEnt_ReadyCom => ref _readyEntity.Get<ReadyComponent>();



    private EcsEntity _selectorEnt;
    internal ref SelectorComponent SelectorEntSelectorCom => ref _selectorEnt.Get<SelectorComponent>();
    internal ref RaycastHit2DComponent SelectorEnt_RayCom => ref _selectorEnt.Get<RaycastHit2DComponent>();
    internal ref UnitTypeComponent SelectorEnt_UnitTypeCom => ref _selectorEnt.Get<UnitTypeComponent>();


    private EcsEntity _soundEnt;
    internal ref SoundComponent SoundEntSoundCom => ref _soundEnt.Get<SoundComponent>();


    private EcsEntity _endGameEnt;
    internal ref EndGameComponent EndGameEntEndGameCom => ref _endGameEnt.Get<EndGameComponent>();


    private EcsEntity _zoneEnt;
    internal ref ZoneComponent ZoneComponent => ref _zoneEnt.Get<ZoneComponent>();


    private EcsEntity _animationEnt;
    internal ref AnimationAttackUnitComponent AnimationAttackUnitComponent => ref _animationEnt.Get<AnimationAttackUnitComponent>();


    #region Ability

    private EcsEntity _uniqueFirstAbilityEnt;
    internal ref ButtonComponent Unique1AbilityEnt_ButtonCom => ref _uniqueFirstAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent UniqueFirstAbilityEnt_TextMeshProGUICom => ref _uniqueFirstAbilityEnt.Get<TextMeshProGUIComponent>();


    private EcsEntity _uniqueSecondAbilityEnt;
    internal ref ButtonComponent Unique2AbilityEnt_ButtonCom => ref _uniqueSecondAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent Unique2AbilityEnt_TextMeshProGUICom => ref _uniqueSecondAbilityEnt.Get<TextMeshProGUIComponent>();


    private EcsEntity _uniqueThirdAbilityEnt;
    internal ref ButtonComponent Unique3AbilityEnt_ButtonCom => ref _uniqueThirdAbilityEnt.Get<ButtonComponent>();
    internal ref TextMeshProGUIComponent Unique3AbilityEnt_TextMeshProGUICom => ref _uniqueThirdAbilityEnt.Get<TextMeshProGUIComponent>();

    #endregion



    internal EntitiesGeneralManager(EcsWorld gameWorld, CanvasGameManager canvasGameManager, StartValuesGameConfig startValuesGameConfig)
    {
        GameDisposables.Add(this);/////////////////////////////////////


        #region Cells

        _cellEnts = new EcsEntity[startValuesGameConfig.CELL_COUNT_X, startValuesGameConfig.CELL_COUNT_Y];
        _cellUnitEnts = new EcsEntity[startValuesGameConfig.CELL_COUNT_X, startValuesGameConfig.CELL_COUNT_Y];
        _cellBuildingEnts = new EcsEntity[startValuesGameConfig.CELL_COUNT_X, startValuesGameConfig.CELL_COUNT_Y];
        _cellEnvironmentEnts = new EcsEntity[startValuesGameConfig.CELL_COUNT_X, startValuesGameConfig.CELL_COUNT_Y];
        _cellSupportVisionEnts = new EcsEntity[startValuesGameConfig.CELL_COUNT_X, startValuesGameConfig.CELL_COUNT_Y];
        _cellEffectEnts = new EcsEntity[startValuesGameConfig.CELL_COUNT_X, startValuesGameConfig.CELL_COUNT_Y];

        for (int x = 0; x < startValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < startValuesGameConfig.CELL_COUNT_Y; y++)
            {
                _cellEnts[x, y] = gameWorld.NewEntity();
                _cellUnitEnts[x, y] = gameWorld.NewEntity();
                _cellBuildingEnts[x, y] = gameWorld.NewEntity();
                _cellEnvironmentEnts[x, y] = gameWorld.NewEntity();
                _cellSupportVisionEnts[x, y] = gameWorld.NewEntity();
                _cellEffectEnts[x, y] = gameWorld.NewEntity();

                _cellEnts[x, y]
                    .Replace(new CellBaseComponent(GameDisposables))
                    .Replace(new CellComponent(x, y));

                _cellUnitEnts[x, y]
                    .Replace(new CellUnitComponent(GameDisposables))
                    .Replace(new OwnerComponent())
                    .Replace(new UnitTypeComponent());

                _cellBuildingEnts[x, y]
                    .Replace(new CellBuildingComponent())
                    .Replace(new BuildingTypeComponent())
                    .Replace(new CellComponent())
                    .Replace(new OwnerComponent());

                _cellEnvironmentEnts[x, y]
                    .Replace(new CellEnvironmentComponent())
                    .Replace(new CellComponent());

                _cellSupportVisionEnts[x, y]
                    .Replace(new CellSupportVisionComponent());

                _cellEffectEnts[x, y]
                    .Replace(new CellEffectComponent());
            }
        }

        #endregion



        _economyEntity = gameWorld.NewEntity()
            .Replace(new EconomyComponent(GameDisposables));

        _mistakeEntity = gameWorld.NewEntity()
            .Replace(new MistakeEconomyComponent());




        #region Economy

        _foodEntity = gameWorld.NewEntity()
            .Replace(new MistakeeeComponent());
        _woodEntity = gameWorld.NewEntity()
            .Replace(new MistakeeeComponent());
        _oreEntity = gameWorld.NewEntity()
            .Replace(new MistakeeeComponent());
        _ironEntity = gameWorld.NewEntity()
            .Replace(new MistakeeeComponent());
        _goldEntity = gameWorld.NewEntity()
            .Replace(new MistakeeeComponent());

        FoodEntityTextMeshProGUIComponent.TextMeshProUGUI = canvasGameManager.FoodAmmountText;
        WoodEntityTextMeshProGUIComponent.TextMeshProUGUI = canvasGameManager.WoodAmmountText;
        OreEntityTextMeshProGUIComponent.TextMeshProUGUI = canvasGameManager.OreAmmountText;
        IronEntityTextMeshProGUIComponent.TextMeshProUGUI = canvasGameManager.IronAmmountText;
        GoldEntityTextMeshProGUIComponent.TextMeshProUGUI = canvasGameManager.GoldAmmountText;

        #endregion




        #region TakerUnits

        _takerKingEntity = gameWorld.NewEntity();

        TakerKingEntityUnitTypeComponent.UnitType = UnitTypes.King;
        TakerKingEntityButtonComponent.Button = canvasGameManager.GameDownTakerKingButton;


        _takerPawnEntity = gameWorld.NewEntity();

        TakerPawnEntityUnitTypeComponent.UnitType = UnitTypes.Pawn;
        TakerPawnEntityButtonComponent.Button = canvasGameManager.GameDownTakerPawnButton;


        _takerRookEntity = gameWorld.NewEntity();

        TakerRookEntityUnitTypeComponent.UnitType = UnitTypes.Rook;
        TakerRookEntityButtonComponent.Button = canvasGameManager.GameDownTakerRookButton;


        _takerBishopEntity = gameWorld.NewEntity();

        TakerBishopEntityUnitTypeComponent.UnitType = UnitTypes.Bishop;
        TakerBishopEntityButtonComponent.Button = canvasGameManager.GameDownTakerBishopButton;

        #endregion




        _donerEntity = gameWorld.NewEntity()
            .Replace(new MistakeeeComponent());
        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary = new Dictionary<bool, bool>();
        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary.Add(true, default);
        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary.Add(false, default);
        DonerEntityButtonComponent.Button = canvasGameManager.DoneButton;


        _truceEntity = gameWorld.NewEntity();
        TruceEnt_ActivatedDictCom.IsActivatedDictionary = new Dictionary<bool, bool>();
        TruceEnt_ActivatedDictCom.IsActivatedDictionary.Add(true, default);
        TruceEnt_ActivatedDictCom.IsActivatedDictionary.Add(false, default);
        TruceEnt_ButtonCom.Button = canvasGameManager.TruceButton;



        _infoEntity = gameWorld.NewEntity()
            .Replace(new UnitsInfoComponent(startValuesGameConfig))
            .Replace(new BuildingsInfoComponent(startValuesGameConfig))
            .Replace(new UpgradeInfoComponent(startValuesGameConfig))
            .Replace(new UpdatorComponent());


        _readyEntity = gameWorld.NewEntity()
            .Replace(new ReadyComponent(GameDisposables));


        _selectorEnt = gameWorld.NewEntity()
            .Replace(new SelectorComponent(startValuesGameConfig))
            .Replace(new RaycastHit2DComponent())
            .Replace(new UnitTypeComponent());

        #region Abilities

        _uniqueFirstAbilityEnt = gameWorld.NewEntity();
        _uniqueSecondAbilityEnt = gameWorld.NewEntity();
        _uniqueThirdAbilityEnt = gameWorld.NewEntity();

        Unique1AbilityEnt_ButtonCom.Button = canvasGameManager.UniqueFirstAbilityButton;
        UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI = canvasGameManager.UniqueFirstAbilityText;

        Unique2AbilityEnt_ButtonCom.Button = canvasGameManager.UniqueSecondAbilityButton;
        Unique2AbilityEnt_TextMeshProGUICom.TextMeshProUGUI = canvasGameManager.UniqueSecondAbilityText;

        Unique3AbilityEnt_ButtonCom.Button = canvasGameManager.UniqueThirdAbilityButton;
        Unique3AbilityEnt_TextMeshProGUICom.TextMeshProUGUI = canvasGameManager.UniqueThirdAbilityText;

        #endregion


        _rPCGeneralEntity = gameWorld.NewEntity();

        RpcGeneralEnt_FromInfoCom.FromInfo = default;






        _inputEnt = gameWorld.NewEntity()
            .Replace(new InputComponent());

        _soundEnt = gameWorld.NewEntity()
            .Replace(new SoundComponent());

        _endGameEnt = gameWorld.NewEntity()
            .Replace(new EndGameComponent());


        _animationEnt = gameWorld.NewEntity()
            .Replace(new AnimationAttackUnitComponent());

        _zoneEnt = gameWorld.NewEntity()
            .Replace(new ZoneComponent());
    }


    internal void FillEntities(ObjectPoolGame objectPoolGame, CanvasGameManager canvasGameManager, StartValuesGameConfig startValuesGameConfig)
    {

        for (int x = 0; x < startValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < startValuesGameConfig.CELL_COUNT_Y; y++)
            {
                CellEnt_CellBaseCom(x, y).Fill(objectPoolGame, x, y);
                CellUnitEnt_CellUnitCom(x, y).Fill(objectPoolGame, x, y);
                CellBuildingEnt_CellBuildingCom(x, y).Fill(objectPoolGame, x, y);
                CellEnvEnt_CellEnvCom(x, y).Fill(objectPoolGame, x, y);

                _cellSupportVisionEnts[x, y]
                    .Replace(new CellSupportVisionComponent(objectPoolGame, x, y));

                _cellEffectEnts[x, y]
                    .Replace(new CellEffectComponent(objectPoolGame, x, y));

            }
        }


        if (Main.Instance.IsMasterClient)
        {
            #region Info

            InfoEnt_UnitsInfoCom.IsSettedKingDict[true] = false;
            InfoEnt_UnitsInfoCom.IsSettedKingDict[false] = false;

            InfoEnt_UnitsInfoCom.AmountKingDict[true] = startValuesGameConfig.AMOUNT_KING_MASTER;
            InfoEnt_UnitsInfoCom.AmountKingDict[false] = startValuesGameConfig.AMOUNT_KING_OTHER;

            InfoEnt_UnitsInfoCom.AmountPawnDict[true] = startValuesGameConfig.AMOUNT_PAWN_MASTER;
            InfoEnt_UnitsInfoCom.AmountPawnDict[false] = startValuesGameConfig.AMOUNT_PAWN_OTHER;

            InfoEnt_UnitsInfoCom.AmountRookDict[true] = startValuesGameConfig.AMOUNT_ROOK_MASTER;
            InfoEnt_UnitsInfoCom.AmountRookDict[false] = startValuesGameConfig.AMOUNT_ROOK_OTHER;

            InfoEnt_UnitsInfoCom.AmountBishopDict[true] = startValuesGameConfig.AMOUNT_BISHOP_MASTER;
            InfoEnt_UnitsInfoCom.AmountBishopDict[false] = startValuesGameConfig.AMOUNT_BISHOP_OTHER;

            InfoEnt_BuildingsInfoCom.IsSettedCityDict[true] = default;
            InfoEnt_BuildingsInfoCom.IsSettedCityDict[false] = default;

            InfoEnt_BuildingsInfoCom.AmountFarmDict[true] = default;
            InfoEnt_BuildingsInfoCom.AmountFarmDict[false] = default;

            InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[true] = default;
            InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[false] = default;

            InfoEnt_BuildingsInfoCom.AmountMineDict[true] = default;
            InfoEnt_BuildingsInfoCom.AmountMineDict[false] = default;

            #endregion


            #region Economy

            EconomyEnt_EconomyCom.SetFood(true, startValuesGameConfig.AMOUNT_FOOD_MASTER);
            EconomyEnt_EconomyCom.SetWood(true, startValuesGameConfig.AMOUNT_WOOD_MASTER);
            EconomyEnt_EconomyCom.SetOre(true, startValuesGameConfig.AMOUNT_ORE_MASTER);
            EconomyEnt_EconomyCom.SetIron(true, startValuesGameConfig.AMOUNT_IRON_MASTER);
            EconomyEnt_EconomyCom.SetGold(true, startValuesGameConfig.AMOUNT_GOLD_MASTER);

            EconomyEnt_EconomyCom.SetFood(false, startValuesGameConfig.AMOUNT_FOOD_OTHER);
            EconomyEnt_EconomyCom.SetWood(false, startValuesGameConfig.AMOUNT_WOOD_OTHER);
            EconomyEnt_EconomyCom.SetOre(false, startValuesGameConfig.AMOUNT_ORE_OTHER);
            EconomyEnt_EconomyCom.SetIron(false, startValuesGameConfig.AMOUNT_IRON_OTHER);
            EconomyEnt_EconomyCom.SetGold(false, startValuesGameConfig.AMOUNT_GOLD_OTHER);

            #endregion

            #region Cells

            for (int x = 0; x < Xamount; x++)
            {
                for (int y = 0; y < Yamount; y++)
                {
                    int random;

                    random = Random.Range(1, 100);
                    if (random <= startValuesGameConfig.PERCENT_MOUNTAIN)
                        CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Mountain);

                    if (!CellEnvEnt_CellEnvCom(x, y).HaveMountain)
                    {
                        random = Random.Range(1, 100);
                        if (random <= startValuesGameConfig.PERCENT_TREE)
                        {
                            CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.AdultForest);
                        }


                        random = Random.Range(1, 100);
                        if (random <= startValuesGameConfig.PERCENT_HILL)
                            CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Hill);


                        if (!CellEnvEnt_CellEnvCom(x, y).HaveAdultTree)
                        {
                            random = Random.Range(1, 100);
                            if (random <= startValuesGameConfig.PERCENT_FOOD)
                                CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Fertilizer);
                        }
                    }
                }
            }

            #endregion
        }

        else
        {

        }
    }


    public override void Dispose()
    {
        base.Dispose();

        EconomyEnt_EconomyCom.Dispose();

        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true] = false;
        DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false] = false;

        ReadyEnt_ReadyCom.IsSkipped = default;
        ReadyEnt_ReadyCom.IsActivatedDictionary[true] = false;
        ReadyEnt_ReadyCom.IsActivatedDictionary[false] = false;
        ReadyEnt_ReadyCom.IsActivatedDictionary[Main.Instance.IsMasterClient] = false;

        TruceEnt_ActivatedDictCom.IsActivatedDictionary[true] = default;
        TruceEnt_ActivatedDictCom.IsActivatedDictionary[false] = default;
    }
}
