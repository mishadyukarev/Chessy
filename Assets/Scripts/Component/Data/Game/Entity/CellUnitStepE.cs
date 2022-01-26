using ECS;

namespace Game.Game
{
    public sealed class CellUnitStepE : EntityAbstract
    {
        public ref AmountC AmountC => ref Ent.Get<AmountC>();

        public CellUnitStepE(in EcsWorld gameW) : base(gameW) { }
    }
}
