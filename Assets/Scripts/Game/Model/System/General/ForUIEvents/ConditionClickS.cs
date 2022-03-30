using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class ConditionClickS : SystemModelGameAbs
    {
        internal ConditionClickS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Click(in ConditionUnitTypes conditionT)
        {
            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                if (e.UnitConditionTC(e.CellsC.Selected).Is(conditionT))
                {
                    e.RpcPoolEs.ConditionUnitToMaster(e.CellsC.Selected, ConditionUnitTypes.None);
                }
                else
                {
                    e.RpcPoolEs.ConditionUnitToMaster(e.CellsC.Selected, conditionT);
                }
            }
            else e.Sound(ClipTypes.Mistake).Action.Invoke();

            e.NeedUpdateView = true;
        }
    }
}