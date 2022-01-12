using ECS;

namespace Game.Game
{
    public readonly struct EntityCellRiverPool
    {
        readonly static Entity[] _rivers;

        public static ref T River<T>(in byte idx) where T : struct, IRiverCell => ref _rivers[idx].Get<T>();

        static EntityCellRiverPool()
        {
            _rivers = new Entity[CellValues.ALL_CELLS_AMOUNT];
        }
        public EntityCellRiverPool(in EcsWorld gameW)
        {
            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _rivers[idx] = gameW.NewEntity()
                    .Add(new RiverC(true));
            }
        }
    }
}