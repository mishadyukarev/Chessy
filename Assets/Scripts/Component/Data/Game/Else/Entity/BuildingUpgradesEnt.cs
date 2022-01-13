using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct BuildingUpgradesEnt
    {
        static readonly Dictionary<string, Entity> _upgrades;


        public static ref C Upgrade<C>(in BuildTypes build, in PlayerTypes player, in UpgradeTypes upg) where C : struct, IBuildingUpgradeE => ref _upgrades[build.ToString() + player + upg].Get<C>();
        public static ref C Upgrade<C>(in string key) where C : struct, IBuildingUpgradeE => ref _upgrades[key].Get<C>();


        public static HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _upgrades) hash.Add(item.Key);
                return hash;
            }
        }

        static BuildingUpgradesEnt()
        {
            _upgrades = new Dictionary<string, Entity>();
            for (var build = BuildTypes.First; build < BuildTypes.End; build++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    for (var upg = UpgradeTypes.First; upg < UpgradeTypes.End; upg++)
                    {
                        _upgrades.Add(build.ToString() + player + upg, default);
                    }
                }
            }
        }
        public BuildingUpgradesEnt(in EcsWorld gameW)
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                for (var upg = UpgradeTypes.First; upg < UpgradeTypes.End; upg++)
                {
                    for (var build = BuildTypes.First; build < BuildTypes.End; build++)
                    {
                        _upgrades[build.ToString() + player + upg] = gameW.NewEntity()
                            .Add(new HaveUpgradeC());
                    }
                }
            }
        }
    }

    public interface IBuildingUpgradeE { }
}