using ECS;

namespace Game.Game
{
    public sealed class CellTrailE : EntityAbstract
    {
        public ref DirectTC DirectTC => ref Ent.Get<DirectTC>();
        public ref AmountC Health => ref Ent.Get<AmountC>();

        public CellTrailE(in DirectTypes dir, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new DirectTC(dir));
        }
    }
}