using Chessy.Game;

namespace Chessy.Common
{
    public struct AbilityTC
    {
        public AbilityTypes Ability;

        public bool Is(params AbilityTypes[] abils)
        {
            foreach (var abil in abils) if (Ability == abil) return true;
            return false;
        }

        public void Set(in AbilityTypes ability) => Ability = ability;
        public void Reset() => Ability = default;

    }
}