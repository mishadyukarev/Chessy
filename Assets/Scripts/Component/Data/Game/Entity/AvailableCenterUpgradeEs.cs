using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct AvailableCenterUpgradeEs
    {
        static Dictionary<PlayerTypes, HaveUpgradeE> _haveUpgrades;
        static Dictionary<string, HaveUpgradeE> _haveBuildUpgrades;
        static Dictionary<string, HaveUpgradeE> _haveUnitUpgrades;
        static Dictionary<PlayerTypes, HaveUpgradeE> _haveWaterUpgrades;

        public static HaveUpgradeE HaveUpgrade(in PlayerTypes player) => _haveUpgrades[player];
        public static HaveUpgradeE HaveBuildUpgrade(in BuildingTypes build, in PlayerTypes player) => _haveBuildUpgrades[build.ToString() + player];
        public static HaveUpgradeE HaveUnitUpgrade(in UnitTypes unit, in PlayerTypes player) => _haveUnitUpgrades[unit.ToString() + player];
        public static HaveUpgradeE HaveWaterUpgrade(in PlayerTypes player) => _haveWaterUpgrades[player];

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
            _haveBuildUpgrades = new Dictionary<string, HaveUpgradeE>();
            _haveUnitUpgrades = new Dictionary<string, HaveUpgradeE>();
            _haveWaterUpgrades = new Dictionary<PlayerTypes, HaveUpgradeE>();

            for (var player = PlayerTypes.Start + 1; player < PlayerTypes.End; player++)
            {
                _haveUpgrades.Add(player, new HaveUpgradeE(true, gameW));

                _haveWaterUpgrades.Add(player, new HaveUpgradeE(true, gameW));

                for (var build = BuildingTypes.Farm; build <= BuildingTypes.Mine; build++)
                {
                    _haveBuildUpgrades.Add(build.ToString() + player, new HaveUpgradeE(true, gameW));
                }

                for (var unit = UnitTypes.Start + 1; unit < UnitTypes.End; unit++)
                {
                    _haveUnitUpgrades.Add(unit.ToString() + player, new HaveUpgradeE(true, gameW));
                }
            }
        }
    }
}