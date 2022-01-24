using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct RightZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Unit(EntitiesPool.SelectedIdxE.IdxC.Idx);

            var activeParent = false;


            if (EntitiesPool.SelectedIdxE.IsSelCell)
            {
                if (unit_sel.Have)
                {
                    if (CellUnitVisibleEs.Visible(WhoseMoveE.CurPlayerI, EntitiesPool.SelectedIdxE.IdxC.Idx).IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            UIEntRight.Zone<GameObjectVC>().SetActive(activeParent);
        }
    }
}