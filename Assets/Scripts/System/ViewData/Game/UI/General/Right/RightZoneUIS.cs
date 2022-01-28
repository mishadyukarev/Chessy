namespace Game.Game
{
    struct RightZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref CellUnitEs.Else(Entities.SelectedIdxE.IdxC.Idx).UnitC;

            var activeParent = false;


            if (Entities.SelectedIdxE.IsSelCell)
            {
                if (unit_sel.Have)
                {
                    if (CellUnitEs.VisibleE(Entities.WhoseMove.CurPlayerI, Entities.SelectedIdxE.IdxC.Idx).VisibleC.IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            RightUIEntities.Zone.Zone.SetActive(activeParent);
        }
    }
}