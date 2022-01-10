using System;

namespace Game.Game
{
    public struct ConditionC : IUnitCellE
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
        public void Sync(CondUnitTypes cond) => _condition = cond;
    }
}