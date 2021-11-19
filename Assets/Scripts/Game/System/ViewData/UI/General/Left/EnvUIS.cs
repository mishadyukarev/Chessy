using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class EnvUIS : IEcsRunSystem
    {
        private EcsFilter<BuildC> _buildF = default;
        private EcsFilter<EnvC, EnvResC> _envF = default;

        public void Run()
        {
            ref var build_sel = ref _buildF.Get1(SelIdx.Idx);

            ref var env_sel = ref _envF.Get1(SelIdx.Idx);
            ref var envRes_sel = ref _envF.Get2(SelIdx.Idx);


            if (SelIdx.IsSelCell && !build_sel.Is(BuildTypes.City))
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