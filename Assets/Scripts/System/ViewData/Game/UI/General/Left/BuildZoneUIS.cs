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
            ref var own_sel = ref Build<PlayerC>(SelIdx<IdxC>().Idx);


            if (SelIdx<SelIdxEC>().IsSelCell && unit_sel.Is(BuildTypes.City))
            {
                if (own_sel.Is(WhoseMoveC.CurPlayerI))
                {
                    Melt<ButtonUIC>().SetActiveParent(true);
                }
                else Melt<ButtonUIC>().SetActiveParent(false);
            }
            else
            {
                Melt<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}