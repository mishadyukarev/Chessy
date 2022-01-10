using System;

namespace Game.Game
{
    public struct HpC : IUnitCellE, ITrailCell
    {
        public const int MIN = 0;


        public int Hp { get; internal set; }

        public bool Have => Hp > MIN;
        public bool IsMinus => Hp < MIN;
        public bool IsZero => Hp == MIN;


        internal HpC(in int hp) => Hp = hp;


        internal void Set(in HpC hpC) => Hp = hpC.Hp;

        internal int SetMinHp() => Hp = MIN;

        internal void Add(int adding = 1)
        {
            if (adding < 0) throw new Exception("Need a positive number");
            else if (adding == 0) throw new Exception("You're adding zero");
            Hp += adding;
        }
        internal void Take(int taking = 1)
        {
            if (taking < 0) throw new Exception("Need a positive number");
            else if (taking == 0) throw new Exception("You're taking zero");
            Hp -= taking;

            if (IsMinus) Hp = MIN;
        }
    }
}