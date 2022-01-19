using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct RightZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Unit<UnitTC>(SelectedIdxE.IdxC.Idx);

            var activeParent = false;


            if (SelectedIdxE.IsSelCell)
            {
                if (unit_sel.Have)
                {
                    if (CellUnitVisibleEs.Visible<IsVisibleC>(WhoseMoveE.CurPlayerI, SelectedIdxE.IdxC.Idx).IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            UIEntRight.Zone<GameObjectVC>().SetActive(activeParent);
        }
    }
}