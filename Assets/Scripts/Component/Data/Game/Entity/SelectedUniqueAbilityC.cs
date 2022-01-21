using ECS;

namespace Game.Game
{
    public struct SelectedUniqueAbilityC
    {
        static Entity _ent;

        public static ref UniqueAbilityC AbilityC => ref _ent.Get<UniqueAbilityC>();


        public SelectedUniqueAbilityC(in EcsWorld gameW)
        {
            _ent = gameW.NewEntity()
                .Add(new UniqueAbilityC());
        }
    }
}