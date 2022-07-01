using Chessy.Model.Cell.Unit;
namespace Chessy.Model
{
    static class MainToolWeaponUnitWorkS
    {
        internal static void Set(ref this MainToolWeaponUnitC mainToolWeaponE, in ToolWeaponTypes twT, in LevelTypes levelT)
        {
            mainToolWeaponE.ToolWeaponT = twT;
            mainToolWeaponE.LevelT = levelT;
        }
        internal static void Clear(ref this MainToolWeaponUnitC mainToolWeaponE)
        {
            mainToolWeaponE.ToolWeaponT = default;
            mainToolWeaponE.LevelT = default;
        }
        internal static void CopyMainTW(ref this MainToolWeaponUnitC mainToolWeaponToE, in MainToolWeaponUnitC mainToolWeaponFromE)
        {
            mainToolWeaponToE.ToolWeaponT = mainToolWeaponFromE.ToolWeaponT;
            mainToolWeaponToE.LevelT = mainToolWeaponFromE.LevelT;
        }

    }
}