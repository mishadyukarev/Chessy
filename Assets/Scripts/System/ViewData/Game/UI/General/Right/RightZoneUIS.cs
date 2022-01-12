using static Game.Game.EntCellUnit;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct RightZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Unit<UnitC>(SelIdx<IdxC>().Idx);

            var activeParent = false;


            if (SelIdx<SelIdxEC>().IsSelCell)
            {
                if (unit_sel.Have)
                {
                    if (Unit<VisibledC>(WhoseMoveC.CurPlayerI, SelIdx<IdxC>().Idx).IsVisibled)
                    {
                        activeParent = true;
                    }
                }
            }

            UIEntRight.Zone<GameObjectVC>().SetActive(activeParent);
        }
    }
}