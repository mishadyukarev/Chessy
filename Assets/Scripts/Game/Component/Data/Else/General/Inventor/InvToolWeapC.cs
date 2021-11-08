using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct InvToolWeapC
    {
        private static Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, byte>>> _toolWeapons;

        public static Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, byte>>> ToolWeapons
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, byte>>>();

                foreach (var item_0 in _toolWeapons)
                {
                    dict.Add(item_0.Key, new Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, byte>>());

                    foreach (var item_1 in item_0.Value)
                    {
                        dict[item_0.Key].Add(item_1.Key, new Dictionary<LevelTWTypes, byte>());

                        foreach (var item_2 in item_1.Value)
                        {
                            dict[item_0.Key][item_1.Key].Add(item_2.Key, item_2.Value);
                        }
                    }
                }

                return dict;
            }
        }

        public InvToolWeapC(bool needNew) : this()
        {
            if (needNew)
            {
                _toolWeapons = new Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, byte>>>();

                for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
                {
                    _toolWeapons[playerType] = new Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, byte>>();

                    _toolWeapons[playerType].Add(ToolWeaponTypes.Pick, new Dictionary<LevelTWTypes, byte>());
                    _toolWeapons[playerType].Add(ToolWeaponTypes.Sword, new Dictionary<LevelTWTypes, byte>());
                    _toolWeapons[playerType].Add(ToolWeaponTypes.Shield, new Dictionary<LevelTWTypes, byte>());

                    _toolWeapons[playerType][ToolWeaponTypes.Pick].Add(LevelTWTypes.Wood, default);
                    _toolWeapons[playerType][ToolWeaponTypes.Sword].Add(LevelTWTypes.Wood, default);
                    _toolWeapons[playerType][ToolWeaponTypes.Shield].Add(LevelTWTypes.Wood, default);

                    _toolWeapons[playerType][ToolWeaponTypes.Pick].Add(LevelTWTypes.Iron, default);
                    _toolWeapons[playerType][ToolWeaponTypes.Sword].Add(LevelTWTypes.Iron, default);
                    _toolWeapons[playerType][ToolWeaponTypes.Shield].Add(LevelTWTypes.Iron, default);
                }
            }
        }

        public static bool HaveTW(PlayerTypes playerType, ToolWeaponTypes tWType, LevelTWTypes levelTWType) => _toolWeapons[playerType][tWType][levelTWType] > 0;
        public static void Set(PlayerTypes playerType, ToolWeaponTypes tWType, LevelTWTypes levelTWType, int value) => _toolWeapons[playerType][tWType][levelTWType] = (byte)value;
        public static int AmountToolWeap(PlayerTypes playerType, ToolWeaponTypes toolWeapType, LevelTWTypes levelTWType) => _toolWeapons[playerType][toolWeapType][levelTWType];

        public static void AddAmountTools(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType, byte adding = 1) => _toolWeapons[playerType][toolWeaponType][levelTWType] += adding;
        public static void TakeAmountTools(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType, byte taking = 1) => _toolWeapons[playerType][toolWeaponType][levelTWType] -= taking;
    }
}
