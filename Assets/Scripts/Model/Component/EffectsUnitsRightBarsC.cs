using System.Collections.Generic;
namespace Chessy.Model
{
    public struct EffectsUnitsRightBarsC
    {
        readonly EffectTypes[] _effectBars;

        public EffectTypes Effect(in ButtonTypes buttonT) => _effectBars[(byte)buttonT];

        internal EffectsUnitsRightBarsC(in bool def)
        {
            _effectBars = new EffectTypes[(byte)ButtonTypes.End];
        }

        internal void Set(in ButtonTypes buttonT, in EffectTypes effectT) => _effectBars[(byte)buttonT] = effectT;
    }
}