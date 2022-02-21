using ECS;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellUnitEffectShieldVE : EntityAbstract
    {
        public ref SpriteRendererVC SR => ref Ent.Get<SpriteRendererVC>();

        internal CellUnitEffectShieldVE(in Transform unitEffectT, in EcsWorld world) : base(world)
        {
            Ent.Add(new SpriteRendererVC(unitEffectT.Find("ShieldEffect_SR").GetComponent<SpriteRenderer>()));
        }
    }
}