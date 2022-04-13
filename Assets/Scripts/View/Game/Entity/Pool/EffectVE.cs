using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.Entity.View.Cell.Unit.Effect
{
    public readonly struct EffectVE
    {
        readonly Dictionary<bool, SpriteRendererVC> _frozenArrawSRs;

        public readonly SpriteRendererVC ShieldSRC;
        public readonly SpriteRendererVC StunSRC;

        public SpriteRendererVC FrozenArraw(in bool isRight) => _frozenArrawSRs[isRight];

        internal EffectVE(in Transform cellUnitT)
        {
            var unitEffectT = cellUnitT.transform.Find("Effect");

            ShieldSRC = new SpriteRendererVC(unitEffectT.Find("ShieldEffect_SR").GetComponent<SpriteRenderer>());
            StunSRC = new SpriteRendererVC(unitEffectT.Find("Stun_SR").GetComponent<SpriteRenderer>());

            var parent = unitEffectT.Find("FrozenArrow+");


            _frozenArrawSRs = new Dictionary<bool, SpriteRendererVC>();

            _frozenArrawSRs.Add(false, new SpriteRendererVC(parent.Find("Cornered_SR+").GetComponent<SpriteRenderer>()));
            _frozenArrawSRs.Add(true, new SpriteRendererVC(parent.Find("Right_SR+").GetComponent<SpriteRenderer>()));
        }
    }
}