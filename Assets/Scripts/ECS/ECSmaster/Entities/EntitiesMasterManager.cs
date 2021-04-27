using Leopotam.Ecs;
using UnityEngine;
using static MainGame;

public class EntitiesMasterManager : EntitiesManager
{
    private EcsEntity _economyEntity;
    private EcsEntity _readyEntity;
    private EcsEntity _theEndGameEntity;


    #region Properties

    #region Solo

    public EcsComponentRef<SetterUnitMasterComponent> SetterUnitMasterComponentRef => _soloEntity.Ref<SetterUnitMasterComponent>();
    public EcsComponentRef<ShiftUnitMasterComponent> ShiftUnitComponentRef => _soloEntity.Ref<ShiftUnitMasterComponent>();
    public EcsComponentRef<DonerMasterComponent> DonerMasterComponentRef => _soloEntity.Ref<DonerMasterComponent>();
    public EcsComponentRef<AttackUnitMasterComponent> AttackUnitMasterComponentRef => _soloEntity.Ref<AttackUnitMasterComponent>();
    internal EcsComponentRef<BuilderCellMasterComponent> BuilderCellMasterComponentRef => _soloEntity.Ref<BuilderCellMasterComponent>();
    internal EcsComponentRef<GetterUnitMasterComponent> GetterUnitMasterComponentRef => _soloEntity.Ref<GetterUnitMasterComponent>();
    internal EcsComponentRef<ProtecterUnitMasterComponent> ProtecterUnitMasterComponentRef => _soloEntity.Ref<ProtecterUnitMasterComponent>();
    internal EcsComponentRef<RefresherMasterComponent> RefresherMasterComponentRef => _soloEntity.Ref<RefresherMasterComponent>();

    #endregion


    internal EcsComponentRef<EconomyMasterComponent> EconomyMasterComponentRef
        => _economyEntity.Ref<EconomyMasterComponent>();
    internal EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> EconomyUnitsMasterComponentRef
        => _economyEntity.Ref<EconomyMasterComponent.UnitsMasterComponent>();
    internal EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> EconomyBuildingsMasterComponentRef
        => _economyEntity.Ref<EconomyMasterComponent.BuildingsMasterComponent>();

    internal EcsComponentRef<ReadyMasterComponent> ReadyMasterComponentRef => _readyEntity.Ref<ReadyMasterComponent>();
    internal EcsComponentRef<TheEndGameMasterComponent> TheEndGameMasterComponentRef => _theEndGameEntity.Ref<TheEndGameMasterComponent>();

    #endregion



    public EntitiesMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities(ECSmanager eCSmanager)
    {
        _soloEntity = _ecsWorld.NewEntity()
            .Replace(new SetterUnitMasterComponent(InstanceGame.StartValuesGameConfig, InstanceGame.CellManager, eCSmanager.SystemsMasterManager))
            .Replace(new ShiftUnitMasterComponent(InstanceGame.StartValuesGameConfig, InstanceGame.CellManager, eCSmanager.SystemsMasterManager))
            .Replace(new DonerMasterComponent())
            .Replace(new AttackUnitMasterComponent(InstanceGame.StartValuesGameConfig, InstanceGame.CellManager, eCSmanager.SystemsMasterManager))
            .Replace(new BuilderCellMasterComponent(InstanceGame.StartValuesGameConfig, InstanceGame.CellManager, eCSmanager.SystemsMasterManager))
            .Replace(new GetterUnitMasterComponent(eCSmanager.SystemsMasterManager))
            .Replace(new ProtecterUnitMasterComponent(eCSmanager))
            .Replace(new RefresherMasterComponent(eCSmanager));


        _economyEntity = _ecsWorld.NewEntity()
            .Replace(new EconomyMasterComponent(InstanceGame.StartValuesGameConfig))
            .Replace(new EconomyMasterComponent.UnitsMasterComponent(InstanceGame.StartValuesGameConfig))
            .Replace(new EconomyMasterComponent.BuildingsMasterComponent(InstanceGame.StartValuesGameConfig));


        _readyEntity = _ecsWorld.NewEntity()
            .Replace(new ReadyMasterComponent());


        _theEndGameEntity = _ecsWorld.NewEntity()
            .Replace(new TheEndGameMasterComponent());


        #region Cells

        for (int x = 0; x < InstanceGame.StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < InstanceGame.StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                int random;
                random = Random.Range(1, 100);
                if (random <= InstanceGame.StartValuesGameConfig.PERCENT_TREE)
                    eCSmanager.EntitiesGeneralManager.CellEnvironmentComponentRef[x,y].Unref().SetResetEnvironment(true, EnvironmentTypes.Tree);

                random = Random.Range(1, 100);
                if (random <= InstanceGame.StartValuesGameConfig.PERCENT_HILL)
                    eCSmanager.EntitiesGeneralManager.CellEnvironmentComponentRef[x, y].Unref().SetResetEnvironment(true, EnvironmentTypes.Hill);

                random = Random.Range(1, 100);
                if (random <= InstanceGame.StartValuesGameConfig.PERCENT_MOUNTAIN)
                    eCSmanager.EntitiesGeneralManager.CellEnvironmentComponentRef[x, y].Unref().SetResetEnvironment(true, EnvironmentTypes.Mountain);



                if (!eCSmanager.EntitiesGeneralManager.CellEnvironmentComponentRef[x, y].Unref().HaveMountain
                    && !eCSmanager.EntitiesGeneralManager.CellEnvironmentComponentRef[x, y].Unref().HaveTree)
                {
                    random = Random.Range(1, 100);
                    if (random <= InstanceGame.StartValuesGameConfig.PERCENT_FOOD)
                        eCSmanager.EntitiesGeneralManager.CellEnvironmentComponentRef[x, y].Unref().SetResetEnvironment(true, EnvironmentTypes.Food);
                }

            }
        }

        #endregion

    }
}
