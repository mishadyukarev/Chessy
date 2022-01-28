namespace Game.Game
{
    struct RightZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Entities.CellEs.UnitEs.Else(Entities.SelectedIdxE.IdxC.Idx).UnitC;

            var activeParent = false;


            if (Entities.SelectedIdxE.IsSelCell)
            {
                if (unit_sel.Have)
                {
                    if (Entities.CellEs.UnitEs.VisibleE(Entities.WhoseMove.CurPlayerI, Entities.SelectedIdxE.IdxC.Idx).VisibleC.IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            EntitiesView.UIEs.RightEs.Zone.Zone.SetActive(activeParent);
        }
    }
}