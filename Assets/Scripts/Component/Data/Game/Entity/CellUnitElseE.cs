using ECS;

namespace Game.Game
{
    public sealed class CellUnitElseE : EntityAbstract
    {
        public ref UnitTC UnitC => ref Ent.Get<UnitTC>();
        public ref LevelTC LevelC => ref Ent.Get<LevelTC>();
        public ref PlayerTC OwnerC => ref Ent.Get<PlayerTC>();
        public ref ConditionUnitC ConditionC => ref Ent.Get<ConditionUnitC>();
        public ref IsCornedArcherC CornedC => ref Ent.Get<IsCornedArcherC>();

        public CellUnitElseE(in EcsWorld gameW) : base(gameW) { }
    }
}