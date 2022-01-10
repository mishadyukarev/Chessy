using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct UniqAbilC : IUnitCell
    {
        Dictionary<UniqButTypes, UniqueAbilityTypes> _abilities;

        public UniqueAbilityTypes Ability(UniqButTypes uniqBut) => _abilities[uniqBut];


        public UniqAbilC(bool needNew)
        {
            if (needNew)
            {
                _abilities = new Dictionary<UniqButTypes, UniqueAbilityTypes>();

                for (var uniqBut = UniqButTypes.First; uniqBut < UniqButTypes.End; uniqBut++)
                {
                    _abilities.Add(uniqBut, default);

                }
            }
            else throw new Exception();
        }


        public void SetAbility(UniqButTypes uniqBut, UniqueAbilityTypes uniqAbil) => _abilities[uniqBut] = uniqAbil;
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