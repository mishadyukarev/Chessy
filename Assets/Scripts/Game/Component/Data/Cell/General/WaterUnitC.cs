using System;

namespace Scripts.Game
{
    public struct WaterUnitC
    {
        private int _water;

        public int Water => _water;
        public bool HaveWater => _water > 0;
        public bool IsMinusWater => _water < 0;
        public bool NeedWater => _water <= 100 * 0.4f;



        public void AddWater(int adding = 1)
        {
            if (adding <= 0) throw new Exception();
            _water += adding;
        }
        public void TakeWater(int taking = 1)
        {
            if (taking <= 0) throw new Exception();
            _water -= taking;
            if (IsMinusWater) _water = 0;
        }

        public int MaxWater(float upgPerc) => (int)(100 + 100 * upgPerc);
        public bool HaveMaxWater(float upgPerc) => _water >= MaxWater(upgPerc);
        public void SetMaxWater(float upgPerc) => _water = MaxWater(upgPerc);
        public void TakeWater() => TakeWater((int)(100 * 0.15f));

        public void Sync(int waterAmount) => _water = waterAmount;
    }
}