using ECS;

namespace Game.Game
{
    public readonly struct CellEnvironmentEs
    {
        readonly CellEnvFertilizerE[] _fertilizers;
        readonly CellEnvYoungForestE[] _youngForests;
        readonly CellEnvAdultForestE[] _adultForests;
        readonly CellEnvHillE[] _hills;
        readonly CellEnvMountainE[] _mountains;

        public CellEnvFertilizerE Fertilizer(in byte idx) => _fertilizers[idx];
        public CellEnvYoungForestE YoungForest(in byte idx) => _youngForests[idx];
        public CellEnvAdultForestE AdultForest(in byte idx) => _adultForests[idx];
        public CellEnvHillE Hill(in byte idx) => _hills[idx];
        public CellEnvMountainE Mountain(in byte idx) => _mountains[idx];




        public CellEnvironmentEs(in EcsWorld gameW)
        {
            var cells = CellStartValues.ALL_CELLS_AMOUNT;

            _fertilizers = new CellEnvFertilizerE[cells];
            _youngForests = new CellEnvYoungForestE[cells];
            _adultForests = new CellEnvAdultForestE[cells];
            _hills = new CellEnvHillE[cells];
            _mountains = new CellEnvMountainE[cells];

            for (byte idx = 0; idx < cells; idx++)
            {
                _fertilizers[idx] = new CellEnvFertilizerE(idx, gameW);
                _youngForests[idx] = new CellEnvYoungForestE(idx, gameW);
                _adultForests[idx] = new CellEnvAdultForestE(idx, gameW);
                _hills[idx] = new CellEnvHillE(idx, gameW);
                _mountains[idx] = new CellEnvMountainE(idx, gameW);
            }
        }
    }
}