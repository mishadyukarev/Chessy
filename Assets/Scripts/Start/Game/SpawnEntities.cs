using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SpawnEntities : IEcsInitSystem
    {
        EcsWorld _curGameW = default;

        public void Init()
        {
            new EntityVPool(_curGameW);
            new EntityUIPool(_curGameW);
            new EntityCellVPool(_curGameW, CellValues.X_AMOUNT, CellValues.Y_AMOUNT);

            var isActiveCells = new bool[CellValues.ALL_CELLS_AMOUNT];
            var idCells = new int[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                isActiveCells[idx] = EntityCellVPool.Cell<CellVC>(idx).IsActiveSelf;
                idCells[idx] = EntityCellVPool.Cell<CellVC>(idx).InstanceID;
            }

            new EntityPool(_curGameW, EntityVPool.Background<GameObjectC>().Name);
            new EntityCellPool(_curGameW, isActiveCells, idCells);    
        }
    }
}