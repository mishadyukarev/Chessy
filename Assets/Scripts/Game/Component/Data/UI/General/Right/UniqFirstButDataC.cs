namespace Scripts.Game
{
    public struct UniqFirstButDataC
    {
        private static UniqFirstAbilTypes _ability;

        public static UniqFirstAbilTypes Ability => _ability;

        public static void SetAbility(UniqFirstAbilTypes ability)
        {
            _ability = ability;
        }
    }
}