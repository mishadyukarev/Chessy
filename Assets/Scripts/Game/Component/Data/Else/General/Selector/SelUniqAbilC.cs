namespace Game.Game
{
    public struct SelUniqAbilC
    {
        public static UniqAbilTypes UniqAbil { get; set; }

        public static bool Is(UniqAbilTypes uniqAbil) => UniqAbil == uniqAbil;

        public SelUniqAbilC(UniqAbilTypes uniqAbil)
        {
            UniqAbil = uniqAbil;
        }
    }
}