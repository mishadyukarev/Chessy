using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellUnitEffectFrozenArrawVE : EntityAbstract
    {
        public ref TransformVC Parent => ref Ent.Get<TransformVC>();
        public ref SpriteRendererVC SR => ref Ent.Get<SpriteRendererVC>();

        internal CellUnitEffectFrozenArrawVE(in Transform unitEffectT, in EcsWorld world) : base(world)
        {
            var parent = unitEffectT.Find("FrozenArrow");
            Ent
                .Add(new TransformVC(parent))
                .Add(new SpriteRendererVC(parent.Find("FrozenArrow_SR").GetComponent<SpriteRenderer>()));
        }
    }
}