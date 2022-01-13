using System;

namespace Game.Game
{
    public struct ConditionUnitC : IUnitCellE
    {
        public ConditionUnitTypes Condition;
        public bool HaveCondition => Condition != default;
        public bool Is(params ConditionUnitTypes[] conds)
        {
            if (conds == default) throw new Exception();

            foreach (var cond in conds) if (cond == Condition) return true;
            return false;
        }



        public void Set(ConditionUnitTypes cond)
        {
            if (cond == default) throw new Exception();

            Condition = cond;
        }
        public void Set(ConditionUnitC condC)
        {
            Condition = condC.Condition;
        }

        public void Reset()
        {
            Condition = default;
        }
        public void Sync(ConditionUnitTypes cond) => Condition = cond;
    }
}