using System;

namespace Chessy.Game
{
    public struct ConditionUnitC
    {
        private CondUnitTypes _condition;

        public CondUnitTypes Condition => _condition;
        public bool HaveCondition => Condition != default;

        public void SetNew(CondUnitTypes cond)
        {
            if (_condition == cond) throw new Exception();
            if (cond == default) throw new Exception();

            _condition = cond;
        }
        public void Def()
        {
            if (_condition == default) throw new Exception();

            _condition = default;
        }

        public bool Is(CondUnitTypes cond) => Condition == cond;
        public bool Is(CondUnitTypes[] cond)
        {
            foreach (var condUnitType in cond)
                if (Is(condUnitType)) return true;
            return false;
        }


        public void Sync(CondUnitTypes cond) => _condition = cond;
    }
}