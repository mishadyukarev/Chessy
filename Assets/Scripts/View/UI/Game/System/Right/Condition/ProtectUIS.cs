using Chessy.Game.Model.Entity.Cell.Unit;
using UnityEngine;

namespace Chessy.Game
{
    public struct ProtectUIS
    {
        public void Run(in RightProtectUIE protectUIE, in UnitEs unit_sel, in PlayerTypes curPlayer)
        {
            var isEnableButt = false;

            if (unit_sel.MainE.UnitTC.HaveUnit)
            {
                if (unit_sel.MainE.PlayerTC.Is(curPlayer))
                {
                    isEnableButt = true;

                    protectUIE.Button(UnitTypes.King).SetActive(false);
                    protectUIE.Button(UnitTypes.Pawn).SetActive(false);
                    protectUIE.Button(UnitTypes.Elfemale).SetActive(false);

                    protectUIE.Button(unit_sel.MainE.UnitTC.UnitT).SetActive(true);

                    if (unit_sel.MainE.ConditionTC.Is(ConditionUnitTypes.Protected))
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
