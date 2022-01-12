using ECS;

namespace Game.Game
{
    public readonly struct EntityCellFirePool
    {
        readonly static Entity[] _fires;

        public static ref T Fire<T>(in byte idx) where T : struct, IFireCell => ref _fires[idx].Get<T>();

        static EntityCellFirePool()
        {
            _fires = new Entity[CellValues.ALL_CELLS_AMOUNT];
        }
        public EntityCellFirePool(in EcsWorld gameW)
        {
            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _fires[idx] = gameW.NewEntity()
                    .Add(new HaveEffectC());
            }
        }
    }
}