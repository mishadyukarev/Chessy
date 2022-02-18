//using ECS;
//using Photon.Realtime;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public struct AvailableCenterUpgradeEs
//    {
//        Dictionary<PlayerTypes, HaveUpgradeC> _haveUpgrades;
//        Dictionary<string, HaveUpgradeC> _build;
//        Dictionary<string, HaveUpgradeC> _unit;
//        Dictionary<PlayerTypes, HaveUpgradeC> _water;

//        public HaveUpgradeC HaveUpgrade(in PlayerTypes player) => _haveUpgrades[player];
//        public HaveUpgradeC HaveBuildUpgrade(in BuildingTypes build, in PlayerTypes player) => _build[build.ToString() + player];
//        public HaveUpgradeC HaveUnitUpgrade(in UnitTypes unit, in PlayerTypes player) => _unit[unit.ToString() + player];
//        public HaveUpgradeC HaveWaterUpgrade(in PlayerTypes player) => _water[player];

//        public HashSet<PlayerTypes> Keys
//        {
//            get
//            {
//                var keys = new HashSet<PlayerTypes>();
//                foreach (var item in _haveUpgrades) keys.Add(item.Key);
//                return keys;
//            }
//        }

//        public AvailableCenterUpgradeEs(in EcsWorld gameW)
//        {
//            _haveUpgrades = new Dictionary<PlayerTypes, HaveUpgradeC>();
//            _build = new Dictionary<string, HaveUpgradeC>();
//            _unit = new Dictionary<string, HaveUpgradeC>();
//            _water = new Dictionary<PlayerTypes, HaveUpgradeC>();

//            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
//            {
//                _haveUpgrades.Add(player, new HaveUpgradeC(true));

//                _water.Add(player, new HaveUpgradeC(true));

//                for (var build = BuildingTypes.Farm; build <= BuildingTypes.Woodcutter; build++)
//                {
//                    _build.Add(build.ToString() + player, new HaveUpgradeC(true));
//                }

//                for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
//                {
//                    _unit.Add(unit.ToString() + player, new HaveUpgradeC(true));
//                }
//            }
//        }
//    }
//}