//using System;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public struct CooldownUniqC : IUnitCellE
//    {
//        private Dictionary<UniqueAbilityTypes, int> _cooldowns;

//        public Dictionary<UniqueAbilityTypes, int> Cooldowns
//        {
//            get
//            {
//                var dict = new Dictionary<UniqueAbilityTypes, int>();
//                foreach (var item in _cooldowns) dict.Add(item.Key, item.Value);
//                return dict;
//            }
//        }
//        public bool HaveCooldown(UniqueAbilityTypes uniqAbil) => _cooldowns[uniqAbil] > 0;
//        public int Cooldown(UniqueAbilityTypes uniqAbil) => _cooldowns[uniqAbil];


//        public CooldownUniqC(bool needNew)
//        {
//            if (needNew)
//            {
//                _cooldowns = new Dictionary<UniqueAbilityTypes, int>();

//                for (var i = (UniqueAbilityTypes)1; i < (UniqueAbilityTypes)typeof(UniqueAbilityTypes).GetEnumNames().Length; i++)
//                {
//                    _cooldowns.Add(i, default);
//                }

//            }

//            else throw new Exception();
//        }


//        public void SetCooldown(UniqueAbilityTypes uniqAbil, int cooldown) => _cooldowns[uniqAbil] = cooldown;
//        public void TakeCooldown(UniqueAbilityTypes uniqAbil) => _cooldowns[uniqAbil] -= 1;

//        public void Replace(CooldownUniqC cdownUniqC)
//        {
//            foreach (var item in cdownUniqC._cooldowns) _cooldowns[item.Key] = item.Value;
//        }

//        public void Sync(UniqueAbilityTypes uniqAbil, int cooldown)
//        {
//            _cooldowns[uniqAbil] = cooldown;
//        }
//    }
//}