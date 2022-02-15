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

        public CellEnvironmentEs(in CellEs cellEs, in EcsWorld gameW)
        {
            Fertilizer = new CellEnvFertilizerE(cellEs, gameW);
            YoungForest = new CellEnvYoungForestE(cellEs, gameW);
            AdultForest = new CellEnvAdultForestE(cellEs, gameW);
            Hill = new CellEnvHillE(cellEs, gameW);
            Mountain = new CellEnvMountainE(cellEs, gameW);
        }
    }
}