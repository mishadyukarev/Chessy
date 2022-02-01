using ECS;

namespace Game.Game
{
    public abstract class CellAbstE : EntityAbstract
    {
        public readonly byte Idx;

        public CellAbstE(in byte idx, in EcsWorld gameW) : base(gameW)
        {
            Idx = idx;
        }
    }
}