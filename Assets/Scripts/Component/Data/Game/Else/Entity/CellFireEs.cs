using ECS;

namespace Game.Game
{
    public readonly struct CellFireEs
    {
        static Entity[] _fires;

        public static ref T Fire<T>(in byte idx) where T : struct, IFireCell => ref _fires[idx].Get<T>();

        public CellFireEs(in EcsWorld gameW)
        {
            _fires = new Entity[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < _fires.Length; idx++)
            {
                _fires[idx] = gameW.NewEntity()
                    .Add(new HaveEffectC());
            }
        }
    }
}