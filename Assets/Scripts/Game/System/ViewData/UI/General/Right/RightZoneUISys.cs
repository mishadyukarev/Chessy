using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class RightZoneUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC, VisibleC> _unitF = default;

        public void Run()
        {
            ref var unitD_sel = ref _unitF.Get1(SelIdx<IdxC>().Idx);
            ref var unitV_sel = ref _unitF.Get3(SelIdx<IdxC>().Idx);

            var activeParent = false;


            if (SelIdx<SelIdxC>().IsSelCell)
            {
                if (unitD_sel.Have)
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