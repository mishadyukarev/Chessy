using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.Abstractions.Enums.Cell.Pawn;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General
{
    internal struct InventorToolsComponent
    {
        private Dictionary<ToolTypes, byte> _inventorTools;
        private Dictionary<WeaponTypes, byte> _inventorWeapons;

        internal InventorToolsComponent(Dictionary<ToolTypes, byte> inventorTools)
        {
            _inventorTools = inventorTools;

            for (ToolTypes toolType = 0; toolType < (ToolTypes)Enum.GetNames(typeof(ToolTypes)).Length; toolType++)
            {
                _inventorTools.Add(toolType, default);
            }


            _inventorWeapons = new Dictionary<WeaponTypes, byte>();

            for (WeaponTypes weaponType = 0; weaponType < (WeaponTypes)Enum.GetNames(typeof(WeaponTypes)).Length; weaponType++)
            {
                _inventorWeapons.Add(weaponType, default);
            }
        }

        internal bool HaveTool(ToolTypes toolType) => _inventorTools[toolType] > 0;
        internal bool HaveTool(PawnExtraToolTypes pawnExtraToolType) => GetAmountTools(pawnExtraToolType) > 0;

        internal void SetAmountTools(ToolTypes toolType, byte value) => _inventorTools[toolType] = value;
        internal void SetAmountTools(PawnExtraToolTypes pawnExtraToolType, byte value)
        {
            switch (pawnExtraToolType)
            {
                case PawnExtraToolTypes.None:
                    throw new Exception();

                case PawnExtraToolTypes.Hoe:
                    _inventorTools[ToolTypes.Hoe] = value;
                    break;

                case PawnExtraToolTypes.Pick:
                    _inventorTools[ToolTypes.Pick] = value;
                    break;

                default:
                    throw new Exception();
            }
        }


        internal byte GetAmountTools(ToolTypes toolType) => _inventorTools[toolType];
        internal byte GetAmountTools(PawnExtraToolTypes pawnExtraToolType)
        {
            switch (pawnExtraToolType)
            {
                case PawnExtraToolTypes.None:
                    throw new Exception();

                case PawnExtraToolTypes.Hoe:
                    return _inventorTools[ToolTypes.Hoe];

                case PawnExtraToolTypes.Pick:
                    return _inventorTools[ToolTypes.Pick];

                default:
                    throw new Exception();
            }
        }
        internal byte GetAmountWeapons(WeaponTypes weaponType) => _inventorWeapons[weaponType];
        internal byte GetAmountWeapons(PawnExtraWeaponTypes pawnExtraWeaponType)
        {
            switch (pawnExtraWeaponType)
            {
                case PawnExtraWeaponTypes.None:
                    throw new Exception();

                case PawnExtraWeaponTypes.Sword:
                    return _inventorWeapons[WeaponTypes.Sword];

                default:
                    throw new Exception();
            }
        }


        internal void AddAmountTools(ToolTypes toolType, byte adding = 1) => _inventorTools[toolType] += adding;
        internal void AddAmountTools(PawnExtraToolTypes pawnExtraToolType, byte adding = 1) => SetAmountTools(pawnExtraToolType, (byte)(GetAmountTools(pawnExtraToolType) + adding));

        internal void TakeAmountTools(ToolTypes toolType, byte taking = 1) => _inventorTools[toolType] -= taking;
        internal void TakeAmountTools(PawnExtraToolTypes pawnExtraToolType, byte taking = 1) => SetAmountTools(pawnExtraToolType, (byte)(GetAmountTools(pawnExtraToolType) - taking));
    }
}
