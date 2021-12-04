namespace Game.Game
{
    public struct UniqueAbilityMC
    {
        static UniqueAbilTypes _uniq;

        public static void Set(UniqueAbilTypes uniq) => _uniq = uniq;
        public static void Get(out UniqueAbilTypes uniq) => uniq = _uniq;
    }
}