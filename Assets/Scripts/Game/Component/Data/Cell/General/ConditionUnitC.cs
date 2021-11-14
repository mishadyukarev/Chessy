using System;

namespace Chessy.Game
{
    public struct ConditionUnitC
    {
        private CondUnitTypes _condition;

        public CondUnitTypes Condition => _condition;
        public bool HaveCondition => Condition != default;
        public bool Is(params CondUnitTypes[] conds)
        {
            if (conds == default) throw new Exception();

            foreach (var cond in conds) if (cond == _condition) return true;
            return false;
        }



        public void Set(CondUnitTypes cond)
        {
            if (_condition == cond) throw new Exception();
            if (cond == default) throw new Exception();

            _condition = cond;
        }
        public void Reset()
        {
            if (_condition == default) throw new Exception();

            _condition = default;
        }
        public void Sync(CondUnitTypes cond) => _condition = cond;
    }
}