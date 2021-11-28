using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class RiverCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in EntityPool.Idxs)
            {
                ref var river_0 = ref EntityPool.RiverCellC<RiverC>(idx_0);

                if(river_0.River == RiverTypes.Start)
                {
                    foreach (var item_0 in river_0.DirectsDict)
                    {
                        EntityVPool.RiverCellVC<RiverVC>(idx_0).SetActiveRive(item_0.Key, item_0.Value);
                    }
                }
            }
        }
    }
}