namespace Chessy.Game
{
    sealed class RightZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RightZoneUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
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

            UIEs.RightEs.Zone.SetActive(activeParent);
        }
    }
}