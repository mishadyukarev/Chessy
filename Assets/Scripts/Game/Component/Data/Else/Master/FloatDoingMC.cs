namespace Game.Game
{
    public struct FloatDoingMC
    {
        private static float _float;

        public static void Set(float f)
        {
            _float = f;
        }
        public static void Get(out float f)
        {
            f = _float;
        }
    }
}