using UnityEngine;

namespace Chessy.Game
{
    static class ProtectUIS
    {
        public static void Run(this in RightProtectUIE protectUIE, in EntitiesModel e)
        {
            var idx_sel = e.SelectedIdxC.Idx;

            var isEnableButt = false;

            if (e.UnitTC(idx_sel).HaveUnit)
            {
                if (e.UnitPlayerTC(idx_sel).Is(e.CurPlayerITC.Player))
                {
                    isEnableButt = true;

                    protectUIE.Button(UnitTypes.King).SetActive(false);
                    protectUIE.Button(UnitTypes.Pawn).SetActive(false);
                    protectUIE.Button(UnitTypes.Elfemale).SetActive(false);

                    protectUIE.Button(e.UnitTC(idx_sel).Unit).SetActive(true);

                    if (e.UnitConditionTC(idx_sel).Is(ConditionUnitTypes.Protected))
                    {
                        protectUIE.ImageUIC.Image.color = Color.yellow;
                    }

                    else
                    {
                        protectUIE.ImageUIC.Image.color = Color.white;
                    }
                }
            }

            protectUIE.ImageUIC.SetActiveParent(isEnableButt);
        }
    }
}
