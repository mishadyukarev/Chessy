namespace Chessy.Game.System.Model
{
    public struct ConditionClickS
    {
        public void Click(in ConditionUnitTypes conditionT, in Chessy.Game.Entity.Model.EntitiesModelGame e)
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
        }
    }
}