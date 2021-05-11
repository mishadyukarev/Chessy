using Leopotam.Ecs;
using static MainGame;

public sealed class EntitiesGeneralManager : EntitiesManager
{
    private EcsEntity[,] _cellsEntity;
    private EcsEntity _selectorEntity;
    private EcsEntity _donerEntity;
    private EcsEntity _inputEntity;
    private EcsEntity _selectedUnitEntity;
    private EcsEntity _economyEntity;
    private EcsEntity _soundEntity;
    private EcsEntity _readyEntity;
    private EcsEntity _selectorUnitEntity;
    private EcsEntity _theEndGameEntity;
    private EcsEntity _startGameEntity;
    private EcsEntity _rayEntity;
    private EcsEntity _animationAttackUnitEntity;
    private EcsEntity _zoneEntity;
    private EcsEntity _whoDoEntity;

    private EcsComponentRef<CellComponent>[,] _cellComponentRef;
    private EcsComponentRef<CellComponent.EnvironmentComponent>[,] _cellEnvironmentComponentRef;
    private EcsComponentRef<CellComponent.SupportVisionComponent>[,] _cellSupportVisionComponentRef;
    private EcsComponentRef<CellComponent.UnitComponent>[,] _cellUnitComponentRef;
    private EcsComponentRef<CellComponent.BuildingComponent>[,] _cellBuildingComponentRef;


    #region Properies


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
    internal EcsComponentRef<EconomyComponent.UnitComponent> EconomyUnitsComponentRef
        => _economyEntity.Ref<EconomyComponent.UnitComponent>();
    internal EcsComponentRef<EconomyComponent.BuildingComponent> EconomyBuildingsComponentRef
        => _economyEntity.Ref<EconomyComponent.BuildingComponent>();

    #endregion


    #region Else

    internal EcsComponentRef<SelectorComponent> SelectorComponentRef => _selectorEntity.Ref<SelectorComponent>();
    internal EcsComponentRef<DonerComponent> DonerComponentRef => _donerEntity.Ref<DonerComponent>();
    internal EcsComponentRef<InputComponent> InputComponentRef => _inputEntity.Ref<InputComponent>();
    internal EcsComponentRef<SelectedUnitComponent> SelectedUnitComponentRef => _selectedUnitEntity.Ref<SelectedUnitComponent>();
    internal EcsComponentRef<SoundComponent> SoundComponentRef => _soundEntity.Ref<SoundComponent>();
    internal EcsComponentRef<ReadyComponent> ReadyComponentRef => _readyEntity.Ref<ReadyComponent>();
    internal EcsComponentRef<SelectorUnitComponent> SelectorUnitComponent => _selectorUnitEntity.Ref<SelectorUnitComponent>();
    internal EcsComponentRef<TheEndGameComponent> TheEndGameComponentRef => _theEndGameEntity.Ref<TheEndGameComponent>();
    internal EcsComponentRef<StartGameComponent> StartGameComponentRef => _startGameEntity.Ref<StartGameComponent>();
    internal EcsComponentRef<RayComponent> RayComponentRef => _rayEntity.Ref<RayComponent>();
    internal EcsComponentRef<AnimationAttackUnitComponent> AnimationAttackUnitComponentRef => _animationAttackUnitEntity.Ref<AnimationAttackUnitComponent>();
    internal EcsComponentRef<ZoneComponent> ZoneComponentRef => _zoneEntity.Ref<ZoneComponent>();

    #endregion

    #endregion



    public EntitiesGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities(ECSmanager eCSmanager)
    {
        var startValuesGameConfig = InstanceGame.StartValuesGameConfig;
        var systemsGeneralManager = eCSmanager.SystemsGeneralManager;
        var cellManager = InstanceGame.CellManager;
        var entitiesGeneralManager = eCSmanager.EntitiesGeneralManager;

        _donerEntity = _ecsWorld.NewEntity()
            .Replace(new DonerComponent());

        _inputEntity = _ecsWorld.NewEntity()
            .Replace(new InputComponent());

        _selectorEntity = _ecsWorld.NewEntity()
            .Replace(new SelectorComponent(startValuesGameConfig));

        _selectedUnitEntity = _ecsWorld.NewEntity()
            .Replace(new SelectedUnitComponent());

        _economyEntity = _ecsWorld.NewEntity()
            .Replace(new EconomyComponent())
            .Replace(new EconomyComponent.UnitComponent())
            .Replace(new EconomyComponent.BuildingComponent());

        _soundEntity = _ecsWorld.NewEntity()
            .Replace(new SoundComponent());

        _readyEntity = _ecsWorld.NewEntity()
            .Replace(new ReadyComponent());

        _selectorUnitEntity = _ecsWorld.NewEntity()
            .Replace(new SelectorUnitComponent());

        _theEndGameEntity = _ecsWorld.NewEntity()
            .Replace(new TheEndGameComponent());

        _startGameEntity = _ecsWorld.NewEntity()
            .Replace(new StartGameComponent());

        _rayEntity = _ecsWorld.NewEntity()
            .Replace(new RayComponent());

        _animationAttackUnitEntity = _ecsWorld.NewEntity()
            .Replace(new AnimationAttackUnitComponent());

        _zoneEntity = _ecsWorld.NewEntity()
            .Replace(new ZoneComponent());


        #region Cells

        var cellsGO = InstanceGame.GameObjectPool.CellsGO;

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
                CellComponent cellComponent = new CellComponent(x, y);
                CellComponent.EnvironmentComponent cellEnvironmentComponent = new CellComponent.EnvironmentComponent(x, y);
                CellComponent.SupportVisionComponent cellSupportVisionComponent = new CellComponent.SupportVisionComponent(x, y);
                CellComponent.UnitComponent cellUnitComponent = new CellComponent.UnitComponent(x, y);
                CellComponent.BuildingComponent cellBuildingComponent = new CellComponent.BuildingComponent(x, y);

                _cellsEntity[x, y] = _ecsWorld.NewEntity();

                _cellsEntity[x, y]
                    .Replace(cellComponent)
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
