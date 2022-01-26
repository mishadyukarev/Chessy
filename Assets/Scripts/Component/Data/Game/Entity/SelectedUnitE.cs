using ECS;

namespace Game.Game
{
    public sealed class SelectedUnitE : EntityAbstract
    {
        public ref UnitTC UnitTC => ref Ent.Get<UnitTC>();
        public ref LevelTC LevelTC => ref Ent.Get<LevelTC>();

        public SelectedUnitE(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}