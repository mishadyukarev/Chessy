using System;
using System.Collections.Generic;

namespace Chessy.Game
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

        public static bool HaveTW(PlayerTypes playerType, TWTypes tWType, LevelTypes levelTWType) => _toolWeapons[playerType][tWType][levelTWType] > 0;
        public static void Set(PlayerTypes playerType, TWTypes tWType, LevelTypes levelTWType, int value) => _toolWeapons[playerType][tWType][levelTWType] = (byte)value;
        public static int AmountToolWeap(PlayerTypes playerType, TWTypes toolWeapType, LevelTypes levelTWType) => _toolWeapons[playerType][toolWeapType][levelTWType];

        public static void AddAmountTools(PlayerTypes playerType, TWTypes toolWeaponType, LevelTypes levelTWType, byte adding = 1) => _toolWeapons[playerType][toolWeaponType][levelTWType] += adding;
        public static void TakeAmountTools(PlayerTypes playerType, TWTypes toolWeaponType, LevelTypes levelTWType, byte taking = 1) => _toolWeapons[playerType][toolWeaponType][levelTWType] -= taking;
    }
}
