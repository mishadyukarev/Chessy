using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct InventorTWCom
    {
        private static Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, byte>>> _inventorTools;

        internal InventorTWCom(bool needNew) : this()
        {
            if (needNew)
            {
                _inventorTools = new Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, byte>>>();

                for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
                {
                    _inventorTools[playerType] = new Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, byte>>();

                    _inventorTools[playerType].Add(ToolWeaponTypes.Pick, new Dictionary<LevelTWTypes, byte>());
                    _inventorTools[playerType].Add(ToolWeaponTypes.Sword, new Dictionary<LevelTWTypes, byte>());
                    _inventorTools[playerType].Add(ToolWeaponTypes.Shield, new Dictionary<LevelTWTypes, byte>());

                    _inventorTools[playerType][ToolWeaponTypes.Pick].Add(LevelTWTypes.Wood, default);
                    _inventorTools[playerType][ToolWeaponTypes.Sword].Add(LevelTWTypes.Wood, default);
                    _inventorTools[playerType][ToolWeaponTypes.Shield].Add(LevelTWTypes.Wood, default);

                    _inventorTools[playerType][ToolWeaponTypes.Pick].Add(LevelTWTypes.Iron, default);
                    _inventorTools[playerType][ToolWeaponTypes.Sword].Add(LevelTWTypes.Iron, default);
                    _inventorTools[playerType][ToolWeaponTypes.Shield].Add(LevelTWTypes.Iron, default);
                }
            }
        }

        internal static bool HaveTW(PlayerTypes playerType, ToolWeaponTypes tWType, LevelTWTypes levelTWType) => _inventorTools[playerType][tWType][levelTWType] > 0;
        internal static void Set(PlayerTypes playerType, ToolWeaponTypes tWType, LevelTWTypes levelTWType, byte value) => _inventorTools[playerType][tWType][levelTWType] = value;
        internal static byte GetAmountTools(PlayerTypes playerType, ToolWeaponTypes toolWeapType, LevelTWTypes levelTWType) => _inventorTools[playerType][toolWeapType][levelTWType];

        internal static void AddAmountTools(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType, byte adding = 1) => _inventorTools[playerType][toolWeaponType][levelTWType] += adding;
        internal static void TakeAmountTools(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType, byte taking = 1) => _inventorTools[playerType][toolWeaponType][levelTWType] -= taking;
    }
}
