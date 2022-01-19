using System;

namespace Game.Game
{
    public static class EnvironmentValues
    {
        #region StartGame

        public static byte StartPercentForSpawn(in EnvironmentTypes env)
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

        #endregion


        public static int MaxResources(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 100;
                case EnvironmentTypes.YoungForest: return 1;
                case EnvironmentTypes.AdultForest: return 100;
                case EnvironmentTypes.Hill: return 100;
                case EnvironmentTypes.Mountain: return 1;
                default: throw new Exception();
            }
        }

        public static int MinResources(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 50;
                case EnvironmentTypes.YoungForest: return 1;
                case EnvironmentTypes.AdultForest: return 50;
                case EnvironmentTypes.Hill: return 50;
                case EnvironmentTypes.Mountain: return 1;
                default: throw new Exception();
            }
        }

        public static int RandomResources(in EnvironmentTypes env) => UnityEngine.Random.Range(MinResources(env), MaxResources(env) + 1);
    }
}
