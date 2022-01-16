using static Game.Game.EntityCellRiverPool;

namespace Game.Game
{
    struct RiverCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                ref var river_0 = ref River<RiverC>(idx_0);

                if (river_0.River == RiverTypes.Start)
                {
                    foreach (var item_0 in river_0.DirectsDict)
                    {
                        CellRiverVEs.River<SpriteRendererVC>(item_0.Key, idx_0).SetActive(item_0.Value);
                    }
                }
            }
        }
    }
}