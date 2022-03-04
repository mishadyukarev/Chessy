namespace Chessy.Game
{
    sealed class RightZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RightZoneUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = E.SelectedIdxC.Idx;

            var activeParent = false;


            if (E.SelectedIdxC.Idx > 0)
            {
                if (E.UnitTC(idx_sel).HaveUnit)
                {
                    if (E.UnitEs(idx_sel).ForPlayer(E.CurPlayerITC.Player).IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            UIE.RightEs.Zone.SetActive(activeParent);
        }
    }
}