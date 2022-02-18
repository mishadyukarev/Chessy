using ECS;

namespace Game.Game
{
    public abstract class CellEntityAbstract : EntityAbstract
    {
        protected readonly CellPoolEs CellEs;

        protected CellEntityAbstract(in CellPoolEs cellEs, in EcsWorld gameW) : base(gameW)
        {
            CellEs = cellEs;
        }
    }
}