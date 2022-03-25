namespace Chessy.Game
{
    sealed class RightZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RightZoneUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = eMGame.CellsC.Selected;

            var activeParent = false;


            if (eMGame.CellsC.Selected > 0)
            {
                if (eMGame.UnitTC(idx_sel).HaveUnit)
                {
                    if (eMGame.UnitEs(idx_sel).ForPlayer(eMGame.CurPlayerITC.Player).IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            eUI.RightEs.Zone.SetActive(activeParent);
        }
    }
}