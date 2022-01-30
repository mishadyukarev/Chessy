//using ECS;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public readonly struct StatUnitsUpgradesE
//    {
//        readonly Dictionary<string, HaveUpgradeE> _upgrades;
//        const string BETWEEN = "_";

//        string Key(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes level, in PlayerTypes player, in UpgradeTypes upg)
//            => stat.ToString() + BETWEEN + unit + BETWEEN + level + BETWEEN + player + BETWEEN + upg;

//        public HaveUpgradeE Upgrade(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes level, in PlayerTypes player, in UpgradeTypes upg) => _upgrades[Key(stat, unit, level, player, upg)];
//        public HaveUpgradeE Upgrade(in string key) => _upgrades[key];


//        public HashSet<string> Keys
//        {
//            get
//            {
//                var hash = new HashSet<string>();
//                foreach (var item in _upgrades) hash.Add(item.Key);
//                return hash;
//            }
//        }

//        public StatUnitsUpgradesE(in EcsWorld gameW)
//        {
//            _upgrades = new Dictionary<string, HaveUpgradeE>();

//            for (var upg = UpgradeTypes.None + 1; upg < UpgradeTypes.End; upg++)
//            {
//                for (var stat = UnitStatTypes.None + 1; stat < UnitStatTypes.End; stat++)
//                {
//                    for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
//                    {
//                        for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
//                        {
//                            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
//                            {
//                                _upgrades.Add(Key(stat, unit, level, player, upg), new HaveUpgradeE(false, gameW));
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}