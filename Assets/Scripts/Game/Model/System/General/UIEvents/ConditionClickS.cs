using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class ConditionClickS : SystemModelGameAbs
    {
        public ConditionClickS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click(in ConditionUnitTypes conditionT)
        {
            if (eMGame.CurPlayerITC.Is(eMGame.WhoseMove.Player))
            {
                if (eMGame.UnitConditionTC(eMGame.CellsC.Selected).Is(conditionT))
                {
                    eMGame.RpcPoolEs.ConditionUnitToMaster(eMGame.CellsC.Selected, ConditionUnitTypes.None);
                }
                else
                {
                    eMGame.RpcPoolEs.ConditionUnitToMaster(eMGame.CellsC.Selected, conditionT);
                }
            }
            else eMGame.Sound(ClipTypes.Mistake).Action.Invoke();
        }
    }
}