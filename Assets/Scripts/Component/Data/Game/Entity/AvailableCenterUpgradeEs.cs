using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct AvailableCenterUpgradeEs
    {
        Dictionary<PlayerTypes, HaveUpgradeE> _haveUpgrades;
        Dictionary<string, HaveUpgradeE> _build;
        Dictionary<string, HaveUpgradeE> _unit;
        Dictionary<PlayerTypes, HaveUpgradeE> _water;

        public HaveUpgradeE HaveUpgrade(in PlayerTypes player) => _haveUpgrades[player];
        public HaveUpgradeE HaveBuildUpgrade(in BuildingTypes build, in PlayerTypes player) => _build[build.ToString() + player];
        public HaveUpgradeE HaveUnitUpgrade(in UnitTypes unit, in PlayerTypes player) => _unit[unit.ToString() + player];
        public HaveUpgradeE HaveWaterUpgrade(in PlayerTypes player) => _water[player];

        public HashSet<PlayerTypes> Keys
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