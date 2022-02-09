using ECS;

namespace Game.Game
{
    public sealed class UnitsInInventorE : EntityAbstract
    {
        readonly UnitTypes _unitT;
        readonly LevelTypes _levelT;

        ref AmountC UnitsRef => ref Ent.Get<AmountC>();
        public AmountC Units => Ent.Get<AmountC>();

        public int AmountUnits => UnitsRef.Amount;
        public bool HaveUnits => Units.Amount > 0;

        internal UnitsInInventorE(in UnitTypes unit, in LevelTypes level, in EcsWorld gameW) : base(gameW)
        {
            _unitT = unit;
            _levelT = level;

            Ent.Add(new AmountC(UnitsInInventorValues.StartAmountUnits(unit, level)));
        }

        public void AddUnit()
        {
            UnitsRef.Amount++;
        }
        public void TakeUnit()
        {
            UnitsRef.Amount--;
        }
        public void Sync(in int amountUnits)
        {
            UnitsRef.Amount = amountUnits;
        }
    }
}