using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct UniqAbilC
    {
        private Dictionary<UniqButtonTypes, UniqAbilTypes> _abilities;
        private Dictionary<UniqAbilTypes, int> _cooldowns;

        public Dictionary<UniqAbilTypes, int> Cooldowns
        {
            get
            {
                var dict = new Dictionary<UniqAbilTypes, int>();
                foreach (var item in _cooldowns) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public UniqAbilTypes Ability(UniqButtonTypes uniqBut) => _abilities[uniqBut];
        public bool HaveCooldown(UniqAbilTypes uniqAbil) => _cooldowns[uniqAbil] > 0;
        public int Cooldown(UniqAbilTypes uniqAbil) => _cooldowns[uniqAbil];

        public UniqAbilC(bool needNew)
        {
            if (needNew)
            {
                _abilities = new Dictionary<UniqButtonTypes, UniqAbilTypes>();
                _cooldowns = new Dictionary<UniqAbilTypes, int>();

                for (var uniqBut = (UniqButtonTypes)1; uniqBut < (UniqButtonTypes)typeof(UniqButtonTypes).GetEnumNames().Length; uniqBut++)
                {
                    _abilities.Add(uniqBut, default);

                }

                for (var i = (UniqAbilTypes)1; i < (UniqAbilTypes)typeof(UniqAbilTypes).GetEnumNames().Length; i++)
                {
                    _cooldowns.Add(i, default);
                }

            }
            else throw new Exception();
        }


        public void SetAbility(UniqButtonTypes uniqBut, UniqAbilTypes uniqAbil) => _abilities[uniqBut] = uniqAbil;
        public void Reset(UniqButtonTypes uniqBut)
        {
            _abilities[uniqBut] = default;
        }
        public void SetCooldown(UniqAbilTypes uniqAbil, int cooldown) => _cooldowns[uniqAbil] = cooldown;
        public void TakeCooldown(UniqAbilTypes uniqAbil) => _cooldowns[uniqAbil] -= 1;

        public void Replace(UniqAbilC uniqAbilC)
        {
            foreach (var item in uniqAbilC._abilities) _abilities[item.Key] = item.Value;
            foreach (var item in uniqAbilC._cooldowns) _cooldowns[item.Key] = item.Value;
        }
    }
}