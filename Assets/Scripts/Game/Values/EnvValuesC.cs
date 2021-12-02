using System;

namespace Game.Game
{
    public struct EnvValuesC
    {
        public static byte MaxAmount(EnvTypes env)
        {
            switch (env)
            {
                case EnvTypes.None: throw new Exception();
                case EnvTypes.Fertilizer: return 100;
                case EnvTypes.YoungForest: return 0;
                case EnvTypes.AdultForest: return 100;
                case EnvTypes.Hill: return 100;
                case EnvTypes.Mountain: return 0;
                default: throw new Exception();
            }
        }

        public static byte StartPercent(EnvTypes env)
        {
            switch (env)
            {
                case EnvTypes.None: throw new Exception();
                case EnvTypes.Fertilizer: return 30;
                case EnvTypes.YoungForest: return 0;
                case EnvTypes.AdultForest: return 50;
                case EnvTypes.Hill: return 15;
                case EnvTypes.Mountain: return 15;
                default: throw new Exception();
            }
        }
    }
}
