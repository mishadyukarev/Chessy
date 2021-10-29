using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct UnitEffectsC
    {
        private Dictionary<StatTypes, bool> _effects;
        public void Set(StatTypes statType, bool isActive = true)
        {
            if (_effects.ContainsKey(statType)) _effects[statType] = isActive;
            else throw new Exception();
        }
        public void Set(UnitEffectsC effectsC)
        {
            Set(StatTypes.Health, effectsC.Have(StatTypes.Health));
            Set(StatTypes.Damage, effectsC.Have(StatTypes.Damage));
            Set(StatTypes.Steps, effectsC.Have(StatTypes.Steps));
        }
        public void Def(StatTypes statType) => Set(statType, false);
        public bool Have(StatTypes statType)
        {
            if (_effects.ContainsKey(statType)) return _effects[statType];
            else throw new Exception();
        }

        public UnitEffectsC(bool needNew) : this()
        {
            if (needNew)
            {
                _effects = new Dictionary<StatTypes, bool>();
                _effects.Add(StatTypes.Health, default);
                _effects.Add(StatTypes.Damage, default);
                _effects.Add(StatTypes.Steps, default);
            }
        }
    }
}