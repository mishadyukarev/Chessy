using System;

namespace Game.Game
{
    internal struct EnvironmentValues
    {
        internal byte MaxAmount(EnvTypes env)
        {
            switch (env)
            {
                case EnvTypes.Fertilizer: return 100;
                case EnvTypes.YoungForest: return 0;
                case EnvTypes.AdultForest: return 100;
                case EnvTypes.Hill: return 100;
                case EnvTypes.Mountain: return 0;
                default: throw new Exception();
            }
        }

        internal byte StartPercent(EnvTypes env)
        {
            switch (env)
            {
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
