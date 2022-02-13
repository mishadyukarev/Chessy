namespace Game.Game
{
    sealed class RightZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RightZoneUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var activeParent = false;


            if (Es.SelectedIdxE.IsSelCell)
            {
                if (UnitEs(idx_sel).UnitE.HaveUnit)
                {
                    if (UnitEs(idx_sel).VisibleE(Es.WhoseMoveE.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            UIEs.RightEs.Zone.Zone.SetActive(activeParent);
        }
    }
}