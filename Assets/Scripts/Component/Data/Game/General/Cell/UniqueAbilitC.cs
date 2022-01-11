//using System;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public struct UniqueAbilitC : IUnitCellE
//    {
//        Dictionary<UniqueButtonTypes, UniqueAbilityTypes> _abilities;

//        public UniqueAbilityTypes Ability(UniqueButtonTypes uniqBut) => _abilities[uniqBut];


//        public UniqueAbilitC(bool needNew)
//        {
//            if (needNew)
//            {
//                _abilities = new Dictionary<UniqueButtonTypes, UniqueAbilityTypes>();

//                for (var uniqBut = UniqueButtonTypes.First; uniqBut < UniqueButtonTypes.End; uniqBut++)
//                {
//                    _abilities.Add(uniqBut, default);

//                }
//            }
//            else throw new Exception();
//        }


//        public void SetAbility(UniqueButtonTypes uniqBut, UniqueAbilityTypes uniqAbil) => _abilities[uniqBut] = uniqAbil;
//        public void Reset(UniqueButtonTypes uniqBut)
//        {
//            _abilities[uniqBut] = default;
//        }

//        public void Replace(UniqueAbilitC uniqAbilC)
//        {
//            foreach (var item in uniqAbilC._abilities) _abilities[item.Key] = item.Value;
//        }
//    }
//}