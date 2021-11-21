using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class BuildZoneUISys : IEcsRunSystem
    {
        private EcsFilter<BuildC, OwnerC> _cellBuildFilter = default;

        public void Run()
        {
            ref var unit_sel = ref _cellBuildFilter.Get1(SelIdx.Idx);
            ref var own_sel = ref _cellBuildFilter.Get2(SelIdx.Idx);


            if (SelIdx.IsSelCell && unit_sel.Is(BuildTypes.City))
            {
                if (own_sel.Is(WhoseMoveC.CurPlayerI))
                {
                    CutyLeftUIC.SetActiveZone(true);
                }
                else CutyLeftUIC.SetActiveZone(false);
            }
            else
            {
                CutyLeftUIC.SetActiveZone(false);
            }
        }
    }
}