using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct UniqAbilC
    {
        private Dictionary<UniqButTypes, UniqAbilTypes> _abilities;

        public UniqAbilTypes Ability(UniqButTypes uniqBut) => _abilities[uniqBut];


        public UniqAbilC(bool needNew)
        {
            if (needNew)
            {
                _abilities = new Dictionary<UniqButTypes, UniqAbilTypes>();

                for (var uniqBut = (UniqButTypes)1; uniqBut < (UniqButTypes)typeof(UniqButTypes).GetEnumNames().Length; uniqBut++)
                {
                    _abilities.Add(uniqBut, default);

                }
            }
            else throw new Exception();
        }


        public void SetAbility(UniqButTypes uniqBut, UniqAbilTypes uniqAbil) => _abilities[uniqBut] = uniqAbil;
        public void Reset(UniqButTypes uniqBut)
        {
            _abilities[uniqBut] = default;
        }

        public void Replace(UniqAbilC uniqAbilC)
        {
            foreach (var item in uniqAbilC._abilities) _abilities[item.Key] = item.Value;
        }
    }
}