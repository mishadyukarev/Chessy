using Leopotam.Ecs;

public sealed class EntitiesGeneralManager : EntitiesManager
{
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
       var startValuesGameConfig = supportManager.StartValuesGameConfig;
        var systemsGeneralManager = eCSmanager.SystemsGeneralManager;
        var cellManager = supportManager.CellManager;
        var entitiesGeneralManager = eCSmanager.EntitiesGeneralManager;

        _soloEntity = _ecsWorld.NewEntity()
            .Replace(new UnitPathsComponent(systemsGeneralManager, startValuesGameConfig, cellManager))
            .Replace(new RayComponent(systemsGeneralManager))
            .Replace(new GetterCellComponent(startValuesGameConfig, systemsGeneralManager));

        _runUpdateEntity = _ecsWorld.NewEntity()
            .Replace(new UIComponent())
            .Replace(new InputComponent())
            .Replace(new SelectorComponent(startValuesGameConfig));


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

        var xAmount = cellsGO.GetUpperBound(startValuesGameConfig.X) + 1;
        var yAmount = cellsGO.GetUpperBound(startValuesGameConfig.Y) + 1;

        _cellsEntity = new EcsEntity[xAmount, yAmount];

        _cellComponentRef = new EcsComponentRef<CellComponent>[xAmount, yAmount];
        _cellEnvironmentComponentRef = new EcsComponentRef<CellComponent.EnvironmentComponent>[xAmount, yAmount];
        _cellSupportVisionComponentRef = new EcsComponentRef<CellComponent.SupportVisionComponent>[xAmount, yAmount];
        _cellUnitComponentRef = new EcsComponentRef<CellComponent.UnitComponent>[xAmount, yAmount];
        _cellBuildingComponentRef = new EcsComponentRef<CellComponent.BuildingComponent>[xAmount, yAmount];

        for (int x = 0; x < xAmount; x++)
        {
            for (int y = 0; y < yAmount; y++)
            {
                bool isStartMaster = false;
                bool isStartOther = false;
                if (y < 3 && x > 2 && x < 12) isStartMaster = true;
                if (y > 8 && x > 2 && x < 12) isStartOther = true;

                CellComponent cellComponent = new CellComponent(isStartMaster, isStartOther, cellsGO[x, y]);
                CellComponent.EnvironmentComponent cellEnvironmentComponent= new CellComponent.EnvironmentComponent(x, y, startSpawnManager, startValuesGameConfig);
                CellComponent.SupportVisionComponent cellSupportVisionComponent= new CellComponent.SupportVisionComponent(x, y, startSpawnManager);
                CellComponent.UnitComponent cellUnitComponent = new CellComponent.UnitComponent(x, y, startSpawnManager, startValuesGameConfig);
                CellComponent.BuildingComponent cellBuildingComponent = new CellComponent.BuildingComponent(x, y, startSpawnManager, startValuesGameConfig);

                _cellsEntity[x, y] = _ecsWorld.NewEntity();

                _cellsEntity[x, y].Replace(cellComponent)
                    .Replace(cellEnvironmentComponent)
                    .Replace(cellSupportVisionComponent)
                    .Replace(cellUnitComponent)
                    .Replace(cellBuildingComponent);


                _cellComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent>();
                _cellEnvironmentComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent.EnvironmentComponent>();
                _cellSupportVisionComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent.SupportVisionComponent>();
                _cellUnitComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent.UnitComponent>();
                _cellBuildingComponentRef[x, y] = _cellsEntity[x, y].Ref<CellComponent.BuildingComponent>();
            }
        }

        #endregion

    }
}
