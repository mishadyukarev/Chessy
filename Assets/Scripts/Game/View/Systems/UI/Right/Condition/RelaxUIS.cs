using Chessy.Game.Entity.View.UI.Right;
using UnityEngine;

namespace Chessy.Game
{
    public struct RelaxUIS
    {
        public void Run(in RelaxUIE relaxE, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            var idx_0 = e.CellsC.Selected;

            var activeButt = false;

            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (e.UnitPlayerTC(idx_0).Is(e.CurPlayerITC.Player))
                {
                    activeButt = true;

                    relaxE.ImageC.Image.color = e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed) ? Color.green : Color.white;

                    for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
                    {
                        relaxE.Button(unitT).SetActive(false);
                    }

                    if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                    {
                        if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                        {
                            relaxE.Button(e.UnitTC(idx_0).Unit).SetActive(true);
                        }
                        else
                        {
                            relaxE.Button(e.UnitTC(idx_0).Unit).SetActive(false);
                        }
                    }
                    else
                    {
                        relaxE.Button(e.UnitTC(idx_0).Unit).SetActive(true);
                    }
                }
            }

            relaxE.ButtonC.Button.gameObject.SetActive(activeButt);
        }
    }
}