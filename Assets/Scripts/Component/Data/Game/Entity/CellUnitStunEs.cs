using ECS;

namespace Game.Game
{
    public sealed class CellUnitStunEs : CellAbstE
    {
        public ref AmountC ForExitStun => ref Ent.Get<AmountC>();

        public CellUnitStunEs(in EcsWorld gameW, in byte idx) : base(gameW, idx) { }

        public void Shift(in CellUnitStunEs stunE_from)
        {
            ForExitStun = stunE_from.ForExitStun;
            stunE_from.ForExitStun.Reset();
        }
    }
}