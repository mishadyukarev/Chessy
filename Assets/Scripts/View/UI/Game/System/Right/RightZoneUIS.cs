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
            var idx_sel = _e.CellsC.Selected;

            var activeParent = false;


            if (_e.CellsC.Selected > 0)
            {
                if (_e.UnitT(idx_sel).HaveUnit())
                {
                    if (_e.UnitVisibleC(idx_sel).IsVisible(_e.CurPlayerIT))
                    {
                        activeParent = true;
                    }
                }
            }

            eUI.RightEs.Zone.SetActive(activeParent);
        }
    }
}