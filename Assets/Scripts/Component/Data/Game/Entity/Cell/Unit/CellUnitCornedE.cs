using ECS;

namespace Game.Game
{
    public sealed class CellUnitCornedE : CellEntityAbstract
    {
        ref IsC IsCornedRef => ref Ent.Get<IsC>();

        public bool IsRight
        {
            get => IsCornedRef.Is;
            set => IsCornedRef.Is = value;
        }

        internal CellUnitCornedE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
        }
    }
}