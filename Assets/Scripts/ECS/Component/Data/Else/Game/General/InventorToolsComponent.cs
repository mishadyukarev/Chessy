using Assets.Scripts.Abstractions.Enums.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General
{
    internal struct InventorToolsComponent
    {
        private Dictionary<PawnExtraToolTypes, byte> _inventorTools;

        internal InventorToolsComponent(Dictionary<PawnExtraToolTypes, byte> inventorTools)
        {
            _inventorTools = inventorTools;

            for (PawnExtraToolTypes toolType = 0; toolType < (PawnExtraToolTypes)Enum.GetNames(typeof(PawnExtraToolTypes)).Length; toolType++)
            {
                _inventorTools.Add(toolType, default);
            }
        }

        internal bool HaveTool(PawnExtraToolTypes toolType) => _inventorTools[toolType] > 0;

        internal void SetAmountTools(PawnExtraToolTypes toolType, byte value) => _inventorTools[toolType] = value;
        internal byte GetAmountTools(PawnExtraToolTypes toolType) => _inventorTools[toolType];
        internal void AddAmountTools(PawnExtraToolTypes toolType, byte adding = 1) => _inventorTools[toolType] += adding;
        internal void TakeAmountTools(PawnExtraToolTypes toolType, byte taking = 1) => _inventorTools[toolType] -= taking;
    }
}
