using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct InventorTWCom
    {
        private Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, byte>> _inventorTools;

        internal InventorTWCom(bool needNew) : this()
        {
            if (needNew)
            {
                _inventorTools = new Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, byte>>();

                _inventorTools[PlayerTypes.First] = new Dictionary<ToolWeaponTypes, byte>();
                _inventorTools[PlayerTypes.Second] = new Dictionary<ToolWeaponTypes, byte>();


                _inventorTools[PlayerTypes.First].Add(ToolWeaponTypes.Pick, default);
                _inventorTools[PlayerTypes.First].Add(ToolWeaponTypes.Sword, default);
                _inventorTools[PlayerTypes.First].Add(ToolWeaponTypes.Crossbow, default);

                _inventorTools[PlayerTypes.Second].Add(ToolWeaponTypes.Pick, default);
                _inventorTools[PlayerTypes.Second].Add(ToolWeaponTypes.Sword, default);
                _inventorTools[PlayerTypes.Second].Add(ToolWeaponTypes.Crossbow, default);
            }
        }

        internal bool HaveTool(PlayerTypes playerType, ToolWeaponTypes toolWeaponType) => _inventorTools[playerType][toolWeaponType] > 0;
        internal void SetAmountTW(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, byte value) => _inventorTools[playerType][toolWeaponType] = value;
        internal byte GetAmountTools(PlayerTypes playerType, ToolWeaponTypes toolWeapType) => _inventorTools[playerType][toolWeapType];

        internal void AddAmountTools(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, byte adding = 1) => _inventorTools[playerType][toolWeaponType] += adding;
        internal void TakeAmountTools(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, byte taking = 1) => _inventorTools[playerType][toolWeaponType] -= taking;
    }
}
