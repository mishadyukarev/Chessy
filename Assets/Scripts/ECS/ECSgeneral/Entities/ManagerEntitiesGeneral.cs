using Leopotam.Ecs;

public sealed class EntitiesGeneralManager : EntitiesManager
{
    private StartValuesGameConfig _startValuesGameConfig;

    private EcsEntity[,] _cellsEntity;
    private EcsEntity _selectedUnitEntity;
    private EcsEntity _economyEntity;
    private EcsEntity _soundEntity;

    private EcsComponentRef<CellComponent>[,] _cellComponentRef;
    private EcsComponentRef<CellComponent.EnvironmentComponent>[,] _cellEnvironmentComponentRef;
    private EcsComponentRef<CellComponent.SupportVisionComponent>[,] _cellSupportVisionComponentRef;
    private EcsComponentRef<CellComponent.UnitComponent>[,] _cellUnitComponentRef;
    private EcsComponentRef<CellComponent.BuildingComponent>[,] _cellBuildingComponentRef;


    #region Properies

    internal EcsComponentRef<S> GetCellComponents<S>(params int[] xy) where S : struct
        => _cellsEntity[xy[_startValuesGameConfig.X], xy[_startValuesGameConfig.Y]].Ref<S>();


    #region Solo

    internal EcsComponentRef<RayComponent> RayComponentRef => _soloEntity.Ref<RayComponent>();
    internal EcsComponentRef<UnitPathsComponent> UnitPathComponentRef => _soloEntity.Ref<UnitPathsComponent>();
    internal EcsComponentRef<GetterCellComponent> GetterCellComponentRef => _soloEntity.Ref<GetterCellComponent>();

    #endregion


    #region RunUpdate

    internal EcsComponentRef<SelectorComponent> SelectorComponentRef => _runUpdateEntity.Ref<SelectorComponent>();
    internal EcsComponentRef<UIComponent> ButtonComponentRef => _runUpdateEntity.Ref<UIComponent>();
    internal EcsComponentRef<InputComponent> InputComponentRef => _runUpdateEntity.Ref<InputComponent>();

    #endregion 


    #region Cell

    internal EcsComponentRef<CellComponent>[,] CellComponentRef => _cellComponentRef;
    internal EcsComponentRef<CellComponent.EnvironmentComponent>[,] CellEnvironmentComponentRef => _cellEnvironmentComponentRef;
    internal EcsComponentRef<CellComponent.SupportVisionComponent>[,] CellSupportVisionComponentRef => _cellSupportVisionComponentRef;
    internal EcsComponentRef<CellComponent.UnitComponent>[,] CellUnitComponentRef => _cellUnitComponentRef;
    internal EcsComponentRef<CellComponent.BuildingComponent>[,] CellBuildingComponentRef => _cellBuildingComponentRef;

    #endregion


    #region Economy

    internal EcsComponentRef<EconomyComponent> EconomyComponentRef
        => _economyEntity.Ref<EconomyComponent>();
    internal EcsComponentRef<EconomyComponent.UnitsComponent> EconomyUnitsComponentRef
        => _economyEntity.Ref<EconomyComponent.UnitsComponent>();
    internal EcsComponentRef<EconomyComponent.BuildingsComponent> EconomyBuildingsComponentRef
        => _economyEntity.Ref<EconomyComponent.BuildingsComponent>();

    #endregion


    #region Else

    internal EcsComponentRef<SelectedUnitComponent> SelectedUnitComponentRef => _selectedUnitEntity.Ref<SelectedUnitComponent>();
    internal EcsComponentRef<SoundComponent> SoundComponentRef => _soundEntity.Ref<SoundComponent>();

    #endregion

    #endregion



    public EntitiesGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities(ECSmanager eCSmanager, SupportGameManager supportManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnManager)
    {
        _startValuesGameConfig = supportManager.StartValuesGameConfig;
        var systemsGeneralManager = eCSmanager.SystemsGeneralManager;
        var cellManager = supportManager.CellManager;
        var entitiesGeneralManager = eCSmanager.EntitiesGeneralManager;

        _soloEntity = _ecsWorld.NewEntity()
            .Replace(new UnitPathsComponent(systemsGeneralManager, _startValuesGameConfig, cellManager))
            .Replace(new RayComponent(systemsGeneralManager))
            .Replace(new GetterCellComponent(_startValuesGameConfig, systemsGeneralManager));

        _runUpdateEntity = _ecsWorld.NewEntity()
            .Replace(new UIComponent())
            .Replace(new InputComponent())
            .Replace(new SelectorComponent(supportManager.StartValuesGameConfig));


        _selectedUnitEntity = _ecsWorld.NewEntity()
            .Replace(new SelectedUnitComponent());

        _economyEntity = _ecsWorld.NewEntity()
            .Replace(new EconomyComponent())
            .Replace(new EconomyComponent.UnitsComponent())
            .Replace(new EconomyComponent.BuildingsComponent());

        _soundEntity = _ecsWorld.NewEntity()
            .Replace(new SoundComponent());


        #region Cells

        var cellsGO = startSpawnManager.CellsGO;
        entitiesGeneralManager.CreateCellArray(_startValuesGameConfig.CELL_COUNT_X, _startValuesGameConfig.CELL_COUNT_Y);
        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
            {
                bool isStartMaster = false;
                bool isStartOther = false;
                if (y < 3 && x > 2 && x < 12)
                isStartMaster = true;
                if (y > 8 && x > 2 && x < 12)
                isStartOther = true;

                CellComponent cellComponent = new CellComponent(isStartMaster, isStartOther, cellsGO[x, y]);


                CellComponent.EnvironmentComponent cellEnvironmentComponent
                    = new CellComponent.EnvironmentComponent(x, y, startSpawnManager);


                CellComponent.SupportVisionComponent cellSupportVisionComponent
                    = new CellComponent.SupportVisionComponent(x, y, startSpawnManager);


                CellComponent.UnitComponent cellUnitComponent = new CellComponent.UnitComponent
                    (x, y, startSpawnManager, _startValuesGameConfig);


                CellComponent.BuildingComponent cellBuildingComponent = new CellComponent.BuildingComponent(x, y, startSpawnManager);


                entitiesGeneralManager.CreateCellArrayEntity(x, y);

                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellEnvironmentComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellSupportVisionComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellUnitComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellBuildingComponent);
            }
        }

        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
            {
                entitiesGeneralManager.AddRefComponentsToCell(x, y);
            }
        }

        #endregion

    }


    #region For Spawn Cell

    public void CreateCellArray(int xAmount, int yAmount)
    {
        _cellsEntity = new EcsEntity[xAmount, yAmount];

        _cellComponentRef = new EcsComponentRef<CellComponent>[xAmount, yAmount];
        _cellEnvironmentComponentRef = new EcsComponentRef<CellComponent.EnvironmentComponent>[xAmount, yAmount];
        _cellSupportVisionComponentRef = new EcsComponentRef<CellComponent.SupportVisionComponent>[xAmount, yAmount];
        _cellUnitComponentRef = new EcsComponentRef<CellComponent.UnitComponent>[xAmount, yAmount];
        _cellBuildingComponentRef = new EcsComponentRef<CellComponent.BuildingComponent>[xAmount, yAmount];
    }

    public void CreateCellArrayEntity(int x, int y) => _cellsEntity[x, y] = _ecsWorld.NewEntity();
    public void AddComponentToCellEntity<T>(int x, int y, T component) where T : struct => _cellsEntity[x, y].Replace(component);

    internal void AddRefComponentsToCell(int x, int y)
    {
        _cellComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent>();
        _cellEnvironmentComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent.EnvironmentComponent>();
        _cellSupportVisionComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent.SupportVisionComponent>();
        _cellUnitComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent.UnitComponent>();
        _cellBuildingComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent.BuildingComponent>();
    }

    #endregion
}
