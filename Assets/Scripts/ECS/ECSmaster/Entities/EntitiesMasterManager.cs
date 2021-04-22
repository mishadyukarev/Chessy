using Leopotam.Ecs;
using UnityEngine;

public class EntitiesMasterManager : EntitiesManager
{
    private EcsEntity _setterUnitEntity;
    private EcsEntity _shiftUnitEntity;
    private EcsEntity _refresherEntity;
    private EcsEntity _attackUnitEntity;
    private EcsEntity _getterUnitEntity;
    private EcsEntity _economyEntity;
    private EcsEntity _builderEntity;

    public EcsComponentRef<SetterUnitMasterComponent> SetterUnitMasterComponentRef => _setterUnitEntity.Ref<SetterUnitMasterComponent>();
    public EcsComponentRef<ShiftUnitMasterComponent> ShiftUnitComponentRef => _shiftUnitEntity.Ref<ShiftUnitMasterComponent>();
    public EcsComponentRef<DonerComponent> RefresherMasterComponentRef => _refresherEntity.Ref<DonerComponent>();
    public EcsComponentRef<AttackUnitMasterComponent> AttackUnitMasterComponentRef => _attackUnitEntity.Ref<AttackUnitMasterComponent>();
    internal EcsComponentRef<BuilderCellMasterComponent> BuilderCellMasterComponentRef => _builderEntity.Ref<BuilderCellMasterComponent>();
    internal EcsComponentRef<GetterUnitMasterComponent> GetterUnitMasterComponentRef => _getterUnitEntity.Ref<GetterUnitMasterComponent>();

    internal EcsComponentRef<EconomyMasterComponent> EconomyMasterComponentRef
        => _economyEntity.Ref<EconomyMasterComponent>();
    internal EcsComponentRef<EconomyMasterComponent.UnitMasterComponent> EconomyUnitsMasterComponentRef
        => _economyEntity.Ref<EconomyMasterComponent.UnitMasterComponent>();
    internal EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> EconomyBuildingsMasterComponentRef
        => _economyEntity.Ref<EconomyMasterComponent.BuildingsMasterComponent>();





    public EntitiesMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    public void CreateEntities(ECSmanager eCSmanager, SupportManager supportManager)
    {
        _setterUnitEntity = _ecsWorld.NewEntity().
            Replace(new SetterUnitMasterComponent(supportManager.StartValuesConfig, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _shiftUnitEntity = _ecsWorld.NewEntity()
            .Replace(new ShiftUnitMasterComponent(supportManager.StartValuesConfig, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _refresherEntity = _ecsWorld.NewEntity()
            .Replace(new DonerComponent());

        _attackUnitEntity = _ecsWorld.NewEntity()
            .Replace(new AttackUnitMasterComponent(supportManager.StartValuesConfig, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _economyEntity = _ecsWorld.NewEntity()
            .Replace(new EconomyMasterComponent(supportManager.StartValuesConfig))
            .Replace(new EconomyMasterComponent.UnitMasterComponent(supportManager.StartValuesConfig))
            .Replace(new EconomyMasterComponent.BuildingsMasterComponent(supportManager.StartValuesConfig));

        _builderEntity = _ecsWorld.NewEntity()
            .Replace(new BuilderCellMasterComponent(supportManager.StartValuesConfig, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _getterUnitEntity = _ecsWorld.NewEntity()
            .Replace(new GetterUnitMasterComponent(eCSmanager.SystemsMasterManager));


        for (int x = 0; x < supportManager.StartValuesConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < supportManager.StartValuesConfig.CELL_COUNT_Y; y++)
            {
                int random;
                random = Random.Range(1, 100);
                if (random <= supportManager.StartValuesConfig.PERCENT_TREE)
                    eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Tree);

                random = Random.Range(1, 100);
                if (random <= supportManager.StartValuesConfig.PERCENT_HILL)
                    eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Hill);

                random = Random.Range(1, 100);
                if (random <= supportManager.StartValuesConfig.PERCENT_MOUNTAIN)
                    eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Mountain);



                if (!eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().HaveHill
                    && !eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().HaveMountain
                    && !eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().HaveTree)
                {
                    random = Random.Range(1, 100);
                    if (random <= supportManager.StartValuesConfig.PERCENT_FOOD)
                        eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Food);
                }

            }
        }
    }
}
