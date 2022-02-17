namespace Game.Game
{
    sealed class RightZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RightZoneUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxC.Idx;

            var activeParent = false;


            if (Es.SelectedIdxC.Idx > 0)
            {
                if (Es.UnitTC(idx_sel).HaveUnit)
                {
                    if (Es.UnitEs(idx_sel).ForPlayer(Es.CurPlayerI.Player).IsVisibleC)
                    {
                        activeParent = true;
                    }
                }
            }

            UIEs.RightEs.Zone.Zone.SetActive(activeParent);
        }
    }
}