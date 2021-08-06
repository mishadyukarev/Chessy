using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.System.Data.Game.General.Cell
{
    internal sealed class CellFireDataSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        private static EcsEntity[,] _cellFireEnts;

        internal static ref HaveFireComponent HaveFireCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<HaveFireComponent>();

        public void Init()
        {
            _cellFireEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    _cellFireEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new HaveFireComponent());
                }
        }


    }
}
