namespace Game.Game
{
    public struct SelUniqAbilC
    {
        public static UniqueAbilTypes UniqAbil { get; set; }

        public static bool Is(UniqueAbilTypes uniqAbil) => UniqAbil == uniqAbil;

        public SelUniqAbilC(UniqueAbilTypes uniqAbil)
        {
            UniqAbil = uniqAbil;
        }
    }
}