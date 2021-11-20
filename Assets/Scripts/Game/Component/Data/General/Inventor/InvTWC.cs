using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct InvTWC
    {
        private static Dictionary<PlayerTypes, Dictionary<TWTypes, Dictionary<LevelTypes, byte>>> _toolWeapons;

        public static Dictionary<PlayerTypes, Dictionary<TWTypes, Dictionary<LevelTypes, byte>>> ToolWeapons
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, Dictionary<TWTypes, Dictionary<LevelTypes, byte>>>();

                foreach (var item_0 in _toolWeapons)
                {
                    dict.Add(item_0.Key, new Dictionary<TWTypes, Dictionary<LevelTypes, byte>>());

                    foreach (var item_1 in item_0.Value)
                    {
                        dict[item_0.Key].Add(item_1.Key, new Dictionary<LevelTypes, byte>());

                        foreach (var item_2 in item_1.Value)
                        {
                            dict[item_0.Key][item_1.Key].Add(item_2.Key, item_2.Value);
                        }
                    }
                }

                return dict;
            }
        }

        public InvTWC(bool needNew) : this()
        {
            if (needNew)
            {
                _toolWeapons = new Dictionary<PlayerTypes, Dictionary<TWTypes, Dictionary<LevelTypes, byte>>>();

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _toolWeapons[player] = new Dictionary<TWTypes, Dictionary<LevelTypes, byte>>();

                    _toolWeapons[player].Add(TWTypes.Pick, new Dictionary<LevelTypes, byte>());
                    _toolWeapons[player].Add(TWTypes.Sword, new Dictionary<LevelTypes, byte>());
                    _toolWeapons[player].Add(TWTypes.Shield, new Dictionary<LevelTypes, byte>());

                    _toolWeapons[player][TWTypes.Pick].Add(LevelTypes.First, default);
                    _toolWeapons[player][TWTypes.Sword].Add(LevelTypes.First, default);
                    _toolWeapons[player][TWTypes.Shield].Add(LevelTypes.First, default);

                    _toolWeapons[player][TWTypes.Pick].Add(LevelTypes.Second, default);
                    _toolWeapons[player][TWTypes.Sword].Add(LevelTypes.Second, default);
                    _toolWeapons[player][TWTypes.Shield].Add(LevelTypes.Second, default);
                }
            }
        }

        public static bool HaveTW(PlayerTypes player, TWTypes tWType, LevelTypes level) => _toolWeapons[player][tWType][level] > 0;
        public static void Set(PlayerTypes player, TWTypes tWType, LevelTypes level, int value) => _toolWeapons[player][tWType][level] = (byte)value;
        public static int AmountToolWeap(PlayerTypes player, TWTypes tw, LevelTypes level) => _toolWeapons[player][tw][level];

        public static void Add(PlayerTypes player, TWTypes tw, LevelTypes level, byte adding = 1) => _toolWeapons[player][tw][level] += adding;
        public static void TakeAmountTools(PlayerTypes player, TWTypes tw, LevelTypes level, byte taking = 1) => _toolWeapons[player][tw][level] -= taking;
    }
}
