namespace Game.Game
{
    sealed class RightZoneUIS : SystemViewAbstract, IEcsRunSystem
    {
        public RightZoneUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var unit_sel = ref Es.CellEs.UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).UnitC;

            var activeParent = false;


            if (Es.SelectedIdxE.IsSelCell)
            {
                if (unit_sel.Have)
                {
                    if (Es.CellEs.UnitEs.VisibleE(Es.WhoseMove.CurPlayerI, Es.SelectedIdxE.IdxC.Idx).VisibleC.IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            EntitiesView.UIEs.RightEs.Zone.Zone.SetActive(activeParent);
        }
    }
}