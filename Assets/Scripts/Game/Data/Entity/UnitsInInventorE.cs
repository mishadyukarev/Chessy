using ECS;

namespace Chessy.Game
{
    public sealed class UnitsInInventorE : EntityAbstract
    {
        public AmountC AmountC => Ent.Get<AmountC>();

        internal UnitsInInventorE(in int amount, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new AmountC(amount));
        }
    }
}