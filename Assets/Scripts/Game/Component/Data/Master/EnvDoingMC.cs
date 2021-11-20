namespace Game.Game
{
    public struct EnvDoingMC
    {
        private static EnvTypes _env;

        public static void Set(EnvTypes env)
        {
            _env = env;
        }

        public static void Get(out EnvTypes env)
        {
            env = _env;
        }
    }
}
