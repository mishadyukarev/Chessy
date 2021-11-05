using System;

namespace Scripts.Game
{
    public struct HpUnitC
    {
        public const int MIN_HP = 0;
        public const int MAX_HP = 100;

        public int AmountHp { get; set; }

        public bool HaveHp => AmountHp > MIN_HP;
        public bool IsMinusHp => AmountHp < MIN_HP;
        public bool IsZeroHp => AmountHp == MIN_HP;
        public bool IsHpDeathAfterAttack => AmountHp <= UnitValues.HP_FOR_DEATH_AFTER_ATTACK;



        public void AddHp(int adding = 1)
        {
            if (adding < MIN_HP) throw new Exception("Need a positive number");
            else if (adding == MIN_HP) throw new Exception("You're adding zero");
            AmountHp += adding;
        }
        public void TakeHp(int taking = 1)
        {
            if (HaveHp)
            {
                if (taking < MIN_HP) throw new Exception("Need a positive number");
                else if (taking == MIN_HP) throw new Exception("You're taking zero");
                AmountHp -= taking;

                if (IsMinusHp) AmountHp = MIN_HP;
            }
            else throw new Exception("Hp <= 0");
        }

        public int SetMinHp() => AmountHp = MIN_HP;
        public bool Is(int amountHp) => AmountHp == amountHp;

        public bool HaveMaxHpUnit => AmountHp >= MAX_HP;
        public void AddHealHp() => AmountHp += (int)(MAX_HP * 1);
        public void SetMaxHp() => AmountHp = MAX_HP;

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
                default: throw new Exception();
            }

            TakeHp((int)(MAX_HP * percent));
        }
    }
}