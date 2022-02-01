using ECS;

namespace Game.Game
{
    public readonly struct CellEnvironmentEs
    {
        public readonly CellEnvFertilizerE Fertilizer;
        public readonly CellEnvYoungForestE YoungForest;
        public readonly CellEnvAdultForestE AdultForest;
        public readonly CellEnvHillE Hill;
        public readonly CellEnvMountainE Mountain;

        public CellEnvironmentEs(in byte idx, in EcsWorld gameW)
        {
            Fertilizer = new CellEnvFertilizerE(idx, gameW);
            YoungForest = new CellEnvYoungForestE(idx, gameW);
            AdultForest = new CellEnvAdultForestE(idx, gameW);
            Hill = new CellEnvHillE(idx, gameW);
            Mountain = new CellEnvMountainE(idx, gameW);
        }
    }
}