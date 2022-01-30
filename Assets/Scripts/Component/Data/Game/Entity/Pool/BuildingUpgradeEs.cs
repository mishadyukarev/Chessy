using ECS;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public struct BuildingUpgradeEs
    {
        readonly Dictionary<string, HaveUpgradeE> _haveUpgrades;

        public HaveUpgradeE HaveUpgrade(in BuildingTypes build, in PlayerTypes player, in UpgradeTypes upg) => _haveUpgrades[build.ToString() + player + upg];

        public BuildingUpgradeEs(in EcsWorld gameW)
        {
            _haveUpgrades = new Dictionary<string, HaveUpgradeE>();
            for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
            {
                for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                {
                    for (var upg = UpgradeTypes.None + 1; upg < UpgradeTypes.End; upg++)
                    {
                        _haveUpgrades.Add(build.ToString() + player + upg, new HaveUpgradeE(false, gameW));
                    }
                }
            }
        }
    }
}