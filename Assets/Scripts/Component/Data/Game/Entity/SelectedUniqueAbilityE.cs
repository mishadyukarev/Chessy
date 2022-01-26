using ECS;

namespace Game.Game
{
    public sealed class SelectedUniqueAbilityE : EntityAbstract
    {
        public ref UniqueAbilityC AbilityC => ref Ent.Get<UniqueAbilityC>();

        public SelectedUniqueAbilityE(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}