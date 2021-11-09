using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct UniqAbilC
    {
        private Dictionary<UniqButtonTypes, UniqAbilTypes> _abilities;
        private Dictionary<UniqButtonTypes, int> _cooldowns;

        public UniqAbilTypes Ability(UniqButtonTypes uniqBut) => _abilities[uniqBut];


        public UniqAbilC(bool needNew)
        {
            if (needNew)
            {
                _abilities = new Dictionary<UniqButtonTypes, UniqAbilTypes>();
                _cooldowns = new Dictionary<UniqButtonTypes, int>();

                for (var uniqBut = (UniqButtonTypes)1; uniqBut < (UniqButtonTypes)typeof(UniqButtonTypes).GetEnumNames().Length; uniqBut++)
                {
                    _abilities.Add(uniqBut, default);
                    _cooldowns.Add(uniqBut, default);
                }

            }
            else throw new Exception();
        }


        public void SetAbility(UniqButtonTypes uniqBut, UniqAbilTypes uniqAbil) => _abilities[uniqBut] = uniqAbil;
        public void Reset(UniqButtonTypes uniqBut)
        {
            _abilities[uniqBut] = default;
        }
    }
}