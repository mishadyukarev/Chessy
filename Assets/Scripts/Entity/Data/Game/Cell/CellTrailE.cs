using ECS;

namespace Game.Game
{
    public sealed class CellTrailE : EntityAbstract
    {
        readonly DirectTypes _direct;
        public HealthC HealthC => Ent.Get<HealthC>();


        internal CellTrailE(in DirectTypes dir, in EcsWorld gameW) : base(gameW)
        {
            _direct = dir;
        }
    }
}