using System;

namespace Game.Game
{
    public struct ConditionC : IUnitCellE
    {
        private ConditionUnitTypes _condition;

        public ConditionUnitTypes Condition => _condition;
        public bool HaveCondition => Condition != default;
        public bool Is(params ConditionUnitTypes[] conds)
        {
            if (conds == default) throw new Exception();

            foreach (var cond in conds) if (cond == _condition) return true;
            return false;
        }



        public void Set(ConditionUnitTypes cond)
        {
            if (cond == default) throw new Exception();

            _condition = cond;
        }
        public void Set(ConditionC condC)
        {
            _condition = condC._condition;
        }

        public void Reset()
        {
            _condition = default;
        }
        public void Sync(ConditionUnitTypes cond) => _condition = cond;
    }
}