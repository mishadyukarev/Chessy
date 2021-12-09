using Leopotam.Ecs;
using UnityEngine;

namespace Game.Game
{
    public sealed class SpawnEntities : IEcsInitSystem
    {
        EcsWorld _curGameW = default;

        public void Init()
        {
            new EntityUIPool(_curGameW);
            new EntityVPool(_curGameW);

            var isActiveCells = new bool[CellValues.ALL_CELLS_AMOUNT];
            var idCells = new int[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                isActiveCells[idx] = EntityVPool.Cell<CellVC>(idx).IsActiveSelf;
                idCells[idx] = EntityVPool.Cell<CellVC>(idx).InstanceID;
            }

            new EntityPool(_curGameW, isActiveCells, idCells, BackgroundVC.Name);
        }
    }
}