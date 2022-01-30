using ECS;

namespace Game.Game
{
    public readonly struct CellUnitStatEs
    {
        readonly CellUnitHpE[] _hps;
        readonly CellUnitStepE[] _steps;
        readonly CellUnitWaterE[] _waters;

        public CellUnitHpE Hp(in byte idx) => _hps[idx];
        public CellUnitStepE Step(in byte idx) => _steps[idx];
        public CellUnitWaterE Water(in byte idx) => _waters[idx];

        public CellUnitStatEs(in EcsWorld gameW)
        {
            _hps = new CellUnitHpE[CellStartValues.ALL_CELLS_AMOUNT];
            _steps = new CellUnitStepE[CellStartValues.ALL_CELLS_AMOUNT];
            _waters = new CellUnitWaterE[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < _waters.Length; idx++)
            {
                _hps[idx] = new CellUnitHpE(gameW);
                _waters[idx] = new CellUnitWaterE(gameW);
                _steps[idx] = new CellUnitStepE(gameW);
            }
        }
    }
}