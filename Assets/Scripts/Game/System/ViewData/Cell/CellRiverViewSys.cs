using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class CellRiverViewSys : IEcsRunSystem
    {
        private EcsFilter<CellRiverDataC, CellRiverViewC> _cellRiverFilt = default;

        public void Run()
        {
            foreach (var idx_0 in _cellRiverFilt)
            {
                ref var river_0 = ref _cellRiverFilt.Get1(idx_0);

                if(river_0.RiverType == RiverTypes.Start)
                {
                    foreach (var item_0 in river_0.Directs)
                    {
                        _cellRiverFilt.Get2(idx_0).SetActiveRive(item_0.Key, item_0.Value);
                    }
                }
            }
        }
    }
}