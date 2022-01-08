using ECS;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class SpawnEntities
    {
        public SpawnEntities()
        {
            var worldTest = new WorldEcs(new Dictionary<int, Entity>());

            new EntityVPool(worldTest);
            new EntityUIPool(worldTest);
            new EntityCellVPool(worldTest, CellValues.X_AMOUNT, CellValues.Y_AMOUNT);

            var isActiveCells = new bool[CellValues.ALL_CELLS_AMOUNT];
            var idCells = new int[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                isActiveCells[idx] = EntityCellVPool.Cell<CellVC>(idx).IsActiveSelf;
                idCells[idx] = EntityCellVPool.Cell<CellVC>(idx).InstanceID;
            }

            new EntityPool(worldTest, EntityVPool.Background<GameObjectC>().Name);
            new EntityCellPool(worldTest, isActiveCells, idCells);
        }
    }
}