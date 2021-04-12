using Leopotam.Ecs;

public class EntitiesMasterManager : EntitiesManager
{
    private SpawnAllForMasterEntity _spawnAllForMasterEntity;

    private EcsEntity _setterUnitEntity;
    private EcsEntity _shiftUnitEntity;
    private EcsEntity _refresherEntity;
    private EcsEntity _attackUnitEntity;
    private EcsEntity _getterUnitEntity;
    private EcsEntity _economyEntity;
    private EcsEntity _builderEntity;

    public EcsComponentRef<SetterUnitMasterComponent> SetterUnitMasterComponentRef => _setterUnitEntity.Ref<SetterUnitMasterComponent>();
    public EcsComponentRef<ShiftUnitMasterComponent> ShiftUnitComponentRef => _shiftUnitEntity.Ref<ShiftUnitMasterComponent>();
    public EcsComponentRef<RefresherMasterComponent> RefresherMasterComponentRef => _refresherEntity.Ref<RefresherMasterComponent>();
    public EcsComponentRef<AttackUnitMasterComponent> AttackUnitMasterComponentRef => _attackUnitEntity.Ref<AttackUnitMasterComponent>();
    internal EcsComponentRef<GetterUnitMasterComponent> GetterUnitMasterComponentRef => _getterUnitEntity.Ref<GetterUnitMasterComponent>();
    internal EcsComponentRef<BuilderCellMasterComponent> BuilderCellMasterComponentRef => _builderEntity.Ref<BuilderCellMasterComponent>();

    internal EcsComponentRef<EconomyMasterComponent> EconomyMasterComponentRef
        => _economyEntity.Ref<EconomyMasterComponent>();
    internal EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> EconomyUnitsMasterComponentRef 
        => _economyEntity.Ref<EconomyMasterComponent.UnitsMasterComponent>();
    internal EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> EconomyBuildingsMasterComponentRef 
        => _economyEntity.Ref<EconomyMasterComponent.BuildingsMasterComponent>();





    public EntitiesMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    public void CreateEntities(ECSmanager eCSmanager, SupportManager supportManager)
    {
        _setterUnitEntity = _ecsWorld.NewEntity().
            Replace(new SetterUnitMasterComponent(supportManager.NameValueManager, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _shiftUnitEntity = _ecsWorld.NewEntity()
            .Replace(new ShiftUnitMasterComponent(supportManager.NameValueManager, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _refresherEntity = _ecsWorld.NewEntity()
            .Replace(new RefresherMasterComponent(eCSmanager.SystemsMasterManager));

        _attackUnitEntity = _ecsWorld.NewEntity()
            .Replace(new AttackUnitMasterComponent(supportManager.NameValueManager, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _getterUnitEntity = _ecsWorld.NewEntity()
            .Replace(new GetterUnitMasterComponent(eCSmanager.SystemsMasterManager));

        _economyEntity = _ecsWorld.NewEntity()
            .Replace(new EconomyMasterComponent(supportManager.NameValueManager))
            .Replace(new EconomyMasterComponent.UnitsMasterComponent(supportManager.NameValueManager))
            .Replace(new EconomyMasterComponent.BuildingsMasterComponent(supportManager.NameValueManager));

        _builderEntity = _ecsWorld.NewEntity()
            .Replace(new BuilderCellMasterComponent(supportManager.NameValueManager, supportManager.CellManager, eCSmanager.SystemsMasterManager));


        _spawnAllForMasterEntity = new SpawnAllForMasterEntity();
        _spawnAllForMasterEntity.SetEnvironment(eCSmanager.EntitiesGeneralManager, supportManager.NameValueManager);
    }
}
