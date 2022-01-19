using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellsForArsonArcherEs
    {
        static Entity[] _cells;

        public static ref C Idxs<C>(in byte idx) where C : struct => ref _cells[idx].Get<C>();

        public CellsForArsonArcherEs(in EcsWorld gameW)
        {
            _cells = new Entity[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < _cells.Length; idx++)
            {
                _cells[idx] = gameW.NewEntity()
                   .Add(new IdxsC(new List<byte>()));
            }
        }
    }
}