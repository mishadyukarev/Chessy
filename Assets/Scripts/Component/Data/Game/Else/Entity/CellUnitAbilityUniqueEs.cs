using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellUnitAbilityUniqueEs
    {
        static Dictionary<UniqueAbilityTypes, Entity[]> _abils;

        public static ref C Cooldown<C>(in UniqueAbilityTypes uniq, in byte idx) where C : struct
        {
            if (!_abils.ContainsKey(uniq)) throw new Exception();
            return ref _abils[uniq][idx].Get<C>();
        }

        public static HashSet<UniqueAbilityTypes> Keys
        {
            get
            {
                var hash = new HashSet<UniqueAbilityTypes>();
                foreach (var item in _abils) hash.Add(item.Key);
                return hash;
            }
        }


        public CellUnitAbilityUniqueEs(in EcsWorld gameW)
        {
            _abils = new Dictionary<UniqueAbilityTypes, Entity[]>();
            for (var uniqAbil = UniqueAbilityTypes.First; uniqAbil < UniqueAbilityTypes.End; uniqAbil++)
            {
                _abils.Add(uniqAbil, new Entity[CellValues.ALL_CELLS_AMOUNT]);

                for (var idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _abils[uniqAbil][idx] = gameW.NewEntity()
                        .Add(new CooldownC());
                }
            }
        }
    }
}