namespace Game.Game
{
    public struct CellUnitExtraToolWeaponE
    {
        public ToolWeaponTC ToolWeaponTC;
        public LevelTC LevelTC;
        public ProtectionC ProtectionC;

        public void Set(in ToolWeaponTC twTC, in LevelTC levTC, in ProtectionC protectionC)
        {
            ToolWeaponTC = twTC;
            LevelTC = levTC;
            ProtectionC = protectionC;
        }
        public void Set(in ToolWeaponTypes twT, in LevelTypes levT, in float protection)
        {
            ToolWeaponTC.ToolWeapon = twT;
            LevelTC.Level = levT;
            ProtectionC.Protection = protection;
        }
    }
}