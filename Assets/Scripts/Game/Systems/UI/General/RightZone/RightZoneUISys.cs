using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class RightZoneUISys : IEcsRunSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<StatZoneViewUICom> _unitZoneFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom, VisibleCom> _cellUnitFilter = default;


        public void Run()
        {
            var idxSelCell = _selFilt.Get1(0).IdxSelCell;
            ref var unitZoneViewCom = ref _unitZoneFilter.Get1(0);

            ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
            ref var selVisUnitCom = ref _cellUnitFilter.Get3(idxSelCell);

            var activeParent = false;


            if (_selFilt.Get1(0).IsSelCell)
            {
                if (selUnitDatCom.HaveUnit)
                {
                    if (selVisUnitCom.IsVisibled(WhoseMoveCom.CurPlayer))
                    {
                        activeParent = true;
                    }
                }
            }

            unitZoneViewCom.SetActiveParentZone(activeParent);
        }
    }
}