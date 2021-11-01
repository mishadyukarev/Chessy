using System;

namespace Scripts.Game
{
    public struct ThirstyUnitC
    {
        public int WaterAmount { get; set; }
        public bool HaveWater => WaterAmount > 0;
        public bool IsMinusWater => WaterAmount < 0;

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
        public void Set(ThirstyUnitC thirstyC) => WaterAmount = thirstyC.WaterAmount;
        public int MaxWater(UnitTypes unitType) => UnitValues.MaxAmountWater(unitType);
        public bool HaveMaxWater(UnitTypes unitType) => WaterAmount == MaxWater(unitType);
        public void SetMaxWater(UnitTypes unitType) => WaterAmount = MaxWater(unitType);
        public bool NeedWater(UnitTypes unitType) => WaterAmount < MaxWater(unitType) * 0.3f;
        public void TakeWater() => TakeWater(1);
    }
}