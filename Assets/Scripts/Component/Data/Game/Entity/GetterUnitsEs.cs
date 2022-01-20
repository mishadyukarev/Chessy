using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct GetterUnitsEs
    {
        static Dictionary<UnitTypes, Entity> _getter;


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

        public GetterUnitsEs(in EcsWorld gameW)
        {
            _getter = new Dictionary<UnitTypes, Entity>();

            _getter.Add(UnitTypes.Pawn, gameW.NewEntity()
                    .Add(new IsActiveC())
                    .Add(new TimerC()));

            _getter.Add(UnitTypes.Archer, gameW.NewEntity()
                    .Add(new IsActiveC())
                    .Add(new TimerC()));
        }
    }
}
