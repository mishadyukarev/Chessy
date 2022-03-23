namespace Chessy.Game
{
    sealed class RightZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RightZoneUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = E.CellsC.Selected;

            var activeParent = false;


            if (E.CellsC.Selected > 0)
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