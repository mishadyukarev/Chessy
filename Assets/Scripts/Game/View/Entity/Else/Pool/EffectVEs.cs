using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.Entity.View.Cell.Unit.Effect
{
    public readonly struct EffectVEs
    {
        readonly Dictionary<string, SpriteRendererVC> _frozenArrawSRs;

        public readonly SpriteRendererVC ShieldSRC;
        public readonly SpriteRendererVC StunSRC;

        public SpriteRendererVC FrozenArraw(in bool isSelected, in bool isRight) => _frozenArrawSRs[isSelected.ToString() + isRight];

        internal EffectVEs(in Transform cellUnitT)
        {
            var unitEffectT = cellUnitT.transform.Find("Effect");

            ShieldSRC = new SpriteRendererVC(unitEffectT.Find("ShieldEffect_SR").GetComponent<SpriteRenderer>());
            StunSRC = new SpriteRendererVC(unitEffectT.Find("Stun_SR").GetComponent<SpriteRenderer>());

            var parent = unitEffectT.Find("FrozenArrow+");


            _frozenArrawSRs = new Dictionary<string, SpriteRendererVC>();

            _frozenArrawSRs.Add(true.ToString() + false, new SpriteRendererVC(parent.Find("Selected+").Find("Cornered+").Find("SR+").GetComponent<SpriteRenderer>()));
            _frozenArrawSRs.Add(true.ToString() + true, new SpriteRendererVC(parent.Find("Selected+").Find("Right+").Find("SR+").GetComponent<SpriteRenderer>()));

            _frozenArrawSRs.Add(false.ToString() + false, new SpriteRendererVC(parent.Find("NotSelected+").Find("Cornered+").Find("SR+").GetComponent<SpriteRenderer>()));
            _frozenArrawSRs.Add(false.ToString() + true, new SpriteRendererVC(parent.Find("NotSelected+").Find("Right+").Find("SR+").GetComponent<SpriteRenderer>()));
        }
    }
}