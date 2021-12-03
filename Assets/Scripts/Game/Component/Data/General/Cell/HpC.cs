using System;

namespace Game.Game
{
    public struct HpC : IUnitStatCell
    {
        public const int MAX_HP = 100;
        public const int MIN_HP = 0;


        public int HP;
        public int Min => MIN_HP;
        public int Max => MAX_HP;

        public bool HaveMax => HP >= MAX_HP;
        public bool Have => HP > MIN_HP;
        public bool IsMinus => HP < MIN_HP;
        public bool IsZero => HP == MIN_HP;


        internal HpC(in int hp) => HP = hp;


        internal void Set(in HpC hpC) => HP = hpC.HP;
        internal void Set(in int hp) => HP = hp;

        public void SetMax() => HP = MAX_HP;
        public int SetMinHp() => HP = MIN_HP;

        public void Add(int adding = 1)
        {
            if (adding < MIN_HP) throw new Exception("Need a positive number");
            else if (adding == MIN_HP) throw new Exception("You're adding zero");
            HP += adding;
        }
        public void Take(int taking = 1)
        {
            if (Have)
            {
                if (taking < MIN_HP) throw new Exception("Need a positive number");
                else if (taking == MIN_HP) throw new Exception("You're taking zero");
                HP -= taking;

                if (IsMinus) HP = MIN_HP;
            }
            else throw new Exception("Hp <= 0");
        }
    }
}