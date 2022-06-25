using Chessy.Model;

namespace Chessy.Model
{
    sealed class RightZoneUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI eUI;

        internal RightZoneUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            var idx_sel = _e.SelectedCellIdx;

            var activeParent = false;


            if (_e.SelectedCellIdx > 0)
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