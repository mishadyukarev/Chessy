using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct InventorUnitsE
    {
        static Dictionary<string, Entity> _units;

        static string Key(UnitTypes unit, LevelTypes level, PlayerTypes player) => unit.ToString() + level + player;

        public static ref AmountC Units(in UnitTypes unit, in LevelTypes level, in PlayerTypes player) => ref _units[Key(unit, level, player)].Get<AmountC>();
        public static ref AmountC Units(in string key) => ref _units[key].Get<AmountC>();

        public static HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _units) hash.Add(item.Key);
                return hash;
            }
        }
        public static bool HaveHero(in PlayerTypes owner, out UnitTypes hero)
        {
            for (var unit = UnitTypes.Elfemale; unit <= UnitTypes.Snowy; unit++)
            {
                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    if (Units(Key(unit, level, owner)).Have)
                    {
                        hero = unit;
                        return true;
                    }
                }
            }

            hero = default;
            return false;
        }

        public InventorUnitsE(in EcsWorld gameW)
        {
            _units = new Dictionary<string, Entity>();

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.Camel; unit++)
            {
                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        _units.Add(Key(unit, level, player), gameW.NewEntity()
                            .Add(new AmountC(EconomyValues.StartAmountUnits(unit, level))));
                    }
                }
            }
        }
    }
}
