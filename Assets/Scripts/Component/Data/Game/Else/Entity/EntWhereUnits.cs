using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntWhereUnits
    {
        static readonly Dictionary<string, Entity> _units;


        static string Key(in (UnitTypes, LevelTypes, PlayerTypes) unit, in byte idx) => unit.Item1.ToString() + unit.Item2 + unit.Item3 + idx;


        public static ref C HaveUnit<C>(in (UnitTypes, LevelTypes, PlayerTypes) unit, in byte idx) where C : struct => ref _units[Key(unit, idx)].Get<C>();
        public static ref C HaveUnit<C>(in UnitTypes unit, in LevelTypes level, in PlayerTypes player, in byte idx) where C : struct => ref _units[Key((unit, level, player), idx)].Get<C>();
        public static ref C HaveUnit<C>(in string key) where C : struct => ref _units[key].Get<C>();

        public static HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _units) hash.Add(item.Key);
                return hash;
            }
        }

        static EntWhereUnits()
        {
            _units = new Dictionary<string, Entity>();

            for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
            {
                for (var lev = LevelTypes.First; lev < LevelTypes.End; lev++)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                        {
                            _units.Add(Key((unit, lev, player), idx), default);
                        }
                    }
                }
            }
        }
        public EntWhereUnits(in EcsWorld gameW)
        {
            foreach (var key in Keys)
            {
                _units[key] = gameW.NewEntity()
                    .Add(new HaveUnitC());
            }
        }
    }
}