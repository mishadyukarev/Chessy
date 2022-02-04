using ECS;
using Photon.Realtime;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct UnitStatUpgradesEs
    {
        readonly Dictionary<string, HaveUpgradeE> _ents;

        string Key(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes lev, in PlayerTypes player, in UpgradeTypes upg) => stat.ToString() + unit + lev + player + upg;
        public HaveUpgradeE Upgrade(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes lev, in PlayerTypes player, in UpgradeTypes upg) => _ents[Key(stat, unit, lev, player, upg)];
        public HaveUpgradeE Upgrade(in UnitStatTypes stat, in CellUnitEs unitEs, in UpgradeTypes upg) => _ents[Key(stat, unitEs.MainE.UnitTC.Unit, unitEs.LevelE.LevelTC.Level, unitEs.OwnerE.OwnerC.Player, upg)];
        public HaveUpgradeE Upgrade(in string key) => _ents[key];

        public HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _ents) hash.Add(item.Key);
                return hash;
            }
        }

        public UnitStatUpgradesEs(in EcsWorld gameW)
        {
            _ents = new Dictionary<string, HaveUpgradeE>();

            for (var stat = UnitStatTypes.None + 1; stat < UnitStatTypes.End; stat++)
            {
                for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
                {
                    for (var lev = LevelTypes.None + 1; lev < LevelTypes.End; lev++)
                    {
                        for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                        {
                            for (var upg = UpgradeTypes.None + 1; upg < UpgradeTypes.End; upg++)
                            {
                                _ents.Add(Key(stat, unit, lev, player, upg), new HaveUpgradeE(false, gameW));
                            }
                        }
                    }
                }
            }
        }

        public void UpgradeCenterWater_Master(in Player sender, in Entities e)
        {
            var whoseMove = e.WhoseMove.WhoseMove.Player;

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    e.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Water, unit, level, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
                }
            }
            e.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            e.AvailableCenterUpgradeEs.HaveWaterUpgrade(whoseMove).HaveUpgrade.Have = false;

            e.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}