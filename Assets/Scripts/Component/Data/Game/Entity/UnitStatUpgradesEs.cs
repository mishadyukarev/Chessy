using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct UnitStatUpgradesEs
    {
        Dictionary<string, HaveUpgradeE> _ents;

        string Key(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes lev, in PlayerTypes player, in UpgradeTypes upg) => stat.ToString() + unit + lev + player + upg;
        public HaveUpgradeE Upgrade(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes lev, in PlayerTypes player, in UpgradeTypes upg) => _ents[Key(stat, unit, lev, player, upg)];

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
    }
}