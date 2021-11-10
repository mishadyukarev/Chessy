using System;

namespace Chessy.Game
{
    public struct HpUnitC
    {
        private int _hp;
        public const int MIN_HP = 0;
        public const int MAX_HP = 100;


        public int Hp => _hp;
        public int MinHp => MIN_HP;
        public int MaxHp => MAX_HP;

        public bool HaveMaxHp => Hp >= MAX_HP;
        public bool HaveHp => Hp > MIN_HP;
        public bool IsMinusHp => Hp < MIN_HP;
        public bool IsZeroHp => Hp == MIN_HP;
        public bool IsHpDeathAfterAttack => Hp <= UnitValues.HP_FOR_DEATH_AFTER_ATTACK;





        public void AddHp(int adding = 1)
        {
            if (adding < MIN_HP) throw new Exception("Need a positive number");
            else if (adding == MIN_HP) throw new Exception("You're adding zero");
            _hp += adding;
        }
        public void TakeHp(int taking = 1)
        {
            if (HaveHp)
            {
                if (taking < MIN_HP) throw new Exception("Need a positive number");
                else if (taking == MIN_HP) throw new Exception("You're taking zero");
                _hp -= taking;

                if (IsMinusHp) _hp = MIN_HP;
            }
            else throw new Exception("Hp <= 0");
        }

        public void SetMaxHp() => _hp = MAX_HP;
        public int SetMinHp() => _hp = MIN_HP;

        public void TakeHpThirsty(UnitTypes unitType)
        {
            float percent = 0;
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: percent = 0.4f; break;
                case UnitTypes.Pawn: percent = 0.5f; break;
                case UnitTypes.Rook: percent = 0.5f; break;
                case UnitTypes.Bishop: percent = 0.5f; break;
                case UnitTypes.Scout: percent = 0.5f; break;
                case UnitTypes.Elfemale: percent = 0.5f; break;
                default: throw new Exception();
            }

            TakeHp((int)(MAX_HP * percent));
        }

        public void Sync(int hp) => _hp = hp;
    }
}