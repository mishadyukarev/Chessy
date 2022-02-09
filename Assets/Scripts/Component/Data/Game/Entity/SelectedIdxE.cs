using ECS;

namespace Game.Game
{
    public sealed class SelectedIdxE : EntityAbstract
    {
        public ref IdxC IdxC => ref Ent.Get<IdxC>();

        public byte Idx
        {
            get => IdxC.Idx;
            set => IdxC.Idx = value;
        }
        public bool IsSelCell => IdxC.Idx != 0;

        public SelectedIdxE(in EcsWorld gameW) : base(gameW) { }

        public void Reset() => IdxC.Idx = 0;
    }
}