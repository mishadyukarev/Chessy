using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using System;

namespace Assets.Scripts.Supports
{
    internal static class ToolWeaponTranslator
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
    }
}
