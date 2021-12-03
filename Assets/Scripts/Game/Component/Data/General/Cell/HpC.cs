using System;

namespace Game.Game
{
    public struct HpC : IUnitStatCell
    {
        private int _hp;

        public const int MAX_HP = 100;
        public const int MIN_HP = 0;


        public int Hp => _hp;
        public int Min => MIN_HP;
        public int Max => MAX_HP;

        public bool HaveMax => Hp >= MAX_HP;
        public bool Have => Hp > MIN_HP;
        public bool IsMinus => Hp < MIN_HP;
        public bool IsZero => Hp == MIN_HP;
        public bool IsHpDeathAfterAttack => Hp <= UnitValues.HP_FOR_DEATH_AFTER_ATTACK;





        internal void Set(in HpC hpC) => _hp = hpC._hp;
        internal void Set(in int hp) => _hp = hp;

        public void SetMax() => _hp = MAX_HP;
        public int SetMinHp() => _hp = MIN_HP;

        public void Add(int adding = 1)
        {
            if (adding < MIN_HP) throw new Exception("Need a positive number");
            else if (adding == MIN_HP) throw new Exception("You're adding zero");
            _hp += adding;
        }
        public void Take(int taking = 1)
        {
            if (Have)
            {
                if (taking < MIN_HP) throw new Exception("Need a positive number");
                else if (taking == MIN_HP) throw new Exception("You're taking zero");
                _hp -= taking;

                if (IsMinus) _hp = MIN_HP;
            }
            else throw new Exception("Hp <= 0");
        }
    }
}