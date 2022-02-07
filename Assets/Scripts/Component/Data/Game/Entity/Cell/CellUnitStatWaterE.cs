using ECS;

namespace Game.Game
{
    public sealed class CellUnitStatWaterE : EntityAbstract
    {
        ref AmountC WaterCRef => ref Ent.Get<AmountC>();
        public AmountC WaterC => Ent.Get<AmountC>();


        public int Water
        {
            get => WaterCRef.Amount;
            set => WaterCRef.Amount = value;
        }
        public const int MAX_WATER_WITHOUT_EFFECTS = CellUnitStatWaterValues.MAX_WATER_WITHOUT_EFFECTS;
        public bool HaveWater => WaterC.Amount > 0;

        public bool Have(in AbilityTypes ability) => WaterC.Amount >= CellUnitStatWaterValues.Need(ability);


        public int MaxWater(in CellUnitEs unitEs, in UnitStatUpgradesEs statUpgEs)
        {
            var maxWater = CellUnitStatWaterValues.MAX_WATER_WITHOUT_EFFECTS;

            if (!unitEs.TypeE.UnitTC.IsAnimal)
            {
                if (statUpgEs.Upgrade(UnitStatTypes.Water, unitEs, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    return maxWater += (int)(maxWater * 0.5f);
                }
            }

            return maxWater;
        }


        internal CellUnitStatWaterE(in EcsWorld gameW) : base(gameW) { }


        public void Shift(in CellUnitStatWaterE waterE_from)
        {
            WaterCRef = waterE_from.WaterC;
            waterE_from.WaterCRef.Amount = 0;
        }
        public void Thirsty(in UnitTypes unitT)
        {
            WaterCRef.Amount -= CellUnitStatWaterValues.NeedWaterThirsty(unitT);
        }

        public void SetMax(in CellUnitEs unitEs, in UnitStatUpgradesEs statUpgEs) => WaterCRef.Amount = MaxWater(unitEs, statUpgEs);
        public void Take(in AbilityTypes ability)
        {
            WaterCRef.Amount -= CellUnitStatWaterValues.Need(ability);
        }
    }
}