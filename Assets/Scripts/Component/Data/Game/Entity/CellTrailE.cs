using ECS;

namespace Game.Game
{
    public sealed class CellTrailE : EntityAbstract
    {
        readonly DirectTypes _direct;
        ref AmountC HealthRef => ref Ent.Get<AmountC>();
        public AmountC Health => Ent.Get<AmountC>();

        public bool HaveTrail => Health.Amount > 0;

        internal CellTrailE(in DirectTypes dir, in EcsWorld gameW) : base(gameW)
        {
            _direct = dir;
        }

        public void SetNew()
        {
            HealthRef.Amount = 7;
        }
        public void Destroy()
        {
            HealthRef.Amount = 0;
        }
        public void TakeEveryUpdate()
        {
            HealthRef.Amount--;
        }
        public void Sync(in int health)
        {
            HealthRef.Amount = health;
        }
    }
}