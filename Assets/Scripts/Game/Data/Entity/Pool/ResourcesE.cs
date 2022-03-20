using ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct ResourcesE
    {
        readonly Dictionary<AbilityTypes, SpriteVC> _abilities;
        readonly Dictionary<EffectTypes, SpriteVC> _effects;

        public SpriteVC Sprite(in AbilityTypes ability) => _abilities[ability];
        public SpriteVC Sprite(in EffectTypes effects) => _effects[effects];


        public ResourcesE(in bool def)
        {
            _abilities = new Dictionary<AbilityTypes, SpriteVC>();
            _effects = new Dictionary<EffectTypes, SpriteVC>();


            var folder = "UniqueAbilities/";
            for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            {
                _abilities.Add(abilityT, new SpriteVC(Resources.Load<Sprite>(folder + abilityT)));
            }

            folder = "Effects/";
            for (var effectT = EffectTypes.None + 1; effectT < EffectTypes.End; effectT++)
            {
                _effects.Add(effectT, new SpriteVC(Resources.Load<Sprite>(folder + effectT)));
            }
        }
    }
}