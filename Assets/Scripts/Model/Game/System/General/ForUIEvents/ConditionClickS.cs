using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class ConditionClickS : SystemModel
    {
        internal ConditionClickS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Click(in ConditionUnitTypes conditionT)
        {
            if (eMG.CurPlayerITC.Is(eMG.WhoseMovePlayerTC.PlayerT))
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
            else sMG.MistakeSs.MistakeS.Mistake(MistakeTypes.NeedWaitQueue);

            eMG.NeedUpdateView = true;
        }
    }
}