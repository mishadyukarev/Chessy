namespace Chessy.Game
{
    sealed class RightZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RightZoneUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = e.CellsC.Selected;

            var activeParent = false;


            if (e.CellsC.Selected > 0)
            {
                if (e.UnitTC(idx_sel).HaveUnit)
                {
                    if (e.UnitEs(idx_sel).ForPlayer(e.CurPlayerITC.Player).IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            eUI.RightEs.Zone.SetActive(activeParent);
        }
    }
}