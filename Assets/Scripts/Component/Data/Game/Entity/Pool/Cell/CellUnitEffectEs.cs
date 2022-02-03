using ECS;

namespace Game.Game
{
    public readonly struct CellUnitEffectEs
    {
        public readonly CellUnitEffectStunEs StunE;
        public readonly CellUnitEffectShieldE ShieldE;
        public readonly CellUnitEffectFrozenArrowE FrozenArrowE;

        public CellUnitEffectEs(in byte idx, in EcsWorld gameW)
        {
            StunE = new CellUnitEffectStunEs(idx, gameW);
            ShieldE = new CellUnitEffectShieldE(idx, gameW);
            FrozenArrowE = new CellUnitEffectFrozenArrowE(idx, gameW);
        }
    }
}