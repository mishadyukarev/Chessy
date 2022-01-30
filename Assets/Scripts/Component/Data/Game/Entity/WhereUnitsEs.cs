using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct WhereUnitsEs
    {
        readonly Dictionary<string, HaveUnitOnCellE> _units;

        string Key(in (UnitTypes, LevelTypes, PlayerTypes) unit, in byte idx) => unit.Item1.ToString() + unit.Item2 + unit.Item3 + idx;

        public HaveUnitOnCellE WhereUnit(in (UnitTypes, LevelTypes, PlayerTypes) unit, in byte idx) => _units[Key(unit, idx)];
        public HaveUnitOnCellE WhereUnit(in UnitTypes unit, in LevelTypes level, in PlayerTypes player, in byte idx) => _units[Key((unit, level, player), idx)];
        public HaveUnitOnCellE WhereUnit(in CellUnitMainE unitMainE, in byte idx) => _units[Key((unitMainE.UnitC.Unit, unitMainE.LevelC.Level, unitMainE.OwnerC.Player), idx)];
        public HaveUnitOnCellE WhereUnit(in string key) => _units[key];

        public HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _units) hash.Add(item.Key);
                return hash;
            }
        }

        public bool HaveUnit(in UnitTypes unit)
        {
            for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
            {
                for (var player = PlayerTypes.None; player < PlayerTypes.End; player++)
                {
                    for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                    {
                        if (WhereUnit(unit, level, player, idx).HaveUnit.Have) return true;
                    }
                }
            }
            return false;
        }
        public byte IdxUnit(in UnitTypes unit, in LevelTypes lev, in PlayerTypes player)
        {
            for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                if (WhereUnit(unit, lev, player, idx).HaveUnit.Have) return idx;
            }
            throw new System.Exception("There's not unit");
        }

        public WhereUnitsEs(in EcsWorld gameW)
        {
            _units = new Dictionary<string, HaveUnitOnCellE>();

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                for (var lev = LevelTypes.First; lev < LevelTypes.End; lev++)
                {
                    for (var player = PlayerTypes.None; player < PlayerTypes.End; player++)
                    {
                        for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                        {
                            _units.Add(Key((unit, lev, player), idx), new HaveUnitOnCellE(gameW));
                        }
                    }
                }
            }
        }
    }
}