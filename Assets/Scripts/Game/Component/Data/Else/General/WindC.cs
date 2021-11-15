namespace Chessy.Game
{
    public struct WindC
    {
        private static DirectTypes _dirWind;

        public static DirectTypes Direct => _dirWind;

        public WindC(DirectTypes dir) => _dirWind = dir;


        public static void Set(DirectTypes dir)
        {
            _dirWind = dir;
        }

        public static void Sync(DirectTypes dir)
        {
            _dirWind = dir;
        }
    }
}