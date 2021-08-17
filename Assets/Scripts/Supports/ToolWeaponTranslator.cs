using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using System;

namespace Assets.Scripts.Supports
{
    internal static class ToolWeaponTranslator
    {
        internal static ToolTypes TransInTool(ToolWeaponTypes toolAndWeaponType)
        {
            switch (toolAndWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    return ToolTypes.Hoe;

                case ToolWeaponTypes.Axe:
                    return ToolTypes.Axe;

                case ToolWeaponTypes.Pick:
                    return ToolTypes.Pick;

                case ToolWeaponTypes.Sword:
                    throw new Exception();

                case ToolWeaponTypes.Bow:
                    throw new Exception();

                case ToolWeaponTypes.Crossbow:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static PawnExtraThingTypes TranslInPawnExtraThingType(ToolWeaponTypes toolAndWeaponType)
        {
            switch (toolAndWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    return PawnExtraThingTypes.Hoe;

                case ToolWeaponTypes.Axe:
                    throw new Exception();

                case ToolWeaponTypes.Pick:
                    return PawnExtraThingTypes.Pick;

                case ToolWeaponTypes.Sword:
                    return PawnExtraThingTypes.Sword;

                case ToolWeaponTypes.Bow:
                    throw new Exception();

                case ToolWeaponTypes.Crossbow:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
    }
}
