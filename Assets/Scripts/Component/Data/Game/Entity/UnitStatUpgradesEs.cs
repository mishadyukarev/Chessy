using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct UnitStatUpgradesEs
    {
        static Dictionary<string, Entity> _ents;


        static string Key(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes lev, in PlayerTypes player, in UpgradeTypes upg) => stat.ToString() + unit + lev + player + upg;

        public static ref C HaveUpgrade<C>(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes lev, in PlayerTypes player, in UpgradeTypes upg) where C : struct => ref _ents[Key(stat, unit, lev, player, upg)].Get<C>();

        public UnitStatUpgradesEs(in EcsWorld gameW)
        {
            _ents = new Dictionary<string, Entity>();

            for (var stat = UnitStatTypes.Start + 1; stat < UnitStatTypes.End; stat++)
            {
                for (var unit = UnitTypes.Start + 1; unit < UnitTypes.End; unit++)
                {
                    for (var lev = LevelTypes.Start + 1; lev < LevelTypes.End; lev++)
                    {
                        for (var player = PlayerTypes.Start + 1; player < PlayerTypes.End; player++)
                        {
                            for (var upg = UpgradeTypes.None + 1; upg < UpgradeTypes.End; upg++)
                            {
                                _ents.Add(Key(stat, unit, lev, player, upg), gameW.NewEntity()
                                    .Add(new HaveUpgradeC(false)));
                            }
                        }
                    }
                }
            }
        }
    }
}