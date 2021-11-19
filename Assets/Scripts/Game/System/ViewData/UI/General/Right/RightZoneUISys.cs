using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class RightZoneUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC, VisibleC> _unitF = default;

        public void Run()
        {
            ref var unitD_sel = ref _unitF.Get1(SelIdx.Idx);
            ref var unitV_sel = ref _unitF.Get3(SelIdx.Idx);

            var activeParent = false;


            if (CellClickC.Is(CellClickTypes.SelCell))
            {
                if (unitD_sel.HaveUnit)
                {
                    if (unitV_sel.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        activeParent = true;
                    }
                }
            }

            StatUIC.SetActiveParentZone(activeParent);
        }
    }
}