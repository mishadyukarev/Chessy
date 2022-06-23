using UnityEngine;

namespace Chessy.Model
{
    public readonly struct Resources
    {
        readonly Sprite[] _abilities;
        readonly Sprite[] _effects;

        public Sprite Sprite(in AbilityTypes ability) => _abilities[(byte)ability];
        public Sprite Sprite(in EffectTypes effects) => _effects[(byte)effects];


        public Resources(in bool def)
        {
            _abilities = new Sprite[(byte)AbilityTypes.End];
            _effects = new Sprite[(byte)EffectTypes.End];


            var folder = "UniqueAbilities/";
            for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
            {
                _abilities[(byte)abilityT] = UnityEngine.Resources.Load<Sprite>(folder + abilityT);
            }

            folder = "Effects/";
            for (var effectT = (EffectTypes)1; effectT < EffectTypes.End; effectT++)
            {
                _effects[(byte)effectT] = UnityEngine.Resources.Load<Sprite>(folder + effectT);
            }
        }
    }
}