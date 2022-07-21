namespace Chessy.Model.Component
{
    public sealed class MainToolWeaponUnitC
    {
        internal ToolsWeaponsWarriorTypes ToolWeaponT;
        internal LevelTypes LevelT;

        public ToolsWeaponsWarriorTypes ToolWeaponType => ToolWeaponT;
        public LevelTypes LevelType => LevelT;

        internal void Clone(in MainToolWeaponUnitC mainToolWeaponUnitC)
        {
            ToolWeaponT = mainToolWeaponUnitC.ToolWeaponT;
            LevelT = mainToolWeaponUnitC.LevelT;
        }
        internal void Set(in ToolsWeaponsWarriorTypes twT, in LevelTypes levelT)
        {
            ToolWeaponT = twT;
            LevelT = levelT;
        }
    }
}