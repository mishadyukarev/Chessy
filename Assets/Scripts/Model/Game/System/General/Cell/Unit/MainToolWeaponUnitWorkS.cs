using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    static class MainToolWeaponUnitWorkS
    {
        internal static void Set(this ToolWeaponMainUnitC mainToolWeaponE, in ToolWeaponTypes twT, in LevelTypes levelT)
        {
            mainToolWeaponE.ToolWeaponT = twT;
            mainToolWeaponE.LevelT = levelT;
        }
        internal static void Clear(this ToolWeaponMainUnitC mainToolWeaponE)
        {
            mainToolWeaponE.ToolWeaponT = default;
            mainToolWeaponE.LevelT = default;
        }
        internal static void CopyMainTW(this ToolWeaponMainUnitC mainToolWeaponToE, in ToolWeaponMainUnitC mainToolWeaponFromE)
        {
            mainToolWeaponToE.ToolWeaponT = mainToolWeaponFromE.ToolWeaponT;
            mainToolWeaponToE.LevelT = mainToolWeaponFromE.LevelT;
        }

    }
}