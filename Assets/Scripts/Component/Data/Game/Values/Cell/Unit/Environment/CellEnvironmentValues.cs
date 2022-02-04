using System;

namespace Game.Game
{
    public static class CellEnvironmentValues
    {
        #region Common

        public const int MIN_RESOURCES = 1;

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
        public static int RandomResources(in EnvironmentTypes env) => UnityEngine.Random.Range(MIN_RESOURCES, MaxResources(env) + 1);
        public static int AddingFromIceWall(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return MaxResources(env) / 8;
                case EnvironmentTypes.AdultForest: return MaxResources(env) / 8;
                default: throw new Exception();
            }
        }

        #endregion


        #region AdultForest


        public static int FireAdultForest => MaxResources(EnvironmentTypes.AdultForest) / 4;
        public static int AddingAfterBuildingFarm => MaxResources(EnvironmentTypes.Fertilizer) / 2;

        #endregion


        #region YoungFores

        public const float PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE = 0.5f;

        #endregion

    }
}
