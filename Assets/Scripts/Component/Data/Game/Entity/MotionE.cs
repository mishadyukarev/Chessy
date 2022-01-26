using ECS;

namespace Game.Game
{
    public sealed class MotionE : EntityAbstract
    {
        public ref IsActiveC IsActiveC => ref Ent.Get<IsActiveC>();
        public ref AmountC AmountMotions => ref Ent.Get<AmountC>();

        public MotionE(in EcsWorld gameW) : base(gameW)
        {
        }
    }
}