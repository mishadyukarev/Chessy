using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class BuildZoneUISys : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilter = default;

        public void Run()
        {
            ref var selUnitDataCom = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selOwnUnitCom = ref _cellBuildFilter.Get2(SelectorC.IdxSelCell);


            if (SelectorC.IsSelCell && selUnitDataCom.Is(BuildingTypes.City))
            {
                if (selOwnUnitCom.IsMine)
                {
                    BuildLeftZoneViewUICom.SetActiveZone(true);
                }
                else BuildLeftZoneViewUICom.SetActiveZone(false);
            }
            else
            {
                BuildLeftZoneViewUICom.SetActiveZone(false);
            }
        }
    }
}