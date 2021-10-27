using System;

namespace Scripts.Game
{
    public static class EnvironValues
    {
        public const int START_FERTILIZER_PERCENT = 30;
        public const int START_FOREST_PERCENT = 40;
        public const int START_HILL_PERCENT = 15;
        public const int START_MOUNTAIN_PERCENT = 15;

        internal static byte MaxAmount(EnvirTypes envType)
        {
            switch (envType)
            {
                case EnvirTypes.None: throw new Exception();
                case EnvirTypes.Fertilizer: return 6;
                case EnvirTypes.YoungForest: return 0;
                case EnvirTypes.AdultForest: return 5;
                case EnvirTypes.Hill: return 5;
                case EnvirTypes.Mountain: return 0;
                default: throw new Exception();
            }
        }
        internal static byte MinAmount(EnvirTypes envType)
        {
            switch (envType)
            {
                case EnvirTypes.None: throw new Exception();
                case EnvirTypes.Fertilizer: return 5;
                case EnvirTypes.YoungForest: return 0;
                case EnvirTypes.AdultForest: return 4;
                case EnvirTypes.Hill: return 2;
                case EnvirTypes.Mountain: return 0;
                default: throw new Exception();
            }
        }
    }
}
