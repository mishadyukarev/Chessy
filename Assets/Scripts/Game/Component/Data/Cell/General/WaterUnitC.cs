using System;

namespace Scripts.Game
{
    public struct WaterUnitC
    {
        public int WaterAmount { get; set; }
        public bool HaveWater => WaterAmount > 0;
        public bool IsMinusWater => WaterAmount < 0;
        public bool NeedWater => WaterAmount < 100 * 0.4f;


        public void AddWater(int adding = 1)
        {
            if (adding <= 0) throw new Exception();
            WaterAmount += adding;
        }
        public void TakeWater(int taking = 1)
        {
            if (taking <= 0) throw new Exception();
            WaterAmount -= taking;
            if (IsMinusWater) WaterAmount = 0;
        }
        public void Set(WaterUnitC thirstyC) => WaterAmount = thirstyC.WaterAmount;

        public int MaxWater(float upgPercent) => (int)(100 + 100 * upgPercent);
        public bool HaveMaxWater(float upgPercent) => WaterAmount >= MaxWater(upgPercent); 
        public void SetMaxWater(float upgPercent) => WaterAmount = MaxWater(upgPercent);
        public void TakeWater() => TakeWater((int)(100 * 0.15f));

        public void Sync(int waterAmount) => WaterAmount = waterAmount;
    }
}