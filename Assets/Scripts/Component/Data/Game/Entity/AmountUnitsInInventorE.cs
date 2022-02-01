using ECS;

namespace Game.Game
{
    public sealed class AmountUnitsInInventorE : EntityAbstract
    {
        ref AmountC UnitsRef => ref Ent.Get<AmountC>();
        public AmountC Units => Ent.Get<AmountC>();

        public bool HaveUnits => Units.Amount > 0;

        public AmountUnitsInInventorE(in int amountUnits, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new AmountC(amountUnits));
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