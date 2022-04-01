namespace Chessy.Game
{
    public struct SelectedToolWeaponC
    {
        public ToolWeaponTypes ToolWeaponT { get; internal set; }
        public LevelTypes LevelT { get; internal set; }

        public SelectedToolWeaponC(in ToolWeaponTypes twT, in LevelTypes levT)
        {
            ToolWeaponT = twT;
            LevelT = levT;
        }
    }
}