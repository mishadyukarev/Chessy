using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using System;

namespace Assets.Scripts.Supports
{
    internal static class Support
    {
        internal static ToolTypes TransInTool(this ToolWeaponTypes toolAndWeaponType)
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
        internal static WeaponTypes TransInWeapon(this ToolWeaponTypes toolAndWeaponType)
        {
            switch (toolAndWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    throw new Exception();

                case ToolWeaponTypes.Axe:
                    throw new Exception();

                case ToolWeaponTypes.Pick:
                    throw new Exception();

                case ToolWeaponTypes.Sword:
                    return WeaponTypes.Sword;

                case ToolWeaponTypes.Bow:
                    return WeaponTypes.Bow;

                case ToolWeaponTypes.Crossbow:
                    return WeaponTypes.Crossbow;

                default:
                    throw new Exception();
            }
        }
        internal static bool IsTool(this ToolWeaponTypes toolWeaponType)
        {
            switch (toolWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    return true;

                case ToolWeaponTypes.Axe:
                    return true;

                case ToolWeaponTypes.Pick:
                    return true;

                case ToolWeaponTypes.Sword:
                    return false;

                case ToolWeaponTypes.Bow:
                    return false;

                case ToolWeaponTypes.Crossbow:
                    return false;

                default:
                    throw new Exception();
            }
        }
        internal static bool Is(this ToolWeaponTypes leftToolWeaponType, ToolWeaponTypes rightToolWeaponType) => leftToolWeaponType == rightToolWeaponType;
        internal static bool IsForArcher(this ToolWeaponTypes toolWeaponType)
        {
            switch (toolWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    return false;

                case ToolWeaponTypes.Axe:
                    return false;

                case ToolWeaponTypes.Pick:
                    return false;

                case ToolWeaponTypes.Sword:
                    return false;

                case ToolWeaponTypes.Bow:
                    return true;

                case ToolWeaponTypes.Crossbow:
                    return true;

                default:
                    throw new Exception();
            }
        }
        internal static bool IsForPawn(this ToolWeaponTypes toolWeaponType)
        {
            switch (toolWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    throw new Exception();

                case ToolWeaponTypes.Axe:
                    return true;

                case ToolWeaponTypes.Pick:
                    return true;

                case ToolWeaponTypes.Sword:
                    return true;

                case ToolWeaponTypes.Bow:
                    return false;

                case ToolWeaponTypes.Crossbow:
                    return false;

                default:
                    throw new Exception();
            }
        }


        internal static bool Is(this UnitTypes leftUnitType, UnitTypes rightUnitType) => leftUnitType == rightUnitType;
        internal static bool Is(this UnitTypes leftUnitType, UnitTypes[] rightUnitTypes)
        {
            foreach (var curUnitType in rightUnitTypes)
                if (Is(leftUnitType, curUnitType)) return true;
            return false;
        }
    }
}
