using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class SpawnEntities
    {
        public SpawnEntities(in WorldEcs worldEcs)
        {
            new EntityVPool(worldEcs, out var actions, out var sounds0, out var sounds1);
            new EntityUIPool(worldEcs);
            new EntityCellVPool(worldEcs, CellValues.X_AMOUNT, CellValues.Y_AMOUNT);

            var isActiveCells = new bool[CellValues.ALL_CELLS_AMOUNT];
            var idCells = new int[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                isActiveCells[idx] = EntityCellVPool.Cell<CellVC>(idx).IsActiveSelf;
                idCells[idx] = EntityCellVPool.Cell<CellVC>(idx).InstanceID;
            }

            var namesMethods = RpcS.NamesMethods;

            new EntityPool(worldEcs, EntityVPool.Background<GameObjectC>().Name, actions, namesMethods, sounds0, sounds1);
            new EntityCellPool(worldEcs, isActiveCells, idCells);
        }
    }
}