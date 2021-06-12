using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Main;

internal sealed partial class EntitiesGeneralManager : EntitiesManager
{
    internal GameObject BackGroundGO;
    internal SpriteRenderer BackGroundSR;
    internal AudioSource MistakeAudioSource;
    internal AudioSource AttackAudioSource;


    #region Cells

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

    internal ref CellBuildingComponent CellBuildEnt_CellBuilCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellBuildingComponent>();
    internal ref BuildingTypeComponent CellBuildEnt_BuilTypeCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();
    internal ref CellComponent CellBuildEnt_CellCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellComponent>();
    internal ref OwnerComponent CellBuildEnt_OwnerCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();

    internal ref CellSupportVisionComponent CellSupVisEnt_CellSupVisCom(params int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<CellSupportVisionComponent>();

    internal ref CellEffectComponent CellEffectEnt_CellEffectCom(params int[] xy) => ref _cellEffectEnts[xy[X], xy[Y]].Get<CellEffectComponent>();

    internal int Xamount => _cellEnts.GetUpperBound(X) + 1;
    internal int Yamount => _cellEnts.GetUpperBound(Y) + 1;

    internal int XyForArray => 2;
    internal int X => 0;
    internal int Y => 1;

    #endregion


    private EcsEntity _buildingsEnt;
    internal ref BuildingsComponent BuildingsEnt_BuildingsCom => ref _buildingsEnt.Get<BuildingsComponent>();
    internal ref UpgradeBuildingsComponent BuildingsEnt_UpgradeBuildingsCom => ref _buildingsEnt.Get<UpgradeBuildingsComponent>();


    private EcsEntity _unitInventorEnt;
    internal ref UnitInventorComponent UnitInventorEnt_UnitInventorCom => ref _unitInventorEnt.Get<UnitInventorComponent>();
    internal ref UpgradeUnitsComponent UnitInventorEnt_UpgradeUnitCom => ref _unitInventorEnt.Get<UpgradeUnitsComponent>();


    private EcsEntity _inputEnt;
    internal ref InputComponent InputEnt_InputCom => ref _inputEnt.Get<InputComponent>();


    private EcsEntity _rPCGeneralEntity;
    internal ref RpcComponent RpcGeneralEnt_FromInfoCom => ref _rPCGeneralEntity.Get<RpcComponent>();


    private EcsEntity _selectorEnt;
    internal ref SelectorComponent SelectorEnt_SelectorCom => ref _selectorEnt.Get<SelectorComponent>();
    internal ref RaycastHit2DComponent SelectorEnt_RayCom => ref _selectorEnt.Get<RaycastHit2DComponent>();
    internal ref UnitTypeComponent SelectorEnt_UnitTypeCom => ref _selectorEnt.Get<UnitTypeComponent>();


    private EcsEntity _soundEnt;
    internal ref SoundComponent SoundEntSoundCom => ref _soundEnt.Get<SoundComponent>();


    private EcsEntity _zoneEnt;
    internal ref ZoneComponent ZoneComponent => ref _zoneEnt.Get<ZoneComponent>();


    private EcsEntity _animationEnt;
    internal ref AnimationAttackUnitComponent AnimationAttackUnitComponent => ref _animationEnt.Get<AnimationAttackUnitComponent>();


    internal EntitiesGeneralManager(EcsWorld gameWorld, ResourcesCommComponent resourcesCommComponent)
    {
        #region Cells

        _cellEnts = new EcsEntity[resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_X, resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_Y];
        _cellUnitEnts = new EcsEntity[resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_X, resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_Y];
        _cellBuildingEnts = new EcsEntity[resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_X, resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_Y];
        _cellEnvironmentEnts = new EcsEntity[resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_X, resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_Y];
        _cellSupportVisionEnts = new EcsEntity[resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_X, resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_Y];
        _cellEffectEnts = new EcsEntity[resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_X, resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_Y];

        for (int x = 0; x < resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_X; x++)
            for (int y = 0; y < resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                _cellEnts[x, y] = gameWorld.NewEntity();
                _cellUnitEnts[x, y] = gameWorld.NewEntity();
                _cellBuildingEnts[x, y] = gameWorld.NewEntity();
                _cellEnvironmentEnts[x, y] = gameWorld.NewEntity();
                _cellSupportVisionEnts[x, y] = gameWorld.NewEntity();
                _cellEffectEnts[x, y] = gameWorld.NewEntity();

                //_cellEnts[x, y]
                //    .Replace(new CellComponent(x, y));

                _cellUnitEnts[x, y]
                    .Replace(new CellUnitComponent())
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


        #endregion


        _economyEnt = gameWorld.NewEntity()
            .Replace(new EconomyComponent())
            .Replace(new EconomyUIComponent())
            .Replace(new MistakeEconomyComponent());

        _unitInventorEnt = gameWorld.NewEntity()
            .Replace(new UnitInventorComponent())
            .Replace(new UpgradeUnitsComponent());

        _buildingsEnt = gameWorld.NewEntity()
            .Replace(new BuildingsComponent(resourcesCommComponent.StartValuesGameConfig))
            .Replace(new UpgradeBuildingsComponent());


        _selectorEnt = gameWorld.NewEntity()
            .Replace(new SelectorComponent(XyForArray))
            .Replace(new RaycastHit2DComponent())
            .Replace(new UnitTypeComponent());


        _rPCGeneralEntity = gameWorld.NewEntity().Replace(new RpcComponent());

        _inputEnt = gameWorld.NewEntity()
            .Replace(new InputComponent());

        _soundEnt = gameWorld.NewEntity()
            .Replace(new SoundComponent());


        _animationEnt = gameWorld.NewEntity()
            .Replace(new AnimationAttackUnitComponent());

        _zoneEnt = gameWorld.NewEntity()
            .Replace(new ZoneComponent());


        ConstructCanvast(gameWorld);
    }


    internal void FillEntities(ResourcesCommComponent resourcesCommComponent)
    {
        SpawnCells(resourcesCommComponent);


        BackGroundGO = GameObject.Instantiate(Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.BackGroundCollider2D,
            Instance.transform.position + new Vector3(7, 5.5f, 2), Instance.transform.rotation, Instance.ParentGOs.transform);
        BackGroundSR = BackGroundGO.GetComponent<SpriteRenderer>();
        BackGroundSR.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);

        MistakeAudioSource = Instance.Builder.CreateGameObject("MistakeAudioSource", new Type[] { typeof(AudioSource) }, Instance.ParentGOs.transform).GetComponent<AudioSource>();
        MistakeAudioSource.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.MistakeAudioClip;

        AttackAudioSource = Instance.Builder.CreateGameObject("AttackAudioSource", new Type[] { typeof(AudioSource) }, Instance.ParentGOs.transform).GetComponent<AudioSource>();
        AttackAudioSource.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.AttackAudioClip;

        EconomyEnt_EconomyCom.Fill();
        EconomyUIEnt_EconomyUICom.Fill();

        UnitInventorEnt_UnitInventorCom.Fill();
        UnitInventorEnt_UpgradeUnitCom.Fill();

        BuildingsEnt_UpgradeBuildingsCom.Fill();


        SpawnAndFillCanvasEntities();


        if (Instance.IsMasterClient)
        {
            #region Info

            UnitInventorEnt_UnitInventorCom.SetSettedKing(true, false);
            UnitInventorEnt_UnitInventorCom.SetSettedKing(false, false);

            UnitInventorEnt_UnitInventorCom.SetAmountUnits(UnitTypes.King, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_KING_MASTER);
            UnitInventorEnt_UnitInventorCom.SetAmountUnits(UnitTypes.King, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_KING_OTHER);

            UnitInventorEnt_UnitInventorCom.SetAmountUnits(UnitTypes.Pawn, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_PAWN_MASTER);
            UnitInventorEnt_UnitInventorCom.SetAmountUnits(UnitTypes.Pawn, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_PAWN_OTHER);

            UnitInventorEnt_UnitInventorCom.SetAmountUnits(UnitTypes.Rook, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_ROOK_MASTER);
            UnitInventorEnt_UnitInventorCom.SetAmountUnits(UnitTypes.Rook, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_ROOK_OTHER);

            UnitInventorEnt_UnitInventorCom.SetAmountUnits(UnitTypes.Bishop, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_BISHOP_MASTER);
            UnitInventorEnt_UnitInventorCom.SetAmountUnits(UnitTypes.Bishop, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_BISHOP_OTHER);

            BuildingsEnt_BuildingsCom.IsSettedCityDict[true] = default;
            BuildingsEnt_BuildingsCom.IsSettedCityDict[false] = default;

            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Farm, true, default);
            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Farm, false, default);

            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Woodcutter, true, default);
            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Woodcutter, false, default);

            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Mine, true, default);
            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Mine, false, default);

            #endregion


            #region Economy

            EconomyEnt_EconomyCom.SetFood(true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_FOOD_MASTER);
            EconomyEnt_EconomyCom.SetWood(true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_WOOD_MASTER);
            EconomyEnt_EconomyCom.SetOre(true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_ORE_MASTER);
            EconomyEnt_EconomyCom.SetIron(true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_IRON_MASTER);
            EconomyEnt_EconomyCom.SetGold(true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_GOLD_MASTER);

            EconomyEnt_EconomyCom.SetFood(false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_FOOD_OTHER);
            EconomyEnt_EconomyCom.SetWood(false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_WOOD_OTHER);
            EconomyEnt_EconomyCom.SetOre(false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_ORE_OTHER);
            EconomyEnt_EconomyCom.SetIron(false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_IRON_OTHER);
            EconomyEnt_EconomyCom.SetGold(false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_GOLD_OTHER);

            #endregion
        }
    }

    private void SpawnCells(ResourcesCommComponent resourcesCommComponent)
    {
        var cellGO = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.CellGO;
        var whiteCellSR = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.WhiteSprite;
        var blackCellSR = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.BlackSprite;

        var CellsGO = new GameObject[resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_X, resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_Y];

        var SupportParentForCells = new GameObject("Cells");
        SupportParentForCells.transform.SetParent(Instance.ParentGOs.transform);

        for (int x = 0; x < resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_X; x++)
            for (int y = 0; y < resourcesCommComponent.StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        CellsGO[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Instance.gameObject);
                        SetActive(CellsGO[x, y], x, y);
                    }
                    if (x % 2 != 0)
                    {
                        CellsGO[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Instance.gameObject);
                        SetActive(CellsGO[x, y], x, y);
                    }
                }
                if (y % 2 != 0)
                {
                    if (x % 2 != 0)
                    {
                        CellsGO[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Instance.gameObject);
                        SetActive(CellsGO[x, y], x, y);
                    }
                    if (x % 2 == 0)
                    {
                        CellsGO[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Instance.gameObject);
                        SetActive(CellsGO[x, y], x, y);
                    }
                }

                CellsGO[x, y].transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);

                var isStartedDict = new Dictionary<bool, bool>();
                if (Instance.TestType == TestTypes.Standart)
                {
                    isStartedDict[true] = true;
                    isStartedDict[false] = true;
                }
                else
                {
                    isStartedDict[true] = y < 3 && x > 2 && x < 12;
                    isStartedDict[false] = y > 8 && x > 2 && x < 12;
                }

                CellEnt_CellCom(x, y).SetXy(x, y);

                CellsGO[x, y].transform.SetParent(SupportParentForCells.transform);
                CellEnt_CellBaseCom(x, y).Fill(SupportParentForCells, CellsGO[x, y], isStartedDict);

                GameObject parentGO = CellsGO[x, y].transform.Find("Environments").gameObject;
                CellEnvEnt_CellEnvCom(x, y).Fill(parentGO);

                parentGO = CellsGO[x, y].transform.Find("SupportVisions").gameObject;
                CellSupVisEnt_CellSupVisCom(x, y).Fill(parentGO);

                parentGO = CellsGO[x, y].transform.Find("Units").gameObject;
                CellUnitEnt_CellUnitCom(x, y).Fill(parentGO);

                parentGO = CellsGO[x, y].transform.Find("Buildings").gameObject;
                CellBuildEnt_CellBuilCom(x, y).Fill(parentGO);


                parentGO = CellsGO[x, y].transform.Find("Effects").gameObject;
                CellEffectEnt_CellEffectCom(x, y).Fill(parentGO);
            }


        GameObject CreateGameObject(GameObject go, Sprite sprite, int x, int y, GameObject mainGameGO2)
        {
            var goo = GameObject.Instantiate(go, mainGameGO2.transform.position + new Vector3(x, y, mainGameGO2.transform.position.z), mainGameGO2.transform.rotation);

            var SR = goo.GetComponent<SpriteRenderer>();
            SR.sprite = sprite;

            return goo;
        }

        void SetActive(GameObject go, int x, int y)
        {
            if (x >= 0 && y == 0 || x >= 0 && y == 11 ||
            x == 0 && y >= 0 || x == 14 && y >= 0 ||
            x == 1 && y == 1 || x == 2 && y == 1 || x == 12 && y == 1 || x == 13 && y == 1 ||
            x == 1 && y == 10 || x == 2 && y == 10 || x == 12 && y == 10 || x == 13 && y == 10)
                go.SetActive(false);
        }


        if (Instance.IsMasterClient)
        {
            for (int x = 0; x < Xamount; x++)
            {
                for (int y = 0; y < Yamount; y++)
                {
                    if (CellBuildEnt_BuilTypeCom(x, y).HaveBuilding)
                    {
                        Instance.ECSmanager.CellManager.CellBuildingWorker.ResetBuilding(x, y);
                    }
                    Instance.ECSmanager.CellManager.CellUnitWorker.ResetUnit(x, y);
                    CellEnvEnt_CellEnvCom(x, y).ResetAll();

                    int random;

                    random = UnityEngine.Random.Range(1, 100);
                    if (random <= resourcesCommComponent.StartValuesGameConfig.PERCENT_MOUNTAIN)
                        CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Mountain);

                    if (!CellEnvEnt_CellEnvCom(x, y).HaveMountain)
                    {
                        random = UnityEngine.Random.Range(1, 100);
                        if (random <= resourcesCommComponent.StartValuesGameConfig.PERCENT_TREE)
                        {
                            CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.AdultForest);
                        }


                        random = UnityEngine.Random.Range(1, 100);
                        if (random <= resourcesCommComponent.StartValuesGameConfig.PERCENT_HILL)
                            CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Hill);


                        if (!CellEnvEnt_CellEnvCom(x, y).HaveAdultTree)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= resourcesCommComponent.StartValuesGameConfig.PERCENT_FOOD)
                                CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Fertilizer);
                        }
                    }
                }
            }
        }
    }
}
