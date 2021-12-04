using System;

namespace Game.Game
{
    public struct HpC : IUnitCell
    {
        public const int MIN = 0;


        public int HP { get; internal set; }

        public bool Have => HP > MIN;
        public bool IsMinus => HP < MIN;
        public bool IsZero => HP == MIN;


        internal HpC(in int hp) => HP = hp;


        internal void Set(in HpC hpC) => HP = hpC.HP;

        internal int SetMinHp() => HP = MIN;

        internal void Add(int adding = 1)
        {
            if (adding < 0) throw new Exception("Need a positive number");
            else if (adding == 0) throw new Exception("You're adding zero");
            HP += adding;
        }
        internal void Take(int taking = 1)
        {
            if (taking < 0) throw new Exception("Need a positive number");
            else if (taking == 0) throw new Exception("You're taking zero");
            HP -= taking;

            if (IsMinus) HP = MIN;
        }
    }
}