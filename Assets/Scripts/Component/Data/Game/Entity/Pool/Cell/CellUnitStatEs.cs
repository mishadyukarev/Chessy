using ECS;

namespace Game.Game
{
    public readonly struct CellUnitStatEs
    {
        public readonly CellUnitStatHpE Hp;
        public readonly CellUnitStatStepE StepE;
        public readonly CellUnitStatWaterE WaterE;

        public CellUnitStatEs(in byte idx, in EcsWorld gameW)
        {
            Hp = new CellUnitStatHpE(idx, gameW);
            WaterE = new CellUnitStatWaterE(gameW);
            StepE = new CellUnitStatStepE(idx, gameW);
        }
    }
}