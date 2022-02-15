using ECS;

namespace Game.Game
{
    public abstract class CellEntityAbstract : EntityAbstract
    {
        protected readonly CellEs CellEs;

        protected CellEntityAbstract(in CellEs cellEs, in EcsWorld gameW) : base(gameW)
        {
            CellEs = cellEs;
        }
    }
}