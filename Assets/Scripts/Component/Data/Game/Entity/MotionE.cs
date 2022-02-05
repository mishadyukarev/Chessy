using ECS;

namespace Game.Game
{
    public sealed class MotionE : EntityAbstract
    {
        ref AmountC AmountMotionsCRef => ref Ent.Get<AmountC>();

        public ref IsActiveC IsActiveC => ref Ent.Get<IsActiveC>();
        public AmountC AmountMotionsC => Ent.Get<AmountC>();

        public MotionE(in EcsWorld gameW) : base(gameW)
        {
        }

        public void AddEveryUpdateMove()
        {
            AmountMotionsCRef.Amount++;
        }
    }
}