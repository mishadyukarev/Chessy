using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class CreateEnts : IEcsInitSystem
    {
        readonly EcsWorld _curGameW = default;

        public void Init()
        {
            var isActiveCells = new bool[CellValuesC.AMOUNT_ALL_CELLS];
            var idCells = new int[CellValuesC.AMOUNT_ALL_CELLS];

            for (byte idx = 0; idx < CellValuesC.AMOUNT_ALL_CELLS; idx++)
            {
                isActiveCells[idx] = EntityVPool.GetCellVC<CellVC>(idx).IsActiveSelf;
                idCells[idx] = EntityVPool.GetCellVC<CellVC>(idx).InstanceID;
            }

            new EntityPool(_curGameW, isActiveCells, idCells);    
        }
    }
}