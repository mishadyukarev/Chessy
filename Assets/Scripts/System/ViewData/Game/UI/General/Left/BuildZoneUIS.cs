using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityLeftCityUIPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct BuildZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Build<BuildC>(SelIdx<IdxC>().Idx);
            ref var own_sel = ref Build<OwnerC>(SelIdx<IdxC>().Idx);


            if (SelIdx<SelIdxC>().IsSelCell && unit_sel.Is(BuildTypes.City))
            {
                if (own_sel.Is(WhoseMoveC.CurPlayerI))
                {
                    Melt<ButtonVC>().SetActiveParent(true);
                }
                else Melt<ButtonVC>().SetActiveParent(false);
            }
            else
            {
                Melt<ButtonVC>().SetActiveParent(false);
            }
        }
    }
}