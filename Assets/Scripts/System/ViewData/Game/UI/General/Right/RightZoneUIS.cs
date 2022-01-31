namespace Game.Game
{
    sealed class RightZoneUIS : SystemViewAbstract, IEcsRunSystem
    {
        public RightZoneUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var unit_sel = UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).UnitTC;

            var activeParent = false;


            if (Es.SelectedIdxE.IsSelCell)
            {
                if (unit_sel.Have)
                {
                    if (UnitEs.VisibleE(Es.WhoseMove.CurPlayerI, Es.SelectedIdxE.IdxC.Idx).IsVisibleC.IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            EntitiesView.UIEs.RightEs.Zone.Zone.SetActive(activeParent);
        }
    }
}