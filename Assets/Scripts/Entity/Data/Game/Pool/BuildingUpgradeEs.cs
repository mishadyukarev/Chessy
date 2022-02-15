using ECS;
using Photon.Realtime;
using System.Collections.Generic;

namespace Game.Game
{
    public struct BuildingUpgradeEs
    {
        readonly Dictionary<string, HaveUpgradeE> _haveUpgrades;

        public HaveUpgradeE HaveUpgrade(in BuildingTypes build, in PlayerTypes player, in UpgradeTypes upg) => _haveUpgrades[build.ToString() + player + upg];
        public HaveUpgradeE HaveUpgrade(in CellBuildingE buildE, in UpgradeTypes upg) => _haveUpgrades[buildE.Building.ToString() + buildE.Owner + upg];

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

        public void UpgradeCenter_Master(in BuildingTypes build, in Player sender, in Entities e)
        {
            var whoseMove = e.WhoseMovePlayerTC.CurPlayerI;

            e.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            e.BuildingUpgradeEs.HaveUpgrade(build, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
            e.AvailableCenterUpgradeEs.HaveBuildUpgrade(build, whoseMove).HaveUpgrade.Have = false;

            e.RpcE.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}