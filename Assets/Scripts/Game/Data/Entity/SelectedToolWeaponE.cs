namespace Game.Game
{
    public struct SelectedToolWeaponE
    {
        public ToolWeaponTC ToolWeaponTC;
        public LevelTC LevelTC;

        public SelectedToolWeaponE(in ToolWeaponTypes twT, in LevelTypes levT)
        {
            ToolWeaponTC = new ToolWeaponTC(twT);
            LevelTC = new LevelTC(levT);
        }

        public void Set(in ToolWeaponTypes twT, in LevelTypes levT)
        {
            ToolWeaponTC.ToolWeapon = twT;
            LevelTC.Level = levT;
        }
    }
}