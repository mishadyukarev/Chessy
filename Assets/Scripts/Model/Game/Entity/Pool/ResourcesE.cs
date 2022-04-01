using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct ResourcesE
    {
        readonly Dictionary<AbilityTypes, Sprite> _abilities;
        readonly Dictionary<EffectTypes, Sprite> _effects;

        public Sprite Sprite(in AbilityTypes ability) => _abilities[ability];
        public Sprite Sprite(in EffectTypes effects) => _effects[effects];


        public ResourcesE(in bool def)
        {
            _abilities = new Dictionary<AbilityTypes, Sprite>();
            _effects = new Dictionary<EffectTypes, Sprite>();


            var folder = "UniqueAbilities/";
            for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            {
                _abilities.Add(abilityT, Resources.Load<Sprite>(folder + abilityT));
            }

            folder = "Effects/";
            for (var effectT = EffectTypes.None + 1; effectT < EffectTypes.End; effectT++)
            {
                _effects.Add(effectT, Resources.Load<Sprite>(folder + effectT));
            }
        }
    }
}