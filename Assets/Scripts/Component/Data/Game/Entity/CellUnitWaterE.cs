using ECS;

namespace Game.Game
{
    public sealed class CellUnitWaterE : EntityAbstract
    {
        public ref AmountC Water => ref Ent.Get<AmountC>();


        public int MaxWater(in CellUnitMainE unitElseE, in UnitStatUpgradesEs statUpgEs)
        {
            var maxWater = CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS;

            if (!unitElseE.UnitTC.IsAnimal)
            {
                if (statUpgEs.Upgrade(UnitStatTypes.Water, unitElseE, UpgradeTypes.PickCenter).HaveUpgrade.Have)
                {
                    return maxWater += (int)(maxWater * 0.5f);
                }
            }

            return maxWater;
        }


        public CellUnitWaterE(in EcsWorld gameW) : base(gameW) { }


        public void Shift(in CellUnitWaterE waterE_from)
        {
            Water = waterE_from.Water;
            waterE_from.Water.Amount = 0;
        }

        public void SetMax(in CellUnitMainE unitElse, in UnitStatUpgradesEs statUpgEs) => Water.Amount = MaxWater(unitElse, statUpgEs);
    }
}