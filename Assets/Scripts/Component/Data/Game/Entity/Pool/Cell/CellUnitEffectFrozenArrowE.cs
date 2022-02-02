using ECS;

namespace Game.Game
{
    public sealed class CellUnitEffectFrozenArrowE : EntityAbstract
    {
        ref IsC IsFrozenArrawRef => ref Ent.Get<IsC>();
        public IsC IsFrozenArraw => Ent.Get<IsC>();

        public bool HaveEffect => IsFrozenArraw.Is;

        internal CellUnitEffectFrozenArrowE(in EcsWorld world) : base(world)
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