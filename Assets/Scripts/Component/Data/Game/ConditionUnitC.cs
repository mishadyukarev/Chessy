using System;

namespace Game.Game
{
    public struct ConditionUnitC
    {
        ConditionUnitTypes _condition;

        public ConditionUnitTypes Condition
        {
            get => _condition;
            set
            {
                if (value == ConditionUnitTypes.End) throw new Exception();
                //if (value == _condition) throw new Exception();
                _condition = value;
            }
        }
        public bool HaveCondition => Condition != default;
        public bool Is(params ConditionUnitTypes[] conds)
        {
            if (conds == default) throw new Exception();

            foreach (var cond in conds) if (cond == Condition) return true;
            return false;
        }

        public void Reset()
        {
            Condition = default;
        }
    }
}