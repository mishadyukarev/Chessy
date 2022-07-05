namespace Chessy.Model
{
    public struct SelectedToolWeaponC
    {
        public ToolsWeaponsWarriorTypes ToolWeaponT { get; internal set; }
        public LevelTypes LevelT { get; internal set; }

        public SelectedToolWeaponC(in ToolsWeaponsWarriorTypes twT, in LevelTypes levT)
        {
            ToolWeaponT = twT;
            LevelT = levT;
        }
    }
}