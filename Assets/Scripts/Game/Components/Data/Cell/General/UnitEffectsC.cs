﻿using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct UnitEffectsC
    {
        private Dictionary<UnitStatTypes, bool> _effects;
        public void Set(UnitStatTypes statType, bool isActive = true)
        {
            if (_effects.ContainsKey(statType)) _effects[statType] = isActive;
            else throw new Exception();
        }
        public void Set(UnitEffectsC effectsC)
        {
            Set(UnitStatTypes.Hp, effectsC.Have(UnitStatTypes.Hp));
            Set(UnitStatTypes.Damage, effectsC.Have(UnitStatTypes.Damage));
            Set(UnitStatTypes.Steps, effectsC.Have(UnitStatTypes.Steps));
        }
        public void Def(UnitStatTypes statType) => Set(statType, false);
        public void DefAllEffects()
        {
            Set(UnitStatTypes.Hp, false);
            Set(UnitStatTypes.Damage, false);
            Set(UnitStatTypes.Steps, false);
        }
        public bool Have(UnitStatTypes statType)
        {
            if (_effects.ContainsKey(statType)) return _effects[statType];
            else throw new Exception();
        }

        public UnitEffectsC(bool needNew) : this()
        {
            if (needNew)
            {
                _effects = new Dictionary<UnitStatTypes, bool>();
                _effects.Add(UnitStatTypes.Hp, default);
                _effects.Add(UnitStatTypes.Damage, default);
                _effects.Add(UnitStatTypes.Steps, default);
            }
        }
    }
}