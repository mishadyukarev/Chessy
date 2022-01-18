﻿using System;

namespace Game.Game
{
    public struct EnvironmentValues
    {
        public byte MaxAmount(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 100;
                case EnvironmentTypes.YoungForest: return 0;
                case EnvironmentTypes.AdultForest: return 100;
                case EnvironmentTypes.Hill: return 100;
                case EnvironmentTypes.Mountain: return 0;
                default: throw new Exception();
            }
        }

        public byte StartPercentForSpawn(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 30;
                case EnvironmentTypes.YoungForest: return 0;
                case EnvironmentTypes.AdultForest: return 50;
                case EnvironmentTypes.Hill: return 15;
                case EnvironmentTypes.Mountain: return 15;
                default: throw new Exception();
            }
        }
    }
}
