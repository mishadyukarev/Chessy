using UnityEngine;

namespace Chessy.Game
{
    sealed class RightUnitProtectUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RightUnitProtectUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = E.SelectedIdxC.Idx;

            var isEnableButt = false;

            if (E.UnitTC(idx_sel).HaveUnit)
            {
                if (E.UnitPlayerTC(idx_sel).Is(E.CurPlayerITC.Player))
                {
                    isEnableButt = true;

                    UIE.RightEs.ProtectE.Button(UnitTypes.King).SetActive(false);
                    UIE.RightEs.ProtectE.Button(UnitTypes.Pawn).SetActive(false);
                    UIE.RightEs.ProtectE.Button(UnitTypes.Elfemale).SetActive(false);

                    UIE.RightEs.ProtectE.Button(E.UnitTC(idx_sel).Unit).SetActive(true);

                    if (E.UnitConditionTC(idx_sel).Is(ConditionUnitTypes.Protected))
                    {
                        UIE.RightEs.ProtectE.ImageUIC.Image.color = Color.yellow;
                    }

                    else
                    {
                        UIE.RightEs.ProtectE.ImageUIC.Image.color = Color.white;
                    }
                }
            }

            UIE.RightEs.ProtectE.ImageUIC.SetActiveParent(isEnableButt);
        }
    }
}
