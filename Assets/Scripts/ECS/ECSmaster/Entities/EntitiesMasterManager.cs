using Leopotam.Ecs;
using UnityEngine;
using static MainGame;

public class EntitiesMasterManager : EntitiesManager
{

    #region Properties

    internal EcsComponentRef<MotionComponent> RefresherMasterComponentRef => _elseEntity.Ref<MotionComponent>();
    internal EcsComponentRef<ReadyMasterComponent> ReadyMasterComponentRef => _elseEntity.Ref<ReadyMasterComponent>();
    internal EcsComponentRef<FromInfoComponent> FromInfoComponentRef => _elseEntity.Ref<FromInfoComponent>();

    #endregion



    public EntitiesMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities(ECSmanager eCSmanager)
    {
        _elseEntity = _ecsWorld.NewEntity()
            .Replace(new MotionComponent())
            .Replace(new ReadyMasterComponent())
            .Replace(new FromInfoComponent());

        #region Cells

        for (int x = 0; x < InstanceGame.StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < InstanceGame.StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                int random;
                random = Random.Range(1, 100);
                if (random <= InstanceGame.StartValuesGameConfig.PERCENT_TREE)
                    eCSmanager.EntitiesGeneralManager.CellEnvironmentComponentRef[x, y].Unref().SetResetEnvironment(true, EnvironmentTypes.Tree);

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
