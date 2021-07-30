using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellFireDataContainerEnts
    {
        private EcsEntity[,] _cellFireEnts;
        internal ref HaveFireComponent CellFireEnt_HaverEffectCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<HaveFireComponent>();
        internal ref TimeStepsComponent CellFireEnt_TimeStepsCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<TimeStepsComponent>();


        internal CellFireDataContainerEnts(EcsWorld gameWorld)
        {
            _cellFireEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    _cellFireEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new HaveFireComponent())
                        .Replace(new TimeStepsComponent());
                }
        }
    }
}
