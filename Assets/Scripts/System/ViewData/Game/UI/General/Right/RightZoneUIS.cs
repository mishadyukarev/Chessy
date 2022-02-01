namespace Game.Game
{
    sealed class RightZoneUIS : SystemViewAbstract, IEcsRunSystem
    {
        public RightZoneUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var activeParent = false;


            if (Es.SelectedIdxE.IsSelCell)
            {
                if (UnitEs(idx_sel).MainE.HaveUnit(UnitStatEs(idx_sel)))
                {
                    if (UnitEs(idx_sel).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            VEs.UIEs.RightEs.Zone.Zone.SetActive(activeParent);
        }
    }
}