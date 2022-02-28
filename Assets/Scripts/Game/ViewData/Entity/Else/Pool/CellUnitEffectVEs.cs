using UnityEngine;

namespace Chessy.Game
{
    public readonly struct CellUnitEffectVEs
    {
        public readonly SpriteRendererVC ShieldVE;
        public readonly SpriteRendererVC StunVE;

        public readonly TransformVC Parent;
        public readonly SpriteRendererVC FrozenArrawVE;

        internal CellUnitEffectVEs(in Transform cellUnitT)
        {
            var unitEffectT = cellUnitT.transform.Find("Effect");

            ShieldVE = new SpriteRendererVC(unitEffectT.Find("ShieldEffect_SR").GetComponent<SpriteRenderer>());
            StunVE = new SpriteRendererVC(unitEffectT.Find("Stun_SR").GetComponent<SpriteRenderer>());

            var parent = unitEffectT.Find("FrozenArrow");
            Parent = new TransformVC(parent);
            FrozenArrawVE = new SpriteRendererVC(parent.Find("FrozenArrow_SR").GetComponent<SpriteRenderer>());
        }
    }
}