using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct AvailableCenterUpgradeEs
    {
        static Dictionary<PlayerTypes, HaveUpgradeE> _haveUpgrades;
        static Dictionary<string, HaveUpgradeE> _build;
        static Dictionary<string, HaveUpgradeE> _unit;
        static Dictionary<PlayerTypes, HaveUpgradeE> _water;

        public static HaveUpgradeE HaveUpgrade(in PlayerTypes player) => _haveUpgrades[player];
        public static HaveUpgradeE HaveBuildUpgrade(in BuildingTypes build, in PlayerTypes player) => _build[build.ToString() + player];
        public static HaveUpgradeE HaveUnitUpgrade(in UnitTypes unit, in PlayerTypes player) => _unit[unit.ToString() + player];
        public static HaveUpgradeE HaveWaterUpgrade(in PlayerTypes player) => _water[player];

        public static HashSet<PlayerTypes> Keys
        {
            get
            {
                var keys = new HashSet<PlayerTypes>();
                foreach (var item in _haveUpgrades) keys.Add(item.Key);
                return keys;
            }
        }

        public AvailableCenterUpgradeEs(in EcsWorld gameW)
        {
            _haveUpgrades = new Dictionary<PlayerTypes, HaveUpgradeE>();
            _build = new Dictionary<string, HaveUpgradeE>();
            _unit = new Dictionary<string, HaveUpgradeE>();
            _water = new Dictionary<PlayerTypes, HaveUpgradeE>();

            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _haveUpgrades.Add(player, new HaveUpgradeE(true, gameW));

                _water.Add(player, new HaveUpgradeE(true, gameW));

                for (var build = BuildingTypes.Farm; build <= BuildingTypes.Mine; build++)
                {
                    _build.Add(build.ToString() + player, new HaveUpgradeE(true, gameW));
                }

                for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
                {
                    _unit.Add(unit.ToString() + player, new HaveUpgradeE(true, gameW));
                }
            }
        }
    }
}