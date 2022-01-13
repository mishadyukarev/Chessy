namespace Game.Game
{
    public struct SelUniqAbilC
    {
        public static UniqueAbilityTypes UniqAbil { get; set; }

        public static bool Is(UniqueAbilityTypes uniqAbil) => UniqAbil == uniqAbil;

        public SelUniqAbilC(UniqueAbilityTypes uniqAbil)
        {
            UniqAbil = uniqAbil;
        }
    }
}