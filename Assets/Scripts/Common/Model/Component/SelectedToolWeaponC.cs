namespace Chessy.Game
{
    public struct SelectedToolWeaponC
    {
        public ToolWeaponTypes ToolWeaponT;
        public LevelTypes LevelT;

        public SelectedToolWeaponC(in ToolWeaponTypes twT, in LevelTypes levT)
        {
            ToolWeaponT = twT;
            LevelT = levT;
        }
    }
}