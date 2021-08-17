using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.Supports;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General
{
    internal struct InventorToolsComp
    {
        private Dictionary<bool, Dictionary<ToolTypes, byte>> _inventorTools;

        internal InventorToolsComp(Dictionary<bool, Dictionary<ToolTypes, byte>> inventorTools)
        {
            _inventorTools = inventorTools;
            for (byte i = 0; i < 2; i++)
            {
                var isMaster = true;
                if (i == 1) isMaster = false;

                var dict = new Dictionary<ToolTypes, byte>();
                for (ToolTypes toolType = 0; toolType < (ToolTypes)Enum.GetNames(typeof(ToolTypes)).Length; toolType++)
                {
                    dict.Add(toolType, default);
                }
                _inventorTools.Add(isMaster, dict);
            }
        }

        internal bool HaveTool(bool key, ToolTypes toolType) => _inventorTools[key][toolType] > 0;
        internal bool HaveTool(bool key, ToolWeaponTypes toolWeaponType) => _inventorTools[key][ToolWeaponTranslator.TransInTool(toolWeaponType)] > 0;

        internal void SetAmountTools(bool key, ToolTypes toolType, byte value) => _inventorTools[key][toolType] = value;

        internal byte GetAmountTools(bool key, ToolTypes toolType) => _inventorTools[key][toolType];

        internal void AddAmountTools(bool key, ToolTypes toolType, byte adding = 1) => _inventorTools[key][toolType] += adding;
        internal void TakeAmountTools(bool key, ToolTypes toolType, byte taking = 1) => _inventorTools[key][toolType] -= taking;
        internal void TakeAmountTools(bool key, ToolWeaponTypes toolWeaponType, byte taking = 1) => _inventorTools[key][ToolWeaponTranslator.TransInTool(toolWeaponType)] -= taking;
    }
}
