namespace Chessy.Game
{
    public struct AbilityTC
    {
        public AbilityTypes Ability { get; internal set; }

        public bool Is(params AbilityTypes[] abils)
        {
            foreach (var abil in abils) if (Ability == abil) return true;
            return false;
        }

        internal void Reset() => Ability = default;
    }
}