using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class CellRiverViewSys : IEcsRunSystem
    {
        private EcsFilter<CellRiverDataC, CellRiverViewC> _cellRiverFilt = default;

        public void Run()
        {
            foreach (var idx_0 in _cellRiverFilt)
            {
                ref var riverC_0 = ref _cellRiverFilt.Get1(idx_0);

                if (riverC_0.RiverType == RiverTypes.Start)
                {
                    foreach (var dirType_1 in riverC_0.DirectTypes)
                    {
                        _cellRiverFilt.Get2(idx_0).SetActiveRive(dirType_1, true);
                    }
                }
            }
        }
    }
}