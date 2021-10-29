namespace Scripts.Game
{
    public struct HpComponent
    {
        public int AmountHealth { get; set; }
        public void AddAmountHealth(int adding = 1) => AmountHealth += adding;
        public void TakeAmountHealth(int taking = 1) => AmountHealth -= taking;
        public void TakeAmountHealth(int max, float taking) => AmountHealth -= (int)(max * taking);

        public bool HaveAmountHealth => AmountHealth > 0;

        public int MaxAmountHealth(UnitTypes unitType) => UnitValues.StandAmountHealth(unitType);
        public bool HaveMaxAmountHealth(UnitTypes unitType) => AmountHealth >= MaxAmountHealth(unitType);
        public void AddStandartHeal(UnitTypes unitType) => AddAmountHealth((int)(UnitValues.StandAmountHealth(unitType) * UnitValues.ForAddingHealth(unitType)));
        public void SetMaxAmountHealth(UnitTypes unitType) => AmountHealth = MaxAmountHealth(unitType);
    }
}