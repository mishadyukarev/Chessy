using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct UniqAbilC
    {
        private Dictionary<UniqButtonTypes, UniqAbilTypes> _abilities;

        public UniqAbilTypes Ability(UniqButtonTypes uniqBut) => _abilities[uniqBut];


        public UniqAbilC(bool needNew)
        {
            if (needNew)
            {
                _abilities = new Dictionary<UniqButtonTypes, UniqAbilTypes>();

                for (var uniqBut = (UniqButtonTypes)1; uniqBut < (UniqButtonTypes)typeof(UniqButtonTypes).GetEnumNames().Length; uniqBut++)
                {
                    _abilities.Add(uniqBut, default);

                }
            }
            else throw new Exception();
        }


        public void SetAbility(UniqButtonTypes uniqBut, UniqAbilTypes uniqAbil) => _abilities[uniqBut] = uniqAbil;
        public void Reset(UniqButtonTypes uniqBut)
        {
            _abilities[uniqBut] = default;
        }

        public void Replace(UniqAbilC uniqAbilC)
        {
            foreach (var item in uniqAbilC._abilities) _abilities[item.Key] = item.Value;
        }
    }
}