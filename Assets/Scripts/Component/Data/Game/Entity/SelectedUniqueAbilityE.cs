using ECS;

namespace Game.Game
{
    public sealed class SelectedUniqueAbilityE : EntityAbstract
    {
        public ref AbilityC AbilityC => ref Ent.Get<AbilityC>();

        public SelectedUniqueAbilityE(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}