using System.Collections.Generic;
namespace Chessy.Model
{
    public struct EffectsUnitsRightBarsC
    {
        readonly Dictionary<ButtonTypes, EffectTypes> _effectBars;

        public EffectTypes Effect(in ButtonTypes buttonT) => _effectBars[buttonT];

        internal EffectsUnitsRightBarsC(in Dictionary<ButtonTypes, EffectTypes> dict)
        {
            _effectBars = dict;
        }

        internal void Set(in ButtonTypes buttonT, in EffectTypes effectT) => _effectBars[buttonT] = effectT;
    }
}