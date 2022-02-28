using UnityEngine;

namespace Chessy.Game
{
    sealed class RightUnitProtectUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RightUnitProtectUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
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

                    UIEs.RightEs.ProtectE.Button(UnitTypes.King).SetActive(false);
                    UIEs.RightEs.ProtectE.Button(UnitTypes.Pawn).SetActive(false);
                    UIEs.RightEs.ProtectE.Button(UnitTypes.Elfemale).SetActive(false);
                    UIEs.RightEs.ProtectE.Button(UnitTypes.Scout).SetActive(false);

                    UIEs.RightEs.ProtectE.Button(E.UnitTC(idx_sel).Unit).SetActive(true);

                    if (E.UnitConditionTC(idx_sel).Is(ConditionUnitTypes.Protected))
                    {
                        UIEs.RightEs.ProtectE.ImageUIC.Image.color = Color.yellow;
                    }

                    else
                    {
                        UIEs.RightEs.ProtectE.ImageUIC.Image.color = Color.white;
                    }
                }
            }

            UIEs.RightEs.ProtectE.ImageUIC.SetActiveParent(isEnableButt);
        }
    }
}
