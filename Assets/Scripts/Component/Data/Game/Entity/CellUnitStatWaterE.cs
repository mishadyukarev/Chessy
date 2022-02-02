using ECS;

namespace Game.Game
{
    public sealed class CellUnitStatWaterE : EntityAbstract
    {
        ref AmountC WaterRef => ref Ent.Get<AmountC>();
        public AmountC Water => Ent.Get<AmountC>();


        public const int MAX_WATER_WITHOUT_EFFECTS = CellUnitStatWaterValues.MAX_WATER_WITHOUT_EFFECTS;

        public bool HaveWater => Water.Amount > 0;

        public bool Have(in AbilityTypes ability) => Water.Amount >= CellUnitStatWaterValues.Need(ability);
        

        public int MaxWater(in CellUnitMainE unitElseE, in UnitStatUpgradesEs statUpgEs)
        {
            var maxWater = CellUnitStatWaterValues.MAX_WATER_WITHOUT_EFFECTS;

            if (!unitElseE.UnitTC.IsAnimal)
            {
                if (statUpgEs.Upgrade(UnitStatTypes.Water, unitElseE, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    return maxWater += (int)(maxWater * 0.5f);
                }
            }

            return maxWater;
        }


        internal CellUnitStatWaterE(in EcsWorld gameW) : base(gameW) { }


        public void Shift(in CellUnitStatWaterE waterE_from)
        {
            WaterRef = waterE_from.Water;
            waterE_from.WaterRef.Amount = 0;
        }
        public void Thirsty(in UnitTypes unitT)
        {
            WaterRef.Amount -= CellUnitStatWaterValues.NeedWaterThirsty(unitT);
        }

        public void SetMax(in CellUnitMainE unitMainE, in UnitStatUpgradesEs statUpgEs) => WaterRef.Amount = MaxWater(unitMainE, statUpgEs);
        public void Take(in AbilityTypes ability)
        {
            WaterRef.Amount -= CellUnitStatWaterValues.Need(ability);
        }
    }
}