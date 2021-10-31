using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class EnvironmentUISystem : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;

        public void Run()
        {
            ref var selBuildDatC = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selEnvDatC = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);


            if (SelectorC.IsSelCell && !selBuildDatC.Is(BuildTypes.City))
            {
                EnvirZoneViewUICom.SetActiveParent(true);
            }
            else
            {
                EnvirZoneViewUICom.SetActiveParent(false);
            }


            EnvirZoneViewUICom.SetTextResour(ResTypes.Food, selEnvDatC.AmountRes(EnvTypes.Fertilizer).ToString());
            EnvirZoneViewUICom.SetTextResour(ResTypes.Wood, selEnvDatC.AmountRes(EnvTypes.AdultForest).ToString());
            EnvirZoneViewUICom.SetTextResour(ResTypes.Ore, selEnvDatC.AmountRes(EnvTypes.Hill).ToString());
        }
    }
}