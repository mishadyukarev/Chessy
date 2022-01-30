using ECS;

namespace Game.Game
{
    public sealed class CellTrailE : EntityAbstract
    {
        public DirectTC DirectTC => Ent.Get<DirectTC>();
        public ref AmountC Health => ref Ent.Get<AmountC>();

        public bool HaveTrail => Health.Have;

        public CellTrailE(in DirectTypes dir, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new DirectTC(dir));
        }

        public void SetNew()
        {
            Health.Amount = 7;
        }

        public void Destroy()
        {
            Health.Reset();
        }
    }
}