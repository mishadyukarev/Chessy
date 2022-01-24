using ECS;

namespace Game.Game
{
    public sealed class CellUnitElseEs : CellAbstractEs
    {
        public ref LevelTC Level(in byte idx) => ref Cells[idx].Get<LevelTC>();
        public ref PlayerTC Owner(in byte idx) => ref Cells[idx].Get<PlayerTC>();
        public ref ConditionUnitC Condition(in byte idx) => ref Cells[idx].Get<ConditionUnitC>();
        public ref IsCornedArcherC Corned(in byte idx) => ref Cells[idx].Get<IsCornedArcherC>();

        public CellUnitElseEs(in EcsWorld gameW) : base(gameW) { }
    }
}