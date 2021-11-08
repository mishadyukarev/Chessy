namespace Chessy.Game
{
    public struct UniqSecButDataC
    {
        private static UniqSecAbilTypes _ability;

        public static UniqSecAbilTypes Ability => _ability;

        public static void SetAbility(UniqSecAbilTypes ability)
        {
            _ability = ability;
        }
        public static void DefAbility()
        {
            _ability = default;
        }
    }
}