using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct WhereUnitsE
    {
        static Dictionary<string, Entity> _units;

        static string Key(in (UnitTypes, LevelTypes, PlayerTypes) unit, in byte idx) => unit.Item1.ToString() + unit.Item2 + unit.Item3 + idx;

        public static ref HaveUnitC HaveUnit(in (UnitTypes, LevelTypes, PlayerTypes) unit, in byte idx) => ref _units[Key(unit, idx)].Get<HaveUnitC>();
        public static ref HaveUnitC HaveUnit(in UnitTypes unit, in LevelTypes level, in PlayerTypes player, in byte idx) => ref _units[Key((unit, level, player), idx)].Get<HaveUnitC>();
        public static ref HaveUnitC HaveUnit(in string key) => ref _units[key].Get<HaveUnitC>();

        public static HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _units) hash.Add(item.Key);
                return hash;
            }
        }

        public static bool HaveUnit(in UnitTypes unit)
        {
            for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
            {
                for (var player = PlayerTypes.None; player < PlayerTypes.End; player++)
                {
                    for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                    {
                        if (HaveUnit(unit, level, player, idx).Have) return true;
                    }
                }
            }
            return false;
        }
        public static byte IdxUnit(in UnitTypes unit, in LevelTypes lev, in PlayerTypes player)
        {
            for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                if (HaveUnit(unit, lev, player, idx).Have) return idx;
            }
            throw new System.Exception("There's not unit");
        }

        public WhereUnitsE(in EcsWorld gameW)
        {
            _units = new Dictionary<string, Entity>();

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                for (var lev = LevelTypes.First; lev < LevelTypes.End; lev++)
                {
                    for (var player = PlayerTypes.None; player < PlayerTypes.End; player++)
                    {
                        for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                        {
                            _units.Add(Key((unit, lev, player), idx), gameW.NewEntity()
                                .Add(new HaveUnitC()));
                        }
                    }
                }
            }
        }
    }
}