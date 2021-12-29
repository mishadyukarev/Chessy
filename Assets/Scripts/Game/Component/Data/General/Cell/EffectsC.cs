using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EffectsC : IUnitCell
    {
        Dictionary<UnitStatTypes, bool> _effects;
        public Dictionary<UnitStatTypes, bool> Effects
        {
            get
            {
                var eff = new Dictionary<UnitStatTypes, bool>();
                foreach (var item in _effects) eff.Add(item.Key, item.Value);
                return eff;
            }
        }

        public bool Have(UnitStatTypes statType)
        {
            if (!_effects.ContainsKey(statType)) throw new Exception();

            return _effects[statType];
        }


        internal EffectsC(bool needNew) : this()
        {
            if (needNew)
            {
                _effects = new Dictionary<UnitStatTypes, bool>();
                _effects.Add(UnitStatTypes.Damage, default);
                _effects.Add(UnitStatTypes.Steps, default);
            }
        }

        public void Set(UnitStatTypes statType, bool isActive = true)
        {
            if (_effects.ContainsKey(statType)) _effects[statType] = isActive;
            else throw new Exception();
        }
        public void Set(EffectsC effectsC)
        {
            Set(UnitStatTypes.Damage, effectsC.Have(UnitStatTypes.Damage));
            Set(UnitStatTypes.Steps, effectsC.Have(UnitStatTypes.Steps));
        }
        public void Def(UnitStatTypes statType)
        {
            if (!_effects.ContainsKey(statType)) throw new Exception();
            Set(statType, false);
        }
        public void DefAllEffects()
        {
            Set(UnitStatTypes.Damage, false);
            Set(UnitStatTypes.Steps, false);
        }

        public void Sync(UnitStatTypes statType, bool isActive)
        {
            if (!_effects.ContainsKey(statType)) throw new Exception();
            _effects[statType] = isActive;
        }
    }
}