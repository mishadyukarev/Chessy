namespace Game.Game
{
    public struct UniqueAbilityC
    {
        public UniqueAbilityTypes Ability;

        public bool Is(in UniqueAbilityTypes unique) => Ability == unique;

        public void Reset() => Ability = default;
    }
}