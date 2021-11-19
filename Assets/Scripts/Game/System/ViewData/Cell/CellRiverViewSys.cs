using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class CellRiverViewSys : IEcsRunSystem
    {
        private EcsFilter<RiverC> _riverF = default;
        private EcsFilter<RiverVC> _riverVF = default;

        public void Run()
        {
            foreach (var idx_0 in _riverVF)
            {
                ref var river_0 = ref _riverF.Get1(idx_0);

                if(river_0.River == RiverTypes.Start)
                {
                    foreach (var item_0 in river_0.DirectsDict)
                    {
                        _riverVF.Get1(idx_0).SetActiveRive(item_0.Key, item_0.Value);
                    }
                }
            }
        }
    }
}