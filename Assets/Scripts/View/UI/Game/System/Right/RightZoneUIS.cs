using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    sealed class RightZoneUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame eUI;

        internal RightZoneUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal void Run()
        {
            var idx_sel = e.CellsC.Selected;

            var activeParent = false;


            if (e.CellsC.Selected > 0)
            {
                if (e.UnitTC(idx_sel).HaveUnit)
                {
                    if (e.UnitEs(idx_sel).ForPlayer(e.CurPlayerITC.PlayerT).IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            eUI.RightEs.Zone.SetActive(activeParent);
        }
    }
}