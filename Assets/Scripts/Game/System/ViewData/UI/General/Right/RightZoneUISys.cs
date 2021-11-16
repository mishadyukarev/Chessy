using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class RightZoneUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC, VisibleC> _cellUnitFilter = default;

        public void Run()
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelIdx.Idx);
            ref var selVisUnitCom = ref _cellUnitFilter.Get3(SelIdx.Idx);

            var activeParent = false;


            if (SelIdx.IsSelCell)
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