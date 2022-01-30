using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct InventorUnitsEs
    {
        readonly Dictionary<string, AmountUnitsE> _units;

        string Key(UnitTypes unit, LevelTypes level, PlayerTypes player) => unit.ToString() + level + player;

        public AmountUnitsE Units(in UnitTypes unit, in LevelTypes level, in PlayerTypes player) => _units[Key(unit, level, player)];
        public AmountUnitsE Units(in string key) => _units[key];

        public HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _units) hash.Add(item.Key);
                return hash;
            }
        }
        public bool HaveHero(in PlayerTypes owner, out UnitTypes hero)
        {
            for (var unit = UnitTypes.Elfemale; unit <= UnitTypes.Snowy; unit++)
            {
                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    if (Units(Key(unit, level, owner)).Units.Have)
                    {
                        hero = unit;
                        return true;
                    }
                }
            }

            hero = default;
            return false;
        }

        public InventorUnitsEs(in EcsWorld gameW)
        {
            _units = new Dictionary<string, AmountUnitsE>();

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.Camel; unit++)
            {
                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                    {
                        _units.Add(Key(unit, level, player), new AmountUnitsE(EconomyValues.StartAmountUnits(unit, level), gameW));
                    }
                }
            }
        }
    }
}
