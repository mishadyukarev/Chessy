using ECS;

namespace Game.Game
{
    public sealed class CellTrailE : EntityAbstract
    {
        public ref AmountC Health => ref Ent.Get<AmountC>();

        public CellTrailE(in EcsWorld gameW) : base(gameW)
        {
        }
    }
}