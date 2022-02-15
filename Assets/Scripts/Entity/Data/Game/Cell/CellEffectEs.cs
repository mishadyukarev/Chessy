using ECS;

namespace Game.Game
{
    public readonly struct CellEffectEs
    {
        public readonly CellEffectFireE FireE;

        public CellEffectEs(in CellEs cellEs, in EcsWorld gameW)
        {
            FireE = new CellEffectFireE(cellEs, gameW);
        }
    }
}