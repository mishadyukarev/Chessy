using ECS;

namespace Game.Game
{
    public sealed class CurrentIdxE : EntityAbstract
    {
        public ref IdxC IdxC => ref Ent.Get<IdxC>();

        public bool IsStartDirectToCell => IdxC.Idx == default;

        public CurrentIdxE(in EcsWorld gameW) : base(gameW) { }
    }
}