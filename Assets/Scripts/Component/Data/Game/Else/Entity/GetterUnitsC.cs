using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct GetterUnitsC
    {
        static readonly Dictionary<UnitTypes, Entity> _getter;


        public static ref C GetterUnit<C>(in UnitTypes unit) where C : struct => ref _getter[unit].Get<C>();


        public HashSet<UnitTypes> Keys
        {
            get
            {
                var keys = new HashSet<UnitTypes>();
                foreach (var item in _getter) keys.Add(item.Key);
                return keys;
            }
        }

        static GetterUnitsC()
        {
            _getter = new Dictionary<UnitTypes, Entity>();
            _getter.Add(UnitTypes.Pawn, default);
            _getter.Add(UnitTypes.Archer, default);
        }
        public GetterUnitsC(in EcsWorld gameW)
        {
            foreach (var key in Keys)
            {
                _getter[key] = gameW.NewEntity()
                    .Add(new IsActivatedC())
                    .Add(new TimerC());
            }
        }
    }
}
