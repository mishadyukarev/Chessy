using System;

namespace Game.Game
{
    public struct WaterC : IUnitCellE
    {
        public int Water { get; internal set; }
        public bool Have => Water > 0;
        public bool IsMinus => Water < 0;


        internal void Add(in int adding = 1)
        {
            if (adding <= 0) throw new Exception();
            Water += adding;
        }
        internal void Take(in int taking = 1)
        {
            if (taking <= 0) throw new Exception();
            Water -= taking;
            if (IsMinus) Water = 0;
        }

        internal void Set(in WaterC waterC) => Water = waterC.Water;
    }
}