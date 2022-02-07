using ECS;

namespace Game.Game
{
    public sealed class CellUnitEffectFrozenArrowE : CellUnitEffectE
    {
        ref IsC IsFrozenArrawRef => ref Ent.Get<IsC>();

        public bool IsFrozenArraw
        {
            get => IsFrozenArrawRef.Is;
            set => IsFrozenArrawRef.Is = value;
        }

        internal CellUnitEffectFrozenArrowE(in byte idx, in EcsWorld world) : base(EffectTypes.FrozenArraw, idx, world)
        {
        }

        public void Set(in bool haveEffect) => IsFrozenArrawRef.Is = haveEffect;
        public void Enable() => IsFrozenArrawRef.Is = true;
        public void Disable() => IsFrozenArrawRef.Is = false;

        public void Shift(in CellUnitEffectFrozenArrowE frozenArrowE_from)
        {
            IsFrozenArrawRef.Is = frozenArrowE_from.IsFrozenArrawRef.Is;
            frozenArrowE_from.IsFrozenArrawRef.Is = false;
        }
    }
}