using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public struct CooldownUniqC
    {
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
        public bool HaveCooldown(UniqAbilTypes uniqAbil) => _cooldowns[uniqAbil] > 0;
        public int Cooldown(UniqAbilTypes uniqAbil) => _cooldowns[uniqAbil];


        public CooldownUniqC(bool needNew)
        {
            if (needNew)
            {
                _cooldowns = new Dictionary<UniqAbilTypes, int>();

                for (var i = (UniqAbilTypes)1; i < (UniqAbilTypes)typeof(UniqAbilTypes).GetEnumNames().Length; i++)
                {
                    _cooldowns.Add(i, default);
                }

            }

            else throw new Exception();
        }


        public void SetCooldown(UniqAbilTypes uniqAbil, int cooldown) => _cooldowns[uniqAbil] = cooldown;
        public void TakeCooldown(UniqAbilTypes uniqAbil) => _cooldowns[uniqAbil] -= 1;

        public void Replace(CooldownUniqC cdownUniqC)
        {
            foreach (var item in cdownUniqC._cooldowns) _cooldowns[item.Key] = item.Value;
        }

        public void Sync(UniqAbilTypes uniqAbil, int cooldown)
        {
            _cooldowns[uniqAbil] = cooldown;
        }
    }
}