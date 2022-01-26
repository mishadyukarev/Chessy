using ECS;

namespace Game.Game
{
    public sealed class CellEnvironmentE : EntityAbstract
    {
        public ref AmountC Resources => ref Ent.Get<AmountC>();

        public CellEnvironmentE(in EcsWorld world) : base(world)
        {

        }
    }
}