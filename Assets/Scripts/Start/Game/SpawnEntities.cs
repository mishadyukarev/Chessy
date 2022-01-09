﻿using ECS;

namespace Game.Game
{
    public sealed class SpawnEntities
    {
        public SpawnEntities(in WorldEcs worldEcs)
        {
            new EntityVPool(worldEcs);
            new EntityUIPool(worldEcs);
            new EntityCellVPool(worldEcs, CellValues.X_AMOUNT, CellValues.Y_AMOUNT);

            var isActiveCells = new bool[CellValues.ALL_CELLS_AMOUNT];
            var idCells = new int[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                isActiveCells[idx] = EntityCellVPool.Cell<CellVC>(idx).IsActiveSelf;
                idCells[idx] = EntityCellVPool.Cell<CellVC>(idx).InstanceID;
            }

            new EntityPool(worldEcs, EntityVPool.Background<GameObjectC>().Name);
            new EntityCellPool(worldEcs, isActiveCells, idCells);
        }
    }
}