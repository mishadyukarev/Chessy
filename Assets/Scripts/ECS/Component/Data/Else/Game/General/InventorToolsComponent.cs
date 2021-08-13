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
        private Dictionary<PawnToolTypes, byte> _inventorTools;

        internal InventorToolsComponent(Dictionary<PawnToolTypes, byte> inventorTools)
        {
            _inventorTools = inventorTools;

            for (PawnToolTypes toolType = 0; toolType < (PawnToolTypes)Enum.GetNames(typeof(PawnToolTypes)).Length; toolType++)
            {
                _inventorTools.Add(toolType, default);
            }
        }

        internal bool HaveTool(PawnToolTypes toolType) => _inventorTools[toolType] > 0;

        internal void SetAmountTools(PawnToolTypes toolType, byte value) => _inventorTools[toolType] = value;
        internal byte GetAmountTools(PawnToolTypes toolType) => _inventorTools[toolType];
        internal void AddAmountTools(PawnToolTypes toolType, byte adding = 1) => _inventorTools[toolType] += adding;
        internal void TakeAmountTools(PawnToolTypes toolType, byte taking = 1) => _inventorTools[toolType] -= taking;
    }
}
