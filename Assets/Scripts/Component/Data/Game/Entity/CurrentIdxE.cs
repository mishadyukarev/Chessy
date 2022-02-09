using ECS;

namespace Game.Game
{
    public sealed class CurrentIdxE : EntityAbstract
    {
        public ref IdxC IdxC => ref Ent.Get<IdxC>();

        public byte Idx
        {
            get => IdxC.Idx;
            set => IdxC.Idx = value;
        }

        public bool IsStartDirectToCell => IdxC.Idx == default;

        public CurrentIdxE(in EcsWorld gameW) : base(gameW) { }
    }
}