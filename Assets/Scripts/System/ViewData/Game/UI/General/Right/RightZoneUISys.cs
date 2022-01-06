using Leopotam.Ecs;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class RightZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var unitD_sel = ref Unit<UnitC>(SelIdx<IdxC>().Idx);
            ref var unitV_sel = ref Unit<VisibleC>(SelIdx<IdxC>().Idx);

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