using System;

namespace Game.Game
{
    public struct ConditionC : IUnitCell, IDamageUnit
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
        public void Set(ConditionC condC)
        {
            _condition = condC._condition;
        }

        public void Reset()
        {
            if (_condition == default) throw new Exception();

            _condition = default;
        }
        public void Sync(CondUnitTypes cond) => _condition = cond;

        public int Damage(int standDamage)
        {
            switch (_condition)
            {
                case CondUnitTypes.None: return 0;

                case CondUnitTypes.Protected:
                    return (int)(standDamage * 0.2f);

                case CondUnitTypes.Relaxed:
                    return -(int)(standDamage * 0.2f);

                default: throw new Exception();
            }
        }
    }
}