using System;

namespace Scripts.Game
{
    public struct ThirstyC
    {
        public int WaterAmount { get; set; }
        public bool HaveWater => WaterAmount > 0;

        public void AddWater(int adding = 1)
        {
            if (adding <= 0) throw new Exception();
            WaterAmount += adding;
        }
        public void TakeWater(int taking = 1)
        {
            if (taking <= 0) throw new Exception();
            WaterAmount -= taking;
        }
        public void Set(ThirstyC thirstyC) => WaterAmount = thirstyC.WaterAmount;
        public int MaxAmountWater(UnitTypes unitType) => UnitValues.MaxAmountWater(unitType);
        public bool HaveMaxWater(UnitTypes unitType) => WaterAmount == MaxAmountWater(unitType);
        public void SetMaxWater(UnitTypes unitType) => WaterAmount = MaxAmountWater(unitType);
        public bool NeedWater(UnitTypes unitType) => WaterAmount < MaxAmountWater(unitType) * 0.3f;
        public void TakeWater(UnitTypes unitType) => TakeWater((int)(MaxAmountWater(unitType) * 0.15f));
    }
}