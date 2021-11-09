using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class BuildZoneUISys : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataC, OwnerC> _cellBuildFilter = default;

        public void Run()
        {
            ref var selUnitDataCom = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selOwnUnitCom = ref _cellBuildFilter.Get2(SelectorC.IdxSelCell);


            if (SelectorC.IsSelCell && selUnitDataCom.Is(BuildTypes.City))
            {
                if (selOwnUnitCom.Is(WhoseMoveC.CurPlayerI))
                {
                    CutyLeftZoneViewUIC.SetActiveZone(true);
                }
                else CutyLeftZoneViewUIC.SetActiveZone(false);
            }
            else
            {
                CutyLeftZoneViewUIC.SetActiveZone(false);
            }
        }
    }
}