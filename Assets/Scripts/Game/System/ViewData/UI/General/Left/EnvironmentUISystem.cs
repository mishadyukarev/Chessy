using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class EnvironmentUISystem : IEcsRunSystem
    {
        private EcsFilter<BuildC> _cellBuildFilter = default;
        private EcsFilter<EnvC, EnvResC> _cellEnvFilter = default;

        public void Run()
        {
            ref var selBuildDatC = ref _cellBuildFilter.Get1(IdxSel.Idx);

            ref var env_sel = ref _cellEnvFilter.Get1(IdxSel.Idx);
            ref var envRes_sel = ref _cellEnvFilter.Get2(IdxSel.Idx);


            if (IdxSel.IsSelCell && !selBuildDatC.Is(BuildTypes.City))
            {
                EnvirZoneViewUICom.SetActiveParent(true);
            }
            else
            {
                EnvirZoneViewUICom.SetActiveParent(false);
            }


            EnvirZoneViewUICom.SetTextResour(ResTypes.Food, envRes_sel.AmountRes(EnvTypes.Fertilizer).ToString());
            EnvirZoneViewUICom.SetTextResour(ResTypes.Wood, envRes_sel.AmountRes(EnvTypes.AdultForest).ToString());
            EnvirZoneViewUICom.SetTextResour(ResTypes.Ore, envRes_sel.AmountRes(EnvTypes.Hill).ToString());
        }
    }
}