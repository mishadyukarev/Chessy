using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class RightZoneUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom, VisibleC> _cellUnitFilter = default;

        public void Run()
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selVisUnitCom = ref _cellUnitFilter.Get3(SelectorC.IdxSelCell);

            var activeParent = false;


            if (SelectorC.IsSelCell)
            {
                if (selUnitDatCom.HaveUnit)
                {
                    if (selVisUnitCom.IsVisibled(WhoseMoveC.CurPlayer))
                    {
                        activeParent = true;
                    }
                }
            }

            StatZoneViewUIC.SetActiveParentZone(activeParent);
        }
    }
}