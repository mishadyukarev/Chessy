using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SpawnEntities : IEcsInitSystem
    {
        EcsWorld _curGameW;

        public void Init()
        {
            new EntityVPool(_curGameW);

            var isActiveCells = new bool[CellValues.AMOUNT_ALL_CELLS];
            var idCells = new int[CellValues.AMOUNT_ALL_CELLS];

            for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
            {
                isActiveCells[idx] = EntityVPool.Cell<CellVC>(idx).IsActiveSelf;
                idCells[idx] = EntityVPool.Cell<CellVC>(idx).InstanceID;
            }

            new EntityPool(_curGameW, isActiveCells, idCells);
        }
    }
}