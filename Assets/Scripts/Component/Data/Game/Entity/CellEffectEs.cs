using ECS;

namespace Game.Game
{
    public readonly struct CellEffectEs
    {
        public readonly CellEffectFireE FireE;

        public CellEffectEs(in byte idx, in EcsWorld gameW)
        {
            FireE = new CellEffectFireE(idx, gameW);
        }
    }
}