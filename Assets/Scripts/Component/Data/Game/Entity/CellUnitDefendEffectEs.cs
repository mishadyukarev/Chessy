using ECS;

namespace Game.Game
{
    public sealed class CellUnitDefendEffectEs : CellAbstractEs
    {
        public ref AmountC DefendAttack(in byte idx) => ref Cells[idx].Get<AmountC>();

        public CellUnitDefendEffectEs(in EcsWorld gameW) : base(gameW) { }
    }
}