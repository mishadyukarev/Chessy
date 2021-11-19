namespace Game.Game
{
    public struct BuildDoingMC
    {
        private static BuildTypes Build;

        public static void Set(BuildTypes build)
        {
            Build = build;
        }
        public static void Get(out BuildTypes build)
        {
            build = Build;
        }
    }
}
