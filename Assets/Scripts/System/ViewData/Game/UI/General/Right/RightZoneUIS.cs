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
                if (UnitEs.Main(idx_sel).HaveUnit(UnitStatEs))
                {
                    if (UnitEs.VisibleE(Es.WhoseMove.CurPlayerI, idx_sel).IsVisibleC.IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            EntitiesView.UIEs.RightEs.Zone.Zone.SetActive(activeParent);
        }
    }
}