using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.Workers.Game.Else.Fire
{
    internal struct CellFireDataContainer
    {
        private static EcsEntity[,] _cellFireEnts;

        internal CellFireDataContainer(EcsWorld gameWorld)
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

        internal static void SetFire(bool haveEffect, int[] xy) => _cellFireEnts[xy[X], xy[Y]].Get<HaveFireComponent>().HaveFire = haveEffect;
        internal static bool HaveFire(int[] xy) => _cellFireEnts[xy[X], xy[Y]].Get<HaveFireComponent>().HaveFire;
        internal static void EnableFire(int[] xy) => SetFire(true, xy);
        internal static void ResetFire(int[] xy) => SetFire(false, xy);
    }
}
