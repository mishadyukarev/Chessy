using Leopotam.Ecs;

public sealed class EntitiesGeneralManager : EntitiesManager
{
    private NameValueManager _nameValueManager;
    private SpawnAllGeneralEntities _spawnAllForEntities;

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

    private EcsComponentRef<CellComponent>[,] _cellComponentRef;
    private EcsComponentRef<CellComponent.EnvironmentComponent>[,] _cellEnvironmentComponentRef;
    private EcsComponentRef<CellComponent.SupportVisionComponent>[,] _cellSupportVisionComponentRef;
    private EcsComponentRef<CellComponent.UnitComponent>[,] _cellUnitComponentRef;
    private EcsComponentRef<CellComponent.BuildingComponent>[,] _cellBuildingComponentRef; 


    internal EcsComponentRef<S> GetCellComponents<S>(params int[] xy) where S : struct 
        => _cellsEntity[xy[_nameValueManager.X], xy[_nameValueManager.Y]].Ref<S>();

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





    public EntitiesGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    public void CreateEntities(ECSmanager eCSmanager, SupportManager supportManager, PhotonManager photonManager)
    {
        _nameValueManager = supportManager.NameValueManager;
        var systemsGeneralManager = eCSmanager.SystemsGeneralManager;
        var cellManager = supportManager.CellManager;
        var resourcesLoadManager = supportManager.ResourcesLoadManager;


        _buttonEntity = _ecsWorld.NewEntity()
            .Replace(new ButtonComponent());

        _inputEntity = _ecsWorld.NewEntity()
            .Replace(new InputComponent());

        _selectedUnitEntity = _ecsWorld.NewEntity()
            .Replace(new SelectedUnitComponent());

        _unitPathEntity = _ecsWorld.NewEntity()
            .Replace(new UnitPathComponent(systemsGeneralManager, _nameValueManager, cellManager));

        _supportVisionEntity = _ecsWorld.NewEntity()
            .Replace(new SupportVisionComponent(systemsGeneralManager, _nameValueManager, cellManager));

        _rayEntity = _ecsWorld.NewEntity()
            .Replace(new RayComponent(systemsGeneralManager));

        _getterCellEntity = _ecsWorld.NewEntity()
            .Replace(new GetterCellComponent(_nameValueManager, systemsGeneralManager));

        _selectorEntity = _ecsWorld.NewEntity()
            .Replace(new SelectorComponent(supportManager.NameValueManager));

        _economyEntity = _ecsWorld.NewEntity()
            .Replace(new EconomyComponent())
            .Replace(new EconomyComponent.UnitsComponent())
            .Replace(new EconomyComponent.BuildingsComponent());


        _spawnAllForEntities = new SpawnAllGeneralEntities();
        _spawnAllForEntities.SpawnCells(this, resourcesLoadManager, _nameValueManager);
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
