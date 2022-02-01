using ECS;

namespace Game.Game
{
    public readonly struct CellEffectEs
    {
        public readonly CellFireE FireE;

        public CellEffectEs(in EcsWorld gameW)
        {
            FireE = new CellFireE(gameW);
        }
    }
}