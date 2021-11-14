using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class RightZoneUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC, VisibleC> _cellUnitFilter = default;

        public void Run()
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(IdxSel.Idx);
            ref var selVisUnitCom = ref _cellUnitFilter.Get3(IdxSel.Idx);

            var activeParent = false;


            if (IdxSel.IsSelCell)
            {
                if (selUnitDatCom.HaveUnit)
                {
                    if (selVisUnitCom.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        activeParent = true;
                    }
                }
            }

            StatZoneViewUIC.SetActiveParentZone(activeParent);
        }
    }
}