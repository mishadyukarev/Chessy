using System;

namespace Game.Game
{
    public struct WaterC : IUnitStatCell
    {
        int _water;

        public int Water => _water;
        public bool Have => _water > 0;
        public bool IsMinus => _water < 0;




        internal void AddWater(int adding = 1)
        {
            if (adding <= 0) throw new Exception();
            _water += adding;
        }
        internal void TakeWater(int taking = 1)
        {
            if (taking <= 0) throw new Exception();
            _water -= taking;
            if (IsMinus) _water = 0;
        }


        internal void Set(WaterC waterC) => _water = waterC._water;
        internal void Set(in int water) => _water = water;
    }
}