using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class RightZoneUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame eUI;

        internal RightZoneUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            var idx_sel = e.CellsC.Selected;

            var activeParent = false;


            if (e.CellsC.Selected > 0)
            {
                if (e.UnitTC(idx_sel).HaveUnit)
                {
                    if (e.UnitVisibleC(idx_sel).IsVisible(e.CurPlayerITC.PlayerT))
                    {
                        activeParent = true;
                    }
                }
            }

            eUI.RightEs.Zone.SetActive(activeParent);
        }
    }
}