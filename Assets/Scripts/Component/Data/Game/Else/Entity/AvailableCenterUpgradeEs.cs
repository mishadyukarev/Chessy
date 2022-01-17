using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct AvailableCenterUpgradeEs
    {
        static Dictionary<PlayerTypes, Entity> _haveUpgrades;
        static Dictionary<string, Entity> _haveBuildUpgrades;
        static Dictionary<string, Entity> _haveUnitUpgrades;
        static Dictionary<PlayerTypes, Entity> _haveWaterUpgrades;

        public static ref C HaveUpgrade<C>(in PlayerTypes player) where C : struct, IUpgradeE => ref _haveUpgrades[player].Get<C>();
        public static ref C HaveBuildUpgrade<C>(in BuildingTypes build, in PlayerTypes player) where C : struct, IUpgradeE => ref _haveBuildUpgrades[build.ToString() + player].Get<C>();
        public static ref C HaveUnitUpgrade<C>(in UnitTypes unit, in PlayerTypes player) where C : struct, IUpgradeE => ref _haveUnitUpgrades[unit.ToString() + player].Get<C>();
        public static ref C HaveWaterUpgrade<C>(in PlayerTypes player) where C : struct, IUpgradeE => ref _haveWaterUpgrades[player].Get<C>();

        public static HashSet<PlayerTypes> Keys
        {
            get
            {
                var keys = new HashSet<PlayerTypes>();
                foreach (var item in _haveUpgrades) keys.Add(item.Key);
                return keys;
            }
        }
        //public static HashSet<BuildingTypes> KeysBuild
        //{
        //    get
        //    {
        //        var keys = new HashSet<BuildingTypes>();
        //        foreach (var item in _haveBuildUpgrades) keys.Add(item.Key);
        //        return keys;
        //    }
        //}


        public AvailableCenterUpgradeEs(in EcsWorld gameW)
        {
            _haveUpgrades = new Dictionary<PlayerTypes, Entity>();
            _haveBuildUpgrades = new Dictionary<string, Entity>();
            _haveUnitUpgrades = new Dictionary<string, Entity>();
            _haveWaterUpgrades = new Dictionary<PlayerTypes, Entity>();

            for (var player = PlayerTypes.Start + 1; player < PlayerTypes.End; player++)
            {
                _haveUpgrades.Add(player, gameW.NewEntity()
                    .Add(new HaveUpgradeC(true)));

                _haveWaterUpgrades.Add(player, gameW.NewEntity()
                    .Add(new HaveUpgradeC(true)));

                for (var build = BuildingTypes.Farm; build <= BuildingTypes.Mine; build++)
                {
                    _haveBuildUpgrades.Add(build.ToString() + player, gameW.NewEntity()
                        .Add(new HaveUpgradeC(true)));
                }

                for (var unit = UnitTypes.Start + 1; unit < UnitTypes.End; unit++)
                {
                    _haveUnitUpgrades.Add(unit.ToString() + player, gameW.NewEntity()
                        .Add(new HaveUpgradeC(true)));
                }
            }
        }
    }

    public interface IUpgradeE { }
}