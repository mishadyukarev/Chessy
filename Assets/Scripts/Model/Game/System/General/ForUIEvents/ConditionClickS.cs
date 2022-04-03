using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class ConditionClickS : SystemModelGameAbs
    {
        internal ConditionClickS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click(in ConditionUnitTypes conditionT)
        {
            if (eMG.CurPlayerITC.Is(eMG.WhoseMove.PlayerT))
            {
                if (eMG.UnitConditionTC(eMG.CellsC.Selected).Is(conditionT))
                {
                    eMG.RpcPoolEs.ConditionUnitToMaster(eMG.CellsC.Selected, ConditionUnitTypes.None);
                }
                else
                {
                    eMG.RpcPoolEs.ConditionUnitToMaster(eMG.CellsC.Selected, conditionT);
                }
            }
            else eMG.SoundActionC(ClipTypes.Mistake).Action.Invoke();

            eMG.NeedUpdateView = true;
        }
    }
}