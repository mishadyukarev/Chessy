using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellEnvirDataContainerEnts
    {
        private EcsEntity[,] _cellFertilizerEnts;
        internal ref AmountResourcesComponent CellFertilizerEnt_AmountResourcesCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
        internal ref HaveEnvironmentComponent CellFertilizerEnt_HaveEnvCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellYoungForestEnts;
        internal ref HaveEnvironmentComponent CellYoungForestEnt_HaveEnvCom(int[] xy) => ref _cellYoungForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellAdultForestEnts;
        internal ref AmountResourcesComponent CellAdultForestEnt_AmountResourcesCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
        internal ref HaveEnvironmentComponent CellAdultForestEnt_HaveEnvCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellHillEnts;
        internal ref AmountResourcesComponent CellHillEnt_AmountResourcesCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
        internal ref HaveEnvironmentComponent CellHillEnt_HaveEnvCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellMountainEnts;
        internal ref HaveEnvironmentComponent CellMountainEnt_HaveEnvCom(int[] xy) => ref _cellMountainEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        internal CellEnvirDataContainerEnts(EcsWorld gameWorld)
        {
            _cellFertilizerEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellYoungForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellAdultForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellHillEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellMountainEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    _cellFertilizerEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new AmountResourcesComponent())
                        .Replace(new HaveEnvironmentComponent());


                    _cellYoungForestEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new HaveEnvironmentComponent());


                    _cellAdultForestEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new AmountResourcesComponent())
                        .Replace(new HaveEnvironmentComponent());


                    _cellHillEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new AmountResourcesComponent())
                        .Replace(new HaveEnvironmentComponent());


                    _cellMountainEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new HaveEnvironmentComponent());
                }


        }
    }
}
