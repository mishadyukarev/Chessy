using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CooldownUniqC : IUnitCell
    {
        private Dictionary<UniqueAbilTypes, int> _cooldowns;

        public Dictionary<UniqueAbilTypes, int> Cooldowns
        {
            get
            {
                var dict = new Dictionary<UniqueAbilTypes, int>();
                foreach (var item in _cooldowns) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public bool HaveCooldown(UniqueAbilTypes uniqAbil) => _cooldowns[uniqAbil] > 0;
        public int Cooldown(UniqueAbilTypes uniqAbil) => _cooldowns[uniqAbil];


        public CooldownUniqC(bool needNew)
        {
            if (needNew)
            {
                _cooldowns = new Dictionary<UniqueAbilTypes, int>();

                for (var i = (UniqueAbilTypes)1; i < (UniqueAbilTypes)typeof(UniqueAbilTypes).GetEnumNames().Length; i++)
                {
                    _cooldowns.Add(i, default);
                }

            }

            else throw new Exception();
        }


        public void SetCooldown(UniqueAbilTypes uniqAbil, int cooldown) => _cooldowns[uniqAbil] = cooldown;
        public void TakeCooldown(UniqueAbilTypes uniqAbil) => _cooldowns[uniqAbil] -= 1;

        public void Replace(CooldownUniqC cdownUniqC)
        {
            foreach (var item in cdownUniqC._cooldowns) _cooldowns[item.Key] = item.Value;
        }

        public void Sync(UniqueAbilTypes uniqAbil, int cooldown)
        {
            _cooldowns[uniqAbil] = cooldown;
        }
    }
}