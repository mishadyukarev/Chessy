﻿using Chessy.Model.Component;
using Chessy.View.Component;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.View.Entity
{
    public readonly struct EffectVE
    {
        readonly Dictionary<bool, SpriteRendererVC> _frozenArrawSRs;

        public readonly SpriteRendererVC ShieldSRC;
        public readonly SpriteRendererVC StunSRC;
        internal readonly GameObjectVC KingPassiveGOC;

        public SpriteRendererVC FrozenArraw(in bool isRight) => _frozenArrawSRs[isRight];

        internal EffectVE(in Transform cellUnitT)
        {
            var unitEffectT = cellUnitT.transform.Find("Effect");

            ShieldSRC = new SpriteRendererVC(unitEffectT.Find("ShieldEffect_SR").GetComponent<SpriteRenderer>());
            StunSRC = new SpriteRendererVC(unitEffectT.Find("Stun_SR").GetComponent<SpriteRenderer>());

            var parent = unitEffectT.Find("FrozenArrow+");


            KingPassiveGOC = new GameObjectVC(unitEffectT.Find("KingPassive+").gameObject);


            _frozenArrawSRs = new Dictionary<bool, SpriteRendererVC>();

            _frozenArrawSRs.Add(false, new SpriteRendererVC(parent.Find("Cornered_SR+").GetComponent<SpriteRenderer>()));
            _frozenArrawSRs.Add(true, new SpriteRendererVC(parent.Find("Right_SR+").GetComponent<SpriteRenderer>()));
        }
    }
}