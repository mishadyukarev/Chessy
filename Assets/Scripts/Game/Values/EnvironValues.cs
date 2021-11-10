using System;

namespace Chessy.Game
{
    public static class EnvironValues
    {
        public const int START_FERTILIZER_PERCENT = 30;
        public const int START_FOREST_PERCENT = 40;
        public const int START_HILL_PERCENT = 15;
        public const int START_MOUNTAIN_PERCENT = 15;

        public static byte MaxAmount(EnvTypes envType)
        {
            switch (envType)
            {
                case EnvTypes.None: throw new Exception();
                case EnvTypes.Fertilizer: return 10;
                case EnvTypes.YoungForest: return 0;
                case EnvTypes.AdultForest: return 10;
                case EnvTypes.Hill: return 5;
                case EnvTypes.Mountain: return 0;
                default: throw new Exception();
            }
        }
    }
}
