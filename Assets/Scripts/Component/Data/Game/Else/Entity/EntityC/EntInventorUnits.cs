using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EntInventorUnits
    {
        static Dictionary<string, Entity> _units;
        static EconomyValues _values;

        static string Key(UnitTypes unit, LevelTypes level, PlayerTypes player) => unit.ToString() + level + player;

        public static ref C Units<C>(in UnitTypes unit, in LevelTypes level, in PlayerTypes player) where C : struct => ref _units[Key(unit, level, player)].Get<C>();
        public static ref C Units<C>(in string key) where C : struct => ref _units[key].Get<C>();

        public static HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _units) hash.Add(item.Key);
                return hash;
            }
        }


        static EntInventorUnits()
        {
            _units = new Dictionary<string, Entity>();
            for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
            {
                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        _units.Add(Key(unit, level, player), default);
                    }
                }
            }

            _values = new EconomyValues();
        }
        public EntInventorUnits(in EcsWorld gameW)
        {
            foreach (var key in Keys)
            {
                _units[key] = gameW.NewEntity()
                    .Add(new AmountC());
            }

            Units<AmountC>(UnitTypes.Pawn, LevelTypes.First, PlayerTypes.First).Amount += 1;
            Units<AmountC>(UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second).Amount += 1;
        }
    }
}
