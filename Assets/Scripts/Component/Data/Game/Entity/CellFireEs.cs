using ECS;

namespace Game.Game
{
    public struct CellFireEs
    {
        static CellFireE[] _fires;

        public static CellFireE Fire(in byte idx) => _fires[idx];

        public CellFireEs(in EcsWorld gameW)
        {
            _fires = new CellFireE[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < _fires.Length; idx++)
            {
                _fires[idx] = new CellFireE(gameW);
            }
        }
    }
}