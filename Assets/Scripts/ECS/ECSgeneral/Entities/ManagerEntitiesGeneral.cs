using Leopotam.Ecs;

public sealed class EntitiesGeneralManager : EntitiesManager
{
    private StartValuesConfig _startValues;

    private EcsEntity[,] _cellsEntity;
    private EcsEntity _inputEntity;
    private EcsEntity _selectedUnitEntity;
    private EcsEntity _unitPathEntity;
    private EcsEntity _supportVisionEntity;
    private EcsEntity _rayEntity;
    private EcsEntity _getterCellEntity;
    private EcsEntity _selectorEntity;
    private EcsEntity _buttonEntity;
    private EcsEntity _economyEntity;
    private EcsEntity _soundEntity;

    private EcsComponentRef<CellComponent>[,] _cellComponentRef;
    private EcsComponentRef<CellComponent.EnvironmentComponent>[,] _cellEnvironmentComponentRef;
    private EcsComponentRef<CellComponent.SupportVisionComponent>[,] _cellSupportVisionComponentRef;
    private EcsComponentRef<CellComponent.UnitComponent>[,] _cellUnitComponentRef;
    private EcsComponentRef<CellComponent.BuildingComponent>[,] _cellBuildingComponentRef; 


    internal EcsComponentRef<S> GetCellComponents<S>(params int[] xy) where S : struct 
        => _cellsEntity[xy[_startValues.X], xy[_startValues.Y]].Ref<S>();

    internal EcsComponentRef<CellComponent>[,] CellComponentRef => _cellComponentRef;
    internal EcsComponentRef<CellComponent.EnvironmentComponent>[,] CellEnvironmentComponentRef => _cellEnvironmentComponentRef;
    internal EcsComponentRef<CellComponent.SupportVisionComponent>[,] CellSupportVisionComponentRef => _cellSupportVisionComponentRef;
    internal EcsComponentRef<CellComponent.UnitComponent>[,] CellUnitComponentRef => _cellUnitComponentRef;
    internal EcsComponentRef<CellComponent.BuildingComponent>[,] CellBuildingComponentRef => _cellBuildingComponentRef;

    internal EcsComponentRef<EconomyComponent> EconomyComponentRef 
        => _economyEntity.Ref<EconomyComponent>();
    internal EcsComponentRef<EconomyComponent.UnitsComponent> EconomyUnitsComponentRef 
        => _economyEntity.Ref<EconomyComponent.UnitsComponent>();
    internal EcsComponentRef<EconomyComponent.BuildingsComponent> EconomyBuildingsComponentRef
        => _economyEntity.Ref<EconomyComponent.BuildingsComponent>();

    internal EcsComponentRef<RayComponent> RayComponentRef => _rayEntity.Ref<RayComponent>();
    internal EcsComponentRef<InputComponent> InputComponentRef => _inputEntity.Ref<InputComponent>();
    internal EcsComponentRef<SupportVisionComponent> SupportVisionComponentRef => _supportVisionEntity.Ref<SupportVisionComponent>();
    internal EcsComponentRef<UnitPathComponent> UnitPathComponentRef => _unitPathEntity.Ref<UnitPathComponent>();
    internal EcsComponentRef<SelectedUnitComponent> SelectedUnitComponentRef => _selectedUnitEntity.Ref<SelectedUnitComponent>();
    internal EcsComponentRef<GetterCellComponent> GetterCellComponentRef => _getterCellEntity.Ref<GetterCellComponent>();
    internal EcsComponentRef<SelectorComponent> SelectorComponentRef => _selectorEntity.Ref<SelectorComponent>();
    internal EcsComponentRef<ButtonComponent> ButtonComponentRef => _buttonEntity.Ref<ButtonComponent>();
    internal EcsComponentRef<SoundComponent> SoundComponentRef => _soundEntity.Ref<SoundComponent>();





    public EntitiesGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities(ECSmanager eCSmanager, SupportManager supportManager, PhotonManager photonManager, StartSpawnManager startSpawnManager)
    {
        _startValues = supportManager.StartValuesConfig;
        var systemsGeneralManager = eCSmanager.SystemsGeneralManager;
        var cellManager = supportManager.CellManager;
        var resourcesLoadManager = supportManager.ResourcesLoadManager;
        var entitiesGeneralManager = eCSmanager.EntitiesGeneralManager;


        _buttonEntity = _ecsWorld.NewEntity()
            .Replace(new ButtonComponent());

        _inputEntity = _ecsWorld.NewEntity()
            .Replace(new InputComponent());

        _selectedUnitEntity = _ecsWorld.NewEntity()
            .Replace(new SelectedUnitComponent());

        _unitPathEntity = _ecsWorld.NewEntity()
            .Replace(new UnitPathComponent(systemsGeneralManager, _startValues, cellManager));

        _supportVisionEntity = _ecsWorld.NewEntity()
            .Replace(new SupportVisionComponent(systemsGeneralManager, _startValues, cellManager));

        _rayEntity = _ecsWorld.NewEntity()
            .Replace(new RayComponent(systemsGeneralManager));

        _getterCellEntity = _ecsWorld.NewEntity()
            .Replace(new GetterCellComponent(_startValues, systemsGeneralManager));

        _selectorEntity = _ecsWorld.NewEntity()
            .Replace(new SelectorComponent(supportManager.StartValuesConfig));

        _economyEntity = _ecsWorld.NewEntity()
            .Replace(new EconomyComponent())
            .Replace(new EconomyComponent.UnitsComponent())
            .Replace(new EconomyComponent.BuildingsComponent());

        _soundEntity = _ecsWorld.NewEntity()
            .Replace(new SoundComponent());


        #region Cells

        var cellsGO = startSpawnManager.CellsGO;
        entitiesGeneralManager.CreateCellArray(_startValues.CellCountX, _startValues.CellCountY);
        for (int x = 0; x < _startValues.CellCountX; x++)
        {
            for (int y = 0; y < _startValues.CellCountY; y++)
            {
                bool isStartMaster = false;
                bool isStartOther = false;
                /*if (y < 3 && x > 2 && x < 12) */isStartMaster = true;
                /*if (y > 8 && x > 2 && x < 12) */isStartOther = true;

                CellComponent cellComponent = new CellComponent(isStartMaster, isStartOther, cellsGO[x, y]);



                CellComponent.EnvironmentComponent cellEnvironmentComponent
                    = new CellComponent.EnvironmentComponent(x, y, startSpawnManager);


                CellComponent.SupportVisionComponent cellSupportVisionComponent
                    = new CellComponent.SupportVisionComponent(x, y, startSpawnManager);



                CellComponent.UnitComponent cellUnitComponent = new CellComponent.UnitComponent
                    (x, y, startSpawnManager, _startValues);


                CellComponent.BuildingComponent cellBuildingComponent = new CellComponent.BuildingComponent(x, y, startSpawnManager);


                entitiesGeneralManager.CreateCellArrayEntity(x, y);

                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellEnvironmentComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellSupportVisionComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellUnitComponent);
                entitiesGeneralManager.AddComponentToCellEntity(x, y, cellBuildingComponent);
            }
        }

        for (int x = 0; x < _startValues.CellCountX; x++)
        {
            for (int y = 0; y < _startValues.CellCountY; y++)
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
