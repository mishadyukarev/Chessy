using ECS;

namespace Game.Game
{
    public sealed class AmountUnitsE : EntityAbstract
    {
        public ref AmountC Units => ref Ent.Get<AmountC>();

        public AmountUnitsE(in int amountUnits, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new AmountC(amountUnits));
        }
    }
}