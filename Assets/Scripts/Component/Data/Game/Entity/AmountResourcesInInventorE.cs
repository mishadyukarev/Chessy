using ECS;

namespace Game.Game
{
    public sealed class AmountResourcesInInventorE : EntityAbstract
    {
        public ref AmountC Resources => ref Ent.Get<AmountC>();

        public AmountResourcesInInventorE(in int resources, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new AmountC(resources));
        }
    }
}