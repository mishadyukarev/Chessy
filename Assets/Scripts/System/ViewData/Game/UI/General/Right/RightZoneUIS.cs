using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct RightZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unitD_sel = ref Unit<UnitC>(SelIdx<IdxC>().Idx);

            var activeParent = false;


            if (SelIdx<SelIdxC>().IsSelCell)
            {
                if (unitD_sel.Have)
                {
                    if (Unit<VisibledC>(WhoseMoveC.CurPlayerI, SelIdx<IdxC>().Idx).IsVisibled)
                    {
                        activeParent = true;
                    }
                }
            }

            StatUIC.SetActiveParentZone(activeParent);
        }
    }
}