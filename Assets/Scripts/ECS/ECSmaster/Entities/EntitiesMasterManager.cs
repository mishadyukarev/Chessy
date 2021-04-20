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
            Replace(new SetterUnitMasterComponent(supportManager.StartValuesConfig, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _shiftUnitEntity = _ecsWorld.NewEntity()
            .Replace(new ShiftUnitMasterComponent(supportManager.StartValuesConfig, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _refresherEntity = _ecsWorld.NewEntity()
            .Replace(new RefresherMasterComponent(eCSmanager.SystemsMasterManager));

        _attackUnitEntity = _ecsWorld.NewEntity()
            .Replace(new AttackUnitMasterComponent(supportManager.StartValuesConfig, supportManager.CellManager, eCSmanager.SystemsMasterManager));

        _getterUnitEntity = _ecsWorld.NewEntity()
            .Replace(new GetterUnitMasterComponent(eCSmanager.SystemsMasterManager));

        _economyEntity = _ecsWorld.NewEntity()
            .Replace(new EconomyMasterComponent(supportManager.StartValuesConfig))
            .Replace(new EconomyMasterComponent.UnitsMasterComponent(supportManager.StartValuesConfig))
            .Replace(new EconomyMasterComponent.BuildingsMasterComponent(supportManager.StartValuesConfig));

        _builderEntity = _ecsWorld.NewEntity()
            .Replace(new BuilderCellMasterComponent(supportManager.StartValuesConfig, supportManager.CellManager, eCSmanager.SystemsMasterManager));


        for (int x = 0; x < supportManager.StartValuesConfig.CellCountX; x++)
        {
            for (int y = 0; y < supportManager.StartValuesConfig.CellCountY; y++)
            {
                int random;
                random = Random.Range(1, 100);
                if (random <= supportManager.StartValuesConfig.PercentTree)
                    eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Tree);

                random = Random.Range(1, 100);
                if (random <= supportManager.StartValuesConfig.PercentHill)
                    eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Hill);

                random = Random.Range(1, 100);
                if (random <= supportManager.StartValuesConfig.PercentMountain)
                    eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Mountain);



                if (!eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().HaveHill
                    && !eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().HaveMountain
                    && !eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().HaveTree)
                {
                    random = Random.Range(1, 100);
                    if (random <= supportManager.StartValuesConfig.PercentFood)
                        eCSmanager.EntitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Food);
                }

            }
        }
    }
}
