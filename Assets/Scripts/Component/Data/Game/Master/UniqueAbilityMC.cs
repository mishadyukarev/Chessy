namespace Game.Game
{
    public struct UniqueAbilityMC
    {
        static UniqueAbilityTypes _uniq;

        public static void Set(UniqueAbilityTypes uniq) => _uniq = uniq;
        public static void Get(out UniqueAbilityTypes uniq) => uniq = _uniq;
    }
}