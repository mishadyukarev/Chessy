using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct StatUnitsUpgradesE
    {
        static Dictionary<string, Entity> _upgrades;
        const string BETWEEN = "_";

        static string Key(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes level, in PlayerTypes player, in UpgradeTypes upg)
            =>stat.ToString() + BETWEEN + unit + BETWEEN + level + BETWEEN + player + BETWEEN + upg;

        public static ref C Upgrade<C>(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes level, in PlayerTypes player, in UpgradeTypes upg) where C : struct => ref _upgrades[Key(stat, unit, level, player, upg)].Get<C>();
        public static ref C Upgrade<C>(in string key) where C : struct => ref _upgrades[key].Get<C>();


        public static HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _upgrades) hash.Add(item.Key);
                return hash;
            }
        }

        public StatUnitsUpgradesE(in EcsWorld gameW)
        {
            _upgrades = new Dictionary<string, Entity>();

            for (var upg = UpgradeTypes.First; upg < UpgradeTypes.End; upg++)
            {
                for (var stat = UnitStatTypes.First; stat < UnitStatTypes.End; stat++)
                {
                    for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
                    {
                        for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                        {
                            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                            {
                                _upgrades.Add(Key(stat, unit, level, player, upg), gameW.NewEntity()
                                    .Add(new HaveUpgradeC()));
                            }
                        }
                    }
                }
            }
        }
    }
}