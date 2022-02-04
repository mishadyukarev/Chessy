using System;

namespace Game.Game
{
    public struct ConditionUnitTC
    {
        public ConditionUnitTypes Condition;
        public bool HaveCondition => Condition != default;
        public bool Is(params ConditionUnitTypes[] conds)
        {
            if (conds == default) throw new Exception();

            foreach (var cond in conds) if (cond == Condition) return true;
            return false;
        }
    }
}