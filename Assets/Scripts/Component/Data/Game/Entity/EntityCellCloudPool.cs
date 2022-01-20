using ECS;

namespace Game.Game
{
    public readonly struct EntityCellCloudPool
    {
        readonly static Entity[] _clouds;

        public static ref T Cloud<T>(in byte idx) where T : struct, ICloudCell => ref _clouds[idx].Get<T>();

        static EntityCellCloudPool()
        {
            _clouds = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
        }
        public EntityCellCloudPool(in EcsWorld gameW)
        {
            for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                _clouds[idx] = gameW.NewEntity()
                    .Add(new HaveEffectC());
            }
        }
    }
}