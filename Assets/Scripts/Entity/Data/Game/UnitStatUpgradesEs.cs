//using ECS;
//using Photon.Realtime;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public readonly struct UnitStatUpgradesEs
//    {
//        readonly Dictionary<string, HaveUpgradeC> _ents;

//        string Key(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes lev, in PlayerTypes player, in UpgradeTypes upg) => stat.ToString() + unit + lev + player + upg;
//        public HaveUpgradeC Upgrade(in UnitStatTypes stat, in UnitTypes unit, in LevelTypes lev, in PlayerTypes player, in UpgradeTypes upg) => _ents[Key(stat, unit, lev, player, upg)];
//        public HaveUpgradeC Upgrade(in string key) => _ents[key];

//        public UnitStatUpgradesEs(in EcsWorld gameW)
//        {
//            _ents = new Dictionary<string, HaveUpgradeC>();

//            for (var stat = UnitStatTypes.None + 1; stat < UnitStatTypes.End; stat++)
//            {
//                for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
//                {
//                    for (var lev = LevelTypes.None + 1; lev < LevelTypes.End; lev++)
//                    {
//                        for (var player = PlayerTypes.None; player < PlayerTypes.End; player++)
//                        {
//                            for (var upg = UpgradeTypes.None + 1; upg < UpgradeTypes.End; upg++)
//                            {
//                                _ents.Add(Key(stat, unit, lev, player, upg), new HaveUpgradeC(false));
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}