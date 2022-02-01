using ECS;

namespace Game.Game
{
    public sealed class CellTrailE : EntityAbstract
    {
        readonly DirectTypes _direct;
        public ref AmountC Health => ref Ent.Get<AmountC>();

        public bool HaveTrail => Health.Amount > 0;

        public CellTrailE(in DirectTypes dir, in EcsWorld gameW) : base(gameW)
        {
            _direct = dir;
        }

        public void SetNew()
        {
            Health.Amount = 7;
        }

        public void Destroy()
        {
            Health.Amount = 0;
        }
    }
}